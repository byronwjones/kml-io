using System;

namespace BWJ.IO.KML.Xpath
{
    public sealed partial class KmlXpathBuilder : KmlXPathBuilderCore
    {
        public class FiniteSelector : SubelementSelector
        {
            protected FiniteSelector(string path) : base(path) { }

            public SubelementSelector Attr(string name)
            {
                if(String.IsNullOrWhiteSpace(name))
                {
                    throw new ArgumentNullException("name");
                }

                this.Path += $"[@{name.Trim()}]";

                return this;
            }

            public SubelementSelector Attr(string name, string value)
            {
                if (String.IsNullOrWhiteSpace(name))
                {
                    throw new ArgumentNullException("name");
                }
                value = (value ?? string.Empty).Trim();

                this.Path += $"[@{name.Trim()}='{value}']";

                return this;
            }

            public SubelementSelector TakeFirst(int count)
            {
                if (count < 1)
                {
                    throw new ArgumentException("Count must be greater than 0");
                }

                count++;
                this.Path += $"[position()<{count}]";

                return this;
            }
            public SubelementSelector TakeFirst()
            {
                return TakeFirst(1);
            }

            public SubelementSelector TakeLast(int count)
            {
                if (count < 1)
                {
                    throw new ArgumentException("Count must be greater than 0");
                }
                
                this.Path += $"[position()>(last()-{count})]";

                return this;
            }
            public SubelementSelector TakeLast()
            {
                return TakeLast(1);
            }
        }

        private class FiniteSelectorInstance : FiniteSelector
        {
            public FiniteSelectorInstance(string path) : base(path) { }
        }

        private static FiniteSelector CreateFiniteSelector(string path)
        {
            return new FiniteSelectorInstance(path);
        }
    }
}
