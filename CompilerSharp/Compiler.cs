using System;

namespace CompilerSharp
{

    /// <summary>
    /// Concrete compiler class for the pseudocode language.
    /// </summary>
    public class Compiler
    {
        /// <summary>
        /// Translate an expression to the desired programing language code
        /// with equivalent semantic.
        /// </summary>
        public string generateCodeFromExpression(IExpression expression)
        {
            switch (expression.getType())
            {
                case Type.START:
                    if (expression.getFirst() == null) return "";
                    else return generateCodeFromExpression(expression.getFirst());
                case Type.MUL:
                    switch (expression.getFirst().getType())
                    {
                        case Type.MUL:
                        case Type.ADD:
                            {
                                int right = expression.getSecond().getValue();
                                string output = "";
                                if (right == 0 || expression.getFirst().getValue() == 0) return output;
                                string left = generateCodeFromExpression(expression.getFirst());
                                left = left.Replace("\n", "");
                                int initialLoadIndex = left.IndexOf(';') + 1;
                                left = left.Insert(initialLoadIndex, "ADD;");
                                foreach (var i in Enumerable.Range(0, right))
                                    output += $"{left}";
                                output = output.Remove(initialLoadIndex, "ADD;".Count());
                                output = output.Replace(";", $";\n");
                                return output;
                            }
                        case Type.LOAD:
                            {
                                string left = $"{expression.getFirst().ToString()};\n";
                                int right = expression.getSecond().getValue();
                                string output = "";
                                if (right != 0) output = left;
                                foreach (var i in Enumerable.Range(0, right - 1))
                                    output += $"{left}ADD;\n";
                                return output;
                            }
                        default: return "";
                    }
                case Type.ADD:
                    {
                        string left = generateCodeFromExpression(expression.getFirst());
                        string right = generateCodeFromExpression(expression.getSecond());
                        return $"{left}{right}ADD;\n";
                    }
                default: return $"{expression.ToString()};\n";
            }
        }
    }
}