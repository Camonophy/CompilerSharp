using System;
using System.Collections.Generic;
using System.Linq;

namespace CompilerSharp
{
    public class NonTerminalSymbol : ISymbol
    {
        private string nonTerminalSymbol;
        private List<List<ISymbol>> derivationRules = new List<List<ISymbol>>();
        private int derivationRulesIndex = 0;
        private Type type;
        private readonly Symbol symbol = Symbol.NON_TERMINAL;

        public NonTerminalSymbol(string nonTerminalSymbol, List<List<ISymbol>> derivationRules, Type type)
        {
            this.nonTerminalSymbol = nonTerminalSymbol;
            this.derivationRules = derivationRules;
            this.type = type;
        }

        public NonTerminalSymbol(string nonTerminalSymbol, Type type)
        {
            this.nonTerminalSymbol = nonTerminalSymbol;
            this.derivationRules = new List<List<ISymbol>>() { new List<ISymbol>() };
            this.type = type;
        }

        public string getSymbolName() { return this.nonTerminalSymbol; }

        public Type getType() { return this.type; }

        public Symbol getSymbolType() { return this.symbol; }

        public void setSymbolName(string symbolName) { this.nonTerminalSymbol = symbolName; }

        public void setType(Type type) { this.type = type; }

        public int getValue()
        {
            string val = "";
            foreach (ISymbol sym in this.derivationRules)
                val += sym.getValue().ToString();

            if (val.Length == 0) return -1;
            else return int.Parse(val);
        }

        public void addRightSideRule(ISymbol symbol)
        {
            derivationRules.Add(new List<ISymbol> { symbol });
            derivationRulesIndex++;
        }

        public void expandRightSideRule(ISymbol symbol)
        {
            derivationRules[derivationRulesIndex].Add(symbol);
        }

        public List<List<ISymbol>> getDerivationRules()
        {
            return this.derivationRules;
        }

        public List<List<string>> getDerivationRulesAsString()
        {
            List<List<string>> rulesAsString = new List<List<string>>();
            for (int i = 0; i < derivationRules.Count; i++)
            {
                var rules = derivationRules[i];
                rulesAsString.Add(new List<string>());
                foreach (var rule in rules)
                {
                    rulesAsString[i].Add(rule.getSymbolName());
                }
            }

            return rulesAsString;
        }

        public void setDerivationRules(List<List<ISymbol>> derivationRules)
        {
            this.derivationRules = derivationRules;
        }

        public Dictionary<ISymbol, List<TerminalSymbol>> getTerminalSymbolDict(int symbolDepth)
        {
            Dictionary<string, Dictionary<ISymbol, List<TerminalSymbol>>> symbolDict = recurivelyGatherTerminalSymbols(new Dictionary<string, Dictionary<ISymbol, List<TerminalSymbol>>>(), symbolDepth);
            return symbolDict[this.nonTerminalSymbol];

        }

        private Dictionary<string, Dictionary<ISymbol, List<TerminalSymbol>>> recurivelyGatherTerminalSymbols(Dictionary<string, Dictionary<ISymbol, List<TerminalSymbol>>> terminalDictionary, int symbolDepth)
        {
            if (!terminalDictionary.ContainsKey(this.nonTerminalSymbol))
            {
                Dictionary<ISymbol, List<TerminalSymbol>> terminalsDict = new Dictionary<ISymbol, List<TerminalSymbol>>();
                foreach (var rule in derivationRules)
                {
                    if (symbolDepth > rule.Count - 1)
                    {
                        if (terminalsDict.Count == 0)
                        {
                            return new Dictionary<string, Dictionary<ISymbol, List<TerminalSymbol>>>() { [this.nonTerminalSymbol] = new Dictionary<ISymbol, List<TerminalSymbol>>() };
                        }
                        return terminalDictionary;
                    }
                    if (rule[symbolDepth].getSymbolType() == Symbol.TERMINAL)
                    {
                        terminalsDict.Add(rule[symbolDepth], new List<TerminalSymbol>() { (TerminalSymbol)rule[symbolDepth] });
                    }
                    else
                    {
                        NonTerminalSymbol t = (NonTerminalSymbol)rule[symbolDepth];
                        Dictionary<string, Dictionary<ISymbol, List<TerminalSymbol>>> nonTDict = t.recurivelyGatherTerminalSymbols(terminalDictionary, 0);
                        terminalsDict.Add(rule[symbolDepth], new List<TerminalSymbol>());
                        foreach (var derivativeTerminalDict in nonTDict[rule[symbolDepth].getSymbolName()])
                        {
                            foreach (var v in derivativeTerminalDict.Value)
                            {
                                terminalsDict[rule[symbolDepth]].Add(v);
                            }
                        }
                    }
                }
                terminalDictionary.Add(this.nonTerminalSymbol, terminalsDict);
            }
            return terminalDictionary;
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}