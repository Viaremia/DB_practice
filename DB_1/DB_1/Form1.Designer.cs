namespace DB_1
{
    partial class Form1
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

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.фио = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.стац_тел = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.моб_тел = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.дата_рождения = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.эл_фдресс = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.категор_абонента = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.дом_адресс = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.примечания = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id,
            this.фио,
            this.стац_тел,
            this.моб_тел,
            this.дата_рождения,
            this.эл_фдресс,
            this.категор_абонента,
            this.дом_адресс,
            this.примечания});
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(974, 331);
            this.dataGridView1.TabIndex = 0;
            // 
            // id
            // 
            this.id.HeaderText = "Id";
            this.id.Name = "id";
            this.id.Width = 50;
            // 
            // фио
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            this.фио.DefaultCellStyle = dataGridViewCellStyle2;
            this.фио.HeaderText = "ФИО";
            this.фио.Name = "фио";
            this.фио.Width = 170;
            // 
            // стац_тел
            // 
            this.стац_тел.HeaderText = "Стац_тел";
            this.стац_тел.Name = "стац_тел";
            // 
            // моб_тел
            // 
            this.моб_тел.HeaderText = "Моб_тел";
            this.моб_тел.Name = "моб_тел";
            // 
            // дата_рождения
            // 
            this.дата_рождения.HeaderText = "Дата_рождения";
            this.дата_рождения.Name = "дата_рождения";
            // 
            // эл_фдресс
            // 
            this.эл_фдресс.HeaderText = "Эл_адресс";
            this.эл_фдресс.Name = "эл_фдресс";
            // 
            // категор_абонента
            // 
            this.категор_абонента.HeaderText = "Категор_абонента";
            this.категор_абонента.Name = "категор_абонента";
            this.категор_абонента.Width = 110;
            // 
            // дом_адресс
            // 
            this.дом_адресс.HeaderText = "Дом_адресс";
            this.дом_адресс.Name = "дом_адресс";
            // 
            // примечания
            // 
            this.примечания.HeaderText = "Примечания";
            this.примечания.Name = "примечания";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(69, 367);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(179, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Add new";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.AddBtnClick);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(287, 367);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(179, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "Edit";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.EditBtnClick);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(503, 367);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(179, 23);
            this.button3.TabIndex = 2;
            this.button3.Text = "Delete";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.DeleteBtnClick);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(724, 367);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(179, 23);
            this.button4.TabIndex = 3;
            this.button4.Text = "Search";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.SearchBtnClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(1002, 427);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Written book";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormClose);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn фио;
        private System.Windows.Forms.DataGridViewTextBoxColumn стац_тел;
        private System.Windows.Forms.DataGridViewTextBoxColumn моб_тел;
        private System.Windows.Forms.DataGridViewTextBoxColumn дата_рождения;
        private System.Windows.Forms.DataGridViewTextBoxColumn эл_фдресс;
        private System.Windows.Forms.DataGridViewTextBoxColumn категор_абонента;
        private System.Windows.Forms.DataGridViewTextBoxColumn дом_адресс;
        private System.Windows.Forms.DataGridViewTextBoxColumn примечания;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
    }
}

