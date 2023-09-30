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
            sourceLanguageComboBox = new ComboBox();
            sourceLanguageTextBox = new TextBox();
            targetLanguageComboBox = new ComboBox();
            compileCompleteButton = new Button();
            targetLanguageTextBox = new TextBox();
            stepsTextBox = new TextBox();
            stepButton = new Button();
            errorProvider = new ErrorProvider(components);
            ((System.ComponentModel.ISupportInitialize)errorProvider).BeginInit();
            SuspendLayout();
            // 
            // sourceLanguageComboBox
            // 
            sourceLanguageComboBox.FormattingEnabled = true;
            sourceLanguageComboBox.Items.AddRange(new object[] { "Pseudo", "Java" });
            sourceLanguageComboBox.Location = new Point(12, 12);
            sourceLanguageComboBox.Name = "sourceLanguageComboBox";
            sourceLanguageComboBox.Size = new Size(121, 23);
            sourceLanguageComboBox.TabIndex = 0;
            sourceLanguageComboBox.Text = "Ausgangssprache";
            // 
            // sourceLanguageTextBox
            // 
            sourceLanguageTextBox.Location = new Point(12, 51);
            sourceLanguageTextBox.Multiline = true;
            sourceLanguageTextBox.Name = "sourceLanguageTextBox";
            sourceLanguageTextBox.ScrollBars = ScrollBars.Vertical;
            sourceLanguageTextBox.Size = new Size(444, 425);
            sourceLanguageTextBox.TabIndex = 1;
            // 
            // targetLanguageComboBox
            // 
            targetLanguageComboBox.FormattingEnabled = true;
            targetLanguageComboBox.Items.AddRange(new object[] { "Pseudo", "Java" });
            targetLanguageComboBox.Location = new Point(639, 12);
            targetLanguageComboBox.Name = "targetLanguageComboBox";
            targetLanguageComboBox.Size = new Size(121, 23);
            targetLanguageComboBox.TabIndex = 3;
            targetLanguageComboBox.Text = "Zielsprache";
            // 
            // compileCompleteButton
            // 
            compileCompleteButton.BackColor = SystemColors.ActiveCaption;
            compileCompleteButton.FlatAppearance.BorderSize = 0;
            compileCompleteButton.FlatStyle = FlatStyle.Flat;
            compileCompleteButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            compileCompleteButton.Location = new Point(494, 426);
            compileCompleteButton.Name = "compileCompleteButton";
            compileCompleteButton.Size = new Size(110, 40);
            compileCompleteButton.TabIndex = 4;
            compileCompleteButton.Text = "Alles übersetzen";
            compileCompleteButton.UseVisualStyleBackColor = false;
            compileCompleteButton.Click += compileCompleteButton_Click;
            // 
            // targetLanguageTextBox
            // 
            targetLanguageTextBox.Location = new Point(636, 51);
            targetLanguageTextBox.Multiline = true;
            targetLanguageTextBox.Name = "targetLanguageTextBox";
            targetLanguageTextBox.ReadOnly = true;
            targetLanguageTextBox.ScrollBars = ScrollBars.Vertical;
            targetLanguageTextBox.Size = new Size(444, 425);
            targetLanguageTextBox.TabIndex = 5;
            // 
            // stepsTextBox
            // 
            stepsTextBox.Location = new Point(469, 247);
            stepsTextBox.Name = "stepsTextBox";
            stepsTextBox.Size = new Size(47, 23);
            stepsTextBox.TabIndex = 6;
            stepsTextBox.Text = "1";
            stepsTextBox.TextAlign = HorizontalAlignment.Center;
            // 
            // stepButton
            // 
            stepButton.Location = new Point(528, 247);
            stepButton.Name = "stepButton";
            stepButton.Size = new Size(93, 24);
            stepButton.TabIndex = 7;
            stepButton.Text = "Schritt(e)";
            stepButton.UseVisualStyleBackColor = true;
            stepButton.Click += stepButton_Click;
            // 
            // errorProvider
            // 
            errorProvider.BlinkRate = 0;
            errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
            errorProvider.ContainerControl = this;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1092, 484);
            Controls.Add(stepButton);
            Controls.Add(stepsTextBox);
            Controls.Add(targetLanguageTextBox);
            Controls.Add(compileCompleteButton);
            Controls.Add(targetLanguageComboBox);
            Controls.Add(sourceLanguageTextBox);
            Controls.Add(sourceLanguageComboBox);
            Name = "MainForm";
            Text = "CompilerSharp";
            ((System.ComponentModel.ISupportInitialize)errorProvider).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox sourceLanguageComboBox;
        private TextBox sourceLanguageTextBox;
        private ComboBox targetLanguageComboBox;
        private Button compileCompleteButton;
        private TextBox targetLanguageTextBox;
        private TextBox stepsTextBox;
        private Button stepButton;
        private ErrorProvider errorProvider;
    }
}