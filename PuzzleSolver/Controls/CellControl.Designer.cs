namespace PuzzleSolver.Controls
{
    partial class CellControl
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
            labelValue.BackColor = Color.Transparent;
            labelValue.Dock = DockStyle.Fill;
            labelValue.Location = new Point(0, 0);
            labelValue.Name = "labelValue";
            labelValue.Size = new Size(150, 150);
            labelValue.TabIndex = 1;
            labelValue.TextAlign = ContentAlignment.MiddleCenter;
            labelValue.MouseClick += labelValue_MouseClick;
            // 
            // CellControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(labelValue);
            Name = "CellControl";
            ResumeLayout(false);
        }

        #endregion

        private Label labelValue;
    }
}
