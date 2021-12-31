namespace BWJ.IO.KML.Models.Style
{
    public class LineStyle : StyleBase
    {
        public LineStyle()
        {
            Type = KmlElementType.LineStyle;
        }

        public string ColorABGR { get; set; }

        protected override void PreChildRender(int indent)
        {
            RenderLine("<LineStyle>", indent);
            RenderLine($"<color>{ColorABGR}</color>", indent, 1);
            RenderLine("</LineStyle>", indent);
        }
    }
}
