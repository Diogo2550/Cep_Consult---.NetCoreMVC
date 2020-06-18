using Cep_Consult.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.EventSource;
using Moq;
using System;
using Xunit;

namespace XUnitTests {
    public class HomeControllerTest {
        private readonly HomeController c = new HomeController();
        [Fact]
        public void GetCep_Should_Be_Correct() {
            c.GetCep("28960000");

            Assert.Equal("28960-000", c.ViewData["cep"]);
            Assert.Equal("", c.ViewData["logradouro"]);
            Assert.Equal("", c.ViewData["complemento"]);
            Assert.Equal("", c.ViewData["bairro"]);
            Assert.Equal("Iguaba Grande", c.ViewData["localidade"]);
            Assert.Equal("RJ", c.ViewData["uf"]);
        }

        [Fact]
        public void GetCep_Should_Be_Number() {
            var ex = Assert.Throws<ArgumentException>(() => c.GetCep("Diogo"));

            Assert.Equal("Cep deve ser número", ex.Message);
        }

        [Theory]
        [InlineData("289600000")]
        [InlineData("2896000000")]
        [InlineData("2896000")]
        [InlineData("289600")]
        public void GetCep_Should_Have_Eigth_Algarisms(string cep) {
            var ex = Assert.Throws<ArgumentException>(() => c.GetCep(cep));

            Assert.Equal("Cep deve ter 8 algarismos", ex.Message);
        }

        public void GetCep_Cep_Should_Not_Be_Null() {
            var ex = Assert.Throws<ArgumentNullException>(() => c.GetCep(null));

            Assert.Equal("Cep não pode estar vazio", ex.Message);
        }
    }
}