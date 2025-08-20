# Parameters
$toolPath = "./bin/Debug/net8.0/BkToolsMain.exe"
$command = "Crap"
$outputFileName = "c:\dev\adt\reports\Crap-Report.txt"
$coberturaFilePath = "C:\DEV\ADT\Wsp\AVT-Alpha-02\Sandbox\SE1.FcsaApiTestTool\Tests\AVT_IntegrationTests\TestResults\Coverage\coverage.cobertura.xml"

& $toolPath $command --outputFile $outputFileName --coberteraFile $coberturaFilePath
