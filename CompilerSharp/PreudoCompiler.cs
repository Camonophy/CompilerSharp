using System;

/// <summary>
/// Concrete compiler class for the pseudocode language.
/// </summary>
public class PseudoCompiler : GeneralCompiler, ICompiler
{
    public void generateASTfromCode(string code)
    {
        try { JSONHandler.write(parser.expressionToAST(parser.codeToExpression(code))); }
        catch (UnauthorizedAccessException) { throw; }
        catch (IOException) { throw; }
    }

    public string generateCodeFromAST()
    {
        List<List<string>> ast;
        string aval = "";
        try { ast = JSONHandler.read(); } catch (UnauthorizedAccessException) { throw; }
        try { aval = evaluateExpression(parser.ASTtoExpression(ast, 0)); } 
        catch (ArgumentOutOfRangeException) { throw new ArgumentException("Invalid argument number or order."); }

        return aval;
    }

    /// <summary>
    /// Translate an expression to the desired programing language code
    /// with equivalent semantic.
    /// </summary>
    private string evaluateExpression(IExpression expression)
    {
        switch (expression.getType())
        {
            case Type.START:
                if (expression.getFirst() == null) return "";
                else return evaluateExpression(expression.getFirst());
            case Type.MUL:
                switch (expression.getFirst().getType())
                {
                    case Type.MUL:
                    case Type.ADD:
                        {
                            int right = expression.getSecond().getValue();
                            string output = "";
                            if (right == 0 || expression.getFirst().getValue() == 0) return output;
                            string left = evaluateExpression(expression.getFirst());
                            left = left.Replace(Environment.NewLine, "");
                            int initialLoadIndex = left.IndexOf(';') + 1;
                            left = left.Insert(initialLoadIndex, "ADD;");
                            foreach (var i in Enumerable.Range(0, right))
                                output += $"{left}";
                            output = output.Remove(initialLoadIndex, "ADD;".Count());
                            output = output.Replace(";", $";{Environment.NewLine}");
                            return output;
                        }
                    case Type.LOAD:
                        { 
                            string left = $"{expression.getFirst().ToString()};{Environment.NewLine}";
                            int right = expression.getSecond().getValue();
                            string output = "";
                            if (right != 0) output = left;
                            foreach (var i in Enumerable.Range(0, right - 1))
                                output += $"{left}ADD;{Environment.NewLine}";
                            return output;
                        }
                    default: return "";
                }
            case Type.ADD:
                {
                    string left = evaluateExpression(expression.getFirst());
                    string right = evaluateExpression(expression.getSecond());
                    return $"{left}{right}ADD;{Environment.NewLine}";
                }
            default: return $"{expression.ToString()};{Environment.NewLine}";
        }
    }
}
