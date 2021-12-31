using System;

namespace BWJ.IO.KML.Models.Style
{
    public class DynamicStyleSet : KmlElement
    {
        public DynamicStyleSet()
        {
            Type = KmlElementType.DynamicStyleSet;
            ChildTypes = new KmlElementType[]
            { KmlElementType.DynamicStyleSetState };
        }

        public string Name { get; set; }

        protected override void PreChildRender(int indent)
        {
            RenderLine($"<StyleMap id=\"{Name}\">", indent);
        }

        protected override void PostChildRender(int indent)
        {
            RenderLine("</StyleMap>", indent);
        }
    }
}
