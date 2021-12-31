using System;

namespace BWJ.IO.KML.Models
{
    public class Document : KmlElement
    {
        public Document()
        {
            Type = KmlElementType.Document;
            ChildTypes = new KmlElementType[] { KmlElementType.StaticStyleSet, KmlElementType.DynamicStyleSet, KmlElementType.Folder, KmlElementType.Placemark };
        }

        public string Name { get; set; }


        protected override void PreChildRender(int indent)
        {
            RenderLine("<Document>", indent);
            RenderLine($"<name>{Name ?? Guid.NewGuid().ToString("N")}</name>", indent, 1);
        }

        protected override void PostChildRender(int indent)
        {
            RenderLine("</Document>", indent);
        }
    }
}
