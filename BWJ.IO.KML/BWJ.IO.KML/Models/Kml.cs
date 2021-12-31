using System;
using System.Collections.Generic;

namespace BWJ.IO.KML.Models
{
    public class Kml : KmlElement
    {
        public Kml()
        {
            Type = KmlElementType.Document;
            ChildTypes = new KmlElementType[] { KmlElementType.Document };
        }

        protected override void PreChildRender(int indent)
        {
            RenderLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>", indent);
            RenderLine("<kml xmlns=\"http://www.opengis.net/kml/2.2\" xmlns:gx=\"http://www.google.com/kml/ext/2.2\" xmlns:kml=\"http://www.opengis.net/kml/2.2\" xmlns:atom=\"http://www.w3.org/2005/Atom\">", indent);
        }

        protected override void PostChildRender(int indent)
        {
            RenderLine("</kml>", indent);
        }

        public List<KmlElement> FlattenDocument()
        {
            var list = new List<KmlElement>();
            flattenElement(list, this);

            return list;
        }

        private void flattenElement(List<KmlElement> list, KmlElement element)
        {
            foreach(var child in element.Children)
            {
                list.Add(child);
                flattenElement(list, child);
            }
        }
    }
}
