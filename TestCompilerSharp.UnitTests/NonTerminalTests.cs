using CompilerSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestCompilerSharp.UnitTests
{
    public class NonTerminalTests
    {
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
        public void GetTerminalSymbolDict_Test()
        {
            NonTerminalSymbol example = exampleSymbol();
            Dictionary<ISymbol, List<TerminalSymbol>> symDict = example.getTerminalSymbolDict(0);
            Assert.Equal(3, symDict.Count);

            var mul = symDict.Values.ToList()[0];
            Assert.Equal(2, mul.Count);
            Assert.Equal(mulTerminal.getSymbolName(), mul[0].getSymbolName());
            Assert.Equal(loadTerminal.getSymbolName(), mul[1].getSymbolName());

            var num = symDict.Values.ToList()[1];
            Assert.Equal(2, num.Count);
            Assert.Equal(numTerminal1Add2.getSymbolName(), num[0].getSymbolName());
            Assert.Equal(numTerminal1Add1.getSymbolName(), num[1].getSymbolName());

            var semi = symDict.Values.ToList()[2];
            Assert.Equal(1, semi.Count);
            Assert.Equal(load1SemiAdd.getSymbolName(), semi[0].getSymbolName());
        }

        [Fact]
        public void Clone_Test()
        {
            NonTerminalSymbol example = exampleSymbol();
            Assert.Equal(CompilerSharp.Type.START, example.getType());

            NonTerminalSymbol same = example;
            same.setType(CompilerSharp.Type.NONE);
            Assert.Equal(same.getType(), example.getType());

            example = exampleSymbol();
            example.setType(CompilerSharp.Type.ADD);
            NonTerminalSymbol different = (NonTerminalSymbol)example.Clone();
            Assert.Equal(CompilerSharp.Type.ADD, different.getType());

            different.setType(CompilerSharp.Type.MUL);
            Assert.Equal(CompilerSharp.Type.MUL, different.getType());
            Assert.Equal(CompilerSharp.Type.ADD, example.getType());
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

        private NonTerminalSymbol exampleSymbol()
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

            num2Final.setDerivationRules(new List<List<ISymbol>>() { new List<ISymbol>() { numTerminal1Final }, new List<ISymbol>() { numTerminal2Final } });
            load2Final.setDerivationRules(new List<List<ISymbol>>() { new List<ISymbol>() { loadTerminal2Final, num2Final, load2SemiFinal } });
            num1Final.setDerivationRules(new List<List<ISymbol>>() { new List<ISymbol>() { numTerminal1Final } });
            load1Final.setDerivationRules(new List<List<ISymbol>>() { new List<ISymbol>() { loadTerminal1Final, num1Final, load1SemiFinal } });
            mulFinal.setDerivationRules(new List<List<ISymbol>>() { new List<ISymbol>() { mulTerminalFinal, load1Final }, new List<ISymbol>() { load2Final } });
            startFinal.setDerivationRules(new List<List<ISymbol>>() { new List<ISymbol>() { mulFinal }, new List<ISymbol>() { num2Final, load1Final }, new List<ISymbol>() { load1SemiFinal } });

            return startFinal;
        }
    }
}
