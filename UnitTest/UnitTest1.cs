using Database;
using Npgsql;
using Moq;
using MapQuestIntegration;
using static MapQuestIntegration.ApiClient;
using TourPlanner;
using static System.Net.Mime.MediaTypeNames;

namespace UnitTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestConnection()
        {
            bool isConnected = DbConfiguration.TestConnection();
            Assert.IsTrue(isConnected, "The database connection has failed");
        }

        [Test]
        public void GetConnection()
        {
            string expectedConnectionString = "Server=localhost;Port=5434;User Id=postgres;Password=dario;Database=tourplannerdb;";

            NpgsqlConnection con = DbConfiguration.GetConnection();
            string actualConnectionString = con.ConnectionString;

            Assert.AreEqual(expectedConnectionString, actualConnectionString);
        }

        [Test]
        public void WrongConnectionString()
        {
            string expectedConnectionString = "Server=localhost;Port=0000;User Id=postgres;Password=dario;Database=tourplannerdb;";

            NpgsqlConnection con = DbConfiguration.GetConnection();
            string actualConnectionString = con.ConnectionString;

            Assert.AreNotEqual(expectedConnectionString, actualConnectionString);
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

        [Test]
        public async Task GetDirectionsAsync1()
        {
            string distance = "29,73";
            string estimatedTime = "00:33:23";
            string from = "Wels";
            string to = "Linz";
            string apiKey = "djD80t47mqWeYR2wURRAV8oWVp01jITR";

            using (HttpClient client = new HttpClient())
            {
                string baseUrl = "https://www.mapquestapi.com/directions/v2/route";
                string requestUrl = $"{baseUrl}?key={apiKey}&from={from}&to={to}";

                HttpResponseMessage response = await client.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    string res = await response.Content.ReadAsStringAsync();    // Parse and handle the response content
                    var responseObj = Newtonsoft.Json.JsonConvert.DeserializeObject<MapQuestResponse>(res);

                    if (responseObj?.route?.legs?.Count > 0)        // Check if there's a route and it has legs
                    {
                        // Get the distance from the first leg
                        double distanceInMiles = responseObj.route.legs[0].distance;

                        // Convert miles to kilometers
                        double distanceInKilometers = distanceInMiles * 1.60934;
                        string realDistance = distanceInKilometers.ToString("F2");
                        string formattedTime = responseObj.route.formattedTime;

                        Assert.AreEqual(distance, realDistance);
                        Assert.AreEqual(estimatedTime, formattedTime);

                    }

                }
            }
        }

        [Test]
        public async Task GetDirectionsAsync2()
        {
            string distance = "1042,66";
            string estimatedTime = "09:46:28";
            string from = "Wels";
            string to = "Paris";
            string apiKey = "djD80t47mqWeYR2wURRAV8oWVp01jITR";

            using (HttpClient client = new HttpClient())
            {
                string baseUrl = "https://www.mapquestapi.com/directions/v2/route";
                string requestUrl = $"{baseUrl}?key={apiKey}&from={from}&to={to}";

                HttpResponseMessage response = await client.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    string res = await response.Content.ReadAsStringAsync();    // Parse and handle the response content
                    var responseObj = Newtonsoft.Json.JsonConvert.DeserializeObject<MapQuestResponse>(res);

                    if (responseObj?.route?.legs?.Count > 0)        // Check if there's a route and it has legs
                    {
                        // Get the distance from the first leg
                        double distanceInMiles = responseObj.route.legs[0].distance;

                        // Convert miles to kilometers
                        double distanceInKilometers = distanceInMiles * 1.60934;
                        string realDistance = distanceInKilometers.ToString("F2");
                        string formattedTime = responseObj.route.formattedTime;

                        Assert.AreEqual(distance, realDistance);
                        Assert.AreEqual(estimatedTime, formattedTime);

                    }

                }
            }
        }

        [Test]
        public async Task GetDirectionsAsync3()
        {
            string distance = "46,18";
            string estimatedTime = "00:34:39";
            string from = "Wels";
            string to = "Gmunden";
            string apiKey = "djD80t47mqWeYR2wURRAV8oWVp01jITR";

            using (HttpClient client = new HttpClient())
            {
                string baseUrl = "https://www.mapquestapi.com/directions/v2/route";
                string requestUrl = $"{baseUrl}?key={apiKey}&from={from}&to={to}";

                HttpResponseMessage response = await client.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    string res = await response.Content.ReadAsStringAsync();    // Parse and handle the response content
                    var responseObj = Newtonsoft.Json.JsonConvert.DeserializeObject<MapQuestResponse>(res);

                    if (responseObj?.route?.legs?.Count > 0)        // Check if there's a route and it has legs
                    {
                        // Get the distance from the first leg
                        double distanceInMiles = responseObj.route.legs[0].distance;

                        // Convert miles to kilometers
                        double distanceInKilometers = distanceInMiles * 1.60934;
                        string realDistance = distanceInKilometers.ToString("F2");
                        string formattedTime = responseObj.route.formattedTime;

                        Assert.AreEqual(distance, realDistance);
                        Assert.AreEqual(estimatedTime, formattedTime);

                    }

                }
            }
        }

        [Test]
        public async Task GetDirectionsAsync4()
        {
            string distance = "334,65";
            string estimatedTime = "03:15:36";
            string from = "Wien";
            string to = "Prag";
            string apiKey = "djD80t47mqWeYR2wURRAV8oWVp01jITR";

            using (HttpClient client = new HttpClient())
            {
                string baseUrl = "https://www.mapquestapi.com/directions/v2/route";
                string requestUrl = $"{baseUrl}?key={apiKey}&from={from}&to={to}";

                HttpResponseMessage response = await client.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    string res = await response.Content.ReadAsStringAsync();    // Parse and handle the response content
                    var responseObj = Newtonsoft.Json.JsonConvert.DeserializeObject<MapQuestResponse>(res);

                    if (responseObj?.route?.legs?.Count > 0)        // Check if there's a route and it has legs
                    {
                        // Get the distance from the first leg
                        double distanceInMiles = responseObj.route.legs[0].distance;

                        // Convert miles to kilometers
                        double distanceInKilometers = distanceInMiles * 1.60934;
                        string realDistance = distanceInKilometers.ToString("F2");
                        string formattedTime = responseObj.route.formattedTime;

                        Assert.AreEqual(distance, realDistance);
                        Assert.AreEqual(estimatedTime, formattedTime);

                    }

                }
            }
        }

        [Test]
        public async Task GetDirectionsAsync5()
        {
            string distance = "9,97";
            string estimatedTime = "00:13:39";
            string from = "Buchkirchen";
            string to = "Wels";
            string apiKey = "djD80t47mqWeYR2wURRAV8oWVp01jITR";

            using (HttpClient client = new HttpClient())
            {
                string baseUrl = "https://www.mapquestapi.com/directions/v2/route";
                string requestUrl = $"{baseUrl}?key={apiKey}&from={from}&to={to}";

                HttpResponseMessage response = await client.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    string res = await response.Content.ReadAsStringAsync();    // Parse and handle the response content
                    var responseObj = Newtonsoft.Json.JsonConvert.DeserializeObject<MapQuestResponse>(res);

                    if (responseObj?.route?.legs?.Count > 0)        // Check if there's a route and it has legs
                    {
                        // Get the distance from the first leg
                        double distanceInMiles = responseObj.route.legs[0].distance;

                        // Convert miles to kilometers
                        double distanceInKilometers = distanceInMiles * 1.60934;
                        string realDistance = distanceInKilometers.ToString("F2");
                        string formattedTime = responseObj.route.formattedTime;

                        Assert.AreEqual(distance, realDistance);
                        Assert.AreEqual(estimatedTime, formattedTime);

                    }

                }
            }
        }

        [Test]
        public async Task GetDirectionsAsync6()
        {
            string distance = "18,00";
            string estimatedTime = "00:17:00";
            string from = "Gunskirchen";
            string to = "Marchtrenk";
            string apiKey = "djD80t47mqWeYR2wURRAV8oWVp01jITR";

            using (HttpClient client = new HttpClient())
            {
                string baseUrl = "https://www.mapquestapi.com/directions/v2/route";
                string requestUrl = $"{baseUrl}?key={apiKey}&from={from}&to={to}";

                HttpResponseMessage response = await client.GetAsync(requestUrl);

                if (response.IsSuccessStatusCode)
                {
                    string res = await response.Content.ReadAsStringAsync();    // Parse and handle the response content
                    var responseObj = Newtonsoft.Json.JsonConvert.DeserializeObject<MapQuestResponse>(res);

                    if (responseObj?.route?.legs?.Count > 0)        // Check if there's a route and it has legs
                    {
                        // Get the distance from the first leg
                        double distanceInMiles = responseObj.route.legs[0].distance;

                        // Convert miles to kilometers
                        double distanceInKilometers = distanceInMiles * 1.60934;
                        string realDistance = distanceInKilometers.ToString("F2");
                        string formattedTime = responseObj.route.formattedTime;

                        Assert.AreEqual(distance, realDistance);
                        Assert.AreEqual(estimatedTime, formattedTime);

                    }

                }
            }
        }




        [Test]

        public void CheckMainViewList1()    // should not be abkle not store the name, because it is already in the database
        {
            string storedTourName = "one";

            GetTourData checkList = new GetTourData();
            bool check = checkList.CheckTourList(storedTourName);

            Assert.IsTrue(check, "The tour was successfully stored.");

        }

        [Test]
        public void CheckMainViewList2()    // should be able to store the tour name
        {
            string storedTourName = "two";

            GetTourData checkList = new GetTourData();
            bool check = checkList.CheckTourList(storedTourName);

            Assert.IsFalse(check, "The tour could not be stored.");     // if it is false then that means that there are no similar tour names in the list/database

        }
        [Test]
        public void CheckMainViewList3()    // should be able to store the tour name
        {
            string storedTourName = "kkkkkkk";

            GetTourData checkList = new GetTourData();
            bool check = checkList.CheckTourList(storedTourName);

            Assert.IsFalse(check, "The tour could not be stored.");     // if it is false then that means that there are no similar tour names in the list/database

        }
        [Test]
        public void CheckMainViewList4()    // should be able to store the tour name
        {
            string storedTourName = "www";

            GetTourData checkList = new GetTourData();
            bool check = checkList.CheckTourList(storedTourName);

            Assert.IsTrue(check, "The tour could not be stored.");     // if it is false then that means that there are no similar tour names in the list/database

        }
        

        [Test]
        public void InsertLogs()
        {
            TourData log = new TourData();
            log.TourName = "TestName";
            log.DateTime = "TestTime";
            log.Difficulty = "TestDifficulty";
            log.TotalTime = "TestTotalTime";
            log.Rating = "TestRating";
            log.Comment = "TestComment";
            int rowsAffected = 0;

            using (NpgsqlConnection connection = DbConfiguration.GetConnection())
            {
                connection.Open();

                // Define your SQL INSERT statement with placeholders for parameters
                string insertSql = "INSERT INTO logs (tour_name, date_time, difficulty, total_time, rating, comment) VALUES (@TourName, @DateTime, @Difficulty, @TotalTime, @Rating, @Comment)";

                using (NpgsqlCommand cmd = new NpgsqlCommand(insertSql, connection))
                {
                    cmd.Parameters.AddWithValue("@TourName", log.TourName);
                    cmd.Parameters.AddWithValue("@DateTime", log.DateTime);
                    cmd.Parameters.AddWithValue("@Difficulty", log.Difficulty);
                    cmd.Parameters.AddWithValue("@TotalTime", log.TotalTime);
                    cmd.Parameters.AddWithValue("@Rating", log.Rating);
                    cmd.Parameters.AddWithValue("@Comment", log.Comment);

                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }

            Assert.Greater(rowsAffected, 0);

        }

        [Test]
        public void InsertLogs2()
        {
            TourData log = new TourData();
            log.TourName = "ausflug";
            log.DateTime = "mitternacht";
            log.Difficulty = "schwierig";
            log.TotalTime = "4 stunden";
            log.Rating = "7/10";
            log.Comment = "nie wieder";
            int rowsAffected = 0;

            using (NpgsqlConnection connection = DbConfiguration.GetConnection())
            {
                connection.Open();

                // Define your SQL INSERT statement with placeholders for parameters
                string insertSql = "INSERT INTO logs (tour_name, date_time, difficulty, total_time, rating, comment) VALUES (@TourName, @DateTime, @Difficulty, @TotalTime, @Rating, @Comment)";

                using (NpgsqlCommand cmd = new NpgsqlCommand(insertSql, connection))
                {
                    cmd.Parameters.AddWithValue("@TourName", log.TourName);
                    cmd.Parameters.AddWithValue("@DateTime", log.DateTime);
                    cmd.Parameters.AddWithValue("@Difficulty", log.Difficulty);
                    cmd.Parameters.AddWithValue("@TotalTime", log.TotalTime);
                    cmd.Parameters.AddWithValue("@Rating", log.Rating);
                    cmd.Parameters.AddWithValue("@Comment", log.Comment);

                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }

            Assert.Greater(rowsAffected, 0);

        }
        [Test]
        public void InsertLogs3()
        {
            TourData log = new TourData();
            log.TourName = "data";
            log.DateTime = "12.12.2012";
            log.Difficulty = "leicht";
            log.TotalTime = "30 min";
            log.Rating = "10/10";
            log.Comment = "cool";
            int rowsAffected = 0;

            using (NpgsqlConnection connection = DbConfiguration.GetConnection())
            {
                connection.Open();

                // Define your SQL INSERT statement with placeholders for parameters
                string insertSql = "INSERT INTO logs (tour_name, date_time, difficulty, total_time, rating, comment) VALUES (@TourName, @DateTime, @Difficulty, @TotalTime, @Rating, @Comment)";

                using (NpgsqlCommand cmd = new NpgsqlCommand(insertSql, connection))
                {
                    cmd.Parameters.AddWithValue("@TourName", log.TourName);
                    cmd.Parameters.AddWithValue("@DateTime", log.DateTime);
                    cmd.Parameters.AddWithValue("@Difficulty", log.Difficulty);
                    cmd.Parameters.AddWithValue("@TotalTime", log.TotalTime);
                    cmd.Parameters.AddWithValue("@Rating", log.Rating);
                    cmd.Parameters.AddWithValue("@Comment", log.Comment);

                    rowsAffected = cmd.ExecuteNonQuery();
                }
            }

            Assert.Greater(rowsAffected, 0);

        }

        [Test]
        public void RetrieveTourData1()
        {
            string validTourName = "one";
            GetTourData data = new GetTourData();
            TourData tour = new TourData();
            tour = data.RetrieveTourData(validTourName);

            Assert.IsFalse(string.IsNullOrEmpty(tour.Distance));

        }

        [Test]
        public void RetrieveTourData2()
        {
            string validTourName = "iwos";
            GetTourData data = new GetTourData();
            TourData tour = new TourData();
            tour = data.RetrieveTourData(validTourName);

            Assert.IsFalse(string.IsNullOrEmpty(tour.EstimatedTime));

        }
        [Test]
        public void RetrieveTourData3()
        {
            string invalidTourName = "two";
            GetTourData data = new GetTourData();
            TourData tour = new TourData();
            tour = data.RetrieveTourData(invalidTourName);

            Assert.IsTrue(string.IsNullOrEmpty(tour.Distance));

        }

        [Test]
        public void RetrieveTourData4()
        {
            string invalidTourName = "nonexistent";
            GetTourData data = new GetTourData();
            TourData tour = new TourData();
            tour = data.RetrieveTourData(invalidTourName);

            Assert.IsTrue(string.IsNullOrEmpty(tour.Distance));

        }

        [Test]
        public void RetrieveTourData5()
        {
            string invalidTourName = "6un";
            GetTourData data = new GetTourData();
            TourData tour = new TourData();
            tour = data.RetrieveTourData(invalidTourName);

            Assert.IsFalse(string.IsNullOrEmpty(tour.Distance));

        }

        [Test]
        public void TestReadTourListFromDatabase()
        {
            // Arrange: Create an instance of your data access class (e.g., GetTourData)
            GetTourData dataAccess = new GetTourData();

            // Act: Call the method to read tour names from the database
            List<TourData> tourNames = dataAccess.ReadTourListFromDatabase();

            // Assert: Check if the list is not null and contains at least one item
            Assert.That(tourNames, Is.Not.Null);
            Assert.That(tourNames, Is.Not.Empty);
        }

       
        
    }



    
}
