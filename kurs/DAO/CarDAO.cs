using Autobase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Autobase.DAO
{
    public interface CarDAO
    {
        List<Car> Read();
        void Create(Car car);
        void Delete(Car car);
        void Update(Car car);
        Car GetById(int id);
    }
}