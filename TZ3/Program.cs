using OpenWeatherMap;
using System;
using System.IO;
using System.Threading.Tasks;

namespace TZ3
{
    class Program
    {
        private static string path;
        static void Main(string[] args)
        {
            Console.WriteLine("Enter your city");
            string city;
            city = Console.ReadLine();
            //string url = String.Format(@"http://api.openweathermap.org/data/2.5/weather?q={0}&units=metric&appid=47db2e9d7bbbd1f32788194422b353b6", city);
            //HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            //HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            //string response;
            //using (StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream()))
            //{
            //    response = streamReader.ReadToEnd();
            //}
            //WeatherResponse weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(response);
            //Console.WriteLine("Temperature {0}: {1} °С", weatherResponse.Name, weatherResponse.TemperatureInfo.Temp);
            //Console.WriteLine("Humadity is ", weatherResponse.HumanidiInfo.Humadity);

            try
            {
                var currentWeather = GetWeather(city);

                path = "./" + city + "." + DateTime.Now.ToShortDateString() + ".txt";
                using (var writer = new StreamWriter(path, false, System.Text.Encoding.Default))
                {
                    writer.WriteLine("Данные для города {0} на дату {1}\nТемпература = {2}C\nВлажность = {3}%\n" +
                        "Время восхода {4}\nВремя заката {5}", currentWeather.Result.City.Name, currentWeather.Result.LastUpdate.Value.ToString(),
                        currentWeather.Result.Temperature.Value - 273, currentWeather.Result.Humidity.Value,
                        currentWeather.Result.City.Sun.Rise.ToString(), currentWeather.Result.City.Sun.Set.ToString());
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Ошибка: " + e.Message);
            }
        }

        public static Task<CurrentWeatherResponse> GetWeather(string city)
        {
            try
            {
                var client = new OpenWeatherMapClient("47db2e9d7bbbd1f32788194422b353b6");
                return client.CurrentWeather.GetByName(city);
            }
            catch
            {
                throw;
            }
        }
    }
}
