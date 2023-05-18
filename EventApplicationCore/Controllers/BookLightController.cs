using EventApplicationCore.Filters;
using EventApplicationCore.Interface;
using EventApplicationCore.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace EventApplicationCore.Controllers
{
    [ValidateUserSession]
    public class BookLightController : Controller
    {
        private IBookingLight _IBookingLight;
        private ILight _ILight;
        private ITotalbilling _ITotalBilling;

        public BookLightController(IBookingLight IBookingLight, ILight ILight, ITotalbilling ITotalBilling)
        {
            _IBookingLight = IBookingLight;
            _ILight = ILight;
            _ITotalBilling = ITotalBilling;
        }

        [HttpGet]
        public IActionResult BookLight()
        {
            try
            {
                BookingLight bookinglight = new BookingLight();
                bookinglight.LightList = _ILight.GetAllLight();
                bookinglight.LightType = "1";
                SetSlider();
                return View(bookinglight);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BookLight(BookingLight bookinglight)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("BookLight", bookinglight);
                }

                if (bookinglight != null && bookinglight.LightList != null)
                {
                    var result = 0;
                    var validateChecked = 0;
                    BookingLight objbookinglight = null;

                    for (int i = 0; i < bookinglight.LightList.Count(); i++)
                    {
                        if (bookinglight.LightList[i].LightChecked)
                        {
                            validateChecked = 1;


                            objbookinglight = new BookingLight()
                            {
                                LightType = bookinglight.LightType,
                                LightIDSelected = Convert.ToInt32(bookinglight.LightList[i].LightID),
                                BookingID = Convert.ToInt32(HttpContext.Session.GetInt32("BookingID")),
                                Createdby = Convert.ToInt32(HttpContext.Session.GetString("UserID")),
                                CreatedDate = DateTime.Now
                            };
                            result = _IBookingLight.BookingLight(objbookinglight);

                        }
                    }

                    if (validateChecked == 0)
                    {
                        ModelState.AddModelError("", "You have not choose any Lighting !");
                    }

                    SetSlider();

                    if (result > 0)
                    {
                        ModelState.Clear();
                        TempData["BookingLightingMessage"] = "Lighting Booked Successfully";
                        var TotalAmount = _ITotalBilling.TotalAmount(objbookinglight.BookingID);
                        TempData["TotalAmount"] = TotalAmount;
                        //return View("Success");
                        return RedirectToAction("BookFlower", "BookFlower");
                    }
                    else
                    {
                        return View("BookLight", bookinglight);
                    }
                }
                else
                {
                    return View("BookLight", bookinglight);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [NonAction]
        private void SetSlider()
        {
            try
            {
                var Images = _ILight.ShowAllLight();
                ViewBag.SliderLightImages = Images;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}