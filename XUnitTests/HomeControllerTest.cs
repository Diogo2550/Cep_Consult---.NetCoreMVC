using Cep_Consult.Controllers;
using Cep_Consult.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.EventSource;
using Moq;
using System;
using Xunit;

namespace XUnitTests {
    public class HomeControllerTest {
        private readonly HomeController c = new HomeController();

        [Fact]
        public async void GetCep_Should_Be_Correct() {
            await c.GetCep("28960000");

            CEP cep = (CEP)c.ViewData["Cep"];

            Assert.Equal("28960-000", cep.Cep);
            Assert.Equal("", cep.Logradouro);
            Assert.Equal("", cep.Complemento);
            Assert.Equal("", cep.Bairro);
            Assert.Equal("Iguaba Grande", cep.Localidade);
            Assert.Equal("RJ", cep.UF);
        }

        [Fact]
        public async void GetCep_Should_Be_Number() {
            string arg = "testtest";
            await c.GetCep(arg);

            Assert.Equal("O CEP informado deve ser um número", c.ViewData["Error"]);
        }

        [Theory]
        [InlineData("289600000")]
        [InlineData("2896000000")]
        [InlineData("2896000")]
        [InlineData("289600")]
        public async void GetCep_Should_Have_Eigth_Algarisms(string cep) {
            await c.GetCep(cep);

            Assert.Equal("Cep deve ter 8 algarismos", c.ViewData["Error"]);
        }

        [Fact]
        public async void GetCep_Cep_Should_Not_Be_Null() {
            await c.GetCep(null);

            Assert.Equal("Campo cep não deve estar vazio", c.ViewData["Error"]);
        }
    }
}