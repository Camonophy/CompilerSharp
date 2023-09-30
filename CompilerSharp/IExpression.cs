using System;
using System.Configuration;

/// <summary>
/// Represents an interface to structure all operations in a program.
/// </summary>
public interface IExpression
{
    /// <summary>
    /// Return the first given parameter of this expression.
    /// </summary>
    public IExpression getFirst();

    /// <summary>
    /// Change the value of the first parameter of this expression.
    /// </summary>
    public void setLeft(IExpression left);

    /// <summary>
    /// Return the second given parameter of this expression.
    /// </summary>
    public IExpression getSecond();

    /// <summary>
    /// Change the value of the second parameter of this expression.
    /// </summary>
    public void setRight(IExpression right);

    /// <summary>
    /// Return the value of this expression.
    /// </summary>
    public int getValue();

    /// <summary>
    /// Set the value of this expression. 
    /// Usually used for 'LOAD' and 'STORE' operations.
    /// </summary>
    public void setValue(int val);

    /// <summary>
    /// Return the type of this expression.
    /// </summary>
    public Type getType();

    /// <summary>
    /// Return a formated string of the expression in 
    /// for the desired programming language.
    /// </summary>
    public string ToString();

}