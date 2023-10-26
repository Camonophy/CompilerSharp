using CompilerSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace TestCompilerSharp.UnitTests
{
    public class NodeTest
    {

        [Fact]
        public void CompareNonTerminal_Test()
        {
            var symbol = exampleSymbol();
            var tokenS = new List<string> { "LOAD", "3", "+", "+", ";" };
            Node S = new Node(new List<ISymbol> { symbol }, tokenS, 0);
            S.evaluate();
            Assert.Equal(1, 1);
        }

        [Fact]
        public void CompareTerminal_Test()
        {
            var symbol = exampleSymbol();
            var tokenS = new List<string> { "load", "3", "+", "+", ";" };
            Node S = new Node(new List<ISymbol> { symbol }, tokenS, 0);
            var derS = symbol.getDerivationRules()[0];

            var tokenL = new List<string> { "load", "3", "+", "+", ";" };
            Node L = new Node(derS, tokenL, 1);
            var derL = derS[0].getDerivationRules()[0];

            var tokenK = new List<string> { "LOAD", "3", "+", "+", ";" };
            Node K = new Node(derL, tokenK, 2);
            var derK1 = derL[0].getDerivationRules()[0];
            var derK2 = derL[0].getDerivationRules()[1];

            var tokenX1 = new List<string> { "LOAD", "3", "+", "+", ";" };
            Node X1 = new Node(derK1, tokenX1, 3);

            X1.compareTerminal();

            Dictionary<List<Node>, List<string>> n = new Dictionary<List<Node>, List<string>>(X1.getPossibleChildren());
            var tokenX2 = new List<string> { "LOAD", "3", "+", "+", ";" };
            Node X2 = new Node(derK2, tokenX2, 3, n);

            X2.compareTerminal();
            X2.compareTerminal();

            Assert.Equal(0, X1.getChildren().Count);
            Assert.Equal(1, X1.getPossibleChildren().Count);
            Assert.Equal(new List<string> { "3", "+", "+", ";" }, X1.getPossibleChildren().Values.ToList()[0]);

            Assert.Equal(0, X2.getChildren().Count);
            Assert.Equal(2, X2.getPossibleChildren().Count);
            Assert.Equal(new List<string> { "+", "+", ";" }, X2.getPossibleChildren().Values.ToList()[0]);

            K.setPossibleChildren(X2.getPossibleChildren());
            K.symbolIndex = 1;
            K.compareTerminal();

            Assert.Equal(1, K.getChildren().Count);
            Assert.Equal(1, K.getPossibleChildren().Count);
            Assert.Equal(new List<string> { "+", "+", ";" }, K.getPossibleChildren().Values.ToList()[0]);
        }

        [Fact]
        public void PickChildren_Test()
        {
            var symbol = exampleSymbol();
            var tokenS = new List<string> { "load", "3", "+", "+", ";" };
            Node S = new Node(new List<ISymbol> { symbol }, tokenS, 0);
            var derS = symbol.getDerivationRules()[0];

            var tokenL = new List<string> { "load", "3", "+", "+", ";" };
            Node L = new Node(derS, tokenL, 1);
            var derL = derS[0].getDerivationRules()[0];

            var tokenK = new List<string> { "load", "3", "+", "+", ";" };
            Node K = new Node(derL, tokenK, 2);
            var derK1 = derL[0].getDerivationRules()[0];
            var derK2 = derL[0].getDerivationRules()[1];

            var tokenX1 = new List<string> { "load", "3", "+", "+", ";" };
            Node X1 = new Node(derK1, tokenX1, 3);

            var tokenX2 = new List<string> { "load", "3", "+", "+", ";" };
            Node X2 = new Node(derK2, tokenX2, 3);

            var derT1 = derL[2].getDerivationRules()[0];
            var derT2 = derL[2].getDerivationRules()[1];

            var tokenX3 = new List<string> { "load", "3", "+", "+", ";" };
            Node X3 = new Node(derT1, tokenX3, 3);

            var tokenX4 = new List<string> { "load", "3", "+", "+", ";" };
            Node X4 = new Node(derT2, tokenX4, 3);

            List<Node> nodeList = new List<Node> { L, K, X4, X1 };

            S.pickChildren(nodeList);

            Assert.Equal("S", S.getSymbols()[0].getSymbolName());
            var LC = S.getChildren()[0];
            Assert.Equal("L;", LC.getSymbols()[0].getSymbolName() + LC.getSymbols()[1].getSymbolName());
            var KC = LC.getChildren()[0];
            Assert.Equal("K3T", KC.getSymbols()[0].getSymbolName() + KC.getSymbols()[1].getSymbolName() + KC.getSymbols()[2].getSymbolName());
            var X1C = KC.getChildren()[0];
            Assert.Equal("LOAD", X1C.getSymbols()[0].getSymbolName());
            var X4C = KC.getChildren()[1];
            Assert.Equal("++", X4C.getSymbols()[0].getSymbolName() + X4C.getSymbols()[1].getSymbolName());
        }

        private NonTerminalSymbol exampleSymbol()
        {
            NonTerminalSymbol start = new NonTerminalSymbol("S", CompilerSharp.Type.START);
            NonTerminalSymbol startFinal = new NonTerminalSymbol("L", CompilerSharp.Type.START);
            TerminalSymbol semi = new TerminalSymbol(";");
            NonTerminalSymbol mulFinal = new NonTerminalSymbol("K", CompilerSharp.Type.MUL);
            TerminalSymbol mulTerminalFinal = new TerminalSymbol("3");
            NonTerminalSymbol load1Final = new NonTerminalSymbol("T", CompilerSharp.Type.LOAD);
            TerminalSymbol loadTerminal1Final = new TerminalSymbol("LOAD");
            TerminalSymbol load1SemiFinal = new TerminalSymbol("+");

            load1Final.setDerivationRules(new List<List<ISymbol>>() { new List<ISymbol>() { load1SemiFinal }, new List<ISymbol>() { load1SemiFinal, load1SemiFinal } });
            mulFinal.setDerivationRules(new List<List<ISymbol>>() { new List<ISymbol>() { loadTerminal1Final }, new List<ISymbol>() { loadTerminal1Final, mulTerminalFinal } });
            startFinal.setDerivationRules(new List<List<ISymbol>>() { new List<ISymbol>() { mulFinal, mulTerminalFinal, load1Final } });
            start.setDerivationRules(new List<List<ISymbol>>() { new List<ISymbol>() { startFinal, semi } });

            return start;
        }
    }
}
