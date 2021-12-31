using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BWJ.IO.KML.Models
{
    public abstract class KmlElement
    {
        public KmlElementType Type { get; protected set; }
        protected KmlElementType[] ChildTypes { get; set; } = new KmlElementType[0];
        private List<KmlElement> ChildElements = new List<KmlElement>();
        public KmlElement[] Children
        {
            get { return ChildElements.ToArray(); }
        }
        public KmlElement Parent { get; private set; }

        public void AddChild(KmlElement child)
        {
            if(child == null)
            {
                throw new ArgumentNullException("child");
            }
            if(!ChildTypes.Contains(child.Type))
            {
                throw new ArgumentException($"Element '{child.Type}' is not a valid child of element '{this.Type}'.");
            }

            ChildElements.Add(child);
            child.Parent = this;
        }

        private StringBuilder RenderedText;
        protected void RenderLine(string text, int indent, int addIndent = 0)
        {
            string indentedLine = string.Empty;
            indent += addIndent;
            for(int i = 0; i < indent; i++)
            {
                indentedLine += "\t";
            }

            indentedLine += text;
            RenderedText.AppendLine(indentedLine);
        }
        protected virtual void PreChildRender(int indent) { }
        protected virtual void PostChildRender(int indent) { }
        private void RenderChildren(int indent)
        {
            indent++;
            foreach(var child in Children)
            {
                RenderedText.AppendLine(child.Render(indent));
            }
        }

        public string Render(int indent)
        {
            RenderedText = new StringBuilder();
            PreChildRender(indent);
            RenderChildren(indent);
            PostChildRender(indent);

            return RenderedText.ToString();
        }
    }
}
