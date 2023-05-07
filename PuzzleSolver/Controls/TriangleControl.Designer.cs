namespace PuzzleSolver.Controls
{
    partial class TriangleControl
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            labelValue = new Label();
            SuspendLayout();
            // 
            // labelValue
            // 
            labelValue.Dock = DockStyle.Fill;
            labelValue.Location = new Point(0, 0);
            labelValue.Name = "labelValue";
            labelValue.Size = new Size(150, 150);
            labelValue.TabIndex = 0;
            labelValue.TextAlign = ContentAlignment.MiddleCenter;
            labelValue.Click += labelValue_Click;
            // 
            // TriangleControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(labelValue);
            Name = "TriangleControl";
            ResumeLayout(false);
        }

        #endregion

        private Label labelValue;
    }
}
