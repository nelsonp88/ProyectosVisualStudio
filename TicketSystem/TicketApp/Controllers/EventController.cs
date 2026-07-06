using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using TicketApp.Models;

namespace TicketApp.Controllers
{
    public class EventController : Controller
    {
        Uri apiAddress = new Uri("http://localhost:5115");
        private readonly HttpClient _client;

        public EventController()
        {
            _client = new HttpClient();
            _client.BaseAddress = apiAddress;
        }

        [HttpGet("/Home/Event")]
        public IActionResult Index()
        {
            List<EventViewModel> events = new List<EventViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "events/").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                events = JsonConvert.DeserializeObject<List<EventViewModel>>(data);
            }

            return View(events);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EventViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "events/", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Evento creado exitosamente";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Ha ocurrido un error al crear el evento: " + ex.Message;
                return View();
            }

            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                EventViewModel evento = new EventViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "events/" + id.ToString()).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    evento = JsonConvert.DeserializeObject<EventViewModel>(data);
                }
                return View(evento);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Ha ocurrido un error al modificar el evento: " + ex.Message;
                return View();
            }
        }

        [HttpPost]
        public IActionResult Edit(int id, EventViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "events/" + id.ToString(), content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Evento modificado exitosamente";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Ha ocurrido un error al modificar el evento: " + ex.Message;
                return View();
            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                EventViewModel evento = new EventViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "events/" + id.ToString()).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    evento = JsonConvert.DeserializeObject<EventViewModel>(data);
                }
                return View(evento);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Ha ocurrido un error al eliminar el evento: " + ex.Message;
                return View();
            }
        }

        [HttpPost, ActionName("Delete")]

        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "events/" + id.ToString()).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Evento eliminado exitosamente";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Ha ocurrido un error al eliminar el evento: " + ex.Message;
                return View();
            }
            return View();
        }
    }
}
