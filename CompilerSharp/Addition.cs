using System;

/// <summary>
/// Represents an ADD; operator in peudocode.
/// </summary>
public class Addition : IExpression
{
    private IExpression left;
    private IExpression right;
    private readonly Type t = Type.ADD;

    /// <summary>
    /// Constructor for an addition with two parameter.
    /// </summary>
    public Addition(IExpression left, IExpression right)
    {
        this.left = left;
        this.right = right;
    }

    public IExpression getFirst() { return this.left; }

    public void setLeft(IExpression left) { this.left = left; }

    public IExpression getSecond() { return right; }

    public void setRight(IExpression right) { this.right = right; }

    public Type getType() { return this.t; }

    public int getValue() { return this.left.getValue() + this.right.getValue(); }

    public override string ToString() { return "ADD"; }

    public void setValue(int val) { }
}
