using System;

/// <summary>
/// Represents an interface to structure all available compiler.
/// </summary>
interface ICompiler
{
    /// <summary>
    /// Bundle and execute all steps to generate an AST file from a given source code.
    /// </summary>
    void generateASTfromCode(string code);

    /// <summary>
    /// Bundle and execute all steps to generate the target source code from an AST file.
    /// </summary>
    string generateCodeFromAST();

}