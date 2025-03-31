using MauiAppTempoAgora.Models;
using Newtonsoft.Json.Linq;
using System.Net;

namespace MauiAppTempoAgora.Services
{
    public class DataService
    {
        private static readonly string chave = "6135072afe7f6cec1537d5cb08a5a1a2"; // SUA CHAVE DA API

        public static async Task<Tempo?> GetPrevisao(string cidade)
        {
            try
            {
                string url = $"https://api.openweathermap.org/data/2.5/weather?" +
                             $"q={cidade}&units=metric&appid={chave}&lang=pt";

                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage resp = await client.GetAsync(url);

                    if (resp.StatusCode == HttpStatusCode.NotFound)
                    {
                        throw new Exception("Cidade não encontrada. Verifique o nome e tente novamente.");
                    }

                    if (!resp.IsSuccessStatusCode)
                    {
                        throw new Exception("Erro ao obter dados do clima. Tente novamente mais tarde.");
                    }

                    string json = await resp.Content.ReadAsStringAsync();
                    var rascunho = JObject.Parse(json);

                    // Pegando valores de forma segura e usando os nomes exatos do Tempo.cs
                    string? NomeCidade = rascunho["name"]?.ToString();
                    double? Longitude = rascunho["coord"]?["lon"]?.Value<double>();
                    double? Latitude = rascunho["coord"]?["lat"]?.Value<double>();
                    double? TemperaturaMin = rascunho["main"]?["temp_min"]?.Value<double>();
                    double? TemperaturaMax = rascunho["main"]?["temp_max"]?.Value<double>();
                    double? Temperatura = rascunho["main"]?["temp"]?.Value<double>();
                    int? Visibilidade = rascunho["visibility"]?.Value<int>();
                    double? VelocidadeVento = rascunho["wind"]?["speed"]?.Value<double>();
                    string? Condicao = rascunho["weather"]?[0]?["main"]?.ToString();
                    string? Descricao = rascunho["weather"]?[0]?["description"]?.ToString();

                    long? nascerDoSolUnix = rascunho["sys"]?["sunrise"]?.Value<long>();
                    long? porDoSolUnix = rascunho["sys"]?["sunset"]?.Value<long>();

                    // Converter horários Unix para formato legível
                    DateTime? nascerDoSol = nascerDoSolUnix.HasValue
                        ? DateTimeOffset.FromUnixTimeSeconds(nascerDoSolUnix.Value).ToLocalTime().DateTime
                        : (DateTime?)null;

                    DateTime? porDoSol = porDoSolUnix.HasValue
                        ? DateTimeOffset.FromUnixTimeSeconds(porDoSolUnix.Value).ToLocalTime().DateTime
                        : (DateTime?)null;

                    return new Tempo
                    {
                        NomeCidade = NomeCidade,
                        Longitude = Longitude,
                        Latitude = Latitude,
                        TemperaturaMin = TemperaturaMin,
                        TemperaturaMax = TemperaturaMax,
                        Temperatura = Temperatura,
                        Visibilidade = Visibilidade,
                        VelocidadeVento = VelocidadeVento,
                        Condicao = Condicao,
                        Descricao = Descricao,
                        NascerDoSol = nascerDoSol?.ToString("HH:mm") ?? "Desconhecido",
                        PorDoSol = porDoSol?.ToString("HH:mm") ?? "Desconhecido",
                    };
                }
            }
            catch (HttpRequestException)
            {
                throw new Exception("Erro de conexão. Verifique sua internet e tente novamente.");
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao processar os dados: {ex.Message}");
            }
        }
    }
}
