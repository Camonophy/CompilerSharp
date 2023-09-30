using System;

/// <summary>
/// Root expression for a program in pseudocode.
/// </summary>
public class ProgStart : IExpression
{
    private IExpression left;
    private readonly Type t = Type.START;

    /// <summary>
    /// Constructor for an empty program.
    /// </summary>
    public ProgStart() {  }

    /// <summary>
    /// Constructor for a program with an expression.
    /// </summary>
    public ProgStart(IExpression left) { this.left = left; }

    public IExpression getFirst() { return this.left; }

    public IExpression getSecond() { return null; }

    public Type getType() { return this.t; }

    public int getValue() { return -1; }

    public override string ToString() { return "START"; }

    public void setLeft(IExpression left) { this.left = left; }

    public void setRight(IExpression right) { }

    public void setValue(int val) { }

}
