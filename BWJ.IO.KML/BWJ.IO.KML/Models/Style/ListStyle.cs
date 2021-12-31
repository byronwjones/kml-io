namespace BWJ.IO.KML.Models.Style
{
    public class ListStyle : StyleBase
    {
        public ListStyle()
        {
            Type = KmlElementType.ListStyle;
        }

        protected override void PreChildRender(int indent)
        {
            RenderLine("<ListStyle>", indent);
            RenderLine("</ListStyle>", indent);
        }
    }
}
