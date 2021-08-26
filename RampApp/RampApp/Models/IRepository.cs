using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampApp.Models
{
    public interface IRepository
    {
        List<LicencePlate> GetLicencePlates();

        LicencePlate GetLicencePlate(string licence_plate);
    }
}
