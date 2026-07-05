using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using TicketApp.Models;

namespace TicketApp.Controllers
{
    public class TicketController : Controller
    {
        Uri apiAddress = new Uri("http://localhost:5115");
        private readonly HttpClient _client;

        public TicketController()
        {
            _client = new HttpClient();
            _client.BaseAddress = apiAddress;
        }

        [HttpGet("/Home/Ticket")]
        public IActionResult Index()
        {
            List<TicketViewModel> tickets = new List<TicketViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "tickets/").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                tickets = JsonConvert.DeserializeObject<List<TicketViewModel>>(data);
            }

            return View(tickets);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(TicketViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "tickets/", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Entrada creada exitosamente";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Ha ocurrido un error al crear la entrada: " + ex.Message;
                return View();
            }

            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                TicketViewModel ticket = new TicketViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "tickets/" + id.ToString()).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    ticket = JsonConvert.DeserializeObject<TicketViewModel>(data);
                }
                return View(ticket);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Ha ocurrido un error al modificar la entrada: " + ex.Message;
                return View();
            }
        }

        [HttpPost]
        public IActionResult Edit(int id, TicketViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "tickets/" + id.ToString(), content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Entrada modificada exitosamente";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Ha ocurrido un error al modificar la entrada: " + ex.Message;
                return View();
            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                TicketViewModel ticket = new TicketViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "tickets/" + id.ToString()).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    ticket = JsonConvert.DeserializeObject<TicketViewModel>(data);
                }
                return View(ticket);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Ha ocurrido un error al eliminar la entrada: " + ex.Message;
                return View();
            }
        }

        [HttpPost, ActionName("Delete")]

        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "tickets/" + id.ToString()).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Entrada eliminada exitosamente";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Ha ocurrido un error al eliminar la entrada: " + ex.Message;
                return View();
            }
            return View();
        }
    }
}
