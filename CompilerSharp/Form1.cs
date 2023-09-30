using System.Collections.Generic;
using System.Windows.Forms;

namespace CompilerSharp
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();
            sourceLanguageComboBox.SelectedItem = "Pseudo";
            targetLanguageComboBox.SelectedItem = "Pseudo";
        }

        private void compileCompleteButton_Click(object sender, EventArgs e)
        {
            
            try { CodeHandler.setSourceLanguage(CodeHandler.getLanguageFromText(sourceLanguageComboBox.Text.ToString())); errorProvider.SetError(sourceLanguageComboBox, ""); }
            catch (ArgumentException ex) { errorProvider.SetError(sourceLanguageComboBox, ex.Message); return; }
            try { CodeHandler.setTargetLanguage(CodeHandler.getLanguageFromText(targetLanguageComboBox.Text.ToString())); errorProvider.SetError(targetLanguageComboBox, ""); }
            catch (ArgumentException ex) { errorProvider.SetError(targetLanguageComboBox, ex.Message); return; }

            try { CodeHandler.handleSourceCode(sourceLanguageTextBox.Text.ToString()); }
            catch (ArgumentException ex) { errorProvider.SetError(sourceLanguageComboBox, ex.Message); return; }
            catch (UnauthorizedAccessException ex) { errorProvider.SetError(compileCompleteButton, ex.Message); return; }
            catch (IOException ex) { errorProvider.SetError(sourceLanguageComboBox, ex.Message); return; }

            try { targetLanguageTextBox.Text = CodeHandler.evaluateSourceCode(); }
            catch (ArgumentException ex) { errorProvider.SetError(targetLanguageComboBox, ex.Message); return; }
            catch (UnauthorizedAccessException ex) { errorProvider.SetError(compileCompleteButton, ex.Message); return; }

        }

        private void stepButton_Click(object sender, EventArgs e)
        {

        }
    }
}