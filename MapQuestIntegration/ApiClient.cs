using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;


namespace MapQuestIntegration
{



    public class ApiClient
    {

        private readonly string apiKey;

        public ApiClient(string apiKey)
        {
            this.apiKey = apiKey;
        }

        public class MapQuestResponse
        {
            public Route route { get; set; }
        }

        public class Route
        {
            public List<Leg> legs { get; set; }
            public string formattedTime { get; set; } // Add this field
        }

        public class Leg
        {
            public double distance { get; set; }
            public double time { get; set; }
        }

        public async Task<string> GetDirections(string from, string to)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Construct the request URL
                    
                    string baseUrl = "https://www.mapquestapi.com/directions/v2/route";
                    string requestUrl = $"{baseUrl}?key={apiKey}&from={from}&to={to}";

                    // Send the GET request
                    HttpResponseMessage response = await client.GetAsync(requestUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        // Parse and return the response content
                        return await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        return $"Error: {response.StatusCode} - {response.ReasonPhrase}";
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions here
                    return $"Exception: {ex.Message}";
                }
            }
        }

        public class DistanceAndTime
        {
            public string DistanceInKilometers { get; set; }
            public string EstimatedTime { get; set; }
        }

        public async Task<DistanceAndTime> GetDistanceAndTime(ApiClient apiClient, string from, string to)
        {
            try
            {
                //ClearCache();
                string directionsResponse = await apiClient.GetDirections(from, to);

                if (!string.IsNullOrEmpty(directionsResponse) && !directionsResponse.StartsWith("Error"))       // Check if the response contains valid data
                {       
                    var responseObj = Newtonsoft.Json.JsonConvert.DeserializeObject<MapQuestResponse>(directionsResponse); // Parse the JSON response

                    if (responseObj?.route?.legs?.Count > 0)        // Check if there's a route and it has legs
                    {
                        // Get the distance from the first leg
                        double distanceInMiles = responseObj.route.legs[0].distance;

                        double distanceInKilometers = distanceInMiles * 1.60934;        // convert miles to kilometers
                        string formattedTime = responseObj.route.formattedTime;         // the time is in hours
                        
                        return new DistanceAndTime
                        {
                            DistanceInKilometers = distanceInKilometers.ToString("F2"),
                            EstimatedTime = formattedTime
                        };

                        //MessageBox.Show($"Connected to the MapQuest API successfully. Distance from and to: {distanceInKilometers:F2} kilometers");

                    }
                    
                }
                else
                {
                    
                    //MessageBox.Show("Failed to connect to the MapQuest API.");
                }
            }
            catch (Exception ex)
            {
                
                //MessageBox.Show($"Exception: {ex.Message}");
            }

            return null;
        }


        public async Task<byte[]> GetRouteImage(ApiClient apiClient, string from, string to)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    // Construct the request URL for the static map image
                    string baseUrl = "https://www.mapquestapi.com/staticmap/v5/map";
                    string requestUrl = $"{baseUrl}?key={apiKey}&start={from}&end={to}&routeColor:0x0078D7|width:3|";

                    Console.WriteLine("thats the apikey: " + apiKey);
                    Console.WriteLine("thats the baseurl: " + baseUrl);
                    Console.WriteLine("thats the requesturl: " + requestUrl);
                    Console.WriteLine("thats the from: " + from);
                    Console.WriteLine("thats the to: " + to);

                    // Send the GET request to get the route image
                    HttpResponseMessage response = await client.GetAsync(requestUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        // Read the image as a byte array
                        return await response.Content.ReadAsByteArrayAsync();
                    }
                    else
                    {
                        // Handle errors here
                        Console.WriteLine("it do be trash");
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions here
                return null;
            }
        }








    }






    class Program
    {
        static async Task Main(string[] args)
        {
            /*string apiKey = "djD80t47mqWeYR2wURRAV8oWVp01jITR";
            ApiClient apiClient = new ApiClient(apiKey);

            string from = "Origin Address";
            string to = "Destination Address";

            try
            {
                string directionsResponse = await apiClient.GetDirections(from, to);

                // Check if the response contains valid data
                if (!string.IsNullOrEmpty(directionsResponse) && !directionsResponse.StartsWith("Error"))
                {
                    Console.WriteLine("Connected to the MapQuest API successfully.");
                }
                else
                {
                    Console.WriteLine("Failed to connect to the MapQuest API.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }

            Console.ReadKey();*/
        }
    }


}
