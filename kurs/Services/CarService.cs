using Autobase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autobase.Services
{
    public interface CarService
    {
        Car UpdateCar(Car car, string currentUser);
        Car GetDriversCar(string currentUser);
    }
}
