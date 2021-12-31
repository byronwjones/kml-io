using System;

namespace BWJ.IO.KML.Models.Graphic
{
    public class Coordinate
    {
        public Coordinate() { }
        public Coordinate(string coordinates)
        {
            string[] coords = coordinates.Split(',');

            Longitude = Convert.ToDecimal(coords[0]);
            Latitude = Convert.ToDecimal(coords[1]);
            Z = Convert.ToDecimal(coords[2]);
        }

        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public decimal Z { get; set; }

        public override string ToString()
        {
            return $"{Longitude.ToString(DECIMAL_FORMAT)},{Latitude.ToString(DECIMAL_FORMAT)},{Z.ToString(DECIMAL_FORMAT)}";
        }

        private const string DECIMAL_FORMAT = "0.000000000000000";
    }
}
