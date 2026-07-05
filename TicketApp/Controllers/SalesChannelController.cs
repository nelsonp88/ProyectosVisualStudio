using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using TicketApp.Models;

namespace TicketApp.Controllers
{
    public class SalesChannelController : Controller
    {
        Uri apiAddress = new Uri("http://localhost:5115");
        private readonly HttpClient _client;

        public SalesChannelController()
        {
            _client = new HttpClient();
            _client.BaseAddress = apiAddress;
        }

        [HttpGet("/Home/SalesChannel")]
        public IActionResult Index()
        {
            List<SalesChannelViewModel> saleschannels = new List<SalesChannelViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "saleschannels/").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                saleschannels = JsonConvert.DeserializeObject<List<SalesChannelViewModel>>(data);
            }

            return View(saleschannels);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(SalesChannelViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "saleschannels/", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Canal de ventas creado exitosamente";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Ha ocurrido un error al crear el canal de ventas: " + ex.Message;
                return View();
            }

            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                SalesChannelViewModel saleschannel = new SalesChannelViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "saleschannels/" + id.ToString()).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    saleschannel = JsonConvert.DeserializeObject<SalesChannelViewModel>(data);
                }
                return View(saleschannel);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Ha ocurrido un error al modificar el canal de ventas: " + ex.Message;
                return View();
            }
        }

        [HttpPost]
        public IActionResult Edit(int id, SalesChannelViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "saleschannels/" + id.ToString(), content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Canal de ventas modificado exitosamente";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Ha ocurrido un error al modificar el canal de ventas: " + ex.Message;
                return View();
            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                SalesChannelViewModel saleschannel = new SalesChannelViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "saleschannels/" + id.ToString()).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    saleschannel = JsonConvert.DeserializeObject<SalesChannelViewModel>(data);
                }
                return View(saleschannel);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Ha ocurrido un error al eliminar el canal de ventas: " + ex.Message;
                return View();
            }
        }

        [HttpPost, ActionName("Delete")]

        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "saleschannels/" + id.ToString()).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Canal de ventas eliminado exitosamente";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Ha ocurrido un error al eliminar el canal de ventas: " + ex.Message;
                return View();
            }
            return View();
        }
    }
}
