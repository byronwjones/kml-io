using System;

namespace BWJ.IO.KML.Models.Graphic
{
    public abstract class Placemark : KmlElement
    {
        public Placemark()
        {
            Type = KmlElementType.Placemark;
            ChildTypes = new KmlElementType[] { };
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public ViewPoint LookAt { get; set; }

        private string _styleName = null;
        public string StyleName
        {
            get { return _styleName; }
            set
            {
                string v = value;
                if (!string.IsNullOrWhiteSpace(v))
                {
                    v = value.Trim();
                    if (v[0] != '#')
                    {
                        v = "#" + v;
                    }
                }

                _styleName = v;
            }
        }

        protected override void PreChildRender(int indent)
        {
            RenderLine("<Placemark>", indent);
            RenderLine($"<name>{Name ?? Guid.NewGuid().ToString("N")}</name>", indent, 1);
            if (!string.IsNullOrWhiteSpace(StyleName))
            {
                RenderLine($"<description><![CDATA[{Description}]]></description>", indent, 1);
            }
            if(LookAt != null)
            {
                LookAt.Render(indent + 1);
            }
            if (!string.IsNullOrWhiteSpace(StyleName))
            {
                RenderLine($"<styleUrl>{StyleName}</styleUrl>", indent, 1);
            }

            PreElementRender(indent + 1);
            PostElementRender(indent + 1);
            
            RenderLine("</Placemark>", indent);
        }

        protected virtual void PreElementRender(int indent) { }
        protected virtual void PostElementRender(int indent) { }
    }
}
