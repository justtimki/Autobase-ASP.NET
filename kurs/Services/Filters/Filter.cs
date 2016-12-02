using Autobase.Models;
using Autobase.Models.enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autobase.Services.Filters
{
    public interface Filter
    {
        List<Order> FilterOrderByStatus(string status);
    }
}
