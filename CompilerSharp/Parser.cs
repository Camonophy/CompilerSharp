using Newtonsoft.Json.Linq; 
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("TestCompilerSharp.UnitTests")]
namespace CompilerSharp
{

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
        public IExpression symbolToExpression(ISymbol symbol)
        {
            switch (symbol.getType())
            {
                case Type.START:
                    return new ProgStart(symbolToExpression(symbol.getDerivationRules()[0][0]));
                case Type.ADD:
                    var dRules = ((NonTerminalSymbol)symbol).getDerivationRules();
                    var t = dRules[0].FindAll(rule => rule.getSymbolType() == Symbol.NON_TERMINAL);
                    return new Addition(symbolToExpression(t[0]), symbolToExpression(t[1]));
                case Type.MUL:
                    var mRules = ((NonTerminalSymbol)symbol).getDerivationRules();
                    var m = mRules[0].FindAll(rule => rule.getSymbolType() == Symbol.NON_TERMINAL);
                    return new Multiplication(symbolToExpression(m[0]), symbolToExpression(m[1]));
                case Type.LOAD:
                    var lRules = ((NonTerminalSymbol)symbol).getDerivationRules();
                    var l = lRules[0].FindAll(rule => rule.getSymbolType() == Symbol.NON_TERMINAL);
                    if (l.Count > 0)
                        return new Load(symbolToExpression(l[0]).getValue());
                    else
                        foreach (var lRule in lRules[0])
                        {
                            if (((TerminalSymbol)lRule).getValue() > -1)
                            {
                                return new Load(((TerminalSymbol)lRule).getValue());
                            }
                        }
                    return null;
                default: return null;
            }
        }

        public ISymbol codeToAST(ISymbol symbol, List<string> tokenList)
        {
            (List<string> rest, (ISymbol sym, List<ISymbol> derivation)) = recursiveParsing(symbol, tokenList);
            sym.setDerivationRules(new List<List<ISymbol>>() { derivation });
            if(rest.Count == 0)
            {
                return sym;
            }
            throw new ArgumentException("Tokens konnten nicht abgearbeitet werden.");
        }

        private (List<string>, (ISymbol, List<ISymbol>)) recursiveParsing(ISymbol symbol, List<string> tokenList)
        {
            ISymbol sym = (ISymbol)symbol.Clone();

            return (null, (null, null));
        }

        /// <summary>
        /// Test a set of derivative rules against a set of token.
        /// Return a complete set of derivative rules that matches the token set.
        /// If a symbol could not be matched, false is getting returned.
        /// </summary>
        private (List<ISymbol> , List<string>, bool) matchDerivatives(List<ISymbol> derivatives, List<string> tokenList)
        {
            List<ISymbol> matchedSymbols = new List<ISymbol>();
            foreach (var derivative in derivatives)
            {
                (string head, List<string> tail) = popToken(tokenList);
                if (derivative.getSymbolType().Equals(Symbol.TERMINAL))
                {
                    if (!derivative.getSymbolName().Equals(head))
                    {
                        return (matchedSymbols, tokenList, false);
                    }
                    matchedSymbols.Add(derivative);
                    tokenList = tail;
                }
                else
                {
                    (List<string> rest, (ISymbol sym, List<ISymbol> symDer)) = recursiveParsing(derivative, tokenList);
                    if(symDer.Count == 0)
                    {
                        return (matchedSymbols, tokenList, false);
                    }
                    sym.setDerivationRules(new List<List<ISymbol>>() { symDer });
                    matchedSymbols.Add(sym);
                    tokenList = rest;
                }
            }

            return (matchedSymbols, tokenList, true);
        }

        internal protected ISymbol findTerminalDerivationsForSymbol(ISymbol symbol, string token, int depth)
        {
            ISymbol sym = (ISymbol)symbol.Clone();
            if (sym.getSymbolType() == Symbol.NON_TERMINAL)
            {
                List<List<ISymbol>> nextSymbolList = new List<List<ISymbol>>();
                foreach (var derivation in sym.getDerivationRules())
                {
                    if (derivation.Count - 1 < depth)
                    {
                        continue;
                    }
                    else
                    {
                        ISymbol possibleDeriviation = findTerminalDerivationsForSymbol(derivation[depth], token, 0);
                        if (possibleDeriviation is null)
                            continue;
                        nextSymbolList.Add(derivation);
                        break;
                    }
                }

                sym.setDerivationRules(nextSymbolList);
                if (nextSymbolList.Count > 0)
                    return sym;
                else
                    return null;

            }
            else
            {
                if (sym.getSymbolName().ToLower() == token.ToLower())
                    return sym;
                else
                    return null;
            }
        }

