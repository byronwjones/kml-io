namespace BWJ.IO.KML.Models.Style
{
    public class PolyStyle : StyleBase
    {
        public PolyStyle()
        {
            Type = KmlElementType.PolyStyle;
        }

        public string ColorABGR { get; set; }

        protected override void PreChildRender(int indent)
        {
            RenderLine("<PolyStyle>", indent);
            RenderLine($"<color>{ColorABGR}</color>", indent, 1);
            RenderLine("</PolyStyle>", indent);
        }
    }
}
