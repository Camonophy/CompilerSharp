using System;

namespace CompilerSharp
{

    public interface ISymbol : ICloneable
    {
        public string getSymbolName();

        public Type getType();

        public void setSymbolName(string symbolName);

        public void setType(Type type);

        public Symbol getSymbolType();

        public int getValue();

        public List<List<ISymbol>> getDerivationRules();

        public void setDerivationRules(List<List<ISymbol>> derivationRules);
    }
}