using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
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
            return View();
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
