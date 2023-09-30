using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

/// <summary>
/// Represents supported programming languages.
/// </summary>
public enum Language
{
    PSEUDO,
    JAVA,
}

/// <summary>
/// Represents vailable operation types.
/// </summary>
public enum Type
{
    START,
    MUL,
    ADD,
    LOAD,
}

/// <summary>
/// Provides static methods to act as an interface between the GUI 
/// and a set of compiler for different programming languages.
/// </summary>
public class CodeHandler
{

    private static Language sourceLanguage;
    private static Language targetLanguage;
    private static ICompiler compiler;

    /// <summary>
    /// Generate an AST based on the selected language and the passed source code.
    /// </summary>
    public static void handleSourceCode(string sourceCode)
    {
        switch (sourceLanguage)
        {
            case Language.PSEUDO: compiler = new PseudoCompiler(); break;
            case Language.JAVA: compiler = new JavaCompiler(); break;
            default: throw new ArgumentException("Language could not be recognized.");
        }
        try { compiler.generateASTfromCode(sourceCode.ToLower()); } 
        catch (IOException) { throw; }
    }

    /// <summary>
    /// Generate source code in the selected langauge from the generated AST.
    /// </summary>
    public static string evaluateSourceCode()
    {
        switch (targetLanguage)
        {
            case Language.PSEUDO: compiler = new PseudoCompiler(); break;
            case Language.JAVA: compiler = new JavaCompiler(); break;
            default: throw new ArgumentException("Language could not be recognized.");
        }
        return compiler.generateCodeFromAST();
    }


    public static void setSourceLanguage(Language language) { sourceLanguage = language; }

    public static void setTargetLanguage(Language language) { targetLanguage = language; }


    public static Language getLanguageFromText(string text)
    {
        switch(text.ToLower())
        {
            case "pseudo": return Language.PSEUDO;
            case "java": return Language.JAVA;
            default: throw new ArgumentException("Selected language is not valid."); 
        }
    }

}
