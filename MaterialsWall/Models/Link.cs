using System;

namespace Granta.MaterialsWall.Models
{
    public sealed class Link
    {
        public string Url{get {return url;}}
        private readonly string url;

        public string Text{get {return text;}}
        private readonly string text;

        public Link(string url, string text)
        {
            if (url == null)
            {
                throw new ArgumentNullException("url");
            }

            this.url = url;
            this.text = text ?? url;
        }
    }
}
