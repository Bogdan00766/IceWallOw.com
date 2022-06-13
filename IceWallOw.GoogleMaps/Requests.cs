using IceWallOw.GoogleMaps.JSONClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace IceWallOw.GoogleMaps
{
    public static class Requests
    {
        private static String apiKey = "AIzaSyCCBIJCEPFt69itfifvLWmXEHGGbceebEk";
        private static HttpClient client = new HttpClient();

        public static async Task<float> CalculateDistance(string origin, string destination)
        {
            String requestString = $"https://maps.googleapis.com/maps/api/distancematrix/json?destinations={destination}&origins={origin}&key={apiKey}";
            var response = await client.GetAsync(requestString);
            var responseBody = await response.Content.ReadAsStringAsync();
            //Console.WriteLine(responseBody);
            GDResponse? responseJSON = JsonSerializer.Deserialize<GDResponse>(responseBody);
            

            return responseJSON.rows[0].elements[0].distance.value/1000;
        }
    }
}
