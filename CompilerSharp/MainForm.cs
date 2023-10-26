using System.Collections.Generic;
using System.Windows.Forms;

namespace CompilerSharp
{
    public partial class MainForm : Form
    {

        GrammarForm grammarForm = new GrammarForm();
        TreeForm sourceCodeTreeForm = new TreeForm();

        public MainForm()
        {
            InitializeComponent();
            compilerFinished();
        }

        private void compileCompleteButton_Click(object sender, EventArgs e)
        {
            CodeHandler.handleSourceCode(sourceLanguageTextBox.Text.ToString());
            targetLanguageTextBox.Text = CodeHandler.evaluateSourceCode();

            this.stepsCounterLabel.Text = "Fertig!";
            compilerFinished();
        }

        private void stepButton_Click(object sender, EventArgs e)
        {
            int steps = 0;
            try { steps = int.Parse(stepsTextBox.Text.ToString()); }
            catch (FormatException ex) { errorProvider.SetError(stepButton, "Keine gueltige Eingabe an Schritten."); return; }


        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            compilerFinished();
            stepsCounterLabel.Text = "0";
            sourceLanguageTextBox.Clear();
            targetLanguageTextBox.Clear();
        }

        private void sourceLanguageTextBox_TextChanged(object sender, EventArgs e)
        {
            if (sourceLanguageTextBox.Text.Length > 0)
            {
                this.stepButton.Enabled = true;
                this.compileCompleteButton.Enabled = true;
                this.resetButton.Enabled = true;
            }
            else
            {
                compilerFinished();
            }
        }

        private void highlightSourceCode(int lineNumber, int range)
        {
            string[] codeLines = sourceLanguageTextBox.Lines;
            sourceLanguageTextBox.Clear();

            for (int i = 0; i < codeLines.Length; i++)
            {
                if (i >= lineNumber && i < lineNumber + range)
                {
                    sourceLanguageTextBox.SelectionColor = Color.Red;
                }
                sourceLanguageTextBox.AppendText(codeLines[i] + "\n");
                sourceLanguageTextBox.SelectionColor = Color.Black;
            }
        }

        private void compilerFinished()
        {
            this.stepButton.Enabled = false;
            this.compileCompleteButton.Enabled = false;
            this.resetButton.Enabled = false;
            this.sourceLanguageTextBox.Focus();
        }

        private void sourceCodeGrammarButton_Click(object sender, EventArgs e)
        {
            grammarForm.ShowDialog();
        }

        private void sourceCodeTreeButton_Click(object sender, EventArgs e)
        {
            sourceCodeTreeForm.ShowDialog();
        }
    }
}