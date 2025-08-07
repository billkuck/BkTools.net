using System.Xml.Linq;
namespace BkTools.Tools.CodeCoverageTool.TestCoverageXml
{
    public class TestCoverageXmlFile
    {
        public string FilePath { get; private set; }
        public CodeCoverage Coverage { get; private set; }

        private readonly List<CodeCoverageLayerDefinition> Layers = new List<CodeCoverageLayerDefinition>()
        {
            new CodeCoverageLayerDefinition(layerName: "Global", xmlElementName: "CoverageDSPriv", getInstanceName: _ => "Global", "Module"),
            new CodeCoverageLayerDefinition(layerName: "Module", xmlElementName: "Module", getInstanceName: _ => GetName(_, "ModuleName"), "Namespace"),
            new CodeCoverageLayerDefinition(layerName: "Namespace", xmlElementName: "NamespaceTable", getInstanceName: _ => GetName(_, "NamespaceKeyName"), "Class"),
            new CodeCoverageLayerDefinition(layerName: "Class", xmlElementName: "Class", getInstanceName: _ => GetName(_, "ClassName"), "Method"),
            new CodeCoverageLayerDefinition(layerName: "Method", xmlElementName: "Method", getInstanceName: _ => GetName(_, "MethodName")),
        };

        private static string GetName(XElement xElement, string childName) 
            => xElement?.GetSingleElement(childName)?.Value ?? string.Empty;

        public TestCoverageXmlFile(string filePath)
        {
            FilePath = filePath;
            Coverage = new CodeCoverage(
                layerDefinition: Layers.First(),
                layerDefinitions: Layers,
                coverageElement: XDocument.Load(FilePath)?.Root ?? throw new Exception("Failed to load Xml file"),
                parent: null);
        }
    }
}