        /// <summary>
        /// Seperate the code text into a list of tokens.
        /// </summary>
        public List<string> codeToToken(string code)
        {
            char[] alphabet = "abcdefghijklmnopqrstuvwxyz_".ToCharArray();
            char[] numbers = "0123456789".ToCharArray();
            char[] ws = $" \n\r{Environment.NewLine}".ToCharArray();
            List<string> tokens = new List<string>();
            string sequenz = "";

            foreach (char c in code)
            {
                char lowercaseChar = char.ToLower(c);
                if (ws.Contains(lowercaseChar))
                {
                    if (sequenz.Length > 0)
                    {
                        tokens.Add(sequenz);
                        sequenz = "";
                    }
                }
                else if (alphabet.Contains(lowercaseChar) || (numbers.Contains(lowercaseChar) && sequenz.Length > 0))
                {
                    sequenz += c;
                }
                else
                {
                    if (sequenz.Length > 0)
                    {
                        tokens.Add(sequenz);
                        sequenz = "";
                    }
                    tokens.Add(c.ToString());
                }
            }

            return tokens;
        }

        /// <summary>
        /// Generate based on the given expression a list of lists of strings.
        /// Output will generally be used to store the expression in a JSON file.
        /// </summary>
        public List<List<string>> expressionToInternAST(IExpression expression)
        {
            switch (expression.getType())
            {
                case Type.START:
                    if (expression.getFirst() != null)
                    {
                        List<List<string>> program = expressionToInternAST(expression.getFirst());
                        program.Insert(0, new List<string>() { expression.ToString() });
                        return program;
                    }
                    else return new List<List<string>>() { new List<string>() { expression.ToString() } };
                case Type.MUL:
                    {
                        List<List<string>> left = expressionToInternAST(expression.getFirst());
                        List<List<string>> right = expressionToInternAST(expression.getSecond());
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
                        List<List<string>> left = expressionToInternAST(expression.getFirst());
                        List<List<string>> right = expressionToInternAST(expression.getSecond());
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
        public IExpression internASTtoExpression(List<List<string>> ast, ushort depth)
        {
            IExpression op;
            ushort nextDepth = (ushort)(depth + 1);
            string[] splitCode = ast[depth][0].ToLower().Split(" ");
            switch (splitCode[0])
            {
                case "start": op = new ProgStart(internASTtoExpression(ast, nextDepth)); break;
                case "mul": op = new Multiplication(internASTtoExpression(ast, nextDepth), internASTtoExpression(ast, nextDepth)); break;
                case "add": op = new Addition(internASTtoExpression(ast, nextDepth), internASTtoExpression(ast, nextDepth)); break;
                default: op = new Load(int.Parse(splitCode[1])); break;
            }
            ast[depth].RemoveAt(0);
            if (ast[depth].Count == 0) ast.RemoveAt(depth);
            return op;
        }

        internal protected (string, List<string>) popToken(List<string> tokenList)
        {
            string token = tokenList[0];
            List<string> newTokenList = new List<string>(tokenList);
            newTokenList.RemoveAt(0);
            return (token, newTokenList);
        }

        internal protected bool astReady(ISymbol sym)
        {
            if (sym.getDerivationRules().Count > 1)
            {
                return false;
            }
            else
            {
                foreach(var rule in sym.getDerivationRules()[0])
                {
                    if(astReady(rule))
                    {
                        continue;
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }

    /*
            public (bool, ISymbol, List<string>) codeToAST(ISymbol currentSymbol, int symbolDepth, List<string> tokenList)
            {
                ISymbol currentState = (ISymbol)currentSymbol.Clone();
                if (tokenList.Count > 0)
                {
                    (string head, List<string> tail) = popToken(tokenList);
                    if (currentState.getSymbolType().Equals(Symbol.TERMINAL))
                    {
                        return (currentState.getSymbolName().Equals(head), currentState, tail);
                    } 
                    else
                    {
                        var derivations = currentState.getDerivationRules();
                        List<(ISymbol, List<string>)> potentialSymbols = new List<(ISymbol, List<string>)>();

                        foreach(var derivation in derivations)
                        {
                            if(symbolDepth > derivation.Count-1)
                            {
                                foreach(var symbol in derivation)
                                {
                                    (bool correct, ISymbol potentialSymbol, List<string> rest) = codeToAST(symbol, symbolDepth, tokenList);
                                    if (correct) potentialSymbols.Add((potentialSymbol, rest));
                                }
                            } else
                            {
                                (bool correct, ISymbol potentialSymbol, List<string> rest) = codeToAST(derivation[symbolDepth], symbolDepth, tokenList);
                                if (correct) potentialSymbols.Add((potentialSymbol, rest));
                            }
                        }

                        while(potentialSymbols.Count > 1)
                        {
                            ++symbolDepth;
                            List<(ISymbol, List<string>)> newPotentialSymbols = new List<(ISymbol, List<string>)>();
                            foreach (var derivation in potentialSymbols)
                            {
                                (bool correct, ISymbol potentialSymbol, List<string> rest) = codeToAST(derivation.Item1, symbolDepth, tokenList);
                                if (correct) newPotentialSymbols.Add((potentialSymbol, rest));
                            }
                            potentialSymbols = newPotentialSymbols;
                        }
                        if (potentialSymbols.Count == 1)
                        {
                            return codeToAST(potentialSymbols[0].Item1, 0, potentialSymbols[0].Item2);
                        }
                    }
                }
                return (false, currentState, tokenList);
            }
            */
}