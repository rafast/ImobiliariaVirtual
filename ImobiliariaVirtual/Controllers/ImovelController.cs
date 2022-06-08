using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using ImobiliariaVirtual.Models;
using Microsoft.AspNetCore.Mvc;

namespace ImobiliariaVirtual.Controllers
{
    public class ImovelController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            IEnumerable<Imovel> imoveis = null;
            using (var imoveisAPI = new HttpClient())
            {
                imoveisAPI.BaseAddress = new Uri("https://62a0e8127b9345bcbe42018c.mockapi.io/api/v1/");

                var resposta = imoveisAPI.GetAsync("imoveis");
                resposta.Wait();

                var resultado = resposta.Result;
                if (resultado.IsSuccessStatusCode)
                {
                    var conteudoResposta = resultado.Content.ReadAsStringAsync();
                    conteudoResposta.Wait();
                    imoveis = JsonSerializer.Deserialize<IList<Imovel>>(conteudoResposta.Result);
                }
            }
            return View(imoveis);
        }

        //GET Direciona o usuário para o formulário de criação de imóveis
        public IActionResult Create()
        {
            return View();
        }

        //POST Recebe um imóvel da CreateView de imóveis e faz uma chamada para a API criar o novo imóvel
        [HttpPost]
        public IActionResult Create(Imovel obj)
        {
            using (var imoveisAPI = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://62a0e8127b9345bcbe42018c.mockapi.io/api/v1/imoveis");
                string json = JsonSerializer.Serialize(obj);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = imoveisAPI.SendAsync(request);
                response.Wait();

                if (response.Result.IsSuccessStatusCode)
                {
                    TempData["success"] = "Imóvel criado com sucesso!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["error"] = "O servidor está fora do ar. Tente novamente mais tarde.";
                }
            }
            return NotFound();
        }

        //GET Direciona o usuário para a EditView e carrega os dados do imóvel a ser alterado
        public IActionResult Edit(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }

            Imovel imovel = null;
            using (var imoveisAPI = new HttpClient())
            {
                imoveisAPI.BaseAddress = new Uri("https://62a0e8127b9345bcbe42018c.mockapi.io/api/v1/");
                var response = imoveisAPI.GetAsync("imoveis/" + id.ToString());
                response.Wait();

                if (response.Result.IsSuccessStatusCode)
                {
                    var content = response.Result.Content.ReadAsStringAsync();
                    content.Wait();
                    imovel = JsonSerializer.Deserialize<Imovel>(content.Result);
                }
            }

            return View(imovel);
        }

        //POST Recebe um imóvel da EditView de imóveis e faz uma chamada para a API atualizar os dados imóvel
        [HttpPost]
        public IActionResult Edit(Imovel obj)
        {
            using (var imoveisAPI = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, "https://62a0e8127b9345bcbe42018c.mockapi.io/api/v1/imoveis/" + obj.Id);
                string json = JsonSerializer.Serialize(obj);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = imoveisAPI.SendAsync(request);
                response.Wait();

                if (response.Result.IsSuccessStatusCode)
                {
                    TempData["success"] = "Imóvel atualizado com sucesso!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["error"] = "O servidor está fora do ar. Tente novamente mais tarde.";
                }
            }
            return NotFound();
        }

        //GET Direciona o usuário para a DeleteView e carrega os dados do imóvel a ser deletado
        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }

            Imovel imovel = null;
            using (var imoveisAPI = new HttpClient())
            {
                imoveisAPI.BaseAddress = new Uri("https://62a0e8127b9345bcbe42018c.mockapi.io/api/v1/");
                //HTTP GET
                var response = imoveisAPI.GetAsync("imoveis/" + id.ToString());
                response.Wait();

                if (response.Result.IsSuccessStatusCode)
                {
                    var content = response.Result.Content.ReadAsStringAsync();
                    content.Wait();
                    imovel = JsonSerializer.Deserialize<Imovel>(content.Result);
                }
                else
                {
                    TempData["error"] = "O servidor está fora do ar. Tente novamente mais tarde.";
                }
            }

            return View(imovel);
        }

        //POST Recebe um imóvel da DeleteView de imóveis e faz uma chamada para a API deletar o imóvel
        [HttpPost]
        public IActionResult Delete(Imovel obj)
        {
            using (var imoveisAPI = new HttpClient())
            {
                imoveisAPI.BaseAddress = new Uri("https://62a0e8127b9345bcbe42018c.mockapi.io/api/v1/");

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, "https://62a0e8127b9345bcbe42018c.mockapi.io/api/v1/imoveis/" + obj.Id);
                string json = JsonSerializer.Serialize<Imovel>(obj);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = imoveisAPI.SendAsync(request);
                response.Wait();

                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    TempData["success"] = "Imóvel deletado com sucesso!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["error"] = "O servidor está fora do ar. Tente novamente mais tarde.";
                }
            }
            return NotFound();
        }

        //GET Direciona o usuário para a DetailView e carrega os dados do imóvel a ser detalhado
        public IActionResult Detail(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }

            Imovel imovel = null;
            using (var imoveisAPI = new HttpClient())
            {
                imoveisAPI.BaseAddress = new Uri("https://62a0e8127b9345bcbe42018c.mockapi.io/api/v1/");
                var response = imoveisAPI.GetAsync("imoveis/" + id.ToString());
                response.Wait();

                if (response.Result.IsSuccessStatusCode)
                {
                    var content = response.Result.Content.ReadAsStringAsync();
                    content.Wait();
                    imovel = JsonSerializer.Deserialize<Imovel>(content.Result);
                }
                else
                {
                    return NotFound();
                }
            }

            return View(imovel);
        }
    }
}
