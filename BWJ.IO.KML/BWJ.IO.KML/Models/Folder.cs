using System;

namespace BWJ.IO.KML.Models
{
    public class Folder : KmlElement
    {
        public Folder()
        {
            Type = KmlElementType.Folder;
            ChildTypes = new KmlElementType[] { KmlElementType.Folder, KmlElementType.Placemark };
        }

        public string Name { get; set; }
        public bool Open { get; set; }

        protected override void PreChildRender(int indent)
        {
            RenderLine("<Folder>", indent);
            RenderLine($"<name>{Name ?? Guid.NewGuid().ToString("N")}</name>", indent, 1);
            RenderLine($"<open>{(Open ? 1 : 0)}</open>", indent, 1);
        }

        protected override void PostChildRender(int indent)
        {
            RenderLine("</Folder>", indent);
        }
    }
}
