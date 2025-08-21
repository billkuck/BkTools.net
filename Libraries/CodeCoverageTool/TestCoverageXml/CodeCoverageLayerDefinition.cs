using System.Xml.Linq;

namespace BkTools.Tools.CodeCoverage.TestCoverageXml
{
    public class CodeCoverageLayerDefinition
    {
        public string LayerName { get; private set; }
        public string XmlElementName { get; set; }
        public Func<XElement, string> GetInstanceName { get; set; }
        public string? ChildLayerName { get; private set; }

        public CodeCoverageLayerDefinition(string layerName, string xmlElementName, Func<XElement, string> getInstanceName, string? childLayerName = null)
        {
            LayerName = layerName;
            ChildLayerName = childLayerName;
            XmlElementName = xmlElementName;
            GetInstanceName = getInstanceName;
        }
    }
}
