namespace Discount
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
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

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.textbox_num = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textbox_percent = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.BARCODE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PERCENT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button_insert = new System.Windows.Forms.Button();
            this.label_status = new System.Windows.Forms.Label();
            this.timer_msg_clear = new System.Windows.Forms.Timer(this.components);
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.button_add = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button_delete = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // textbox_num
            // 
            this.textbox_num.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textbox_num.Location = new System.Drawing.Point(12, 25);
            this.textbox_num.MaxLength = 13;
            this.textbox_num.Name = "textbox_num";
            this.textbox_num.Size = new System.Drawing.Size(156, 29);
            this.textbox_num.TabIndex = 0;
            this.textbox_num.TextChanged += new System.EventHandler(this.textbox_num_TextChanged);
            this.textbox_num.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textbox_num_KeyDown);
            this.textbox_num.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textbox_num_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Номер карты";
            // 
            // textbox_percent
            // 
            this.textbox_percent.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textbox_percent.Location = new System.Drawing.Point(183, 25);
            this.textbox_percent.MaxLength = 13;
            this.textbox_percent.Name = "textbox_percent";
            this.textbox_percent.Size = new System.Drawing.Size(32, 29);
            this.textbox_percent.TabIndex = 2;
            this.textbox_percent.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textbox_percent_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(191, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(15, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "%";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Sunken;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.BARCODE,
            this.PERCENT});
            this.dataGridView1.Location = new System.Drawing.Point(12, 98);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(203, 436);
            this.dataGridView1.TabIndex = 4;
            // 
            // BARCODE
            // 
            dataGridViewCellStyle1.Format = "0";
            dataGridViewCellStyle1.NullValue = null;
            this.BARCODE.DefaultCellStyle = dataGridViewCellStyle1;
            this.BARCODE.HeaderText = "BARCODE";
            this.BARCODE.MaxInputLength = 13;
            this.BARCODE.Name = "BARCODE";
            this.BARCODE.ReadOnly = true;
            this.BARCODE.Width = 120;
            // 
            // PERCENT
            // 
            this.PERCENT.HeaderText = "PERCENT";
            this.PERCENT.MaxInputLength = 1;
            this.PERCENT.Name = "PERCENT";
            this.PERCENT.ReadOnly = true;
            this.PERCENT.Width = 63;
            // 
            // button_insert
            // 
            this.button_insert.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button_insert.Location = new System.Drawing.Point(43, 60);
            this.button_insert.Name = "button_insert";
            this.button_insert.Size = new System.Drawing.Size(87, 23);
            this.button_insert.TabIndex = 5;
            this.button_insert.Text = "Формировать";
            this.button_insert.UseVisualStyleBackColor = true;
            this.button_insert.Click += new System.EventHandler(this.button_insert_Click);
            // 
            // label_status
            // 
            this.label_status.AutoSize = true;
            this.label_status.BackColor = System.Drawing.Color.Transparent;
            this.label_status.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_status.Location = new System.Drawing.Point(66, 558);
            this.label_status.Name = "label_status";
            this.label_status.Size = new System.Drawing.Size(21, 24);
            this.label_status.TabIndex = 6;
            this.label_status.Text = "_";
            this.label_status.Visible = false;
            // 
            // timer_msg_clear
            // 
            this.timer_msg_clear.Interval = 4000;
            this.timer_msg_clear.Tick += new System.EventHandler(this.timer_msg_clear_Tick);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 541);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(203, 14);
            this.progressBar1.TabIndex = 7;
            // 
            // button_add
            // 
            this.button_add.Enabled = false;
            this.button_add.Location = new System.Drawing.Point(196, 60);
            this.button_add.Name = "button_add";
            this.button_add.Size = new System.Drawing.Size(23, 23);
            this.button_add.TabIndex = 0;
            this.button_add.Text = "+";
            this.button_add.UseVisualStyleBackColor = true;
            this.button_add.Click += new System.EventHandler(this.button_add_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(10, 64);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(34, 17);
            this.checkBox1.TabIndex = 8;
            this.checkBox1.Text = "U";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // button_delete
            // 
            this.button_delete.Location = new System.Drawing.Point(166, 60);
            this.button_delete.Name = "button_delete";
            this.button_delete.Size = new System.Drawing.Size(24, 23);
            this.button_delete.TabIndex = 9;
            this.button_delete.Text = "-";
            this.button_delete.UseVisualStyleBackColor = true;
            this.button_delete.Click += new System.EventHandler(this.button_delete_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(136, 60);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(24, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "P";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(228, 591);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button_delete);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button_add);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label_status);
            this.Controls.Add(this.button_insert);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textbox_percent);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textbox_num);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Discount";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textbox_num;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textbox_percent;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button_insert;
        private System.Windows.Forms.Label label_status;
        private System.Windows.Forms.Timer timer_msg_clear;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button button_add;
        private System.Windows.Forms.DataGridViewTextBoxColumn BARCODE;
        private System.Windows.Forms.DataGridViewTextBoxColumn PERCENT;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Button button_delete;
        private System.Windows.Forms.Button button1;
    }
}

