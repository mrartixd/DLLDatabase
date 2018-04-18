using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Windows.Forms;

namespace WorldDatabase
{
    public class CountryDB
    {
        public static List<Country> GetAllCountries()
        {
            List<Country> list = null;
            using (OleDbConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                string sql = "SELECT * FROM country ORDER BY name";
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                OleDbDataReader reader = cmd.ExecuteReader();
                list = new List<Country>();
                using (reader)
                {
                    while (reader.Read())
                    {
                        Country country = new Country();
                        country.Code = reader[0].ToString();
                        country.Name = reader.GetString(1);
                        country.Continent = reader["Continent"].ToString();
                        country.Region = reader[3].ToString();
                        country.SurfaceArea = reader.GetDouble(4);
                        country.InderYear = reader.GetDouble(5);
                        country.Population = reader.GetDouble(6);
                        country.GovernmentForm = reader[7].ToString();
                        country.HeadOfState = reader[8].ToString();
                        OleDbCommand cmdCity = new OleDbCommand("SELECT * FROM city, country WHERE ID=Capital AND Code=CountryCode AND Code='"+country.Code+"'", conn);
                        OleDbDataReader readerCity = cmdCity.ExecuteReader();
                        while (readerCity.Read())
                        {
                            City city = new City();
                            city.ID = (int)readerCity[0];
                            city.Name = readerCity[1].ToString();
                            city.CountryCode = country.Code;
                            city.District = readerCity[3].ToString();
                            city.Population = readerCity.GetDouble(4);
                            country.Capital = city;
                        }
                        readerCity.Close();
                        list.Add(country);
                    }
                }
            }
            return list;
        }

        public static List<City> GetAllCities()
        {
            List<City> list = null;
            using (OleDbConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                string sql = "select * from city";
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                OleDbDataReader reader = cmd.ExecuteReader();
                list = new List<City>();
                using (reader)
                {
                    while (reader.Read())
                    {
                        City city = new City();
                        city.ID = (int)reader[0];
                        city.Name = reader[1].ToString();
                        city.CountryCode = reader[2].ToString();
                        city.District = reader[3].ToString();
                        city.Population = reader.GetDouble(4);
                        list.Add(city);

                    }

                }
            }
            return list;
        }


        public static List<City> GetAllCitiesbyCountryName(String country)
        {
            List<City> list = null;
            using (OleDbConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                string sql = "select * from city, country where  countrycode=code and country.name='" + country + "'";
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                OleDbCommand cmdCountry = new OleDbCommand("SELECT * FROM country", conn);
                OleDbDataReader reader = cmd.ExecuteReader();
                list = new List<City>();
                using (reader)
                {
                    while(reader.Read())
                    {
                        Country count = new Country();
                        City city = new City();
                        city.ID = (int)reader[0];
                        city.Name = reader[1].ToString();
                        city.CountryCode = count.Code;
                        city.District = reader[3].ToString();
                        city.Population = reader.GetDouble(4);
                        count.Capital = city;
                        list.Add(city);
                        
                    }
                  
                }
            }
                return list;
        }


        public static int InsertNewCity(City city)
        {
            int arv = 1;
            using (OleDbConnection conn = ConnectionDatabase.GetConnection())
            {
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "insert into City (Name,CountryCode,District,Population) values (?,?,?,?)";
                cmd.Parameters.AddWithValue("@name", city.Name);
                cmd.Parameters.AddWithValue("@countrycode", city.CountryCode);
                cmd.Parameters.AddWithValue("@district", city.District);
                cmd.Parameters.AddWithValue("@population", city.Population);
                cmd.Connection = conn;
                conn.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                    //MessageBox.Show("An Item has been successfully added", "Caption", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }
                catch
                {
                    arv = 0;
                }
                
            }

                return arv;
        }

        public static int InsertNewCountry(Country country)
        {
            int arv = 1;
            using (OleDbConnection conn = ConnectionDatabase.GetConnection())
            {
                OleDbCommand cmd = new OleDbCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandText = "insert into Country (Code,Name,Continent,Region,SurfaceArea,InderYear,Population, GovernmentForm,HeadOfState,Capital) values (?,?,?,?,?,?,?,?,?,?)";
                cmd.Parameters.AddWithValue("@code", country.Code);
                cmd.Parameters.AddWithValue("@name", country.Name);
                cmd.Parameters.AddWithValue("@continent", country.Continent);
                cmd.Parameters.AddWithValue("@region", country.Region);
                cmd.Parameters.AddWithValue("@surfacearea", country.SurfaceArea);
                cmd.Parameters.AddWithValue("@indepyear", country.InderYear);
                cmd.Parameters.AddWithValue("@population", country.Population);
                cmd.Parameters.AddWithValue("@governmentform", country.GovernmentForm);
                cmd.Parameters.AddWithValue("@headofstate", country.HeadOfState);
                cmd.Parameters.AddWithValue("@capital", country.Capital);
                cmd.Connection = conn;
                conn.Open();
                try
                {
                    cmd.ExecuteNonQuery();
                    //MessageBox.Show("An Item has been successfully added", "Caption", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                }
                catch
                {
                    arv = 0;
                }

            }
            return arv;
        }

        public static int DeleteCountry(String id)
        {
            int arv = 1;
            using (OleDbConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                OleDbCommand cmd = new OleDbCommand("DELETE FROM [country] WHERE [Code] = '"+ id +"'", conn);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    arv = 0;
                }
                
            }
                return arv;
        }

        public static int DeleteCity(int id)
        {
            int arv = 1;
            using (OleDbConnection conn = ConnectionDatabase.GetConnection())
            {
                conn.Open();
                OleDbCommand cmd = new OleDbCommand("DELETE FROM [city] WHERE [ID] = "+id, conn);
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    arv = 0;
                }

            }
            return arv;
        }
    }
}
