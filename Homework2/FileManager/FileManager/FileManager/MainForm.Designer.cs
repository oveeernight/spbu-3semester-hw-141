namespace FileManager;

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
            this.listBoxLeft = new System.Windows.Forms.ListBox();
            this.listBoxRight = new System.Windows.Forms.ListBox();
            this.buttonView = new System.Windows.Forms.Button();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.buttonMove = new System.Windows.Forms.Button();
            this.buttonCopy = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonArchiving = new System.Windows.Forms.Button();
            this.labelLeft = new System.Windows.Forms.Label();
            this.labelRight = new System.Windows.Forms.Label();
            this.buttonLeftBack = new System.Windows.Forms.Button();
            this.buttonRightBack = new System.Windows.Forms.Button();
            this.comboBoxFont = new System.Windows.Forms.ComboBox();
            this.comboBoxColor = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.buttonDownload = new System.Windows.Forms.Button();
            this.buttonAnalyze = new System.Windows.Forms.Button();
            this.textBoxAnalyze = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // listBoxLeft
            // 
            this.listBoxLeft.FormattingEnabled = true;
            this.listBoxLeft.ItemHeight = 20;
            this.listBoxLeft.Location = new System.Drawing.Point(12, 116);
            this.listBoxLeft.Name = "listBoxLeft";
            this.listBoxLeft.Size = new System.Drawing.Size(611, 464);
            this.listBoxLeft.TabIndex = 0;
            // 
            // listBoxRight
            // 
            this.listBoxRight.FormattingEnabled = true;
            this.listBoxRight.ItemHeight = 20;
            this.listBoxRight.Location = new System.Drawing.Point(646, 116);
            this.listBoxRight.Name = "listBoxRight";
            this.listBoxRight.Size = new System.Drawing.Size(634, 464);
            this.listBoxRight.TabIndex = 1;
            // 
            // buttonView
            // 
            this.buttonView.Location = new System.Drawing.Point(37, 595);
            this.buttonView.Name = "buttonView";
            this.buttonView.Size = new System.Drawing.Size(159, 52);
            this.buttonView.TabIndex = 2;
            this.buttonView.Text = "F4 View";
            this.buttonView.UseVisualStyleBackColor = true;
            // 
            // buttonEdit
            // 
            this.buttonEdit.Location = new System.Drawing.Point(218, 595);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(141, 52);
            this.buttonEdit.TabIndex = 3;
            this.buttonEdit.Text = "F5 Edit";
            this.buttonEdit.UseVisualStyleBackColor = true;
            // 
            // buttonMove
            // 
            this.buttonMove.Location = new System.Drawing.Point(382, 595);
            this.buttonMove.Name = "buttonMove";
            this.buttonMove.Size = new System.Drawing.Size(144, 52);
            this.buttonMove.TabIndex = 4;
            this.buttonMove.Text = "F6 Move";
            this.buttonMove.UseVisualStyleBackColor = true;
            // 
            // buttonCopy
            // 
            this.buttonCopy.Location = new System.Drawing.Point(549, 595);
            this.buttonCopy.Name = "buttonCopy";
            this.buttonCopy.Size = new System.Drawing.Size(150, 52);
            this.buttonCopy.TabIndex = 5;
            this.buttonCopy.Text = "F7 Copy";
            this.buttonCopy.UseVisualStyleBackColor = true;
            // 
            // buttonDelete
            // 
            this.buttonDelete.Location = new System.Drawing.Point(723, 595);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(147, 52);
            this.buttonDelete.TabIndex = 6;
            this.buttonDelete.Text = "F8 Delete";
            this.buttonDelete.UseVisualStyleBackColor = true;
            // 
            // buttonArchiving
            // 
            this.buttonArchiving.Location = new System.Drawing.Point(904, 595);
            this.buttonArchiving.Name = "buttonArchiving";
            this.buttonArchiving.Size = new System.Drawing.Size(151, 52);
            this.buttonArchiving.TabIndex = 8;
            this.buttonArchiving.Text = "F10 Archiving";
            this.buttonArchiving.UseVisualStyleBackColor = true;
            // 
            // labelLeft
            // 
            this.labelLeft.AutoSize = true;
            this.labelLeft.Location = new System.Drawing.Point(71, 90);
            this.labelLeft.Name = "labelLeft";
            this.labelLeft.Size = new System.Drawing.Size(50, 20);
            this.labelLeft.TabIndex = 9;
            this.labelLeft.Text = "label1";
            // 
            // labelRight
            // 
            this.labelRight.AutoSize = true;
            this.labelRight.Location = new System.Drawing.Point(705, 90);
            this.labelRight.Name = "labelRight";
            this.labelRight.Size = new System.Drawing.Size(50, 20);
            this.labelRight.TabIndex = 10;
            this.labelRight.Text = "label2";
            // 
            // buttonLeftBack
            // 
            this.buttonLeftBack.Location = new System.Drawing.Point(12, 81);
            this.buttonLeftBack.Name = "buttonLeftBack";
            this.buttonLeftBack.Size = new System.Drawing.Size(53, 29);
            this.buttonLeftBack.TabIndex = 11;
            this.buttonLeftBack.Text = "<<";
            this.buttonLeftBack.UseVisualStyleBackColor = true;
            this.buttonLeftBack.Click += new System.EventHandler(this.buttonLeftBack_Click);
            // 
            // buttonRightBack
            // 
            this.buttonRightBack.Location = new System.Drawing.Point(646, 81);
            this.buttonRightBack.Name = "buttonRightBack";
            this.buttonRightBack.Size = new System.Drawing.Size(53, 29);
            this.buttonRightBack.TabIndex = 12;
            this.buttonRightBack.Text = "<<";
            this.buttonRightBack.UseVisualStyleBackColor = true;
            this.buttonRightBack.Click += new System.EventHandler(this.buttonRightBack_Click);
            // 
            // comboBoxFont
            // 
            this.comboBoxFont.FormattingEnabled = true;
            this.comboBoxFont.Location = new System.Drawing.Point(77, 34);
            this.comboBoxFont.Name = "comboBoxFont";
            this.comboBoxFont.Size = new System.Drawing.Size(151, 28);
            this.comboBoxFont.TabIndex = 13;
            this.comboBoxFont.SelectedIndexChanged += new System.EventHandler(this.comboBoxFont_SelectedIndexChanged);
            // 
            // comboBoxColor
            // 
            this.comboBoxColor.FormattingEnabled = true;
            this.comboBoxColor.Location = new System.Drawing.Point(1129, 28);
            this.comboBoxColor.Name = "comboBoxColor";
            this.comboBoxColor.Size = new System.Drawing.Size(151, 28);
            this.comboBoxColor.TabIndex = 14;
            this.comboBoxColor.SelectedIndexChanged += new System.EventHandler(this.comboBoxColor_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(1040, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 31);
            this.label2.TabIndex = 16;
            this.label2.Text = "Theme";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(12, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 31);
            this.label1.TabIndex = 17;
            this.label1.Text = "Font";
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(1082, 595);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(151, 52);
            this.buttonSearch.TabIndex = 18;
            this.buttonSearch.Text = "F11 Search";
            this.buttonSearch.UseVisualStyleBackColor = true;
            // 
            // buttonDownload
            // 
            this.buttonDownload.Location = new System.Drawing.Point(1129, 74);
            this.buttonDownload.Name = "buttonDownload";
            this.buttonDownload.Size = new System.Drawing.Size(145, 36);
            this.buttonDownload.TabIndex = 19;
            this.buttonDownload.Text = "F9 Download";
            this.buttonDownload.UseVisualStyleBackColor = true;
            // 
            // buttonAnalyze
            // 
            this.buttonAnalyze.Location = new System.Drawing.Point(870, 30);
            this.buttonAnalyze.Name = "buttonAnalyze";
            this.buttonAnalyze.Size = new System.Drawing.Size(94, 29);
            this.buttonAnalyze.TabIndex = 20;
            this.buttonAnalyze.Text = "Analyze";
            this.buttonAnalyze.UseVisualStyleBackColor = true;
            this.buttonAnalyze.Click += new System.EventHandler(this.buttonAnalyze_Click);
            // 
            // textBoxAnalyze
            // 
            this.textBoxAnalyze.Location = new System.Drawing.Point(455, 30);
            this.textBoxAnalyze.Name = "textBoxAnalyze";
            this.textBoxAnalyze.Size = new System.Drawing.Size(390, 27);
            this.textBoxAnalyze.TabIndex = 21;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1293, 675);
            this.Controls.Add(this.textBoxAnalyze);
            this.Controls.Add(this.buttonAnalyze);
            this.Controls.Add(this.buttonDownload);
            this.Controls.Add(this.buttonSearch);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxColor);
            this.Controls.Add(this.comboBoxFont);
            this.Controls.Add(this.buttonRightBack);
            this.Controls.Add(this.buttonLeftBack);
            this.Controls.Add(this.labelRight);
            this.Controls.Add(this.labelLeft);
            this.Controls.Add(this.buttonArchiving);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonCopy);
            this.Controls.Add(this.buttonMove);
            this.Controls.Add(this.buttonEdit);
            this.Controls.Add(this.buttonView);
            this.Controls.Add(this.listBoxRight);
            this.Controls.Add(this.listBoxLeft);
            this.Name = "MainForm";
            this.Text = "File Manager";
            this.ResumeLayout(false);
            this.PerformLayout();

    }

    #endregion

    private ListBox listBoxLeft;
    private ListBox listBoxRight;
    private Button buttonView;
    private Button buttonEdit;
    private Button buttonMove;
    private Button buttonCopy;
    private Button buttonDelete;
    private Button buttonExit;
    private Label labelLeft;
    private Label labelRight;
    private Button buttonLeftBack;
    private Button buttonRightBack;
    private Button buttonArchiving;
    private ComboBox comboBoxFont;
    private ComboBox comboBoxColor;
    private Label label2;
    private Label label1;
    private Button buttonSearch;
    private Button buttonDownload;
    private Button buttonAnalyze;
    private TextBox textBoxAnalyze;
}