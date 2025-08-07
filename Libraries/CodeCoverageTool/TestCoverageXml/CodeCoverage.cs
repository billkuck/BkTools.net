using System.Xml.Linq;

namespace BkTools.Tools.CodeCoverageTool.TestCoverageXml
{
    public class CodeCoverage
    {
        public CodeCoverageLayerDefinition LayerDefinition { get; set; }
        public string? InstanceName { get; set; }
        public int LinesCovered { get; set; }
        public int LinesNotCovered { get; set; }
        public int LinesPartiallyCovered { get; set; }
        public CodeCoverage? Parent { get; set; }
        public IEnumerable<CodeCoverage> Children { get; set; }

        public CodeCoverage(
            CodeCoverageLayerDefinition layerDefinition, 
            List<CodeCoverageLayerDefinition> layerDefinitions, 
            XElement coverageElement,
            CodeCoverage? parent)
        {
            LayerDefinition = layerDefinition;
            InstanceName = GetInstanceName(layerDefinition, coverageElement);
            LinesCovered = Convert.ToInt32(coverageElement?.GetSingleElement("LinesCovered")?.Value) ;
            LinesNotCovered = Convert.ToInt32(coverageElement?.GetSingleElement("LinesNotCovered")?.Value);
            LinesPartiallyCovered = Convert.ToInt32(coverageElement?.GetSingleElement("LinesPartiallyCovered")?.Value);
            Parent = parent;

            var childLayerDefinition = layerDefinitions.SingleOrDefault(ld => ld.LayerName == layerDefinition.ChildLayerName);
            Children = childLayerDefinition == null 
                ? []
                : coverageElement?
                    .GetElements(childLayerDefinition.XmlElementName)
                    .Select(subElement => new CodeCoverage(childLayerDefinition, layerDefinitions, subElement, this))
                    .ToList() 
                    ?? [];
        }

        private static string? GetInstanceName(CodeCoverageLayerDefinition layerDefinition, XElement subElement) 
            => layerDefinition.GetInstanceName(subElement) 
            ?? throw new Exception("Invalid XML or CodeCoverageLayerDefinition");
    }
}
