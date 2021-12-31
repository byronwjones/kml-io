namespace BWJ.IO.KML.Models.Style
{
    public class IconStyle : StyleBase
    {
        public IconStyle()
        {
            Type = KmlElementType.IconStyle;
        }

        public float Scale { get; set; }
        public string IconUrl { get; set; }

        protected override void PreChildRender(int indent)
        {
            RenderLine("<IconStyle>", indent);
                RenderLine($"<scale>{Scale.ToString("0.0")}</scale>", indent, 1);

                RenderLine("<Icon>", indent, 1);
                    RenderLine($"<href>{IconUrl}</href>", indent, 2);
                RenderLine("</Icon>", indent, 1);
            RenderLine("</IconStyle>", indent);
        }
    }
}
