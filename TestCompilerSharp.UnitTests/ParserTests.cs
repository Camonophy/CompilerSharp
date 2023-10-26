using CompilerSharp;
using Microsoft.VisualBasic;
using System.Drawing;
using System;
using System.Data;

namespace TestCompilerSharp.UnitTests
{
    public class ParserTests
    {
        Parser p = new Parser();

        NonTerminalSymbol startStandart = new NonTerminalSymbol("S", CompilerSharp.Type.START);

        NonTerminalSymbol mul = new NonTerminalSymbol("MUL", CompilerSharp.Type.MUL);
        TerminalSymbol mulTerminal = new TerminalSymbol("*");
        NonTerminalSymbol load1Mul = new NonTerminalSymbol("L", CompilerSharp.Type.LOAD);
        TerminalSymbol loadTerminal1Mul = new TerminalSymbol("LOAD");
        NonTerminalSymbol num1Mul = new NonTerminalSymbol("N", CompilerSharp.Type.LOAD);
        TerminalSymbol numTerminal1Mul1 = new TerminalSymbol("1");
        TerminalSymbol numTerminal1Mul2 = new TerminalSymbol("2");
        TerminalSymbol load1SemiMul = new TerminalSymbol(";");
        NonTerminalSymbol load2Mul = new NonTerminalSymbol("L", CompilerSharp.Type.LOAD);
        TerminalSymbol loadTerminal2Mul = new TerminalSymbol("LOAD");
        NonTerminalSymbol num2Mul = new NonTerminalSymbol("N", CompilerSharp.Type.LOAD);
        TerminalSymbol numTerminal2Mul1 = new TerminalSymbol("1");
        TerminalSymbol numTerminal2Mul2 = new TerminalSymbol("2");
        TerminalSymbol load2SemiMul = new TerminalSymbol(";");

        NonTerminalSymbol load = new NonTerminalSymbol("L", CompilerSharp.Type.LOAD);
        TerminalSymbol loadTerminal = new TerminalSymbol("LOAD");
        NonTerminalSymbol numLOAD = new NonTerminalSymbol("N", CompilerSharp.Type.LOAD);
        TerminalSymbol numTerminal1 = new TerminalSymbol("1");
        TerminalSymbol numTerminal2 = new TerminalSymbol("2");
        TerminalSymbol loadSemiLoad = new TerminalSymbol(";");

        NonTerminalSymbol add = new NonTerminalSymbol("ADD", CompilerSharp.Type.ADD);
        TerminalSymbol addTerminal = new TerminalSymbol("+");
        NonTerminalSymbol load1Add = new NonTerminalSymbol("L", CompilerSharp.Type.LOAD);
        TerminalSymbol loadTerminal1Add = new TerminalSymbol("LOAD");
        NonTerminalSymbol num1Add = new NonTerminalSymbol("N", CompilerSharp.Type.LOAD);
        TerminalSymbol numTerminal1Add1 = new TerminalSymbol("1");
        TerminalSymbol numTerminal1Add2 = new TerminalSymbol("2");
        TerminalSymbol load1SemiAdd = new TerminalSymbol(";");
        NonTerminalSymbol load2Add = new NonTerminalSymbol("L", CompilerSharp.Type.LOAD);
        TerminalSymbol loadTerminal2Add = new TerminalSymbol("LOAD");
        NonTerminalSymbol num2Add = new NonTerminalSymbol("N", CompilerSharp.Type.LOAD);
        TerminalSymbol numTerminal2Add1 = new TerminalSymbol("1");
        TerminalSymbol numTerminal2Add2 = new TerminalSymbol("2");
        TerminalSymbol load2SemiAdd = new TerminalSymbol(";");

        [Fact]
        public void CodeToToken_Test()
        {
            List<string> codeToken =  p.codeToToken("this.token = new Token(24);");
            List<string> token = new List<string>() { "this", ".", "token", "=" , "new", "Token", "(", "2", "4", ")", ";" };

            Assert.Equal(token, codeToken);
        }

        [Fact]
        public void CodeToAST_Test1()
        {
            List<string> codeToken = p.codeToToken("load ;");

            NonTerminalSymbol s = new NonTerminalSymbol("s", CompilerSharp.Type.START);
            NonTerminalSymbol ar = new NonTerminalSymbol("ar", CompilerSharp.Type.ADD);
            NonTerminalSymbol n = new NonTerminalSymbol("n", CompilerSharp.Type.MUL);
            NonTerminalSymbol r = new NonTerminalSymbol("r", CompilerSharp.Type.MUL);
            NonTerminalSymbol l = new NonTerminalSymbol("l", CompilerSharp.Type.LOAD);
            NonTerminalSymbol m = new NonTerminalSymbol("m", CompilerSharp.Type.MUL);

            TerminalSymbol load = new TerminalSymbol("load");
            TerminalSymbol semi = new TerminalSymbol(";");
            TerminalSymbol plus = new TerminalSymbol("+");

            m.setDerivationRules(new List<List<ISymbol>>() { new List<ISymbol>() { semi } });
            n.setDerivationRules(new List<List<ISymbol>>() { new List<ISymbol>() { load } });
            r.setDerivationRules(new List<List<ISymbol>>() { new List<ISymbol>() { load, plus } });
            ar.setDerivationRules(new List<List<ISymbol>>() { new List<ISymbol>() { n }, new List<ISymbol>() { r } });
            l.setDerivationRules(new List<List<ISymbol>>() { new List<ISymbol>() { load, semi } });
            s.setDerivationRules(new List<List<ISymbol>>() { new List<ISymbol>() { m }, new List<ISymbol>() { ar }, new List<ISymbol>() { l } });

            ISymbol result = p.codeToAST(s, codeToken);
            Assert.Equal(s.getSymbolName(), result.getSymbolName());
        }

