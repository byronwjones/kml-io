using System;

namespace BWJ.IO.KML.Xpath
{
    public sealed partial class KmlXpathBuilder : KmlXPathBuilderCore
    {
		public class SubelementSelector : KmlXPathBuilderCore
        {
            protected SubelementSelector(string path)
            {
                this.Path = path;
            }

			public FiniteSelector Children(string element)
            {
                if (String.IsNullOrWhiteSpace(element))
                {
                    throw new ArgumentNullException("element");
                }
                element = element.Trim();

                this.Path += $"/{element}";

                if(this is FiniteSelector)
                {
                    return this as FiniteSelector;
                }
				else
                {
                    return KmlXpathBuilder.CreateFiniteSelector(this.Path);
                }
            }
            public SubelementSelector Children(string element, int index)
            {
                if (String.IsNullOrWhiteSpace(element))
                {
                    throw new ArgumentNullException("element");
                }
				if(index < 1)
                {
                    throw new ArgumentException("Index must be greater than zero");
                }

                element = element.Trim();

                this.Path += $"/{element}[{index}]";

                return this;
            }
			public SubelementSelector Children()
            {
                return Children("*");
            }
			public FiniteSelector C(string element)
            {
                return Children(element);
            }
			public SubelementSelector C(string element, int index)
            {
                return Children(element, index);
            }
			public SubelementSelector C()
            {
                return Children();
            }

            public FiniteSelector Descendants(string element)
            {
                if (String.IsNullOrWhiteSpace(element))
                {
                    throw new ArgumentNullException("element");
                }
                element = element.Trim();

                this.Path += $"//{element}";

                if (this is FiniteSelector)
                {
                    return this as FiniteSelector;
                }
                else
                {
                    return KmlXpathBuilder.CreateFiniteSelector(this.Path);
                }
            }
            public SubelementSelector Descendants()
            {
                return Descendants("*");
            }
            public FiniteSelector D(string element)
            {
                return Descendants(element);
            }
            public SubelementSelector D()
            {
                return Descendants();
            }
        }

		private class SubelementSelectorInstance : SubelementSelector
        {
            public SubelementSelectorInstance(string path) : base(path) { }
        }

		private SubelementSelector CreateSubelementSelector(string path)
        {
            return new SubelementSelectorInstance(path);
        }
    }
}
