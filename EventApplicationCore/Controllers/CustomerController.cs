using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EventApplicationCore.Filters;
using EventApplicationCore.Interface;
using EventApplicationCore.Model;
using Microsoft.AspNetCore.Http;
using EventApplicationCore.Library;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EventApplicationCore.Controllers
{
    [ValidateUserSession]
    public class CustomerController : Controller
    {
        private ILogin _ILogin;
        private IRegistration _IRegistration;
        private IDashboard _IDashboard;
        public CustomerController(ILogin ILogin, IRegistration IRegistration, IDashboard IDashboard)
        {
            _ILogin = ILogin;
            _IRegistration = IRegistration;
            _IDashboard = IDashboard;

        }


        // GET: /<controller>/ Userinformation
        public IActionResult Dashboard(Registration Registration)
        {
            var userId = Convert.ToInt32(HttpContext.Session.GetString("UserID"));
            DashboardModel customerdashboardmodel = _IDashboard.CustomerDashboard(userId);
            return View(customerdashboardmodel);
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View(new ChangePasswordModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(ChangePasswordModel ChangePasswordModel)
        {
            if (!ModelState.IsValid)
            {
                return View(ChangePasswordModel);
            }

            var password = EncryptionLibrary.EncryptText(ChangePasswordModel.Password);
            var registrationModel = _IRegistration.Userinformation(Convert.ToInt32(HttpContext.Session.GetString("UserID")));

            if (registrationModel.Password == password)
            {
                var registration = new Registration();
                registration.Password = EncryptionLibrary.EncryptText(ChangePasswordModel.NewPassword);
                registration.ID = Convert.ToInt32(HttpContext.Session.GetString("UserID"));
                var result = _ILogin.UpdatePassword(registration);

                if (result)
                {
                    TempData["MessageUpdate"] = "Password Updated Successfully";
                    ModelState.Clear();
                    return View(new ChangePasswordModel());
                }
                else
                {
                    return View(ChangePasswordModel);
                }
            }
            else
            {
                TempData["MessageUpdate"] = "Invalid Password";
                return View(ChangePasswordModel);
            }
        }
    }
}
