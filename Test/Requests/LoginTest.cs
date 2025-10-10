using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using Test.Infrastructure.Database;
using AutoTrackMinimalApi.Domain.DTOs;
using AutoTrackMinimalApi.Domain.Entity;
using AutoTrackMinimalApi.Infrastructure.Database;
using AutoTrackMinimalApi.Domain.ModelViews;
using AutoTrackMinimalApi.Domain.DTOs.Enuns;

namespace Test.Requests
{
    [TestClass]
    public class LoginTestsMSTest
    {
        private static CustomWebApplicationFactory? _factory;
        private static HttpClient? _client;

        public LoginTestsMSTest()
        {
            _factory = new CustomWebApplicationFactory();
            _client = _factory.CreateClient();
        }

        [ClassInitialize]
        public static void ClassInit(TestContext context)
        {
            _factory = new CustomWebApplicationFactory();
            _client = _factory.CreateClient();
        }

        [TestMethod]
        public async Task Login_Retorna_Token_Quando_Credenciais_Validas()
        {
            // Semeia um administrador na base de dados em memória
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                // Adiciona o administrador caso ele ainda não exista
                var adminExistente = await dbContext.Administrators.FindAsync(1);
                if (adminExistente == null)
                {
                    dbContext.Administrators.Add(new Administrator
                    {
                        Email = "admin@test.com",
                        Password = "P@ssw0rd",
                        Profile = Profile.Admin
                    });
                    await dbContext.SaveChangesAsync();
                }
            }

            // Cria os dados para login
            var loginDto = new LoginDto
            {
                Email = "admin@test.com",
                Senha = "P@ssw0rd"
            };

            // Realiza a chamada ao endpoint de login
            var response = await _client.PostAsJsonAsync("/administrator/login", loginDto);
            response.EnsureSuccessStatusCode();

            // Converte a resposta
            var loginResult = await response.Content.ReadFromJsonAsync<AdmLoggedInDto>();

            // Verifica se o token foi retornado
            Assert.IsNotNull(loginResult, "O resultado do login não pode ser nulo.");
            Assert.IsFalse(string.IsNullOrEmpty(loginResult?.Token), "O token não foi retornado.");
        }
    }
}