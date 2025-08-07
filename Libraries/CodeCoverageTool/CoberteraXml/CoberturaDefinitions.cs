using System.Xml.Serialization;

namespace BkTools.Tools.CodeCoverageTool.CoberteraXml
{
    public class CoberturaDefinitions
    {
        [XmlRoot(ElementName = "coverage", Namespace = "")]
        public class CoverageNode
        {
            [XmlAttribute(attributeName: "line-rate")]
            public double LineRate { get; set; }

            [XmlAttribute(attributeName: "branch-rate")]
            public double BranchRate { get; set; }

            [XmlAttribute(attributeName: "complexity")]
            public int Complexity { get; set; }

            [XmlAttribute(attributeName: "version")]
            public string? Version { get; set; }

            [XmlAttribute(attributeName: "timestamp")]
            public long TimeStamp { get; set; }

            [XmlAttribute(attributeName: "lines-covered")]
            public int LinesCovered { get; set; }

            [XmlAttribute(attributeName: "lines-valid")]
            public int LinesValid { get; set; }

            [XmlElement(elementName: "packages")]
            public PackagesNode? Packages { get; set; }
            public int CoveredLines { get => Packages?.Items?.Sum(method => method.CoveredLines) ?? 0; }
            public int NotCoveredLines { get => Packages?.Items?.Sum(method => method.NotCoveredLines) ?? 0; }
            public int TotalLines { get => CoveredLines + NotCoveredLines; }
            public double CodeCoverage { get => (double)CoveredLines / TotalLines; }

        }

        public class PackagesNode 
        {
            [XmlElement(elementName: "package")]
            public List<PackageNode>? Items { get; set; }
        }

        public class PackageNode
        {
            [XmlAttribute(attributeName: "name")]
            public string? Name { get; set; }

            [XmlAttribute(attributeName: "line-rate")]
            public double LineRate { get; set; }

            [XmlAttribute(attributeName: "branch-rate")]
            public double BranchRate { get; set; }

            [XmlAttribute(attributeName: "complexity")]
            public int Complexity { get; set; }

            [XmlElement(elementName: "classes")]
            public ClassesNode? Classes { get; set; }
            public int CoveredLines { get => Classes?.Items?.Sum(method => method.CoveredLines) ?? 0; }
            public int NotCoveredLines { get => Classes?.Items ?.Sum(method => method.NotCoveredLines) ?? 0; }
            public int TotalLines { get => CoveredLines + NotCoveredLines; }
            public double CodeCoverage { get => (double)CoveredLines / TotalLines; }
        }

        public class ClassesNode
        {
            [XmlElement(elementName: "class")]
            public List<ClassNode>? Items { get; set; }
        }

        public class ClassNode
        {
            [XmlAttribute(attributeName: "name")]
            public string? Name { get; set; }

            [XmlAttribute(attributeName: "line-rate")]
            public double LineRate { get; set; }

            [XmlAttribute(attributeName: "branch-rate")]
            public double BranchRate { get; set; }

            [XmlAttribute(attributeName: "complexity")]
            public int Complexity { get; set; }

            [XmlAttribute(attributeName: "filename")]
            public string? FileName { get; set; }

            [XmlElement(elementName: "methods")]
            public MethodsNode? Methods { get; set; }
            public int CoveredLines { get => Methods?.Items?.Sum(method => method.CoveredLines) ?? 0; }
            public int NotCoveredLines { get => Methods?.Items?.Sum(method => method.NotCoveredLines) ?? 0; }
            public int TotalLines { get => CoveredLines + NotCoveredLines; }
            public double CodeCoverage { get => (double)CoveredLines / TotalLines; }
        }

        public class MethodsNode
        {
            [XmlElement(elementName: "method")]
            public List<MethodNode>? Items { get; set; }
        }

        public class MethodNode
        {
            [XmlAttribute(attributeName: "name")]
            public string? Name { get; set; }

            [XmlAttribute(attributeName: "line-rate")]
            public double LineRate { get; set; }

            [XmlAttribute(attributeName: "branch-rate")]
            public double BranchRate { get; set; }

            [XmlAttribute(attributeName: "complexity")]
            public int Complexity { get; set; }

            [XmlAttribute(attributeName: "signature")]
            public string? Signature { get; set; }

            [XmlElement(elementName: "lines")]
            public LinesNode? Lines { get; set; }

            public int CoveredLines { get => Lines?.Items?.Count(item => item.Hits > 0) ?? 0; }
            public int NotCoveredLines { get => Lines?.Items?.Count(item => item.Hits == 0) ?? 0; }
            public int TotalLines { get => Lines?.Items?.Count() ?? 0; }
            public double CodeCoverage { get => (double)CoveredLines / TotalLines; }
        }

        public class LinesNode
        {
            [XmlElement(elementName: "line")]
            public List<LineNode>? Items { get; set; }
        }

        public class LineNode
        {
            [XmlAttribute(attributeName: "number")]
            public int LineNumber { get; set; }
            [XmlAttribute(attributeName: "hits")]
            public int Hits { get; set; }
            [XmlAttribute(attributeName: "branch")]
            public string? Branch { get; set; }
        }
    }
}
