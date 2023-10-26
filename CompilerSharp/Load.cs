using CompilerSharp;
using System;

namespace CompilerSharp
{
    /// <summary>
    /// Represents an LOAD; operator in peudocode.
    /// </summary>
    public class Load : IExpression
    {
        private int value = 0;
        private readonly Type t = Type.LOAD;

        /// <summary>
        /// Constructor for a load with one parameter.
        /// </summary>
        public Load(int value) { this.value = value; }

        public Load(ISymbol value) { this.value = value.getValue(); }

        public IExpression getFirst() { return null; }

        public IExpression getSecond() { return null; }

        public Type getType() { return this.t; }

        public int getValue() { return this.value; }

        public override string ToString() { return $"LOAD {this.value}"; }

        public void setLeft(IExpression left) { }

        public void setRight(IExpression right) { }

        public void setValue(int val) { this.value = val; }
    }
}