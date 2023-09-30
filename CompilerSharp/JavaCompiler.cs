using System;

public class JavaCompiler : GeneralCompiler, ICompiler
{

    public void generateASTfromCode(string code)
    {
        code = code.Replace(Environment.NewLine, "");
        List<List<string>> ast = new List<List<string>>();
        string[] codeLines = code.Split(";");

        //TODO
    }

    public string generateCodeFromAST()
    {
        List<List<string>> ast;
        try { ast = JSONHandler.read(); } catch (UnauthorizedAccessException) { throw; }
        string aval = evaluateAST(ast, 0, 0);

        return aval;
    }

    private string evaluateAST(List<List<string>> ast, int depth, int path)
    {
        //TODO
        return null;
    }
}
