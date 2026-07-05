using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            List<EventViewModel> events = new List<EventViewModel>();
            List<ReservationViewModel> reservations = new List<ReservationViewModel>();

            try
            {
                HttpResponseMessage responseEvents = _client.GetAsync(_client.BaseAddress + "events/").Result;
                if (responseEvents.IsSuccessStatusCode)
                {
                    string dataEvents = responseEvents.Content.ReadAsStringAsync().Result;
                    events = JsonConvert.DeserializeObject<List<EventViewModel>>(dataEvents);
                }

                HttpResponseMessage responseReservations = _client.GetAsync(_client.BaseAddress + "reservations/").Result;
                if (responseReservations.IsSuccessStatusCode)
                {
                    string dataSalesChannels = responseReservations.Content.ReadAsStringAsync().Result;
                    reservations = JsonConvert.DeserializeObject<List<ReservationViewModel>>(dataSalesChannels);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions/logging here
            }

            var model = new TicketViewModel
            {
                EventItems = events.Select(e => new SelectListItem(e.Name, e.Id.ToString())),
                ReservationItems = reservations.Select(s => new SelectListItem(s.ReservationCode, s.Id.ToString()))
            };

            ViewBag.EventItems = new SelectList(events, "Id", "Name");
            ViewBag.ReservationItems = new SelectList(reservations, "Id", "ReservationCode");

            return View(model);
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
            List<EventViewModel> events = new List<EventViewModel>();
            List<ReservationViewModel> reservations = new List<ReservationViewModel>();

            HttpResponseMessage responseEvents = _client.GetAsync(_client.BaseAddress + "events/").Result;
            if (responseEvents.IsSuccessStatusCode)
            {
                string dataEvents = responseEvents.Content.ReadAsStringAsync().Result;
                events = JsonConvert.DeserializeObject<List<EventViewModel>>(dataEvents);
            }

            HttpResponseMessage responseReservations = _client.GetAsync(_client.BaseAddress + "reservations/").Result;
            if (responseReservations.IsSuccessStatusCode)
            {
                string dataSalesChannels = responseReservations.Content.ReadAsStringAsync().Result;
                reservations = JsonConvert.DeserializeObject<List<ReservationViewModel>>(dataSalesChannels);
            }
            
            try
            {
                TicketViewModel ticket = new TicketViewModel
                {
                    EventItems = events.Select(e => new SelectListItem(e.Name, e.Id.ToString())),
                    ReservationItems = reservations.Select(s => new SelectListItem(s.ReservationCode, s.Id.ToString()))
                };

                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "tickets/" + id.ToString()).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    ticket = JsonConvert.DeserializeObject<TicketViewModel>(data);
                }

                ViewBag.EventItems = new SelectList(events, "Id", "Name");
                ViewBag.ReservationItems = new SelectList(reservations, "Id", "ReservationCode");

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
