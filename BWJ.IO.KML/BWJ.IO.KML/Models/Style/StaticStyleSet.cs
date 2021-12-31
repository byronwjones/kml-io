namespace BWJ.IO.KML.Models.Style
{
    public class StaticStyleSet : KmlElement
    {
        public StaticStyleSet()
        {
            Type = KmlElementType.StaticStyleSet;
            ChildTypes = new KmlElementType[] 
            { KmlElementType.IconStyle, KmlElementType.LineStyle, KmlElementType.ListStyle, KmlElementType.PolyStyle };
        }

        public string Name { get; set; }

        protected override void PreChildRender(int indent)
        {
            RenderLine($"<Style id=\"{Name}\">", indent);
        }

        protected override void PostChildRender(int indent)
        {
            RenderLine("</Style>", indent);
        }
    }
}
