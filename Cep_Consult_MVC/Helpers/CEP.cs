using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading;
using System.ComponentModel.DataAnnotations;

namespace Cep_Consult.Helpers {
    public class CEP {
        [Required(ErrorMessage = "O campo deve ser preenchido")]
        [MinLength(8, ErrorMessage = "O campo deve ter 8 dígitos")]
        [MaxLength(8, ErrorMessage = "O campo deve ter 8 dígitos")]
        [Display(Name = "Número do Cep:")]
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string UF { get; set; }
        public string Unidade { get; set; }
        public string Ibge { get; set; }
        public string gia { get; set; }
        public bool Erro { get; set; }

        private static HttpClient client;

        private CEP() {
            client = new HttpClient();
        }

        public static async Task<CEP> GetCepFromAPI(string cep) {
            if(cep.Length != 8) throw new Exception("Cep deve ter 8 algarismos");
            try {
                uint numCep = uint.Parse(cep);
            } catch {
                throw new Exception("O CEP informado deve ser um número");
            }

            var Cep = new CEP();
            client.BaseAddress = new Uri($"https://viacep.com.br/ws/{cep}/json/");
            HttpResponseMessage response = await client.GetAsync(client.BaseAddress);
            
            try {
                CEP c = await response.Content.ReadAsAsync<CEP>();

                Cep.Erro = c.Erro;
                Cep.Cep = c.Cep;
                Cep.Logradouro = c.Logradouro;
                Cep.Complemento = c.Complemento;
                Cep.Bairro = c.Bairro;
                Cep.Localidade = c.Localidade;
                Cep.UF = c.UF;
            } catch(Exception) {
                throw new Exception();
            }

            client.Dispose();
            return Cep;
        }
    }
}