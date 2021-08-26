using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RampWebApp.Models
{
    public class LicensePlateService
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<string> ProcessImage(string image_path)
        {
            string SECRET_KEY = "YOUR_KEY";

            var content = new StringContent(image_path);

            var response = await client.PostAsync("https://api.openalpr.com/v3/recognize_bytes?recognize_vehicle=1&country=us&secret_key=" + SECRET_KEY, content).ConfigureAwait(false);

            var buffer = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            var byteArray = buffer.ToArray();
            var responseString = Encoding.UTF8.GetString(byteArray, 0, byteArray.Length);

            return responseString;
        }
    }
}