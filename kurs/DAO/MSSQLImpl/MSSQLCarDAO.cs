using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autobase.Models;
using Autobase.App_Context;

namespace Autobase.DAO.MSSQLImpl
{
    public class MSSQLCarDAO : CarDAO
    {
        readonly ApplicationContext appContext;

        public MSSQLCarDAO(ApplicationContext appContext)
        {
            this.appContext = appContext;
        }

        public void Create(Car car)
        {
            appContext.Cars.Add(car);
            appContext.SaveChanges();
        }

        public void Delete(Car car)
        {
            appContext.Cars.Remove(car);
            appContext.SaveChanges();
        }

        public List<Car> Read()
        {
            List<Car> cars = appContext.Cars.ToList();
            return cars;
        }

        public void Update(Car car)
        {
            //Car carToChange = appContext.Cars.First(c => c.CarId == car.CarId);
            //carToChange.CarCapacity = car.CarCapacity;
            //carToChange.
        }
    }
}