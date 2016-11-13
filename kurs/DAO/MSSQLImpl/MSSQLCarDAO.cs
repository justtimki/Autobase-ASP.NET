using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autobase.Models;
using Autobase.App_Context;
using System.Data.Entity;
using Microsoft.Practices.Unity;

namespace Autobase.DAO.MSSQLImpl
{
    public class MSSQLCarDAO : CarDAO
    {
        [Dependency]
        public ApplicationContext AppContext { get; set; }

        public void Create(Car car)
        {
            AppContext.Cars.Add(car);
            AppContext.SaveChanges();
        }

        public void Delete(Car car)
        {
            AppContext.Cars.Remove(car);
            AppContext.SaveChanges();
        }

        public Car GetById(int id)
        {
            return AppContext.Cars.First(car => car.CarId == id);
        }

        public List<Car> Read()
        {
            List<Car> cars = AppContext.Cars.ToList();
            return cars;
        }

        public void Update(Car car)
        {
            Car carToChange = AppContext.Cars.First(c => c.CarId == car.CarId);
            if (carToChange == null)
            {
                throw new ArgumentException("Car with id " + car.CarId + " not found.");
            }
            carToChange.CarCapacity = car.CarCapacity;
            carToChange.CarName = car.CarName;
            carToChange.CarSpeed = car.CarSpeed;
            carToChange.IsHealthy = car.IsHealthy;

            AppContext.Entry(carToChange).State = EntityState.Modified;
            AppContext.SaveChanges();
        }
    }
}