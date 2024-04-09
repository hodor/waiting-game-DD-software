using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using Newtonsoft.Json.Linq;

namespace SpreadsheetToJson
{
    public static class ExcelToJson
    {
        public static string FindParentDirectoryWithFolder(string currentDirectory, string folderName)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(currentDirectory);

            // Traverse up the directory tree
            while (directoryInfo != null)
            {
                // Check if the desired folder exists in the current directory
                var targetDirectory = Path.Combine(directoryInfo.FullName, folderName);
                if (Directory.Exists(targetDirectory))
                {
                    return targetDirectory;
                }

                // If the current directory is the root, stop the loop
                if (directoryInfo.Parent == null)
                {
                    break;
                }

                // Move up to the parent directory
                directoryInfo = directoryInfo.Parent;
            }

            return null; // Return null if the folder was not found
        }

        public static object ConvertSheetToJson(Worksheet sheet)
        {
            var range = sheet.UsedRange;
            var rowCount = range.Rows.Count;
            var colCount = range.Columns.Count;
            
            // First we identify the keys in the first row and save that data in a List<string>
            var keys = new List<string>();
            for (var col = 1; col <= colCount; col++)
            {
                var cell = (Range)range.Cells[1, col];
                // Make sure we remove any space in the name
                var cleanKey = cell.Value.ToString().Replace(" ", "");
                // Make sure the first character is lowercase, we're using camelCase
                cleanKey = char.ToLower(cleanKey[0]) + cleanKey.Substring(1);
                keys.Add(cleanKey);
            }

            // Then we create a json object where, for each remaining row we populate the value for the keys that we already got
            // Check the number of rows to determine the structure of the output
            if (rowCount == 2)
            {
                // There is only one row of data, so create a single dictionary
                var data = new Dictionary<string, object>();
                for (var col = 1; col <= colCount; col++)
                {
                    var cell = (Range)range.Cells[2, col]; // second row for values
                    data[keys[col - 1]] = cell.Value;
                }

                // Create a new JSON object with the key as the name of the spreadsheet and the value as the dictionary we parsed
                return data;
            }
            else
            {
                // There are multiple rows of data, so create a list of dictionaries
                var jsonData = new List<Dictionary<string, object>>();
                for (var row = 2; row <= rowCount; row++)
                {
                    var data = new Dictionary<string, object>();
                    for (var col = 1; col <= colCount; col++)
                    {
                        var cell = (Range)range.Cells[row, col];
                        var value = cell.Value;
                        // Check if the value is a number and convert it to an integer if possible
                        if (int.TryParse(value?.ToString(), out int number))
                        {
                            // Convert the number to an integer, considering rounding or truncation as necessary
                            data[keys[col - 1]] = number;
                        }
                        else
                        {
                            data[keys[col - 1]] = value;
                        }
                    }
                    jsonData.Add(data);
                }

                // Create a new JSON object with the key as the name of the spreadsheet and the value as the list we parsed
                return jsonData;
            }
        }
    }
    
    public partial class Sheet8
    {
        private void Sheet8_Startup(object sender, System.EventArgs e)
        {
        }
        private void Sheet8_Shutdown(object sender, System.EventArgs e)
        {
        }

        #region VSTO Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.export_btn.Click += new System.EventHandler(this.button1_Click);
            this.Startup += new System.EventHandler(this.Sheet8_Startup);
            this.Shutdown += new System.EventHandler(this.Sheet8_Shutdown);
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            // Create a new blank json object
            Dictionary<string, object> exportedJson = new Dictionary<string, object>();
            /* Create a json object with the following format using the current date time
               "created": {
                 "date": "January 28, 2021 06:08:46 -0300"
               },
             */
            var created = new Dictionary<string, object> { { "date", DateTime.Now.ToString("MMMM dd, yyyy HH:mm:ss zzzz") } };
            exportedJson["created"] = created;
            
            // Iterate through all sheets in the current excel file
            var sheets = Globals.ThisWorkbook.Sheets;
            foreach (Worksheet sheet in sheets)
            {
                // Skip if we are in the current sheet
                if (sheet.Name == "Export")
                {
                    continue;
                }

                var jsonData = ExcelToJson.ConvertSheetToJson(sheet);
                //Include the sheet to the exported json
                exportedJson.Add(sheet.Name, jsonData);
            }

            // Convert the exported json into a beautified json text and save it into a file
            var jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(exportedJson, Newtonsoft.Json.Formatting.Indented);
            // Get the path 5 to the top of the current location
            var excelApp = Globals.ThisWorkbook.ThisApplication;
            var workbook = excelApp.ActiveWorkbook;

            // Get the full path of the workbook
            var fullPath = workbook.FullName;

            // Extract the directory from the full path
            var currentDirectory = Path.GetDirectoryName(fullPath);
            var arProjectPath = ExcelToJson.FindParentDirectoryWithFolder(currentDirectory, "AR_Project");

            if (arProjectPath != null)
            {
                var outputPath = Path.Combine(arProjectPath, "Assets", "StreamingAssets", "config.json");
                try
                {
                    // Save in the specified 'outputPath'
                    File.WriteAllText(outputPath, jsonText);
                    MessageBox.Show($"JSON was successfully exported to {outputPath}", "Export Success");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to save JSON to {outputPath}. Error: {ex.Message}", "Export Error");
                }
            } else
            {
                MessageBox.Show($"Failed to find the folder Assets/StreamingAssets, maybe you're running on a different place. You will have to manually copy the config.json file.", "Export Error");
                var sameDirectoryPath = Path.Combine(currentDirectory, "output.json");
                try
                {
                    File.WriteAllText(sameDirectoryPath, jsonText);
                    MessageBox.Show($"JSON was successfully exported to {sameDirectoryPath}", "Export Success");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to save JSON to {sameDirectoryPath}. Error: {ex.Message}", "Export Error");
                }
            }
            
        }
    }
}
