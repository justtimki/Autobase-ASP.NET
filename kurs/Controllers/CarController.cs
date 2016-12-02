using Autobase.DAO;
using Autobase.Models;
using Autobase.Services;
using Autobase.Utils;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Autobase.Controllers
{
    [Authorize(Roles = "DRIVER")]
    public class CarController : Controller
    {
        [Dependency]
        public CarService carService { get; set; }
                
        public ActionResult CarDetails()
        {
            SetCurrentAccountCar();
            return View("CarDetails");
        }

        public ActionResult UpdateCar(Car car)
        {
            try
            {
                if (verifyCar(car))
                {
                    ViewBag.car = carService.UpdateCar(car, User.Identity.Name);
                }
                else
                {
                    SetCurrentAccountCar();
                }
            }
            catch (Exception e)
            {
                return View("Error");
            }
            return View("CarDetails");
        }

        private void SetCurrentAccountCar()
        {
            ViewBag.car = carService.GetDriversCar(User.Identity.Name);
        }

        private bool verifyCar(Car car)
        {
            bool isValid = true;
            if (!ValidationUtil.isNameValid(car.CarName))
            {
                ModelState.AddModelError("CarName", "Car Name shouldn't be empty or contains illegal chars.");
                isValid = false;
            }
            if (!ValidationUtil.isNumberValid(car.CarCapacity))
            {
                ModelState.AddModelError("CarCapacity", "Car Capacity should be more than 0");
                isValid = false;
            }
            if (!ValidationUtil.isNumberValid(car.CarSpeed))
            {
                ModelState.AddModelError("CarCapacity", "Car Speed should be more than 0");
                isValid = false;
            }

            return isValid;
        }
    }
}