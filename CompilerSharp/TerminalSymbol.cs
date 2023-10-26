using System;

namespace CompilerSharp
{
    public class TerminalSymbol : ISymbol
    {
        private string terminalSymbol;
        private Type type;
        private int value;
        private readonly Symbol symbol = Symbol.TERMINAL;

        public TerminalSymbol(string terminalSymbol)
        {
            this.terminalSymbol = terminalSymbol;
            this.type = Type.NONE;
            try { this.value = int.Parse(terminalSymbol); }
            catch { this.value = -1; }
        }

        public string getSymbolName()
        {
            return this.terminalSymbol;
        }

        public Type getType()
        {
            return this.type;
        }

        public int getValue()
        {
            return this.value;
        }

        public void setSymbolName(string symbolName)
        {
            this.terminalSymbol = symbolName;
        }

        public void setType(Type type) { }

        public Symbol getSymbolType() { return this.symbol; }

        public List<List<ISymbol>> getDerivationRules()
        {
            return new List<List<ISymbol>> { new List<ISymbol>() { } };
        }

        public void setDerivationRules(List<List<ISymbol>> derivationRules) { }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}