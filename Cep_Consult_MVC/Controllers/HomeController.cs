using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net.Http;
using Cep_Consult.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cep_Consult.Controllers {
    public class HomeController : Controller {

        private HttpClient client = new HttpClient();

        // GET: /<controller>/
        public IActionResult Index() {
            return View();
        }

        [ActionName("BuscarCep")]
        public async Task<IActionResult> GetCep(string cep) {
            CEP c = new CEP();
            try { 
                c = await GetCepFromAPI(cep);
            } catch(NullReferenceException) {
                ViewData.Add("Error", "Campo cep não deve estar vazio");
            } catch(ArgumentException) {
                ViewData.Add("Error", "O CEP informado deve ser um número");
            } catch (Exception err) {
                ViewData.Add("Error", err.Message);
            }

            return View("index", c);            
        }

        [NonAction]
        public async Task<CEP> GetCepFromAPI(string cep) {
            if(cep.Length != 8) throw new Exception("Cep deve ter 8 algarismos");
            try {
                uint numCep = uint.Parse(cep);
            } catch {
                throw new Exception("O CEP informado deve ser um número");
            }

            client.BaseAddress = new Uri($"https://viacep.com.br/ws/{cep}/json/");
            HttpResponseMessage response = await client.GetAsync(client.BaseAddress);
            var Cep = new CEP();

            try {
                CEP c = await response.Content.ReadAsAsync<CEP>();

                Cep = c;
            } catch(Exception) {
                throw new Exception();
            }

            return Cep;
        }
    }
}
