using Newtonsoft.Json.Linq;
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
    public partial class GrammarForm : Form
    {
        private string[] nonTerminals = new string[0];
        private string[] terminals = new string[0];
        private string focusedTableName = "startTabPage";

        public GrammarForm()
        {
            InitializeComponent();
        }


        private void addLeftSideRuleComboBox(string leftBoxSymbol, string rightSideSymbol)
        {
            int row = tableCoordinates[focusedTableName]["newRow"];
            tableCoordinates[focusedTableName]["row"] = row;
            tableCoordinates[focusedTableName]["newRow"]++;
            tableCoordinates[focusedTableName]["column"] = 3;
            leftComboBoxDict[focusedTableName] = newNonTerminalComboBox();
            leftComboBoxDict[focusedTableName].Items.AddRange(new string[] { leftBoxSymbol });
            leftComboBoxDict[focusedTableName].SelectedItem = leftBoxSymbol;
            tableCoordinates[focusedTableName]["rightSideRule"] = 0;
            ComboBox right = newAllSymbolComboBox();
            right.Items.AddRange(new string[] { rightSideSymbol });
            right.SelectedItem = rightSideSymbol;
            Label arrow = newGrammarArrowLabel();
            Button[] buttons = tableButtonDictionary[focusedTableName];
            TableLayoutPanel table = tableDict[focusedTableName];
            grammars[focusedTableName].Add(leftComboBoxDict[focusedTableName], new List<List<ComboBox>>()
            {
                 new List<ComboBox>() { right }
            });

            table.RowCount++;
            table.RowStyles.Add(new RowStyle());
            table.Controls.Add(leftComboBoxDict[focusedTableName], 0, row);
            table.Controls.Add(arrow, 1, row);
            table.Controls.Add(right, 2, row);
            table.Controls.Add(buttons[2], 3, row);
            table.Controls.Add(buttons[1], 2, row + 1);
            table.Controls.Add(buttons[0], 0, tableCoordinates[focusedTableName]["newRow"] + 1);
            buttons[2].Show();
        }

        private void addRightSideRuleComboBox(string rightSideSymbol)
        {
            int row = tableCoordinates[focusedTableName]["newRow"];
            tableCoordinates[focusedTableName]["row"] = row;
            tableCoordinates[focusedTableName]["newRow"]++;
            tableCoordinates[focusedTableName]["column"] = 3;
            ComboBox right = newAllSymbolComboBox();
            right.Items.AddRange(new string[] { rightSideSymbol });
            right.SelectedItem = rightSideSymbol;
            Label arrow = newGrammarArrowLabel();
            Button[] buttons = tableButtonDictionary[focusedTableName];
            TableLayoutPanel table = tableDict[focusedTableName];
            grammars[focusedTableName][leftComboBoxDict[focusedTableName]].Add(new List<ComboBox>() { right } );

            tableCoordinates[focusedTableName]["rightSideRule"]++;
            table.RowCount++;
            table.RowStyles.Add(new RowStyle());
            table.Controls.Add(arrow, 1, row);
            table.Controls.Add(right, 2, row);
            table.Controls.Add(buttons[2], 3, row);
            table.Controls.Add(buttons[1], 2, row + 1);
            table.Controls.Add(buttons[0], 0, tableCoordinates[focusedTableName]["newRow"] + 1);
            buttons[2].Show();
        }

        private void expandRightSideRuleComboBox(string rightSideSymbol)
        {
            ComboBox allSymbols = newAllSymbolComboBox();
            allSymbols.Items.AddRange(new string[] { rightSideSymbol });
            allSymbols.SelectedItem = rightSideSymbol;
            Button[] buttons = tableButtonDictionary[focusedTableName];
            TableLayoutPanel table = tableDict[focusedTableName];
            grammars[focusedTableName][leftComboBoxDict[focusedTableName]][tableCoordinates[focusedTableName]["rightSideRule"]].Add(allSymbols);

            table.Controls.Add(allSymbols, tableCoordinates[focusedTableName]["column"], tableCoordinates[focusedTableName]["row"]);
            tableCoordinates[focusedTableName]["column"]++;
            table.Controls.Add(buttons[2], tableCoordinates[focusedTableName]["column"], tableCoordinates[focusedTableName]["row"]);
            if (tableCoordinates[focusedTableName]["column"] == 20) { buttons[2].Hide(); }
        }

        private void tabSelection_Click(object sender, EventArgs e)
        {
            tableDict[focusedTableName].Hide();
            tableDict[focusedTableName].SendToBack();
            focusedTableName = grammarTabControl.SelectedTab.Name;
            tableDict[focusedTableName].Show();
            tableDict[focusedTableName].BringToFront();
        }

        private void allSymbolComboBox_Click(object sender, EventArgs e)
        {
            ((ComboBox)sender).Items.Clear();
            ((ComboBox)sender).Items.AddRange(nonTerminals.Concat(terminals).ToArray());
        }

        private void nonTerminalComboBox_Click(object sender, EventArgs e)
        {
            ((ComboBox)sender).Items.Clear();
            ((ComboBox)sender).Items.AddRange(nonTerminals.ToArray());
        }

        // TODO: catch
        private void saveGrammarButton_Click(object sender, EventArgs e)
        {
            try { FileHandler.writeText(nonTerminalsTextBox.Text.ToString(), "NonTerminals.txt"); }
            catch { }
            try { FileHandler.writeText(terminalsTextBox.Text.ToString(), "Terminals.txt"); }
            catch { }
            FileHandler.writeGrammar(comboBoxesToString(), "Grammar.txt");
        }

        private void loadGrammarButton_Click(object sender, EventArgs e)
        {
            try { nonTerminalsTextBox.Text = FileHandler.readText("NonTerminals.txt"); }
            catch { }
            try { terminalsTextBox.Text = FileHandler.readText("Terminals.txt"); }
            catch { }
            updateTerminals();
            updateNonTerminals();
            string focusedTable = focusedTableName;
            try
            {
                reset();
                restoreGrammar(FileHandler.readGrammar("Grammar.txt"));
            }
            catch { }
            focusedTableName = focusedTable;
        }

        private void restoreGrammar(Dictionary<string, Dictionary<string, List<List<string>>>> grammarDict)
        {
            for (int i = 0; i < grammarDict.Count; i++)     // Tables
            {
                focusedTableName = grammarDict.Keys.ToArray()[i];
                for (int j = 0; j < grammarDict.Values.ToArray()[i].Count; j++)  // Left sides
                {
                    addLeftSideRuleComboBox(grammarDict.Values.ToArray()[i].Keys.ToArray()[j], grammarDict.Values.ToArray()[i].Values.ToArray()[j][0][0]);
                    for (int n = 1; n < grammarDict.Values.ToArray()[i].Values.ToArray()[j][0].Count; n++) // Expand first line
                    {
                        expandRightSideRuleComboBox(grammarDict.Values.ToArray()[i].Values.ToArray()[j][0][n]);
                    }

                    for (int m = 1; m < grammarDict.Values.ToArray()[i].Values.ToArray()[j].Count; m++) // Add right side
                    {
                        addRightSideRuleComboBox(grammarDict.Values.ToArray()[i].Values.ToArray()[j][m][0]);
                        for (int n = 1; n < grammarDict.Values.ToArray()[i].Values.ToArray()[j].ToArray()[m].Count; n++) // Expand right side
                        {
                            expandRightSideRuleComboBox(grammarDict.Values.ToArray()[i].Values.ToArray()[j][m][n]);
                        }
                    }
                }
            }
        }

        private Dictionary<string, Dictionary<string, List<List<string>>>> comboBoxesToString()
        {
            var comboBoxLabel = new Dictionary<string, Dictionary<string, List<List<string>>>>();

            for (int i = 0; i < grammars.Keys.Count; i++)
            {
                comboBoxLabel.Add(grammars.Keys.ToArray()[i], new Dictionary<string, List<List<string>>>());
                var comboBoxDict = grammars.Values.ToArray()[i];
                for (int j = 0;  j < comboBoxDict.Values.Count; j++)
                {
                    string leftRule = comboBoxDict.Keys.ToArray()[j].Text;
                    List<List<string>> rules = new List<List<string>>();
                    for (int n = 0; n < comboBoxDict.Values.ToArray()[j].Count; n++)
                    {
                        rules.Add(new List<string>());
                        for (int m = 0; m < comboBoxDict.Values.ToArray()[j][n].Count; m++)
                        {
                            rules[n].Add(comboBoxDict.Values.ToArray()[j][n][m].Text);
                        }
                    }
                    comboBoxLabel[grammars.Keys.ToArray()[i]].Add(leftRule, rules);
                }
            }
            return comboBoxLabel;
        }

        private (ISymbol, Dictionary<string, NonTerminalSymbol>) comboBoxesToSymbol(string symbol, Dictionary<string, Dictionary<string, List<List<string>>>> comboBoxDict, Dictionary<string, string> symbolTableDict, Dictionary<string, NonTerminalSymbol> knownNonTerminals)
        {
            if (symbolTableDict.ContainsKey(symbol))
            {
                if (knownNonTerminals.ContainsKey(symbol))
                {
                    NonTerminalSymbol t = knownNonTerminals[symbol];
                    return (t, knownNonTerminals);
                }

                else
                {
                    Type type = tabToType(symbolTableDict[symbol]);
                    List<List<ISymbol>> rules = new List<List<ISymbol>>();
                    int symbolIndex = 0;
                    foreach (List<string> leftSideDict in comboBoxDict[symbolTableDict[symbol]][symbol])
                    {
                        rules.Add(new List<ISymbol>());
                        foreach (string rightSymbol in leftSideDict)
                        {
                            (ISymbol newSymbol, knownNonTerminals) = comboBoxesToSymbol(rightSymbol, comboBoxDict, symbolTableDict, knownNonTerminals);
                            rules[symbolIndex].Add(newSymbol);
                        }
                        symbolIndex++;
                    }
                    NonTerminalSymbol newNonTerminal = new NonTerminalSymbol(symbol, rules, type);
                    knownNonTerminals.Add(symbol, newNonTerminal);
                    return (newNonTerminal, knownNonTerminals);
                }
            }
            else
                return (new TerminalSymbol(symbol), knownNonTerminals);
        }

        private Dictionary<string, string> assignNonTerminalsToTable(Dictionary<string, Dictionary<string, List<List<string>>>> comboBoxDict)
        {
            Dictionary<string, string> symbolTableDict = new Dictionary<string, string>();
            foreach (var table in comboBoxDict)
                foreach (var leftSideDict in table.Value)
                    if (!symbolTableDict.ContainsKey(leftSideDict.Key)) symbolTableDict.Add(leftSideDict.Key, table.Key);
            return symbolTableDict;
        }

        private void reset()
        {
            grammars.Clear();
            leftComboBoxDict.Clear();
            tableCoordinates.Clear();
            tableButtonDictionary.Clear();
            foreach (string key in tableDict.Keys)
            {
                Controls.Remove(tableDict[key]);
                tableDict[key] = newTableLayoutPanel(key == focusedTableName, key);
            }
            grammarTabControl.SendToBack();
        }

        private Type tabToType(string tab)
        {
            switch (tab)
            {
                case "startTabPage": return Type.START;
                case "additionTabPage": return Type.ADD;
                case "multiplicationTabPage": return Type.MUL;
                case "loadTabPage": return Type.LOAD;
                default: return Type.NONE;
            }
        }

        private Dictionary<ISymbol, List<ISymbol>> buildBottomUp(Dictionary<string, string> symbolTableDict, Dictionary<string, Dictionary<string, List<List<string>>>> comboBoxDict)
        {
            Dictionary<ISymbol, List<ISymbol>> bottomUpSymbol = new Dictionary<ISymbol, List<ISymbol>>();
            foreach (string sym in terminals.Concat(nonTerminals))
            {
                ISymbol newSymbol;
                List<ISymbol> symbolList = new List<ISymbol>();
                
                if (terminals.Contains(sym)) 
                    newSymbol = new TerminalSymbol(sym);
                else 
                    newSymbol = new NonTerminalSymbol(sym, tabToType(symbolTableDict[sym]));
                
                foreach (var tabDict in comboBoxDict)                    // Tables
                    foreach (var nonTermDict in tabDict.Value)           // Left side rules
                        foreach (var rightSide in nonTermDict.Value)    // Right side rules
                            if (rightSide.Contains(sym))                // Right side has symbol
                                symbolList.Add(new NonTerminalSymbol(nonTermDict.Key, tabToType(symbolTableDict[nonTermDict.Key]))); // Add left side symbol to this symbol list
                
                bottomUpSymbol.Add(newSymbol, symbolList);
            }

            return bottomUpSymbol;
        }

        private void addLeftSideRuleButton_Click(object sender, EventArgs e) { addLeftSideRuleComboBox("", ""); }

        private void addRightSideRuleButton_Click(object sender, EventArgs e) { addRightSideRuleComboBox(""); }

        private void expandRghtSideRuleButton_Click(object sender, EventArgs e) { expandRightSideRuleComboBox(""); }

        private void nonTerminalsTextBox_TextChanged(object sender, EventArgs e) { updateNonTerminals(); }

        private void terminalTextBox_TextChanged(object sender, EventArgs e) { updateTerminals(); }

        private void updateNonTerminals() { this.nonTerminals = nonTerminalsTextBox.Text.Replace(" ", "").Split("/"); }

        private void updateTerminals() { this.terminals = terminalsTextBox.Text.Replace(" ", "").Split("/"); }

        private void finishedButton_Click(object sender, EventArgs e) {
            Dictionary<string, Dictionary<string, List<List<string>>>> comboBoxDict = comboBoxesToString();
            Dictionary<string, string> symbolTableDict = assignNonTerminalsToTable(comboBoxDict);
            Dictionary<ISymbol, List<ISymbol>> bottomUpDict = buildBottomUp(symbolTableDict, comboBoxDict);
            (ISymbol symbol, _) = comboBoxesToSymbol(symbolTableDict.First().Key, comboBoxDict, symbolTableDict, new Dictionary<string, NonTerminalSymbol>());
            CodeHandler.setSourceGrammar((NonTerminalSymbol) symbol, bottomUpDict); 
            this.Close(); 
        }

        private void resetButton_Click(object sender, EventArgs e) { reset(); }
    }
}
