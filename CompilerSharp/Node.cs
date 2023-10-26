using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("TestCompilerSharp.UnitTests")]
namespace CompilerSharp
{
    public class Node : ICloneable
    {

        // Symbole in diesem Knoten
        private List<ISymbol> symbols;                                      

        // Aktuelle tokens an diesem Knoten
        private List<string> tokens;

        // Feste nachfolgende Knoten
        private List<Node> children;

        // Moegliche nachfolgende Knoten mit deren Tokenfortschritt
        private Dictionary<List<Node>, List<string>> possibleChildren;

        // An welchem Symbol in diesem Knoten grad gearbeitet wird
        internal protected int symbolIndex;

        // Auf welcher Hoehe sich der Knoten befindet
        private int nodeDepth;

        // Dieser Knoten ist potentiell korrekt
        private bool valid;

        public Node(List<ISymbol> symbols, List<string> tokens, int nodeHeight)
        {
            this.symbols = symbols;
            this.tokens = tokens;
            this.children = new List<Node>();
            this.possibleChildren = new Dictionary<List<Node>, List<string>>();
            this.symbolIndex = 0;
            this.valid = true;
            this.nodeDepth = nodeHeight;                   // Jeder Kindsknoten hat diese hoehe + 1
        }

        public Node(List<ISymbol> symbols, List<string> tokens, int nodeHeight, Dictionary<List<Node>, List<string>> possibleChildren)
        {
            this.symbols = symbols;
            this.tokens = tokens;
            this.children = new List<Node>();
            this.possibleChildren = new Dictionary<List<Node>, List<string>>();
            foreach (var entry in possibleChildren)
            {
                List<Node> nodeList = new List<Node>(entry.Key);
                List<string> tokenList = new List<string>(entry.Value);
                this.possibleChildren.Add(nodeList, tokenList);
            }

            this.symbolIndex = 0;
            this.valid = true;
            this.nodeDepth = nodeHeight;                   // Jeder Kindsknoten hat diese hoehe + 1
        }

        public void evaluate()
        {
            foreach(var sym in symbols)
            {
                if(sym.getSymbolType().Equals(Symbol.TERMINAL))
                {
                    compareTerminal();
                } 
                else
                {
                    compareNonTerminal();
                }
                if (!this.valid) return;
            }
        }

        public List<string> getTokens() { return this.tokens; }

        public void setTokens(List<string> tokens) { this.tokens = tokens; }

        public List<ISymbol> getSymbols() { return this.symbols; }

        public void setSymbol(List<ISymbol> symbols) { this.symbols = symbols; }

        public int getNodeDepth() { return this.nodeDepth; }

        public void setNodeDepth(int nodeDepth) { this.nodeDepth = nodeDepth; }

        public List<Node> getChildren() { return this.children; }

        public Dictionary<List<Node>, List<string>> getPossibleChildren() { return this.possibleChildren; }

        public void setPossibleChildren(Dictionary<List<Node>, List<string>> possibleChildren) { this.possibleChildren = possibleChildren; }

        internal protected void compareNonTerminal()
        {
            var sym = this.symbols[symbolIndex];
            foreach (var rule in sym.getDerivationRules())
            {
                Node newNode = new Node(rule, tokens, this.nodeDepth + 1, this.possibleChildren);
                newNode.evaluate();
                if (newNode.isValid())
                {
                    this.possibleChildren = newNode.getPossibleChildren();
                }
            }
            symbolIndex++;
        }


