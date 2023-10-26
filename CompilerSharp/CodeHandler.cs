using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace CompilerSharp
{

    /// <summary>
    /// Represents vailable operation types.
    /// </summary>
    public enum Type
    {
        START,
        MUL,
        ADD,
        LOAD,
        NONE,
    }

    public enum Symbol
    {
        NON_TERMINAL,
        TERMINAL
    }

    /// <summary>
    /// Provides static methods to act as an interface between the GUI 
    /// and a set of compiler for different programming languages.
    /// </summary>
    public class CodeHandler
    {
        private static Compiler compiler = new Compiler();
        private static Parser parser = new Parser();
        private static Dictionary<ISymbol, List<ISymbol>> bottomUpDict;
        private static NonTerminalSymbol symbol;

        /// <summary>
        /// Generate an AST based on the selected language and the passed source code.
        /// </summary>
        public static void handleSourceCode(string sourceCode)
        {
            if (sourceCode == null) return;
            List<string> codeAsToken = parser.codeToToken(sourceCode);
            ISymbol s = parser.codeToAST(symbol, codeAsToken);
            try { FileHandler.writeAST(parser.expressionToInternAST(parser.symbolToExpression(s)), "AST.txt"); }
            catch (UnauthorizedAccessException) { throw; }
            catch (IOException) { throw; }
        }

        /// <summary>
        /// Generate source code in the selected langauge from the generated AST.
        /// </summary>
        public static string evaluateSourceCode()
        {
            List<List<string>> ast;
            string aval = "";
            try { ast = FileHandler.readAST("AST.txt"); } catch (UnauthorizedAccessException) { throw; }
            try { aval = compiler.generateCodeFromExpression(parser.internASTtoExpression(ast, 0)); }
            catch (ArgumentOutOfRangeException) { throw new ArgumentException("Invalid argument number or order."); }
            return aval;
        }

        public static void setSourceGrammar(NonTerminalSymbol symbolS, Dictionary<ISymbol, List<ISymbol>> bottomUpD)
        {
            symbol = symbolS;
            bottomUpDict = bottomUpD;
        }
    }
}