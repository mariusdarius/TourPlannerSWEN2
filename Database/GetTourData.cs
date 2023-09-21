using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class GetTourData
    {
        

        public TourData RetrieveTourData(string tourName) 
        {
            TourData tour = new TourData();
            tour.TourName = tourName;
            tour = AccessDatabase(tour);

            return tour;
        }    

        public TourData AccessDatabase(TourData tour) 
        {
            bool isConnected = DbConfiguration.TestConnection();

            if (!isConnected)
            {
                throw new InvalidOperationException("Database connection failed."); ;
            }

            try
            {
                using (NpgsqlConnection connection = DbConfiguration.GetConnection())
                {
                    connection.Open();

                    // Define your SQL INSERT statement with placeholders for parameters
                    string readSql = "SELECT * FROM tours WHERE tour_name = @tourName";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(readSql, connection))
                    {
                        cmd.Parameters.AddWithValue("@tourName", tour.TourName);

                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Update the 'tour' object with retrieved data
                                tour.Description = reader["description"].ToString();
                                tour.FromLocation = reader["from_location"].ToString();
                                tour.ToLocation = reader["to_location"].ToString();
                                tour.Transport = reader["transport_type"].ToString();
                                tour.Distance = reader["distance"].ToString();
                                tour.EstimatedTime = reader["estimated_time"].ToString();
                            }
                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
            }

            return tour;
        }

        public void InsertLogs(TourData log)
        {
            bool isConnected = DbConfiguration.TestConnection();

            if (!isConnected)
            {
                throw new InvalidOperationException("Database connection failed."); ;
            }

            try
            {
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

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Insert was successful.");
                        }
                        else
                        {
                            Console.WriteLine("Insert failed.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
            }


        }

        public List<TourData> RetrieveAllTourLogsData(string tourName)
        {
            List<TourData> tourLogs = new List<TourData>();

            bool isConnected = DbConfiguration.TestConnection();

            if (!isConnected)
            {
                throw new InvalidOperationException("Database connection failed.");
            }

            try
            {
                using (NpgsqlConnection connection = DbConfiguration.GetConnection())
                {
                    connection.Open();

                    // Define your SQL SELECT statement to retrieve all logs for the given tour name
                    string readSql = "SELECT * FROM logs WHERE tour_name = @tourName";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(readSql, connection))
                    {
                        cmd.Parameters.AddWithValue("@tourName", tourName);

                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TourData tour = new TourData
                                {
                                    DateTime = reader["date_time"].ToString(),
                                    Difficulty = reader["difficulty"].ToString(),
                                    TotalTime = reader["total_time"].ToString(),
                                    Rating = reader["rating"].ToString(),
                                    Comment = reader["comment"].ToString(),
                                    TourName = reader["tour_name"].ToString()
                                };

                                tourLogs.Add(tour);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed
            }

            return tourLogs;
        }

        public List<TourData> ReadTourListFromDatabase()
        {
            List<TourData> tourNames = new List<TourData>();

            bool isConnected = DbConfiguration.TestConnection();

            if (!isConnected)
            {
                throw new InvalidOperationException("Database connection failed.");
            }

            try
            {
                using (NpgsqlConnection connection = DbConfiguration.GetConnection())
                {
                    connection.Open();

                    // Define your SQL SELECT statement to retrieve tour names
                    string readSql = "SELECT tour_name FROM tours";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(readSql, connection))
                    {
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                TourData tour = new TourData
                                {
                                    TourName = reader["tour_name"].ToString()
                                };
                                //MessageBox.Show("the tour name is: " + tour.TourName);
                                tourNames.Add(tour);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed
            }

            return tourNames;
        }




        public TourData RetrieveFromAndToForRoute(TourData data)
        {
            bool isConnected = DbConfiguration.TestConnection();

            if (!isConnected)
            {
                throw new InvalidOperationException("Database connection failed.");
            }

            using (NpgsqlConnection connection = DbConfiguration.GetConnection())
            {
                connection.Open();

                string readSql = "SELECT from_location, to_location, distance, estimated_time  FROM tours WHERE tour_name = @tourName";

                using (NpgsqlCommand cmd = new NpgsqlCommand(readSql, connection))
                {
                    cmd.Parameters.AddWithValue("@tourName", data.TourName);

                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            data.FromLocation = reader["from_location"].ToString(); 
                            data.ToLocation = reader["to_location"].ToString(); 
                            data.Distance = reader["distance"].ToString(); 
                            data.EstimatedTime = reader["estimated_time"].ToString(); 
                        }
                        
                    }
                }
            }

            return data;
        }

        public bool CheckTourList(string listName)
        {
            bool isConnected = DbConfiguration.TestConnection();

            if (!isConnected)
            {
                throw new InvalidOperationException("Database connection failed.");
            }

            using (NpgsqlConnection connection = DbConfiguration.GetConnection())
            {
                connection.Open();

                string readSql = "SELECT tour_name FROM tours WHERE tour_name = @tourName";

                using (NpgsqlCommand cmd = new NpgsqlCommand(readSql, connection))
                {
                    cmd.Parameters.AddWithValue("@tourName", listName);

                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return true;        // if a matching tour name is found in the database it will return true which means that the new tour will not be added to the list
                        }
                        else
                        {
                            return false;       // if there isnt any tour name with the exact same name it will return false and be added to the database as a new entry
                        }
                    }
                }
            }
        }




    }



}

