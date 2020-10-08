using System;
using System.Windows.Forms;
using System.Data.OleDb;

namespace DB_1
{
    public partial class Form1 : Form
    {
        private OleDbConnection connection = new OleDbConnection("Provider = Microsoft.Jet.OLEDB.4.0; Data Source = Writen_book.mdb");
        private string name, phone_numb, mob_phone_numb, birthday, email, peop_type, adress, remark;
        HandleFormAdd hf;

        public Form1()
        {
            InitializeComponent();
            FormLoad();
            
        }

        private void SearchBtnClick(object sender, EventArgs e)
        {
            string query = "SELECT [ФИО] FROM [телефоны] ";
            using (WhereForm wf = new WhereForm(this, query, ref connection))
            {
                wf.ShowDialog();
                if (wf.DialogResult == DialogResult.OK)
                    wf.Close();
            }
        }

        private void AddBtnClick(object sender, EventArgs e)
        {
            string query = null;
            int ID = 1;
            using (OleDbCommand command = new OleDbCommand("SELECT MAX([Код]) FROM телефоны ", connection))
            {
                OleDbDataReader reader = command.ExecuteReader();
                reader.Read();
                ID += int.Parse(reader[0].ToString());
                reader.Close();
            }

            using (hf = new HandleFormAdd(this))
            {
                hf.ShowDialog();
                if (hf.DialogResult == DialogResult.OK)
                {
                    hf.Close();
                    if (phone_numb == "")
                    {
                        query = $"INSERT INTO [телефоны] VALUES ('{ID}', '{name}', NULL, '{int.Parse(mob_phone_numb)}', {birthday}, '{email}', '{peop_type}', '{adress}', '{remark}'); ";
                    }
                    else if (mob_phone_numb == "")
                    {
                        query = $"INSERT INTO [телефоны] VALUES ('{ID}', '{name}', '{int.Parse(phone_numb)}', NULL, {birthday}, '{email}', '{peop_type}', '{adress}', '{remark}'); ";
                    }
                    else
                    {
                        query = $"INSERT INTO [телефоны] VALUES ('{ID}', '{name}', '{int.Parse(phone_numb)}', '{int.Parse(mob_phone_numb)}', {birthday}, '{email}', '{peop_type}', '{adress}', '{remark}'); ";
                    }
                    try
                    {
                        using (OleDbCommand command = new OleDbCommand(query, connection))
                        {
                            int i = command.ExecuteNonQuery();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            DataGridLoad();
        }
        //реализовать метод
        private void EditBtnClick(object sender, EventArgs e)
        {
            using (HandleFormEdit.HandleFormEdit hfe = new HandleFormEdit.HandleFormEdit(this, ref connection))
            {
                hfe.ShowDialog();
                if (hfe.DialogResult == DialogResult.OK)
                {
                    hfe.Close();
                    DataGridLoad();
                }
            }
        }
        
        private void DeleteBtnClick(object sender, EventArgs e)
        {
            string query = "DELETE FROM [телефоны] ";
            using (WhereForm wf = new WhereForm(this, query, ref connection))
            {
                wf.ShowDialog();
                if (wf.DialogResult == DialogResult.OK)
                    wf.Close();
            }
            DataGridLoad();
        }
        
        public void SetData(string name, string phone_numb, string mob_phone_numb, string birthday, string email, string peop_type, string adress, string remark)
        {
            this.name = name;
            this.phone_numb = phone_numb;
            this.mob_phone_numb = mob_phone_numb;
            this.birthday = birthday;
            NullCheck(ref this.birthday);
            this.email = email;
            this.peop_type = peop_type;
            this.adress = adress;
            this.remark = remark;
        }

        private void NullCheck(ref string value)
        {
            string temp = null;
            if (value == "")
                value = "NULL";
            else
            {
                temp += "\'" + value + "\'";
                value = temp;
            }
                    
        }

        private void FormLoad()
        {
            connection.Open();
            DataGridLoad();
        }

        private void DataGridLoad()
        {
            dataGridView1.Rows.Clear();
            using (OleDbCommand command = new OleDbCommand("SELECT * FROM [телефоны]", connection))
            {
                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                    dataGridView1.Rows.Add(reader[0], reader[1], reader[2], reader[3], reader[4], reader[5], reader[6], reader[7], reader[8]);

                reader.Close();
            }
        }

        private void FormClose(object sender, FormClosingEventArgs e)
        {
            connection.Close();
        }
    }
}
