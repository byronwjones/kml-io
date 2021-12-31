namespace BWJ.IO.KML.Models.Graphic
{
    public class AddressPlacemark : Placemark
    {
        public string Address { get; set; }
        protected override void PreElementRender(int indent)
        {
            RenderLine($"<address>{Address}</address>", indent);
        }
    }
}
