using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RampApp.Models
{
    public class Repository : IRepository
    {
        public LicencePlate GetLicencePlate(string licence_plate)
        {
                LicencePlate licencePlate = new LicencePlate();

                using (LicencePlateDB licensePlateDB = new LicencePlateDB())
                {
                    licencePlate = licensePlateDB.LicencePlate.SingleOrDefault(plate => plate.Value == licence_plate);
                }

            return licencePlate;
        }

        public List<LicencePlate> GetLicencePlates()
        {
            List<LicencePlate> licencePlates = new List<LicencePlate>();
            using (LicencePlateDB licensePlateDB = new LicencePlateDB())
            {
                foreach (LicencePlate lp in licensePlateDB.LicencePlate)
                {
                    //System.Diagnostics.Debug.WriteLine(lp.Value);
                    licencePlates.Add(lp);
                }
            }
            return licencePlates;
        }
    }
}