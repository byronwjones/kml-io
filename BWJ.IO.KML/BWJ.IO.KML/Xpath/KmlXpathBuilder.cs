using System;

namespace BWJ.IO.KML.Xpath
{
    public sealed partial class KmlXpathBuilder : KmlXPathBuilderCore
    {
        public string altitude = "d:altitude";
        public string color = "d:color";
        public string coordinates = "d:coordinates";
        public string Document = "d:Document";
        public string description = "d:description";
        public string Folder = "d:Folder";
        public string heading = "d:heading";
        public string hotSpot = "d:hotSpot";
        public string href = "d:href";
        public string Icon = "d:Icon";
        public string IconStyle = "d:IconStyle";
        public string key = "d:key";
        public string kml = "d:kml";
        public string latitude = "d:latitude";
        public string LinearRing = "d:LinearRing";
        public string LineStyle = "d:LineStyle";
        public string ListStyle = "d:ListStyle";
        public string longitude = "d:longitude";
        public string LookAt = "d:LookAt";
        public string name = "d:name";
        public string open = "d:open";
        public string outerBoundaryIs = "d:outerBoundaryIs";
        public string Pair = "d:Pair";
        public string Placemark = "d:Placemark";
        public string Point = "d:Point";
        public string Polygon = "d:Polygon";
        public string PolyStyle = "d:PolyStyle";
        public string range = "d:range";
        public string scale = "d:scale";
        public string Style = "d:Style";
        public string StyleMap = "d:StyleMap";
        public string styleUrl = "d:styleUrl";
        public string tessellate = "d:tessellate";
        public string tilt = "d:tilt";
        public string altitudeMode = "gx:altitudeMode";
        public string drawOrder = "gx:drawOrder";


        public SubelementSelector Every(string element)
        {
            if (string.IsNullOrWhiteSpace(element))
            {
                throw new ArgumentNullException("element");
            }

            return CreateSubelementSelector(element.Trim());
        }
        public SubelementSelector Every()
        {
            return Every("*");
        }
        
        public SubelementSelector FromParent()
        {
            return CreateSubelementSelector("..");
        }

        public SubelementSelector FromCurrent()
        {
            return CreateSubelementSelector(".");
        }

        public SubelementSelector FromRoot()
        {
            return CreateSubelementSelector("/");
        }
    }
}
