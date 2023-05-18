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
    public class BookFoodController : Controller
    {
        private IBookFood _IBookFood;
        private IFood _IFood;
        private IDishtypes _IDishtypes;
        private ITotalbilling _ITotalBilling;
        public BookFoodController(IBookFood IBookFood, IDishtypes IDishtypes, IFood IFood, ITotalbilling ITotalBilling)
        {
            _IBookFood = IBookFood;
            _IFood = IFood;
            _IDishtypes = IDishtypes;
            _ITotalBilling = ITotalBilling;
        }

     
        [HttpGet]
        public IActionResult BookFood()
        {
            try
            {
                BookingFood bookingfood = new BookingFood();
                bookingfood.FoodList = _IFood.GetAllFood();
                bookingfood.FoodType = "1";
                bookingfood.MealType = "1";
                SetSlider();
                return View(bookingfood);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BookFood(BookingFood bookingfood)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("BookFood", bookingfood);
                }

                if (bookingfood != null && bookingfood.FoodList != null)
                {
                    var result = 0;
                    var validateChecked = 0;
                    BookingFood objFood = null;

                    for (int i = 0; i < bookingfood.FoodList.Count(); i++)
                    {
                        if (bookingfood.FoodList[i].FoodChecked)
                        {
                            validateChecked = 1;

                            objFood = new BookingFood()
                            {
                                FoodType = bookingfood.FoodType,
                                MealType = bookingfood.MealType,
                                DishType = bookingfood.DishType,
                                DishName = Convert.ToInt32(bookingfood.FoodList[i].FoodID),
                                BookingID = Convert.ToInt32(HttpContext.Session.GetInt32("BookingID")),
                                Createdby = Convert.ToInt32(HttpContext.Session.GetString("UserID")),
                                CreatedDate = DateTime.Now
                            };
                            result = _IBookFood.BookFood(objFood);
                        }
                    }

                    if (validateChecked == 0)
                    {
                        ModelState.AddModelError("", "You have not choose any Food !");
                    }

                    SetSlider();

                    if (result > 0)
                    {
                        ModelState.Clear();
                        TempData["BookingFoodMessage"] = "Food Booked Successfully";
                        var TotalAmount = _ITotalBilling.TotalAmount(objFood.BookingID);
                        TempData["TotalAmount"] = TotalAmount;
                        //return View("Success");
                        return RedirectToAction("BookLight", "BookLight");
                    }
                    else
                    {
                        return View("BookFood", bookingfood);
                    }
                }
                else
                {
                    return View("BookFood", bookingfood);
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
                var Images = _IFood.ShowAllFood();
                ViewBag.SliderFoodImages = Images;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public JsonResult BindDishtype()
        {
            try
            {
                var liDishtype = _IDishtypes.GetDishtypeList();
                return Json(data: liDishtype);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
