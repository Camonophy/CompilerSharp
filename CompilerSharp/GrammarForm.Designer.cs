namespace CompilerSharp
{
    partial class GrammarForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            grammarHeadlineLabel = new Label();
            nonTerminalLabel = new Label();
            nonTerminalsTextBox = new TextBox();
            terminalsTextBox = new TextBox();
            terminalLabel = new Label();
            derivationLabel = new Label();
            bindingSource1 = new BindingSource(components);
            leftComboBoxDict = new Dictionary<string, ComboBox>();
            tableButtonDictionary = new Dictionary<string, Button[]>();
            tableCoordinates = new Dictionary<string, Dictionary<string, int>>();
            grammars = new Dictionary<string, Dictionary<ComboBox, List<List<ComboBox>>>>();
            tableDict = new Dictionary<string, TableLayoutPanel>();
            tableDict.Add("startTabPage", newTableLayoutPanel(true, "startTabPage"));
            tableDict.Add("additionTabPage", newTableLayoutPanel(true, "additionTabPage"));
            tableDict.Add("multiplicationTabPage", newTableLayoutPanel(true, "multiplicationTabPage"));
            tableDict.Add("loadTabPage", newTableLayoutPanel(true, "loadTabPage"));
            saveGrammarButton = new Button();
            loadGrammarButton = new Button();
            resetButton = new Button();
            grammarTabControl = new TabControl();
            startTabPage = new TabPage();
            additionTabPage = new TabPage();
            multiplicationTabPage = new TabPage();
            loadTabPage = new TabPage();
            finishedButton = new Button();
            ((System.ComponentModel.ISupportInitialize)bindingSource1).BeginInit();
            grammarTabControl.SuspendLayout();
            SuspendLayout();
            // 
            // grammarHeadlineLabel
            // 
            grammarHeadlineLabel.AutoSize = true;
            grammarHeadlineLabel.Font = new Font("Segoe UI", 16F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point);
            grammarHeadlineLabel.Location = new Point(12, 9);
            grammarHeadlineLabel.Name = "grammarHeadlineLabel";
            grammarHeadlineLabel.Size = new Size(479, 30);
            grammarHeadlineLabel.TabIndex = 0;
            grammarHeadlineLabel.Text = "Grammatik für die Ausgangssprache: Pseudo";
            // 
            // nonTerminalLabel
            // 
            nonTerminalLabel.AutoSize = true;
            nonTerminalLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            nonTerminalLabel.Location = new Point(12, 50);
            nonTerminalLabel.Name = "nonTerminalLabel";
            nonTerminalLabel.Size = new Size(186, 21);
            nonTerminalLabel.TabIndex = 1;
            nonTerminalLabel.Text = "Nichtterminalsymbole:";
            // 
            // nonTerminalsTextBox
            // 
            nonTerminalsTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            nonTerminalsTextBox.Location = new Point(12, 74);
            nonTerminalsTextBox.Name = "nonTerminalsTextBox";
            nonTerminalsTextBox.Size = new Size(878, 29);
            nonTerminalsTextBox.TabIndex = 2;
            nonTerminalsTextBox.TextChanged += nonTerminalsTextBox_TextChanged;
            // 
            // terminalsTextBox
            // 
            terminalsTextBox.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            terminalsTextBox.Location = new Point(12, 139);
            terminalsTextBox.Name = "terminalsTextBox";
            terminalsTextBox.Size = new Size(878, 29);
            terminalsTextBox.TabIndex = 4;
            terminalsTextBox.TextChanged += terminalTextBox_TextChanged;
            // 
            // terminalLabel
            // 
            terminalLabel.AutoSize = true;
            terminalLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            terminalLabel.Location = new Point(12, 115);
            terminalLabel.Name = "terminalLabel";
            terminalLabel.Size = new Size(146, 21);
            terminalLabel.TabIndex = 3;
            terminalLabel.Text = "Terminalsymbole:";
            // 
            // derivationLabel
            // 
            derivationLabel.AutoSize = true;
            derivationLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            derivationLabel.Location = new Point(12, 181);
            derivationLabel.Name = "derivationLabel";
            derivationLabel.Size = new Size(146, 21);
            derivationLabel.TabIndex = 5;
            derivationLabel.Text = "Ableitungsregeln:";
            // 
            // saveGrammarButton
            // 
            saveGrammarButton.FlatStyle = FlatStyle.Flat;
            saveGrammarButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            saveGrammarButton.Location = new Point(519, 15);
            saveGrammarButton.Name = "saveGrammarButton";
            saveGrammarButton.Size = new Size(82, 42);
            saveGrammarButton.TabIndex = 8;
            saveGrammarButton.Text = "Speichern";
            saveGrammarButton.UseVisualStyleBackColor = true;
            saveGrammarButton.Click += saveGrammarButton_Click;
            // 
            // loadGrammarButton
            // 
            loadGrammarButton.FlatStyle = FlatStyle.Flat;
            loadGrammarButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            loadGrammarButton.Location = new Point(616, 15);
            loadGrammarButton.Name = "loadGrammarButton";
            loadGrammarButton.Size = new Size(82, 42);
            loadGrammarButton.TabIndex = 9;
            loadGrammarButton.Text = "Laden";
            loadGrammarButton.UseVisualStyleBackColor = true;
            loadGrammarButton.Click += loadGrammarButton_Click;
            // 
            // grammarTabControl
            // 
            grammarTabControl.Controls.Add(startTabPage);
            grammarTabControl.Controls.Add(additionTabPage);
            grammarTabControl.Controls.Add(multiplicationTabPage);
            grammarTabControl.Controls.Add(loadTabPage);
            grammarTabControl.Location = new Point(188, 181);
            grammarTabControl.Name = "grammarTabControl";
            grammarTabControl.SelectedIndex = 0;
            grammarTabControl.Size = new Size(691, 95);
            grammarTabControl.TabIndex = 10;
            grammarTabControl.SelectedIndexChanged += tabSelection_Click;
            // 
            // startTabPage
            // 
            startTabPage.Location = new Point(4, 24);
            startTabPage.Name = "startTabPage";
            startTabPage.Padding = new Padding(3);
            startTabPage.Size = new Size(683, 67);
            startTabPage.TabIndex = 0;
            startTabPage.Text = "Start";
            startTabPage.UseVisualStyleBackColor = true;
            // 
            // additionTabPage
            // 
            additionTabPage.Location = new Point(4, 24);
            additionTabPage.Name = "additionTabPage";
            additionTabPage.Padding = new Padding(3);
            additionTabPage.Size = new Size(683, 67);
            additionTabPage.TabIndex = 1;
            additionTabPage.Text = "Add";
            additionTabPage.UseVisualStyleBackColor = true;
            // 
            // multiplicationTabPage
            // 
            multiplicationTabPage.Location = new Point(4, 24);
            multiplicationTabPage.Name = "multiplicationTabPage";
            multiplicationTabPage.Padding = new Padding(3);
            multiplicationTabPage.Size = new Size(683, 67);
            multiplicationTabPage.TabIndex = 2;
            multiplicationTabPage.Text = "Mul";
            multiplicationTabPage.UseVisualStyleBackColor = true;
            // 
            // loadTabPage
            // 
            loadTabPage.Location = new Point(4, 24);
            loadTabPage.Name = "loadTabPage";
            loadTabPage.Padding = new Padding(3);
            loadTabPage.Size = new Size(683, 67);
            loadTabPage.TabIndex = 3;
            loadTabPage.Text = "Load";
            loadTabPage.UseVisualStyleBackColor = true;
            // 
            // finishedButton
            // 
            finishedButton.FlatStyle = FlatStyle.Flat;
            finishedButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            finishedButton.Location = new Point(713, 15);
            finishedButton.Name = "finishedButton";
            finishedButton.Size = new Size(82, 42);
            finishedButton.TabIndex = 11;
            finishedButton.Text = "Fertig";
            finishedButton.UseVisualStyleBackColor = true;
            finishedButton.Click += finishedButton_Click;
            // 
            // resetButton
            // 
            resetButton.FlatStyle = FlatStyle.Flat;
            resetButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            resetButton.Location = new Point(808, 15);
            resetButton.Name = "resetButton";
            resetButton.Size = new Size(82, 42);
            resetButton.TabIndex = 12;
            resetButton.Text = "Reset";
            resetButton.UseVisualStyleBackColor = true;
            resetButton.Click += resetButton_Click;
            // 
            // GrammarForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(902, 777);
            Controls.Add(resetButton);
            Controls.Add(finishedButton);
            Controls.Add(grammarTabControl);
            Controls.Add(loadGrammarButton);
            Controls.Add(saveGrammarButton);
            Controls.Add(derivationLabel);
            Controls.Add(terminalsTextBox);
            Controls.Add(terminalLabel);
            Controls.Add(nonTerminalsTextBox);
            Controls.Add(nonTerminalLabel);
            Controls.Add(grammarHeadlineLabel);
            Name = "GrammarForm";
            Text = "Grammatik";
            ((System.ComponentModel.ISupportInitialize)bindingSource1).EndInit();
            grammarTabControl.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox newNonTerminalComboBox()
        {
            ComboBox nonTerminalComboBox = new ComboBox();
            nonTerminalComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            nonTerminalComboBox.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point);
            nonTerminalComboBox.FormattingEnabled = true;
            nonTerminalComboBox.Location = new Point(159, 3);
            nonTerminalComboBox.Size = new Size(91, 38);
            nonTerminalComboBox.Click += nonTerminalComboBox_Click;

            return nonTerminalComboBox;
        }

        private ComboBox newAllSymbolComboBox()
        {
            ComboBox allSymbolComboBox = new ComboBox();
            allSymbolComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            allSymbolComboBox.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point);
            allSymbolComboBox.FormattingEnabled = true;
            allSymbolComboBox.Location = new Point(61, 3);
            allSymbolComboBox.Name = "allSymbolComboBox";
            allSymbolComboBox.Size = new Size(92, 38);
            allSymbolComboBox.TabIndex = 2;
            allSymbolComboBox.Click += allSymbolComboBox_Click;

            return allSymbolComboBox;
        }

        private Label newGrammarArrowLabel()
        {
            Label grammarArrowLabel = new Label();
            grammarArrowLabel.AutoSize = true;
            grammarArrowLabel.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point);
            grammarArrowLabel.Location = new Point(101, 0);
            grammarArrowLabel.Size = new Size(38, 30);
            grammarArrowLabel.Text = "->";

            return grammarArrowLabel;
        }

        private TableLayoutPanel newTableLayoutPanel(bool focus, String name)
        {
            TableLayoutPanel pan = new TableLayoutPanel();

            grammars.Add(name, new Dictionary<ComboBox, List<List<ComboBox>>>());
            leftComboBoxDict.Add(name, new ComboBox());
            Button[] buttons = new Button[3] { newAddLeftSideRuleButton(), newAddRightSideRuleButton(), newExpandRightSideRuleButton() };
            tableButtonDictionary.Add(name, buttons);
            tableCoordinates.Add(name, new Dictionary<string, int> { ["rightSideRule"] = 0, ["rules"] = 0, ["row"] = 0, ["newRow"] = 0, ["column"] = 0 });
            pan.AutoScroll = true;
            pan.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            pan.ColumnCount = 20;
            for (int i = 0; i < pan.ColumnCount; i++) { pan.ColumnStyles.Add(new ColumnStyle()); }
            pan.Controls.Add(buttons[0], 19, 0);
            pan.Location = new Point(12, 205);
            pan.MaximumSize = new Size(878, 616);
            pan.Name = name;
            pan.RowCount = 2;
            pan.RowStyles.Add(new RowStyle());
            pan.RowStyles.Add(new RowStyle());
            pan.Size = new Size(878, 550);

            if (focus) { pan.Show(); pan.BringToFront(); }
            else { pan.Hide(); pan.SendToBack(); }

            Controls.Add(pan);

            return pan;
        }

        private Button newAddLeftSideRuleButton()
        {
            Button button = new Button();

            button.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            button.Location = new Point(3, 3);
            button.Size = new Size(52, 40);
            button.Text = "+";
            button.UseVisualStyleBackColor = true;
            button.Click += addLeftSideRuleButton_Click;

            return button;
        }

        private Button newAddRightSideRuleButton()
        {
            Button button = new Button();

            button.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            button.Location = new Point(61, 3);
            button.Size = new Size(52, 40);
            button.Text = "+";
            button.UseVisualStyleBackColor = true;
            button.Click += addRightSideRuleButton_Click;

            return button;
        }

        private Button newExpandRightSideRuleButton()
        {
            Button button = new Button();

            button.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            button.Location = new Point(119, 3);
            button.Size = new Size(52, 40);
            button.Text = "+";
            button.UseVisualStyleBackColor = true;
            button.Click += expandRghtSideRuleButton_Click;

            return button;
        }

        private int tabIndex = 10;
        private Label grammarHeadlineLabel;
        private Label nonTerminalLabel;
        private Label terminalLabel;
        private Label derivationLabel;
        private Button saveGrammarButton;
        private Button loadGrammarButton;
        private Button finishedButton;
        private Button resetButton;
        private TextBox nonTerminalsTextBox;
        private TextBox terminalsTextBox;
        private BindingSource bindingSource1;
        private TabControl grammarTabControl;
        private TabPage additionTabPage;
        private TabPage multiplicationTabPage;
        private TabPage loadTabPage;
        private TabPage startTabPage;
        private Dictionary<string, Button[]> tableButtonDictionary;
        private Dictionary<string, ComboBox> leftComboBoxDict;
        private Dictionary<string, Dictionary<string, int>> tableCoordinates;
        private Dictionary<string, Dictionary<ComboBox, List<List<ComboBox>>>> grammars;
        private Dictionary<string, TableLayoutPanel> tableDict;
    }
}