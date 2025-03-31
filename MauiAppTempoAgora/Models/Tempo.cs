namespace MauiAppTempoAgora.Models
{
    public class Tempo
    {
        public string? NomeCidade { get; set; }  // Nome da cidade
        public double? Longitude { get; set; }   // Longitude
        public double? Latitude { get; set; }    // Latitude
        public double? TemperaturaMin { get; set; } // Temperatura mínima
        public double? TemperaturaMax { get; set; } // Temperatura máxima
        public double? Temperatura { get; set; } // Temperatura principal
        public int? Visibilidade { get; set; }   // Visibilidade (m)
        public double? VelocidadeVento { get; set; } // Velocidade do vento (m/s)
        public string? Condicao { get; set; }    // Condição principal do clima (ex: Chuva, Nublado)
        public string? Descricao { get; set; }   // Descrição detalhada (ex: "chuva moderada")
        public string? NascerDoSol { get; set; } // Horário do nascer do sol
        public string? PorDoSol { get; set; }    // Horário do pôr do sol
    }
}

