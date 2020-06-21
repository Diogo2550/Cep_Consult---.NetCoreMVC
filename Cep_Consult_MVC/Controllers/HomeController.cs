using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cep_Consult.Helpers;
using System.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cep_Consult.Controllers {
    public class HomeController : Controller {
        // GET: /<controller>/
        public IActionResult Index() {
            return View();
        }

        public async Task<IActionResult> GetCep(string numCep) {
            try { 
                CEP cep = await CEP.GetCepFromAPI(numCep);
                ViewData.Add("Cep", cep);
            } catch(NullReferenceException) {
                ViewData.Add("Error", "Campo cep não deve estar vazio");
            } catch(ArgumentException) {
                ViewData.Add("Error", "O CEP informado deve ser um número");
            } catch (Exception err) {
                ViewData.Add("Error", err.Message);
            }

            return View("index");            
        }
    }
}
