using BWJ.IO.KML.Models;
using BWJ.IO.KML.Models.Graphic;
using BWJ.IO.KML.Models.Style;
using BWJ.IO.KML.Xpath;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace BWJ.IO.KML
{
    public class KmlReader
    {
        public Kml Read(string path)
        {
            string text = string.Empty;
            using (StreamReader r = new StreamReader(path))
            {
                text = r.ReadToEnd();
            }

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(text);

            nsManager = new XmlNamespaceManager(doc.NameTable);
            nsManager.AddNamespace("d", "http://www.opengis.net/kml/2.2");
            nsManager.AddNamespace("gx", "http://www.google.com/kml/ext/2.2");
            nsManager.AddNamespace("kml", "http://www.opengis.net/kml/2.2");
            nsManager.AddNamespace("atom", "http://www.w3.org/2005/Atom");
            
            XmlNode docNode = doc.SelectSingleNode(x.FromRoot().C(x.Document).S(), nsManager);
            parseDocument(docNode);

            return kml;
        }

        private void parseDocument(XmlNode node)
        {
            Document doc = new Document();
            kml.AddChild(doc);

            //get the document name
            var name = SingleNodeFromCurrent(node, x.name);
            if(name != null)
            {
                doc.Name = name.InnerXml;
            }

            //load the static styles
            var staticStyles = MultipleNodesFromCurrent(node, x.Style);
            foreach(var sStyle in staticStyles)
            {
                doc.AddChild(parseStaticStyleSet((XmlNode)sStyle));
            }

            //load the dynamic styles
            var dynamicStyles = MultipleNodesFromCurrent(node, x.StyleMap);
            foreach (var dStyle in dynamicStyles)
            {
                doc.AddChild(parseStaticStyleSet((XmlNode)dStyle));
            }

            //load the folders
            var folders = MultipleNodesFromCurrent(node, x.Folder);
            foreach (var fld in folders)
            {
                doc.AddChild(parseFolder((XmlNode)fld));
            }

            //load the loose placemarks
            var placemarks = MultipleNodesFromCurrent(node, x.Placemark);
            foreach (var pMark in placemarks)
            {
                doc.AddChild(parsePlacemark((XmlNode)pMark));
            }
        }

        private Folder parseFolder(XmlNode node)
        {
            var folder = new Folder();

            //get folder name
            var name = SingleNodeFromCurrent(node, x.name);
            if(name != null)
            {
                folder.Name = name.InnerXml;
            }
            //get whether or not folder is open
            var open = SingleNodeFromCurrent(node, x.open);
            if (open != null)
            {
                folder.Open = open.InnerXml == "1";
            }

            //load all subfolders
            var folders = MultipleNodesFromCurrent(node, x.Folder);
            foreach(var fld in folders)
            {
                folder.AddChild(parseFolder((XmlNode)fld));
            }

            //load all placemarks in this folder
            var placemarks = MultipleNodesFromCurrent(node, x.Placemark);
            foreach (var pMark in placemarks)
            {
                folder.AddChild(parsePlacemark((XmlNode)pMark));
            }

            return folder;
        }

        private Placemark parsePlacemark(XmlNode node)
        {
            Placemark p = null;

            //load point placemark
            var point = SingleNodeFromCurrent(node, x.Point);
            if(point != null)
            {
                p = parsePoint(node);
            }
            else
            {
                p = parsePolygon(node);
            }

            return p;
        }
        private PointPlacemark parsePoint(XmlNode node)
        {
            var point = new PointPlacemark();
            parsePlacemarkCommon(point, node);

            //shift node reference to the Point element
            node = SingleNodeFromCurrent(node, x.Point);

            var dOrder = SingleNodeFromCurrent(node, x.drawOrder);
            if (dOrder != null)
            {
                point.DrawOrder = Convert.ToInt32(dOrder.InnerXml);
            }
            var coords = SingleNodeFromCurrent(node, x.coordinates);
            if (coords != null)
            {
                point.Coordinate = new Coordinate(coords.InnerXml);
            }

            return point;
        }
        private PolygonPlacemark parsePolygon(XmlNode node)
        {
            var pgon = new PolygonPlacemark();
            parsePlacemarkCommon(pgon, node);

            //shift node reference to the Polygon element
            node = SingleNodeFromCurrent(node, x.Polygon);

            var tessellate = SingleNodeFromCurrent(node, x.tessellate);
            if (tessellate != null)
            {
                pgon.Tessellate = tessellate.InnerXml == "1";
            }
            var coords = SingleNodeFromCurrent(node, x.outerBoundaryIs, x.LinearRing, x.coordinates);
            if (coords != null)
            {
                pgon.Coordinates = new List<Coordinate>();
                var arrCoords = coords.InnerXml.Trim().Split(' ');
                foreach(var c in arrCoords)
                {
                    if(!string.IsNullOrWhiteSpace(c))
                    {
                        pgon.Coordinates.Add(new Coordinate(c));
                    }
                }
            }

            return pgon;
        }

        private void parsePlacemarkCommon(Placemark pmark, XmlNode node)
        {
            var name = SingleNodeFromCurrent(node, x.name);
            if (name != null)
            {
                pmark.Name = name.InnerXml;
            }
            var desc = SingleNodeFromCurrent(node, x.description);
            if (desc != null)
            {
                pmark.Description = desc.InnerXml;
            }
            var style = SingleNodeFromCurrent(node, x.styleUrl);
            if (style != null)
            {
                pmark.StyleName = style.InnerXml;
            }

            var view = SingleNodeFromCurrent(node, x.LookAt);
            if (view != null)
            {
                pmark.LookAt = parseViewPoint(view);
            }

        }

        private ViewPoint parseViewPoint(XmlNode node)
        {
            var view = new ViewPoint();

            var lon = SingleNodeFromCurrent(node, x.longitude);
            if (lon != null)
            {
                view.Longitude = Convert.ToDecimal(lon.InnerXml);
            }
            var lat = SingleNodeFromCurrent(node, x.latitude);
            if (lat != null)
            {
                view.Latitude = Convert.ToDecimal(lat.InnerXml);
            }
            var alt = SingleNodeFromCurrent(node, x.altitude);
            if (alt != null)
            {
                view.Altitude = Convert.ToDecimal(alt.InnerXml);
            }
            var head = SingleNodeFromCurrent(node, x.heading);
            if (head != null)
            {
                view.Heading = Convert.ToDecimal(head.InnerXml);
            }
            var tilt = SingleNodeFromCurrent(node, x.tilt);
            if (tilt != null)
            {
                view.Tilt = Convert.ToDecimal(tilt.InnerXml);
            }
            var range = SingleNodeFromCurrent(node, x.range);
            if (range != null)
            {
                view.Range = Convert.ToDecimal(range.InnerXml);
            }
            var altMode = SingleNodeFromCurrent(node, x.altitudeMode);
            if (altMode != null)
            {
                AltitudeMode am;
                Enum.TryParse(altMode.InnerXml, out am);
                view.AltitudeMode = am;
            }

            return view;
        }

        private DynamicStyleSet parseDynamicStyleSet(XmlNode node)
        {
            var styleSet = new DynamicStyleSet();
            styleSet.Name = node.Attributes["id"].Value;

            var states = MultipleNodesFromCurrent(node, x.Pair);
            foreach (var state in states)
            {
                styleSet.AddChild(parseDynamicStyleSetState((XmlNode)state));
            }

            return styleSet;
        }
        private DynamicStyleSetState parseDynamicStyleSetState(XmlNode node)
        {
            var state = new DynamicStyleSetState();

            var key = SingleNodeFromCurrent(node, x.key);
            if (key != null)
            {
                StyleState s;
                Enum.TryParse(key.InnerXml, out s);
                state.State = s;
            }
            var url = SingleNodeFromCurrent(node, x.styleUrl);
            if (url != null)
            {
                state.StyleSet = url.InnerXml;
            }

            return state;
        }

        private StaticStyleSet parseStaticStyleSet(XmlNode node)
        {
            var styleSet = new StaticStyleSet();
            styleSet.Name = node.Attributes["id"].Value;

            var icon = SingleNodeFromCurrent(node, x.IconStyle);
            if (icon != null)
            {
                styleSet.AddChild(parseIconStyle(icon));
            }
            var line = SingleNodeFromCurrent(node, x.LineStyle);
            if (line != null)
            {
                styleSet.AddChild(parseLineStyle(line));
            }
            var poly = SingleNodeFromCurrent(node, x.PolyStyle);
            if (poly != null)
            {
                styleSet.AddChild(parsePolyStyle(poly));
            }
            var list = SingleNodeFromCurrent(node, x.ListStyle);
            if (list != null)
            {
                styleSet.AddChild(parseListStyle(list));
            }

            return styleSet;
        }

        private ListStyle parseListStyle(XmlNode node)
        {
            var style = new ListStyle();

            return style;
        }
        private PolyStyle parsePolyStyle(XmlNode node)
        {
            var style = new PolyStyle();

            var color = SingleNodeFromCurrent(node, x.color);
            if (color != null)
            {
                style.ColorABGR = color.InnerXml;
            }

            return style;
        }
        private LineStyle parseLineStyle(XmlNode node)
        {
            var style = new LineStyle();

            var color = SingleNodeFromCurrent(node, x.color);
            if (color != null)
            {
                style.ColorABGR = color.InnerXml;
            }

            return style;
        }
        private IconStyle parseIconStyle(XmlNode node)
        {
            var style = new IconStyle();

            var scale = SingleNodeFromCurrent(node, x.scale);
            if (scale != null)
            {
                style.Scale = Convert.ToSingle(scale.InnerXml);
            }

            var icon = SingleNodeFromCurrent(node, x.Icon, x.href);
            if(icon != null)
            {
                style.IconUrl = icon.InnerXml;
            }

            return style;
        }

        private XmlNode SingleNodeFromCurrent(XmlNode currentNode, params string[] children)
        {
            var xpath = buildXpathFromCurrent(children);
            return currentNode.SelectSingleNode(xpath, nsManager);
        }
        private XmlNodeList MultipleNodesFromCurrent(XmlNode currentNode, params string[] children)
        {
            var xpath = buildXpathFromCurrent(children);
            return currentNode.SelectNodes(xpath, nsManager);
        }
        private string buildXpathFromCurrent(params string[] children)
        {
            KmlXpathBuilder.SubelementSelector s = x.FromCurrent();
            foreach(var c in children)
            {
                s = s.Children(c);
            }

            return s.ToString();
        }

        private XmlNamespaceManager nsManager;
        private Kml kml = new Kml();
        private KmlXpathBuilder x = new KmlXpathBuilder();
    }
}
