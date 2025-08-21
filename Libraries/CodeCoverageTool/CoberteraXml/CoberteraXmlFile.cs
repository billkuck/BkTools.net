using BkTools.General;

namespace BkTools.Tools.CodeCoverage.CoberteraXml
{
    public class CoberteraXmlFile
    {
        public string FilePath { get; private set; }
        public CoberturaDefinitions.CoverageNode Coverage { get; private set; }

        public CoberteraXmlFile(string filePath)
        {
            FilePath = filePath;
            Coverage = Load(filePath);
        }

        private static CoberturaDefinitions.CoverageNode Load(string filePath) 
            => XmlSerialized<CoberturaDefinitions.CoverageNode>.LoadFromXmlFile(filePath)!;
    }
}
