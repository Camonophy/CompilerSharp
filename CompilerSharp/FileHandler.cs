using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace CompilerSharp
{

    /// <summary>
    /// Provides static methods to read and write AST to and from files.
    /// </summary>
    public static class FileHandler
    {

        public static List<List<string>> readAST(string file)
        {
            try
            {
                List<List<string>> obj = JsonConvert.DeserializeObject<List<List<string>>>(File.ReadAllText(file));
                if (obj is null) throw new ArgumentNullException($"{file} file could not be found.");
                return obj;
            }
            catch (UnauthorizedAccessException) { throw; }
        }

        public static string readText(string file)
        {
            return File.ReadAllText(file);
        }

        public static void writeText(string text, string file)
        {
            File.WriteAllText(file, text);
        }

        public static void writeGrammar(Dictionary<string, Dictionary<string, List<List<string>>>> comboBoxLabel, string file)
        {
            string text = JsonConvert.SerializeObject(comboBoxLabel);
            File.WriteAllText(file, text);
        }

        public static Dictionary<string, Dictionary<string, List<List<string>>>> readGrammar(string file)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, List<List<string>>>>>(File.ReadAllText(file));
        }

        public static void writeAST(List<List<string>> items, string file)
        {
            try { File.WriteAllText(file, System.Text.Json.JsonSerializer.Serialize(items)); }
            catch (UnauthorizedAccessException) { throw; }
        }
    }
}