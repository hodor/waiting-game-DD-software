using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Output.CSV
{
    public enum AddLineType
    {
        Before,
        After
    }

    public static class CSVUtils
    {
        private static string _currentPath;
        private static StreamWriter _lastUsedSW;

        public static void SetCurrentPath(string path)
        {
            _currentPath = path;
        }

        private static StreamWriter GetWriter(string path)
        {
            return new StreamWriter(new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.None),
                Encoding.UTF8);
        }

        /// <summary>
        ///     Convert an array to a csv line
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        private static string Cols(string[] arr)
        {
            return string.Join(",", arr);
        }

        public static void WriteLineAtEnd(string[] arr, bool close = true, string path = null)
        {
            if (path == null) path = _currentPath;
            StreamWriter writer;
            if (_lastUsedSW == null)
                _lastUsedSW = GetWriter(path);
            var cols = Cols(arr);
            _lastUsedSW.WriteLine(cols);

            if (close)
            {
                _lastUsedSW.Flush();
                _lastUsedSW.Close();
                _lastUsedSW = null;
            }
        }

        public static void ReplaceLineThatContains(string containingText, string[] newValue, string path = null)
        {
            if (path == null) path = _currentPath;
            var lines = File.ReadAllLines(path);
            var outLines = new string[lines.Length];
            for (var i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                if (line.Contains(containingText)) line = Cols(newValue);

                outLines[i] = line;
            }

            File.WriteAllLines(path, outLines, Encoding.UTF8);
        }

        public static void AddLine(AddLineType lineType, int index, string[] value, string path = null)
        {
            if (path == null) path = _currentPath;
            var lines = File.ReadAllLines(_currentPath);
            var outLines = new List<string>();
            for (var i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                if (i == index)
                {
                    if (lineType == AddLineType.Before)
                        outLines.Add(Cols(value));
                    outLines.Add(line);
                    if (lineType == AddLineType.After)
                        outLines.Add(Cols(value));
                }
                else
                {
                    outLines.Add(line);
                }
            }

            File.WriteAllLines(_currentPath, outLines.ToArray(), Encoding.UTF8);
        }
    }
}