using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using TicketApp.Models;

namespace TicketApp.Controllers
{
    public class ReservationController : Controller
    {
        Uri apiAddress = new Uri("http://localhost:5115");
        private readonly HttpClient _client;

        public ReservationController()
        {
            _client = new HttpClient();
            _client.BaseAddress = apiAddress;
        }

        [HttpGet("/Home/Reservation")]
        public IActionResult Index()
        {
            List<ReservationViewModel> reservations = new List<ReservationViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "reservations/").Result;

            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                reservations = JsonConvert.DeserializeObject<List<ReservationViewModel>>(data);
            }

            return View(reservations);
        }

        [HttpGet]
        public IActionResult Create()
        {
            List<UserViewModel> users = new List<UserViewModel>();
            List<EventViewModel> events = new List<EventViewModel>();
            List<SalesChannelViewModel> salesChannels = new List<SalesChannelViewModel>();

            try
            {
                HttpResponseMessage responseUsers = _client.GetAsync(_client.BaseAddress + "users/").Result;
                if (responseUsers.IsSuccessStatusCode)
                {
                    string dataUsers = responseUsers.Content.ReadAsStringAsync().Result;
                    users = JsonConvert.DeserializeObject<List<UserViewModel>>(dataUsers);
                }

                HttpResponseMessage responseEvents = _client.GetAsync(_client.BaseAddress + "events/").Result;
                if (responseEvents.IsSuccessStatusCode)
                {
                    string dataEvents = responseEvents.Content.ReadAsStringAsync().Result;
                    events = JsonConvert.DeserializeObject<List<EventViewModel>>(dataEvents);
                }

                HttpResponseMessage responseSalesChannels = _client.GetAsync(_client.BaseAddress + "saleschannels/").Result;
                if (responseSalesChannels.IsSuccessStatusCode)
                {
                    string dataSalesChannels = responseSalesChannels.Content.ReadAsStringAsync().Result;
                    salesChannels = JsonConvert.DeserializeObject<List<SalesChannelViewModel>>(dataSalesChannels);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions/logging here
            }

            var model = new ReservationViewModel
            {
                UserItems = users.Select(u => new SelectListItem(u.Name, u.Id.ToString())),
                EventItems = events.Select(e => new SelectListItem(e.Name, e.Id.ToString())),
                SalesChannelItems = salesChannels.Select(s => new SelectListItem(s.Name, s.Id.ToString()))
            };
            // Bind data to SelectList (Value field, Text field)
            ViewBag.UserItems = new SelectList(users, "Id", "Name");
            ViewBag.EventItems = new SelectList(events, "Id", "Name");
            ViewBag.SalesChannelItems = new SelectList(salesChannels, "Id", "Name");

            return View(model);
        }

        [HttpPost]
        public IActionResult Create(ReservationViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PostAsync(_client.BaseAddress + "reservations/", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Reserva creada exitosamente";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Ha ocurrido un error al crear la reserva: " + ex.Message;
                return View();
            }

            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            List<UserViewModel> users = new List<UserViewModel>();
            List<EventViewModel> events = new List<EventViewModel>();
            List<SalesChannelViewModel> salesChannels = new List<SalesChannelViewModel>();

            HttpResponseMessage responseUsers = _client.GetAsync(_client.BaseAddress + "users/").Result;
            if (responseUsers.IsSuccessStatusCode)
            {
                string dataUsers = responseUsers.Content.ReadAsStringAsync().Result;
                users = JsonConvert.DeserializeObject<List<UserViewModel>>(dataUsers);
            }

            HttpResponseMessage responseEvents = _client.GetAsync(_client.BaseAddress + "events/").Result;
            if (responseEvents.IsSuccessStatusCode)
            {
                string dataEvents = responseEvents.Content.ReadAsStringAsync().Result;
                events = JsonConvert.DeserializeObject<List<EventViewModel>>(dataEvents);
            }

            HttpResponseMessage responseSalesChannels = _client.GetAsync(_client.BaseAddress + "saleschannels/").Result;
            if (responseSalesChannels.IsSuccessStatusCode)
            {
                string dataSalesChannels = responseSalesChannels.Content.ReadAsStringAsync().Result;
                salesChannels = JsonConvert.DeserializeObject<List<SalesChannelViewModel>>(dataSalesChannels);
            }

            try
            {
                ReservationViewModel reservation = new ReservationViewModel 
                {
                    UserItems = users.Select(u => new SelectListItem(u.Name, u.Id.ToString())),
                    EventItems = events.Select(e => new SelectListItem(e.Name, e.Id.ToString())),
                    SalesChannelItems = salesChannels.Select(s => new SelectListItem(s.Name, s.Id.ToString()))
                };

                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "reservations/" + id.ToString()).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    reservation = JsonConvert.DeserializeObject<ReservationViewModel>(data);
                }

                ViewBag.UserItems = new SelectList(users, "Id", "Name");
                ViewBag.EventItems = new SelectList(events, "Id", "Name");
                ViewBag.SalesChannelItems = new SelectList(salesChannels, "Id", "Name");

                return View(reservation);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Ha ocurrido un error al modificar la reserva: " + ex.Message;
                return View();
            }
        }

        [HttpPost]
        public IActionResult Edit(int id, ReservationViewModel model)
        {
            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = _client.PutAsync(_client.BaseAddress + "reservations/" + id.ToString(), content).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Reserva modificada exitosamente";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Ha ocurrido un error al modificar la reserva: " + ex.Message;
                return View();
            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                ReservationViewModel reservation = new ReservationViewModel();
                HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "reservations/" + id.ToString()).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    reservation = JsonConvert.DeserializeObject<ReservationViewModel>(data);
                }
                return View(reservation);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Ha ocurrido un error al eliminar la reserva: " + ex.Message;
                return View();
            }
        }

        [HttpPost, ActionName("Delete")]

        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                HttpResponseMessage response = _client.DeleteAsync(_client.BaseAddress + "reservations/" + id.ToString()).Result;

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Reserva eliminada exitosamente";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Ha ocurrido un error al eliminar la reserva: " + ex.Message;
                return View();
            }
            return View();
        }
    }
}
