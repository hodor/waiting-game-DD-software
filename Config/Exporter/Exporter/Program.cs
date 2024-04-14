using Exporter;

var currentDirectory = Directory.GetCurrentDirectory();
var excelConfigFile = Path.Combine(currentDirectory,"config.xlsx");
if (!File.Exists(excelConfigFile))
{
    Console.WriteLine($"The file {excelConfigFile} does not exist. Please make sure that the config.xlsx file lives in the same folder.");
    return;
}
var jsonConverter = new SheetToJson(excelConfigFile);
var jsonText = jsonConverter.ConvertExcelToJsonString();
var arProjectPath = ProgramFinder.FindConfigFileFromCurrent(currentDirectory);

if (arProjectPath != null)
{
    var outputPath = Path.Combine(arProjectPath, "StreamingAssets", "config.json");
    try
    {
        // Save in the specified 'outputPath'
        File.WriteAllText(outputPath, jsonText);
        Console.WriteLine($"JSON was successfully exported to {outputPath}", "Export Success");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Failed to save JSON to {outputPath}. Error: {ex.Message}", "Export Error");
    }
}
else
{
    Console.WriteLine($"Failed to find the folder Assets/StreamingAssets, maybe you're running on a different place. You will have to manually copy the config.json file.");
    var sameDirectoryPath = Path.Combine(currentDirectory, "config.json");
    try
    {
        File.WriteAllText(sameDirectoryPath, jsonText);
        Console.WriteLine($"JSON was successfully exported to {sameDirectoryPath}", "Export Success");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Failed to save JSON to {sameDirectoryPath}. Error: {ex.Message}", "Export Error");
    }
}
Console.ReadKey();