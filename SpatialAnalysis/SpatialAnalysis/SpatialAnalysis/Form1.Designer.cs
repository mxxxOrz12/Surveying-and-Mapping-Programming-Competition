namespace SpatialAnalysis
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.打开数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.计算标准差椭圆ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.莫兰指数计算ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.计算全局莫兰指数ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.计算局部莫兰指数ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.清空数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.打开报告ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.打开数据ToolStripMenuItem,
            this.计算标准差椭圆ToolStripMenuItem,
            this.莫兰指数计算ToolStripMenuItem,
            this.清空数据ToolStripMenuItem,
            this.打开报告ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1306, 32);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 打开数据ToolStripMenuItem
            // 
            this.打开数据ToolStripMenuItem.Name = "打开数据ToolStripMenuItem";
            this.打开数据ToolStripMenuItem.Size = new System.Drawing.Size(94, 28);
            this.打开数据ToolStripMenuItem.Text = "打开数据";
            this.打开数据ToolStripMenuItem.Click += new System.EventHandler(this.打开数据ToolStripMenuItem_Click);
            // 
            // 计算标准差椭圆ToolStripMenuItem
            // 
            this.计算标准差椭圆ToolStripMenuItem.Name = "计算标准差椭圆ToolStripMenuItem";
            this.计算标准差椭圆ToolStripMenuItem.Size = new System.Drawing.Size(148, 28);
            this.计算标准差椭圆ToolStripMenuItem.Text = "计算标准差椭圆";
            this.计算标准差椭圆ToolStripMenuItem.Click += new System.EventHandler(this.计算标准差椭圆ToolStripMenuItem_Click);
            // 
            // 莫兰指数计算ToolStripMenuItem
            // 
            this.莫兰指数计算ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.计算全局莫兰指数ToolStripMenuItem,
            this.计算局部莫兰指数ToolStripMenuItem});
            this.莫兰指数计算ToolStripMenuItem.Name = "莫兰指数计算ToolStripMenuItem";
            this.莫兰指数计算ToolStripMenuItem.Size = new System.Drawing.Size(130, 28);
            this.莫兰指数计算ToolStripMenuItem.Text = "莫兰指数计算";
            // 
            // 计算全局莫兰指数ToolStripMenuItem
            // 
            this.计算全局莫兰指数ToolStripMenuItem.Name = "计算全局莫兰指数ToolStripMenuItem";
            this.计算全局莫兰指数ToolStripMenuItem.Size = new System.Drawing.Size(252, 30);
            this.计算全局莫兰指数ToolStripMenuItem.Text = "计算全局莫兰指数";
            this.计算全局莫兰指数ToolStripMenuItem.Click += new System.EventHandler(this.计算全局莫兰指数ToolStripMenuItem_Click);
            // 
            // 计算局部莫兰指数ToolStripMenuItem
            // 
            this.计算局部莫兰指数ToolStripMenuItem.Name = "计算局部莫兰指数ToolStripMenuItem";
            this.计算局部莫兰指数ToolStripMenuItem.Size = new System.Drawing.Size(252, 30);
            this.计算局部莫兰指数ToolStripMenuItem.Text = "计算局部莫兰指数";
            this.计算局部莫兰指数ToolStripMenuItem.Click += new System.EventHandler(this.计算局部莫兰指数ToolStripMenuItem_Click);
            // 
            // 清空数据ToolStripMenuItem
            // 
            this.清空数据ToolStripMenuItem.Name = "清空数据ToolStripMenuItem";
            this.清空数据ToolStripMenuItem.Size = new System.Drawing.Size(94, 28);
            this.清空数据ToolStripMenuItem.Text = "清空数据";
            // 
            // 打开报告ToolStripMenuItem
            // 
            this.打开报告ToolStripMenuItem.Name = "打开报告ToolStripMenuItem";
            this.打开报告ToolStripMenuItem.Size = new System.Drawing.Size(94, 28);
            this.打开报告ToolStripMenuItem.Text = "打开报告";
            this.打开报告ToolStripMenuItem.Click += new System.EventHandler(this.打开报告ToolStripMenuItem_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Times New Roman", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.dataGridView1.Location = new System.Drawing.Point(0, 35);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 30;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.PaleGreen;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.RowTemplate.Height = 30;
            this.dataGridView1.Size = new System.Drawing.Size(1306, 655);
            this.dataGridView1.TabIndex = 1;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "ID";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "X";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Y";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Value";
            this.Column4.Name = "Column4";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1306, 693);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "空间数据探索性分析";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 打开数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 计算标准差椭圆ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 莫兰指数计算ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 清空数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 打开报告ToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.ToolStripMenuItem 计算全局莫兰指数ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 计算局部莫兰指数ToolStripMenuItem;
    }
}

