using System;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Collections.Generic;

namespace DB_1.HandleFormEdit
{
    public partial class HandleFormEdit : Form
    {
        private Form1 f1;
        private OleDbConnection connection;

        List<Label> tempTxt;
        List<TextBox> tempVal; 

        public HandleFormEdit()
        {
            InitializeComponent();
        }
        public HandleFormEdit(Form1 f1, ref OleDbConnection connection)
        {
            InitializeComponent();
            this.f1 = f1;
            this.connection = connection;
            f1.Enabled = false;
        }

        private void OkBtnClick(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            tempTxt = new List<Label>() { name_txt, phone_txt, mob_phone_txt, birthday_txt, email_txt, category_txt, adress_txt, remark_txt};
            tempVal = new List<TextBox>() { name_val, phone_val, mob_phone_val, birthday_val, email_val, category_val, adress_val, remark_val};

            string query = "UPDATE [телефоны] SET ";
            GetData(ref query, tempTxt, tempVal); 
           
            using (WhereForm wf = new WhereForm(this, query, ref connection))
            {
                wf.ShowDialog();
                if (wf.DialogResult == DialogResult.OK)
                    wf.Close();
            }
            Hide();
        }
        
        private void GetData(ref string query, List<Label> tempTxt, List<TextBox> tempVal)
        {
            for (int i = 0; i < tempTxt.Count; i++)
            {
                if (tempVal[i].Text != "")
                {
                    if (tempTxt[i].Text == "Стац_Тел" || tempTxt[i].Text == "Моб_тел")
                        query += $"[{tempTxt[i].Text}] = {int.Parse(tempVal[i].Text)},";
                    else if (tempTxt[i].Text == "Дата_рождения")
                    {
                        string[] temp;
                        string str = "s";
                        tempVal[i].Text = tempVal[i].Text.Replace('.', '/');
                        temp = tempVal[i].Text.Split('/');
                        tempVal[i].Text = tempVal[i].Text.Replace(temp[0], str);
                        tempVal[i].Text = tempVal[i].Text.Replace(temp[1], temp[0]);
                        tempVal[i].Text = tempVal[i].Text.Replace(str, temp[1]);

                        query += $"[{tempTxt[i].Text}] = '{tempVal[i].Text}',";
                    }
                    else
                        query += $"[{tempTxt[i].Text}] = '{tempVal[i].Text}',";
                }
            }
            if (query[query.Length-1] == ',')
            {
                query = query.Substring(0, query.Length -1);
            }
        }

        private void KeyPressCheck(object sender, KeyPressEventArgs e)
        {
            char symb = e.KeyChar;
            if (!char.IsDigit(symb) && symb != 8 && symb != 46)
                e.Handled = true;
        }

        private void FormClose(object sender, FormClosingEventArgs e)
        {
            f1.Enabled = true;
        }
    }
        
}
