using MauiAppTempoAgora.Models;
using MauiAppTempoAgora.Services;
using Microsoft.Maui.Networking; // Para verificar conexão com a internet

namespace MauiAppTempoAgora
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txt_cidade.Text))
                {
                    lbl_res.Text = "Por favor, digite o nome de uma cidade.";
                    return;
                }

                // Verificar se há conexão com a internet antes de buscar os dados
                if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
                {
                    await DisplayAlert("Sem Conexão", "Você está sem internet. Verifique sua conexão e tente novamente.", "OK");
                    return;
                }

                Tempo? t = await DataService.GetPrevisao(txt_cidade.Text);

                if (t != null)
                {
                    string dados_previsao = $"📍 Cidade: {t.NomeCidade} \n" +
                                            $"🌍 Latitude: {t.Latitude} | Longitude: {t.Longitude} \n" +
                                            $"🌅 Nascer do Sol: {t.NascerDoSol} \n" +
                                            $"🌇 Pôr do Sol: {t.PorDoSol} \n" +
                                            $"🌡️ Temp Máx: {t.TemperaturaMax}°C | Temp Min: {t.TemperaturaMin}°C \n" +
                                            $"🌡️ Temperatura Atual: {t.Temperatura}°C \n" +
                                            $"🌬️ Vento: {t.VelocidadeVento} m/s \n" +
                                            $"👀 Visibilidade: {t.Visibilidade} m \n" +
                                            $"☁️ Clima: {t.Condicao} - {t.Descricao} \n";

                    lbl_res.Text = dados_previsao;
                }
                else
                {
                    lbl_res.Text = "Sem dados de previsão para esta cidade.";
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Erro", ex.Message, "OK");
            }
        }
    }
}


