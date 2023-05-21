namespace PuzzleSolver.Forms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            menu = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            loadToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            фToolStripMenuItem = new ToolStripSeparator();
            exitToolStripMenuItem = new ToolStripMenuItem();
            puzzleToolStripMenuItem = new ToolStripMenuItem();
            sudokuToolStripMenuItem = new ToolStripMenuItem();
            routingToolStripMenuItem = new ToolStripMenuItem();
            triangleToolStripMenuItem = new ToolStripMenuItem();
            trafficToolStripMenuItem = new ToolStripMenuItem();
            status = new StatusStrip();
            statusLabel = new ToolStripStatusLabel();
            tool = new ToolStrip();
            initButton = new ToolStripButton();
            solveButton = new ToolStripButton();
            validateButton = new ToolStripButton();
            panel = new Panel();
            menu.SuspendLayout();
            status.SuspendLayout();
            tool.SuspendLayout();
            SuspendLayout();
            // 
            // menu
            // 
            menu.ImageScalingSize = new Size(20, 20);
            menu.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, puzzleToolStripMenuItem });
            menu.Location = new Point(0, 0);
            menu.Name = "menu";
            menu.Size = new Size(800, 28);
            menu.TabIndex = 0;
            menu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { loadToolStripMenuItem, saveToolStripMenuItem, фToolStripMenuItem, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(59, 24);
            fileToolStripMenuItem.Text = "&Файл";
            // 
            // loadToolStripMenuItem
            // 
            loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            loadToolStripMenuItem.Size = new Size(175, 26);
            loadToolStripMenuItem.Text = "&Открыть...";
            loadToolStripMenuItem.Click += loadToolStripMenuItem_Click;
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(175, 26);
            saveToolStripMenuItem.Text = "&Сохранить...";
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            // 
            // фToolStripMenuItem
            // 
            фToolStripMenuItem.Name = "фToolStripMenuItem";
            фToolStripMenuItem.Size = new Size(172, 6);
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(175, 26);
            exitToolStripMenuItem.Text = "&Выход";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // puzzleToolStripMenuItem
            // 
            puzzleToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { sudokuToolStripMenuItem, routingToolStripMenuItem, triangleToolStripMenuItem, trafficToolStripMenuItem });
            puzzleToolStripMenuItem.Name = "puzzleToolStripMenuItem";
            puzzleToolStripMenuItem.Size = new Size(116, 24);
            puzzleToolStripMenuItem.Text = "&Головоломка";
            // 
            // sudokuToolStripMenuItem
            // 
            sudokuToolStripMenuItem.Name = "sudokuToolStripMenuItem";
            sudokuToolStripMenuItem.Size = new Size(224, 26);
            sudokuToolStripMenuItem.Text = "&Судоку";
            sudokuToolStripMenuItem.Click += sudokuToolStripMenuItem_Click;
            // 
            // routingToolStripMenuItem
            // 
            routingToolStripMenuItem.Name = "routingToolStripMenuItem";
            routingToolStripMenuItem.Size = new Size(224, 26);
            routingToolStripMenuItem.Text = "&Коммутация";
            routingToolStripMenuItem.Click += routingToolStripMenuItem_Click;
            // 
            // triangleToolStripMenuItem
            // 
            triangleToolStripMenuItem.Name = "triangleToolStripMenuItem";
            triangleToolStripMenuItem.Size = new Size(224, 26);
            triangleToolStripMenuItem.Text = "&Треугольник";
            triangleToolStripMenuItem.Click += triangleToolStripMenuItem_Click;
            // 
            // trafficToolStripMenuItem
            // 
            trafficToolStripMenuItem.Name = "trafficToolStripMenuItem";
            trafficToolStripMenuItem.Size = new Size(224, 26);
            trafficToolStripMenuItem.Text = "&Маршрут";
            trafficToolStripMenuItem.Click += trafficToolStripMenuItem_Click;
            // 
            // status
            // 
            status.ImageScalingSize = new Size(20, 20);
            status.Items.AddRange(new ToolStripItem[] { statusLabel });
            status.Location = new Point(0, 428);
            status.Name = "status";
            status.Size = new Size(800, 22);
            status.TabIndex = 1;
            status.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(0, 16);
            // 
            // tool
            // 
            tool.ImageScalingSize = new Size(20, 20);
            tool.Items.AddRange(new ToolStripItem[] { initButton, solveButton, validateButton });
            tool.Location = new Point(0, 28);
            tool.Name = "tool";
            tool.Size = new Size(800, 27);
            tool.TabIndex = 2;
            tool.Text = "toolStrip1";
            // 
            // initButton
            // 
            initButton.CheckOnClick = true;
            initButton.Image = (Image)resources.GetObject("initButton.Image");
            initButton.ImageTransparentColor = Color.Magenta;
            initButton.Name = "initButton";
            initButton.Size = new Size(199, 24);
            initButton.Text = "Начальная расстановка";
            // 
            // solveButton
            // 
            solveButton.Image = (Image)resources.GetObject("solveButton.Image");
            solveButton.ImageTransparentColor = Color.Magenta;
            solveButton.Name = "solveButton";
            solveButton.Size = new Size(143, 24);
            solveButton.Text = "Найти решение";
            solveButton.Click += solveButton_Click;
            // 
            // validateButton
            // 
            validateButton.Image = (Image)resources.GetObject("validateButton.Image");
            validateButton.ImageTransparentColor = Color.Magenta;
            validateButton.Name = "validateButton";
            validateButton.Size = new Size(177, 24);
            validateButton.Text = "Проверить решение";
            validateButton.Click += validateButton_Click;
            // 
            // panel
            // 
            panel.Dock = DockStyle.Fill;
            panel.Location = new Point(0, 55);
            panel.Name = "panel";
            panel.Size = new Size(800, 373);
            panel.TabIndex = 3;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panel);
            Controls.Add(tool);
            Controls.Add(status);
            Controls.Add(menu);
            MainMenuStrip = menu;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Решатель головоломок";
            WindowState = FormWindowState.Maximized;
            menu.ResumeLayout(false);
            menu.PerformLayout();
            status.ResumeLayout(false);
            status.PerformLayout();
            tool.ResumeLayout(false);
            tool.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menu;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem loadToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripSeparator фToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem puzzleToolStripMenuItem;
        private ToolStripMenuItem sudokuToolStripMenuItem;
        private StatusStrip status;
        private ToolStrip tool;
        private ToolStripButton initButton;
        private ToolStripStatusLabel statusLabel;
        private ToolStripButton solveButton;
        private ToolStripButton validateButton;
        private ToolStripMenuItem routingToolStripMenuItem;
        private ToolStripMenuItem triangleToolStripMenuItem;
        private Panel panel;
        private ToolStripMenuItem trafficToolStripMenuItem;
    }
}