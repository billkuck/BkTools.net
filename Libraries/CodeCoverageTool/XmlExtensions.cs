using System.Xml.Linq;

namespace BkTools.Tools.CodeCoverageTool
{
    public static class XmlExtensions
    {
        public static XElement? GetSingleElement(this XElement element, string localName)
            => element.Elements().FirstOrDefault(child => child.Name.LocalName == localName);

        public static List<XElement> GetElements(this XElement element, string localName)
            => element.Elements().Where(child => child.Name.LocalName == localName).ToList();
    }
}
