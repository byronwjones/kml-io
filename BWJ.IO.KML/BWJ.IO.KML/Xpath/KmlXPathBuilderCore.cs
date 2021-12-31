namespace BWJ.IO.KML.Xpath
{
    public abstract class KmlXPathBuilderCore
    {
        protected string Path = string.Empty;

        public string S()
        {
            return ToString();
        }

        public override string ToString()
        {
            return Path;
        }
    }
}
