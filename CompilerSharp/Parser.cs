using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;

/// <summary>
/// Provides methods to translate pseudocode from and to an AST, expression 
/// and strings.
/// </summary>
public class Parser
{

    /// <summary>
    /// Takes a source code text, makes a first validation 
    /// and generates a pseudo code expression if correct.
    /// </summary>
    public IExpression codeToExpression(string code)
    {
        ProgStart start = new ProgStart();
        List<IExpression> ast = new List<IExpression>();
        int astIndex = 0;
        code = code.Replace(Environment.NewLine, "");
        try
        {
            if (!code.EndsWith(';') && code.Count() > 0) throw new IOException();
            string[] codeLines = code.Split(";");

            foreach (string line in codeLines)
            {
                if (line.Count() == 0) continue;
                if (line.Count() < 3 && line.Count() > 0) throw new IOException();
                string[] splitCode = line.Split(" ");
                switch (splitCode[0])
                {
                    case "load":
                        {
                            ast.Add(new Load(int.Parse(splitCode[1])));
                        }
                        astIndex++;
                        break;
                    case "mul":
                        {
                            ast[astIndex - 2] = new Multiplication(ast[astIndex - 2], ast[astIndex - 1]);
                            ast.RemoveAt(astIndex - 1);
                        }
                        astIndex--;
                        break;
                    case "add":
                        {
                            ast[astIndex - 2] = new Addition(ast[astIndex - 2], ast[astIndex - 1]);
                            ast.RemoveAt(astIndex - 1);
                        }
                        astIndex--;
                        break;
                    default: throw new IOException();
                }
            }
        }
        catch { throw new IOException("There is an error in the code. Maybe a typo or missing symbols/arguments."); }
        if (astIndex > 1) throw new ArgumentException("Syntax error: Unused loads or invalid arithmetic operations. " +
            "More than one value are left on the stack.");
        else if(astIndex == 1) start.setLeft(ast[0]);
        return start;
    }

    /// <summary>
    /// Generate based on the given expression a list of lists of strings.
    /// Output will generally be used to store the expression in a JSON file.
    /// </summary>
    public List<List<string>> expressionToAST(IExpression expression)
    {
        switch (expression.getType())
        {
            case Type.START:
                if (expression.getFirst() != null)
                {
                    List<List<string>> program = expressionToAST(expression.getFirst());
                    program.Insert(0, new List<string>() { expression.ToString() });
                    return program;
                }
                else return new List<List<string>>() { new List<string>() { expression.ToString() } };
            case Type.MUL:
                {
                    List<List<string>> left = expressionToAST(expression.getFirst());
                    List<List<string>> right = expressionToAST(expression.getSecond());
                    List<List<string>> ast = new List<List<string>>() { new List<string>() { expression.ToString() } };
                    for (int i = 0; i < Math.Min(left.Count, right.Count); i++)
                    {
                        ast.Add(left[0].Concat(right[0]).ToList());
                        left.RemoveAt(0);
                        right.RemoveAt(0);
                    }
                    ast = ast.Concat(left).Concat(right).ToList();
                    return ast;
                }
            case Type.ADD:
                {
                    List<List<string>> left = expressionToAST(expression.getFirst());
                    List<List<string>> right = expressionToAST(expression.getSecond());
                    List<List<string>> ast = new List<List<string>>() { new List<string>() { expression.ToString() } };
                    for (int i = 0; i < Math.Min(left.Count, right.Count); i++)
                    {
                        ast.Add(left[0].Concat(right[0]).ToList());
                        left.RemoveAt(0);
                        right.RemoveAt(0);
                    }
                    ast = ast.Concat(left).Concat(right).ToList();
                    return ast;
                }
            default: return new List<List<string>>() { new List<string>() { expression.ToString() } };
        }
    }

    /// <summary>
    /// Generate a list of lists of strings from an expression. 
    /// <paramref name="depth"/> is used to control the traversation of the AST.
    /// This method is usually used to recover an AST from a generated AST JSON file.
    /// </summary>
    public IExpression ASTtoExpression(List<List<string>> ast, ushort depth)
    {
        IExpression op;
        ushort nextDepth = (ushort)(depth + 1);
        string[] splitCode = ast[depth][0].ToLower().Split(" ");
        switch (splitCode[0])
        {
            case "start":   op = new ProgStart(ASTtoExpression(ast, nextDepth)); break;
            case "mul":     op = new Multiplication(ASTtoExpression(ast, nextDepth), ASTtoExpression(ast, nextDepth));  break;
            case "add":     op = new Addition(ASTtoExpression(ast, nextDepth), ASTtoExpression(ast, nextDepth)); break;
            default:        op = new Load(int.Parse(splitCode[1])); break;
        }
        ast[depth].RemoveAt(0);
        if (ast[depth].Count == 0) ast.RemoveAt(depth);
        return op;
    }
}
