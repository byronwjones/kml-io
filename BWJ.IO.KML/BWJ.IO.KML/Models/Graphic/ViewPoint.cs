namespace BWJ.IO.KML.Models.Graphic
{
    public class ViewPoint : KmlElement
    {
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        
        public decimal Altitude { get; set; }
        public decimal Heading { get; set; }
        public decimal Tilt { get; set; }
        public decimal Range { get; set; }
        public AltitudeMode AltitudeMode { get; set; }

        protected override void PreChildRender(int indent)
        {
            RenderLine("<LookAt>", indent);
            RenderLine($"<longitude>{Longitude.ToString(DECIMAL_FORMAT)}</longitude>", indent, 1);
            RenderLine($"<latitude>{Latitude.ToString(DECIMAL_FORMAT)}</latitude>", indent, 1);
            RenderLine($"<altitude>{Altitude.ToString("0")}</altitude>", indent, 1);
            RenderLine($"<heading>{Heading.ToString(DECIMAL_FORMAT)}</heading>", indent, 1);
            RenderLine($"<tilt>{Tilt.ToString(DECIMAL_FORMAT)}</tilt>", indent, 1);
            RenderLine($"<range>{Range.ToString(DECIMAL_FORMAT)}</range>", indent, 1);
            RenderLine($"<gx:altitudeMode>{AltitudeMode.ToString()}</gx:altitudeMode>", indent, 1);
            RenderLine("</LookAt>", indent);
        }

        private const string DECIMAL_FORMAT = "0.000000000000000";
    }
}
