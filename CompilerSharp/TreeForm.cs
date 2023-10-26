using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompilerSharp
{
    public partial class TreeForm : Form
    {

        Graph tree = new Graph();
        GViewer treeViewer = new GViewer();

        public TreeForm()
        {
            InitializeComponent();

            NonTerminalSymbol startFinal = new NonTerminalSymbol("S", CompilerSharp.Type.START);
            NonTerminalSymbol mulFinal = new NonTerminalSymbol("MUL", CompilerSharp.Type.MUL);
            TerminalSymbol mulTerminalFinal = new TerminalSymbol("*");
            NonTerminalSymbol load1Final = new NonTerminalSymbol("L", CompilerSharp.Type.LOAD);
            TerminalSymbol loadTerminal1Final = new TerminalSymbol("LOAD");
            NonTerminalSymbol num1Final = new NonTerminalSymbol("N", CompilerSharp.Type.LOAD);
            TerminalSymbol numTerminal1Final = new TerminalSymbol("2");
            TerminalSymbol load1SemiFinal = new TerminalSymbol(";");
            NonTerminalSymbol load2Final = new NonTerminalSymbol("L", CompilerSharp.Type.LOAD);
            TerminalSymbol loadTerminal2Final = new TerminalSymbol("LOAD");
            NonTerminalSymbol num2Final = new NonTerminalSymbol("N", CompilerSharp.Type.LOAD);
            TerminalSymbol numTerminal2Final = new TerminalSymbol("1");
            TerminalSymbol load2SemiFinal = new TerminalSymbol(";");

            num2Final.setDerivationRules(new List<List<ISymbol>>() { new List<ISymbol>() { numTerminal2Final } });
            load2Final.setDerivationRules(new List<List<ISymbol>>() { new List<ISymbol>() { loadTerminal2Final, num2Final, load2SemiFinal } });
            num1Final.setDerivationRules(new List<List<ISymbol>>() { new List<ISymbol>() { numTerminal1Final } });
            load1Final.setDerivationRules(new List<List<ISymbol>>() { new List<ISymbol>() { loadTerminal1Final, num1Final, load1SemiFinal } });
            mulFinal.setDerivationRules(new List<List<ISymbol>>() { new List<ISymbol>() { mulTerminalFinal, load1Final, load2Final } });
            startFinal.setDerivationRules(new List<List<ISymbol>>() { new List<ISymbol>() { mulFinal } });

            plotTree(startFinal);

            treeViewer.Graph = tree;
            this.Controls.Add(treeViewer);
        }

        public void plotTree(ISymbol symbol)
        {
            tree = new Graph();
            createTree(symbol, tree);
            treeViewer.Graph = tree;
        }

        private void createTree(ISymbol symbol, Graph tree)
        {
            tree.AddNode(symbol.getSymbolName());

            foreach(var derivative in symbol.getDerivationRules()[0])
            {
                createTree(derivative, tree);
                tree.AddEdge(symbol.getSymbolName(), derivative.getSymbolName());
            }
        }
    }
}
