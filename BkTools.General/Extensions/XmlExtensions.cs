using System.Xml.Linq;

namespace BkTools.General.Extensions
{
    public static class XmlExtensions
    {
        public static XElement? GetSingleElement(this XElement element, string localName)
            => element.Elements().FirstOrDefault(child => child.Name.LocalName == localName);

        public static List<XElement> GetElements(this XElement element, string localName)
            => element.Elements().Where(child => child.Name.LocalName == localName).ToList();
    }
}