        public void CodeToAST_Test2()
        {
            List<string> codeToken = p.codeToToken("* load 2; load 1;");
            ISymbol startFinal = exampleSymbol();
            initialize();
            ISymbol s = p.codeToAST(startStandart, codeToken);

            Assert.Equal(s.getSymbolName(), startStandart.getSymbolName());

            s = s.getDerivationRules()[0][0];
            Assert.Equal(s.getSymbolName(), mul.getSymbolName());

            var mulT = s.getDerivationRules()[0][0];
            var load1 = s.getDerivationRules()[0][1];
            var load2 = s.getDerivationRules()[0][2];
            Assert.Equal(mulT.getSymbolName(), mulTerminal.getSymbolName());
            Assert.Equal(load1.getSymbolName(), load.getSymbolName());
            Assert.Equal(load2.getSymbolName(), load.getSymbolName());

            var loadT1  = load1.getDerivationRules()[0][0];
            var num1    = load1.getDerivationRules()[0][1];
            var loadS1  = load1.getDerivationRules()[0][2];
            Assert.Equal(loadT1.getSymbolName(), loadTerminal.getSymbolName());
            Assert.Equal(num1.getSymbolName(), num1.getSymbolName());
            Assert.Equal(loadS1.getSymbolName(), load1SemiMul.getSymbolName());

            var num1T = num1.getDerivationRules()[0][0];
            Assert.Equal(num1T.getSymbolName(), numTerminal2Add2.getSymbolName());

            var loadT2 = load2.getDerivationRules()[0][0];
            var num2 = load2.getDerivationRules()[0][1];
            var loadS2 = load2.getDerivationRules()[0][2];
            Assert.Equal(loadT2.getSymbolName(), loadTerminal.getSymbolName());
            Assert.Equal(num2.getSymbolName(), num2.getSymbolName());
            Assert.Equal(loadS2.getSymbolName(), load1SemiMul.getSymbolName());

            var num2T = num2.getDerivationRules()[0][0];
            Assert.Equal(num2T.getSymbolName(), numTerminal2Add1.getSymbolName());
        }

        [Fact]
        public void FindTerminalDerivationsForSymbol_Test()
        {
            ISymbol startFinal = exampleSymbol();
            initialize();

            ISymbol start = p.findTerminalDerivationsForSymbol(startStandart, "*", 0);
            
            Assert.Equal(startFinal.getSymbolName(), start.getSymbolName());        // S == S

            startFinal = (NonTerminalSymbol) startFinal.getDerivationRules()[0][0];
            start = (NonTerminalSymbol)start.getDerivationRules()[0][0];
            Assert.Equal(startFinal.getSymbolName(), start.getSymbolName());        // MUL == MUL

            Assert.Equal(startFinal.getDerivationRules()[0][0].getSymbolName(), start.getDerivationRules()[0][0].getSymbolName());  // * == *
            Assert.Equal(startFinal.getDerivationRules()[0][1].getSymbolName(), start.getDerivationRules()[0][1].getSymbolName());  // L == L
            Assert.Equal(startFinal.getDerivationRules()[0][2].getSymbolName(), start.getDerivationRules()[0][2].getSymbolName());  // L == L

            NonTerminalSymbol firstLFinal = (NonTerminalSymbol) startFinal.getDerivationRules()[0][1];
            NonTerminalSymbol firstLStandard = (NonTerminalSymbol)start.getDerivationRules()[0][1];

            Assert.Equal(firstLFinal.getDerivationRules()[0][0].getSymbolName(), firstLStandard.getDerivationRules()[0][0].getSymbolName());  // LOAD == LOAD
            Assert.Equal(firstLFinal.getDerivationRules()[0][1].getSymbolName(), firstLStandard.getDerivationRules()[0][1].getSymbolName());  // N == N
            Assert.Equal(firstLFinal.getDerivationRules()[0][2].getSymbolName(), firstLStandard.getDerivationRules()[0][2].getSymbolName());  // ; == ;

            NonTerminalSymbol firstNFinal = (NonTerminalSymbol)firstLFinal.getDerivationRules()[0][1];

            Assert.Equal(firstNFinal.getDerivationRules()[0][0].getSymbolName(), numTerminal1Add2.getSymbolName());  // 2 == 2

            NonTerminalSymbol secondLFinal = (NonTerminalSymbol)startFinal.getDerivationRules()[0][2];
            NonTerminalSymbol secondLStandard = (NonTerminalSymbol)start.getDerivationRules()[0][2];

            Assert.Equal(secondLFinal.getDerivationRules()[0][0].getSymbolName(), secondLStandard.getDerivationRules()[0][0].getSymbolName());  // LOAD == LOAD
            Assert.Equal(secondLFinal.getDerivationRules()[0][1].getSymbolName(), secondLStandard.getDerivationRules()[0][1].getSymbolName());  // N == N
            Assert.Equal(secondLFinal.getDerivationRules()[0][2].getSymbolName(), secondLStandard.getDerivationRules()[0][2].getSymbolName());  // ; == ;

            NonTerminalSymbol secondNFinal = (NonTerminalSymbol)secondLFinal.getDerivationRules()[0][1];
            NonTerminalSymbol secondNStandard = (NonTerminalSymbol)secondLStandard.getDerivationRules()[0][1];

            Assert.Equal(secondNFinal.getDerivationRules()[0][0].getSymbolName(), secondNStandard.getDerivationRules()[0][0].getSymbolName());  // 1 == 1

            //////////////////////////////////////////////////////////////////////////////////////////////////////

            NonTerminalSymbol s = new NonTerminalSymbol("s", CompilerSharp.Type.START);
            TerminalSymbol load = new TerminalSymbol("load");
            TerminalSymbol semi = new TerminalSymbol(";");
            s.setDerivationRules(new List<List<ISymbol>>() { new List<ISymbol>() { load }, new List<ISymbol>() { load, semi } });
            start = p.findTerminalDerivationsForSymbol((ISymbol)s.Clone(), ";", 1);

            Assert.Equal(1, start.getDerivationRules().Count);
            Assert.Equal(load.getSymbolName(), start.getDerivationRules()[0][0].getSymbolName());
        }

