using System;
using System.Data.OleDb;
using System.Windows.Forms;

namespace DB_1
{
    public partial class WhereForm : Form
    {
        private HandleFormEdit.HandleFormEdit hfe = null;
        private Form1 f1 = null;
        private string query = null;
        private OleDbConnection connection;

        public WhereForm()
        {
            InitializeComponent();
        }

        public WhereForm(HandleFormEdit.HandleFormEdit hfe, string query, ref OleDbConnection connection)
        {
            InitializeComponent();
            this.hfe = hfe;
            this.query = query;
            this.connection = connection;
            hfe.Enabled = false;
        }

        public WhereForm(Form1 f1, string query, ref OleDbConnection connection)
        {
            InitializeComponent();
            this.f1 = f1;
            this.query = query;
            this.connection = connection;
            f1.Enabled = false;
        }

        private void OkBtnClick(object sender, EventArgs e)
        {
            string[] temp = null;
            string tempStr = "str";
            DialogResult = DialogResult.OK;
            if (cmbBox.Text == "Код" || cmbBox.Text == "Стац_Тел" || cmbBox.Text == "Моб_тел")
                query += $"WHERE [{cmbBox.Text}] = {int.Parse(txtBox.Text)};";
            else if (cmbBox.Text == "Дата_рождения")
            {   
                txtBox.Text = txtBox.Text.Replace('.', '/');
                temp = txtBox.Text.Split('/');

                txtBox.Text = txtBox.Text.Replace(temp[0], tempStr);
                txtBox.Text = txtBox.Text.Replace(temp[1], temp[0]);
                txtBox.Text = txtBox.Text.Replace(tempStr, temp[1]);
                //for (int i = 0; i < temp.Length; i++)
                //    tempStr += temp[i] + '/';
                query += $"WHERE (((телефоны.[{cmbBox.Text}]) = #{txtBox.Text}#));";
            }
            else
                query += $"WHERE [{cmbBox.Text}] = '{txtBox.Text}';";
            try
            {
                temp = query.Split(' ');
                using (OleDbCommand command = new OleDbCommand(query, connection))
                {
                    if (temp[0] == "SELECT")
                    {
                        OleDbDataReader reader = command.ExecuteReader();
                        reader.Read();
                        if(reader[0] != null)
                            MessageBox.Show("Нашёл!", "УРА!");
                    }
                    else
                    {
                        command.ExecuteNonQuery();
                    }
                }
               
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning");
            }
            
            Hide();
        }

        private void KeyPressCheck(object senser, KeyPressEventArgs e)
        {
            char symb = e.KeyChar;
            if (cmbBox.Text == "Код" || cmbBox.Text == "Стац_Тел" || cmbBox.Text == "Моб_тел" || cmbBox.Text == "Дата_рождения")
            {
                if (!char.IsDigit(symb) && symb != 8 && symb != 46)
                    e.Handled = true;
            }
        }
                
        private void FormClose(object sender, FormClosingEventArgs e)
        {
            if(hfe != null)
                hfe.Enabled = true;
            if (f1 != null)
                f1.Enabled = true;
        }
    }
}
