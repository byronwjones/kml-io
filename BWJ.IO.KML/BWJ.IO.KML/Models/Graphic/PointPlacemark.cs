namespace BWJ.IO.KML.Models.Graphic
{
    public class PointPlacemark : Placemark
    {
        public int DrawOrder { get; set; }
        public Coordinate Coordinate { get; set; }

        protected override void PreElementRender(int indent)
        {
            Coordinate = Coordinate ?? new Coordinate();

            RenderLine("<Point>", indent);
                RenderLine($"<gx:drawOrder>{DrawOrder}</gx:drawOrder>", indent, 1);
                RenderLine($"<coordinates>{Coordinate}</coordinates>", indent, 1);
            RenderLine("</Point>", indent);
        }
    }
}
