using System.Collections.Generic;

namespace BWJ.IO.KML.Models.Graphic
{
    public class PolygonPlacemark : Placemark
    {
        public bool Tessellate { get; set; }
        public List<Coordinate> Coordinates { get; set; }

        protected override void PreElementRender(int indent)
        {
            Coordinates = Coordinates ?? new List<Coordinate>();

            RenderLine("<Polygon>", indent);
                RenderLine($"<tessellate>{(Tessellate ? "1" : "0")}</tessellate>", indent, 1);
                RenderLine("<outerBoundaryIs>", indent, 1);
                    RenderLine("<LinearRing>", indent, 2);
                        RenderLine("<coordinates>", indent, 3);
                            RenderLine(string.Join(" ", Coordinates), indent, 4);
                        RenderLine("</coordinates>", indent, 3);
                    RenderLine("</LinearRing>", indent, 2);
                RenderLine("</outerBoundaryIs>", indent, 1);
            RenderLine("</Polygon>", indent);
        }
    }
}
