using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RampApp.Models;
using RampWebApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace RampApp.Controllers
{
    public class HomeController : Controller
    {
        private IRepository repository = RepositoryFactory.GetRepository();
        private static string temp;

        private static LicencePlate licencePlate1;

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult LicensePlate()
        {
            System.Diagnostics.Debug.WriteLine(temp);

            return View(licencePlate1);
        }

        [HttpPost]
        //[Route("Home/upload")]
        public ActionResult LicensePlate(HttpPostedFileBase file)
        {
            // extract only the fielname           

            string imageName = Path.GetFileName(file.FileName);

            // var length = file.InputStream.Length; //Length: 103050706

            var length = file.InputStream.Length;

            byte[] fileData = null;

            using (var binaryReader = new BinaryReader(file.InputStream))
            {
                fileData = binaryReader.ReadBytes(file.ContentLength);
            }

            string imagebase64 = Convert.ToBase64String(fileData);

            LicensePlateService licensePlateService = new LicensePlateService();

             Task<string> recognizeTask = Task.Run(() => licensePlateService.ProcessImage(imagebase64));
             recognizeTask.Wait();
             string task_result = recognizeTask.Result;
             var license_plate = JsonConvert.DeserializeObject<Results>(task_result);
             string plate = license_plate.results[0].Plate;
            //System.Diagnostics.Debug.WriteLine(plate);

            LicencePlate licencePlate = repository.GetLicencePlate(plate);

            //var licencePlate1 = repository.GetLicencePlate(plate);

            temp = null;
            licencePlate1 = null;
           

            if (licencePlate != null)
            {
                temp = licencePlate.Value;

                licencePlate1 = licencePlate;

                System.Diagnostics.Debug.WriteLine("License plate exists");
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("License plate doesn't exist");
            }

            return View(licencePlate);
        }
    }
}