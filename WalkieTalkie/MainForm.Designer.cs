namespace WalkieTalkie;

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
        buttonSend = new Button();
        SuspendLayout();
        // 
        // buttonSend
        // 
        buttonSend.Dock = DockStyle.Fill;
        buttonSend.Font = new Font("Segoe UI", 24F, FontStyle.Regular, GraphicsUnit.Point, 204);
        buttonSend.Location = new Point(0, 0);
        buttonSend.Name = "buttonSend";
        buttonSend.Size = new Size(309, 195);
        buttonSend.TabIndex = 0;
        buttonSend.Text = "ОЖИДАНИЕ";
        buttonSend.UseVisualStyleBackColor = true;
        buttonSend.Click += buttonSend_Click;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(309, 195);
        Controls.Add(buttonSend);
        FormBorderStyle = FormBorderStyle.Fixed3D;
        Name = "MainForm";
        Text = "Walkie Talkie";
        ResumeLayout(false);
    }

    #endregion

    private Button buttonSend;
}
