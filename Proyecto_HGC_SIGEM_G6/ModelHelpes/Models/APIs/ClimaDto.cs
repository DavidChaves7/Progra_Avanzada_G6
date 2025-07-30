using System.Collections.Generic;

namespace ModelHelper.Models
{
    public class ClimaDto
    {
        public List<Weather> Weather { get; set; }
        public Main Main { get; set; }
    }

    public class Weather
    {
        public string Description { get; set; }
    }

    public class Main
    {
        public float Temp { get; set; }
        public int Humidity { get; set; }
    }
}
