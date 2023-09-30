using Newtonsoft.Json;
using System;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.RegularExpressions;

/// <summary>
/// Provides static methods to read and write AST to and from files.
/// </summary>
public static class JSONHandler
{
	private static string path = ".";
    private static string fileName = "AST.json";

    public static void setPath(string newPath)
    {
        path = newPath;
    }

    public static List<List<string>> read()
    {
        try 
        {
            List<List<string>> obj = JsonConvert.DeserializeObject<List<List<string>>>(File.ReadAllText($"{path}/{fileName}"));
            if(obj is null ) throw new ArgumentNullException($"{fileName} file could not be found under {path}.");
            return obj; 
        }
        catch (UnauthorizedAccessException) { throw; }
    }

    public static void write(List<List<string>> items)
	{
        try{ File.WriteAllText($"{path}/{fileName}", System.Text.Json.JsonSerializer.Serialize(items)); } 
        catch (UnauthorizedAccessException) { throw; }
    }
}
