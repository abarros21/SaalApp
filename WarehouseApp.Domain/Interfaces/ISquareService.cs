using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarehouseApp.Domain.Interfaces
{
    public interface ISquareService
    {
        List<Square> CalcDistances(Warehouse warehouse);
    }
}
