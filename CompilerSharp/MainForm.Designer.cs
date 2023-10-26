namespace CompilerSharp
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            sourceLanguageTextBox = new RichTextBox();
            compileCompleteButton = new Button();
            targetLanguageTextBox = new RichTextBox();
            stepsTextBox = new TextBox();
            stepButton = new Button();
            errorProvider = new ErrorProvider(components);
            totalStepsLabel = new Label();
            stepsCounterLabel = new Label();
            resetButton = new Button();
            sourceCodeGrammarButton = new Button();
            sourceCodeTreeButton = new Button();
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // 
            // sourceLanguageTextBox
            // 
            sourceLanguageTextBox.Location = new Point(12, 51);
            sourceLanguageTextBox.Name = "sourceLanguageTextBox";
            sourceLanguageTextBox.ScrollBars = RichTextBoxScrollBars.Vertical;
            sourceLanguageTextBox.Size = new Size(444, 425);
            sourceLanguageTextBox.TabIndex = 1;
            sourceLanguageTextBox.Text = "";
            sourceLanguageTextBox.TextChanged += sourceLanguageTextBox_TextChanged;
            // 
            // compileCompleteButton
            // 
            compileCompleteButton.BackColor = Color.GreenYellow;
            compileCompleteButton.FlatAppearance.BorderSize = 2;
            compileCompleteButton.FlatStyle = FlatStyle.Flat;
            compileCompleteButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            compileCompleteButton.Location = new Point(487, 337);
            compileCompleteButton.Name = "compileCompleteButton";
            compileCompleteButton.Size = new Size(121, 40);
            compileCompleteButton.TabIndex = 4;
            compileCompleteButton.Text = "Alles übersetzen";
            compileCompleteButton.UseVisualStyleBackColor = false;
            compileCompleteButton.Click += compileCompleteButton_Click;
            // 
            // targetLanguageTextBox
            // 
            targetLanguageTextBox.Location = new Point(636, 51);
            targetLanguageTextBox.Name = "targetLanguageTextBox";
            targetLanguageTextBox.ReadOnly = true;
            targetLanguageTextBox.ScrollBars = RichTextBoxScrollBars.Vertical;
            targetLanguageTextBox.Size = new Size(444, 425);
            targetLanguageTextBox.TabIndex = 5;
            targetLanguageTextBox.Text = "";
            // 
            // stepsTextBox
            // 
            stepsTextBox.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            stepsTextBox.Location = new Point(469, 247);
            stepsTextBox.Name = "stepsTextBox";
            stepsTextBox.Size = new Size(47, 23);
            stepsTextBox.TabIndex = 6;
            stepsTextBox.Text = "1";
            stepsTextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // stepButton
            // 
            stepButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            stepButton.Location = new Point(528, 247);
            stepButton.Name = "stepButton";
            stepButton.Size = new Size(93, 24);
            stepButton.TabIndex = 7;
            stepButton.Text = "Weiter";
            stepButton.UseVisualStyleBackColor = true;
            stepButton.Click += stepButton_Click;
            // 
            // errorProvider
            // 
            errorProvider.BlinkRate = 0;
            errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errorProvider.ContainerControl = this;
            // 
            // totalStepsLabel
            // 
            totalStepsLabel.AutoSize = true;
            totalStepsLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold | FontStyle.Underline, GraphicsUnit.Point);
            totalStepsLabel.Location = new Point(469, 282);
            totalStepsLabel.Name = "totalStepsLabel";
            totalStepsLabel.Size = new Size(117, 19);
            totalStepsLabel.TabIndex = 8;
            totalStepsLabel.Text = "Schritte gesamt:";
            // 
            // stepsCounterLabel
            // 
            stepsCounterLabel.AutoSize = true;
            stepsCounterLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point);
            stepsCounterLabel.Location = new Point(469, 301);
            stepsCounterLabel.Name = "stepsCounterLabel";
            stepsCounterLabel.Size = new Size(17, 19);
            stepsCounterLabel.TabIndex = 9;
            stepsCounterLabel.Text = "0";
            stepsCounterLabel.TextAlign = ContentAlignment.TopCenter;
            // 
            // resetButton
            // 
            resetButton.BackColor = Color.OrangeRed;
            resetButton.FlatAppearance.BorderSize = 2;
            resetButton.FlatStyle = FlatStyle.Flat;
            resetButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            resetButton.Location = new Point(487, 399);
            resetButton.Name = "resetButton";
            resetButton.Size = new Size(121, 40);
            resetButton.TabIndex = 10;
            resetButton.Text = "Reset";
            resetButton.UseVisualStyleBackColor = false;
            resetButton.Click += resetButton_Click;
            // 
            // sourceCodeGrammarButton
            // 
            sourceCodeGrammarButton.Location = new Point(12, 12);
            sourceCodeGrammarButton.Name = "sourceCodeGrammarButton";
            sourceCodeGrammarButton.Size = new Size(127, 33);
            sourceCodeGrammarButton.TabIndex = 11;
            sourceCodeGrammarButton.Text = "Ausgangsgrammatik";
            sourceCodeGrammarButton.UseVisualStyleBackColor = true;
            sourceCodeGrammarButton.Click += sourceCodeGrammarButton_Click;
            // 
            // sourceCodeTreeButton
            // 
            sourceCodeTreeButton.Location = new Point(168, 12);
            sourceCodeTreeButton.Name = "sourceCodeTreeButton";
            sourceCodeTreeButton.Size = new Size(127, 33);
            sourceCodeTreeButton.TabIndex = 12;
            sourceCodeTreeButton.Text = "Syntaxbaum";
            sourceCodeTreeButton.UseVisualStyleBackColor = true;
            sourceCodeTreeButton.Click += sourceCodeTreeButton_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1092, 484);
            Controls.Add(sourceCodeTreeButton);
            Controls.Add(sourceCodeGrammarButton);
            Controls.Add(resetButton);
            Controls.Add(stepsCounterLabel);
            Controls.Add(totalStepsLabel);
            Controls.Add(stepButton);
            Controls.Add(stepsTextBox);
            Controls.Add(targetLanguageTextBox);
            Controls.Add(compileCompleteButton);
            Controls.Add(sourceLanguageTextBox);
            Name = "MainForm";
            Text = "CompilerSharp";
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private RichTextBox sourceLanguageTextBox;
        private Button compileCompleteButton;
        private RichTextBox targetLanguageTextBox;
        private TextBox stepsTextBox;
        private Button stepButton;
        private ErrorProvider errorProvider;
        private Label totalStepsLabel;
        private Label stepsCounterLabel;
        private Button resetButton;
        private Button sourceCodeGrammarButton;
        private Button sourceCodeTreeButton;
    }
}