        [Fact]
        public void PopToken_Test()
        {
            List<string> token = new List<string>() { "this", ".", "token", "=", "new", "Token", "(", "2", "4", ")", ";" };
            (string c, List<string> s) = p.popToken(token);
            Assert.Equal("this", c);
            Assert.Equal(new List<string>() { ".", "token", "=", "new", "Token", "(", "2", "4", ")", ";" }, s);

            token = new List<string>() { "this" };
            (c, s) = p.popToken(token);
            Assert.Equal("this", c);
            Assert.Equal(new List<string>() { }, s);
        }

        [Fact]
        public void AstReady_Test()
        {
            var first = p.astReady(exampleSymbol());
            Assert.True(first);

            initialize();
            var second = p.astReady(startStandart);
            Assert.False(second);
        }

        private void initialize()
        {
            num2Mul.setDerivationRules(new List<List<ISymbol>>() { new List<ISymbol>() { numTerminal2Mul1 }, new List<ISymbol>() { numTerminal2Mul2 } });
            load2Mul.setDerivationRules(new List<List<ISymbol>>() { new List<ISymbol>() { loadTerminal2Mul, num2Mul, load2SemiMul } });
            num1Mul.setDerivationRules(new List<List<ISymbol>>() { new List<ISymbol>() { numTerminal1Mul1 }, new List<ISymbol>() { numTerminal1Mul2 } });
            load1Mul.setDerivationRules(new List<List<ISymbol>>() { new List<ISymbol>() { loadTerminal1Mul, num1Mul, load1SemiMul } });
            mul.setDerivationRules(new List<List<ISymbol>>() { new List<ISymbol>() { mulTerminal, load1Mul, load2Mul } });

            numLOAD.setDerivationRules(new List<List<ISymbol>>() { new List<ISymbol>() { numTerminal1, numTerminal2 } });
            load.setDerivationRules(new List<List<ISymbol>>() { new List<ISymbol>() { loadTerminal, numLOAD, loadSemiLoad } });

            num2Add.setDerivationRules(new List<List<ISymbol>>() { new List<ISymbol>() { numTerminal2Add1 }, new List<ISymbol>() { numTerminal2Add2 } });
            load2Add.setDerivationRules(new List<List<ISymbol>>() { new List<ISymbol>() { loadTerminal2Add, num2Add, load2SemiAdd } });
            num1Add.setDerivationRules(new List<List<ISymbol>>() { new List<ISymbol>() { numTerminal1Add1 }, new List<ISymbol>() { numTerminal1Add2 } });
            load1Add.setDerivationRules(new List<List<ISymbol>>() { new List<ISymbol>() { loadTerminal1Add, num1Add, load1SemiAdd } });
            add.setDerivationRules(new List<List<ISymbol>>() { new List<ISymbol>() { addTerminal, load1Add, load2Add } });

            startStandart.setDerivationRules(new List<List<ISymbol>>() { new List<ISymbol>() { add }, new List<ISymbol>() { load }, new List<ISymbol>() { mul } });
        }

        private ISymbol exampleSymbol()
        {
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

            return startFinal;
        }
    }
}