        internal protected void compareTerminal()
        {
            (var head, var tail) = popToken(this.tokens);
            if (possibleChildren.Count > 0)
            {
                foreach (var entry in possibleChildren)
                {
                    if (entry.Value.Count == 0)
                    {
                        valid = false;                  // At least one terminal symbol too much
                        return;
                    }
                    else
                    {
                        if (this.symbols[symbolIndex].getSymbolName().Equals(entry.Value[0]))
                        {
                            entry.Value.RemoveAt(0);    // Terminal match
                            break;
                        }
                        else
                        {
                            if (entry.Key[0].getNodeDepth() > this.nodeDepth)
                            {
                                this.possibleChildren.Remove(entry.Key);     // Terminal missmatch
                            }
                        }
                    }
                }
                if (possibleChildren.Count == 0)                    // No existing children sequenz is matching; create new
                {
                    if (head.Equals(this.symbols[symbolIndex].getSymbolName()))
                    {
                        this.tokens = tail;
                        symbolIndex++;
                        this.possibleChildren.Add(new List<Node>() { (this) }, tail);
                    }
                    else
                    {
                        valid = false;
                        return;
                    }
                } else if(possibleChildren.Count == 1)
                {
                    this.tokens = tail;
                    if (!possibleChildren.Keys.ToList()[0][0].Equals(this) && possibleChildren.Keys.ToList()[0][0].nodeDepth > this.nodeDepth)
                    {
                        var sequenz = possibleChildren.Keys.ToList()[0];
                        pickChildren(sequenz);
                    }
                    else if (!possibleChildren.Keys.ToList()[0][0].Equals(this)) {
                        symbolIndex++;
                        this.possibleChildren.Add(new List<Node>() { (this) }, tail);
                    }
                }
                // Child sequenzes are left
                // In this case, no exisitng terminal sequenzed adds up with my symbol
                // foreach?
                else
                {
                    this.tokens = tail;
                    if (!possibleChildren.Keys.ToList()[0][0].Equals(this))
                    {
                        var sequenz = possibleChildren.Keys.ToList()[0];
                        pickChildren(sequenz);
                    }
                }
            }
            else
            {
                if (head.Equals(this.symbols[symbolIndex].getSymbolName()))
                {
                    this.tokens = tail;
                    symbolIndex++;
                    this.possibleChildren.Add(new List<Node>() { (this) }, tail);
                }
                else
                {
                    valid = false;
                    return;
                }
            }
        }


            private void addNodeToKeys()
        {
            foreach (var entry in this.possibleChildren)
            {
                entry.Key.Add(this);
            }
        }

        internal protected List<Node> pickChildren(List<Node> nodes)
        {
            Node node;
            List<Node> newNodes = nodes;
            List<Node> safeNode;
            for (int i = 0; i < nodes.Count; i++)
            {
                safeNode = newNodes;
                (node, newNodes) = popNode(newNodes);
                if (node.getNodeDepth() <= this.getNodeDepth())
                {
                    return safeNode;
                } 
                else
                {
                    newNodes = node.pickChildren(newNodes);
                    this.children.Insert(0, node);
                }

                if (newNodes.Count == 0) return newNodes;
            }
            return nodes;
        }

        private (string, List<string>) popToken(List<string> tokenList)
        {
            string token = tokenList[0];
            List<string> newTokenList = new List<string>(tokenList);
            newTokenList.RemoveAt(0);
            return (token, newTokenList);
        }

        private (Node, List<Node>) popNode(List<Node> nodeList)
        {
            Node node = nodeList[0];
            List<Node> newNodeList = new List<Node>(nodeList);
            newNodeList.RemoveAt(0);
            return (node, newNodeList);
        }

        public bool isValid() { return this.valid; }

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        /*
        internal protected void compareTerminal()
        {
            (var head, var tail) = popToken(this.tokens);
            if(possibleChildren.Count > 0)
            {
                foreach(var entry in possibleChildren)
                {
                    if (entry.Value.Count == 0)
                    {
                        valid = false;                  // At least one terminal symbol too much
                        return;
                    } 
                    else
                    {
                        if(this.symbols[symbolIndex].getSymbolName().Equals(entry.Value[0]))
                        {
                            entry.Value.RemoveAt(0);    // Terminal match
                            return;
                        }
                    }
                }
                if (possibleChildren.Count == 0)                    // No existing children sequenz is matching; create new
                {
                    if (head.Equals(this.symbols[symbolIndex].getSymbolName()))
                    {
                        symbolIndex++;
                        this.possibleChildren.Add(new List<Node>() { (this) }, tail);
                    }
                    else
                    {
                        valid = false;
                        return;
                    }
                }
                else                                                 // Exactly one child sequenz is now left
                {
                    this.tokens = tail;
                    if (!possibleChildren.Keys.ToList()[0][0].Equals(this))
                    {
                        var sequenz = possibleChildren.Keys.ToList()[0];
                        pickChildren(sequenz);
                    }
                }
            } 
            else
            {
                if (head.Equals(this.symbols[symbolIndex].getSymbolName()))
                {
                    this.tokens = tail;
                    symbolIndex++;
                    this.possibleChildren.Add(new List<Node>() { (this) }, tail);
                } 
                else
                {
                    valid = false;
                    return;
                }
            }
        }*/
    }
}
