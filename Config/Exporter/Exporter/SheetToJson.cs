using System;
using System.Security.Cryptography.X509Certificates;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;


namespace Exporter
{
    internal class SheetToJson(string excelFile)
    {
        public string ExcelFilePath = excelFile;

        public string ConvertExcelToJsonString()
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
            
            using var workbook = new XLWorkbook(ExcelFilePath);
            foreach (var worksheet in workbook.Worksheets)
            {
                var jsonData = ConvertSheetToJson(worksheet);
                //Include the sheet to the exported json
                exportedJson.Add(worksheet.Name, jsonData);
            }

            // Convert the exported json into a beautified json text and save it into a file
            var jsonText = Newtonsoft.Json.JsonConvert.SerializeObject(exportedJson, Newtonsoft.Json.Formatting.Indented);

            return jsonText;
        }

        private object GetCellValue(XLCellValue cellValue)
        {
            var value = cellValue.ToString();
            if (int.TryParse(value, out int number))
            {
                return number;
            }
            else if (bool.TryParse(value, out bool boolResult))
            {
                return boolResult;
            }
            else
            {
                return value;
            }
        }
        
        private object ConvertSheetToJson(IXLWorksheet worksheet)
        {
            var range = worksheet.RangeUsed();
            var rowCount = range.RowCount();
            var colCount = range.ColumnCount();

            // Read keys (headers)
            var keys = new List<string>();
            for (int col = 1; col <= colCount; col++)
            {
                var cellValue = range.Cell(1, col).GetString();
                var cleanKey = cellValue.Replace(" ", "");
                cleanKey = char.ToLower(cleanKey[0]) + cleanKey.Substring(1);
                keys.Add(cleanKey);
            }

            // Create JSON object
            if (rowCount == 1)
            {
                var data = new Dictionary<string, object>();
                for (int col = 1; col <= colCount; col++)
                {
                    data[keys[col - 1]] = GetCellValue(range.Cell(2, col).Value);
                }

                return data; // Single row of data
            }
            else
            {
                var jsonData = new List<Dictionary<string, object>>();
                for (int row = 2; row <= rowCount; row++)
                {
                    var data = new Dictionary<string, object>();
                    for (int col = 1; col <= colCount; col++)
                    {
                        data[keys[col - 1]] = GetCellValue(range.Cell(row, col).Value);
                    }

                    jsonData.Add(data);
                }

                return jsonData;
            }
        }
    }
}
