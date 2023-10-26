using CompilerSharp;
using System;

namespace CompilerSharp
{

    /// <summary>
    /// Represents a MUL; operator in peudocode.
    /// </summary>
    public class Multiplication : IExpression
    {
        private IExpression left;
        private IExpression right;
        private readonly Type t = Type.MUL;

        /// <summary>
        /// Constructor for a multiplication with two parameter.
        /// </summary>
        public Multiplication(IExpression left, IExpression right)
        {
            this.left = left;
            this.right = right;
        }

        public IExpression getFirst() { return this.left; }

        public void setLeft(IExpression left) { this.left = left; }

        public IExpression getSecond() { return right; }

        public void setRight(IExpression right) { this.right = right; }

        public Type getType() { return this.t; }

        public int getValue() { return this.left.getValue() * this.right.getValue(); }

        public override string ToString() { return "MUL"; }

        public void setValue(int val) { }
    }
}