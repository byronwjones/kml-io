namespace BWJ.IO.KML.Models.Style
{
    public class DynamicStyleSetState : KmlElement
    {
        public DynamicStyleSetState()
        {
            Type = KmlElementType.DynamicStyleSetState;
        }

        public StyleState State { get; set; }

        private string _styleSet = string.Empty;
        public string StyleSet
        {
            get { return _styleSet; }
            set
            {
                string v = value;
                if(!string.IsNullOrWhiteSpace(v))
                {
                    v = value.Trim();
                    if(v[0] != '#')
                    {
                        v = "#" + v;
                    }
                }

                _styleSet = v;
            }
        }

        protected override void PreChildRender(int indent)
        {
            RenderLine("<Pair>", indent);
            RenderLine($"<key>{State}</key>", indent, 1);
            RenderLine($"<styleUrl>{StyleSet}</styleUrl>", indent, 1);
            RenderLine("</Pair>", indent);
        }
    }
}
