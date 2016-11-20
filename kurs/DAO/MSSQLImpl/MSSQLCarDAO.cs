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
            AppContext.Entry(car).State = EntityState.Modified;
            AppContext.SaveChanges();
        }
    }
}