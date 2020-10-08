using System;
using System.Windows.Forms;
using System.IO;

namespace DB_1
{
    public partial class HandleFormAdd : Form
    {
        Form1 f1;
        public HandleFormAdd()
        {
            InitializeComponent();
            FormLoad();
        }
        public HandleFormAdd(Form1 form1)
        {
            InitializeComponent();
            FormLoad();
            f1 = form1;
            f1.Enabled = false;
        }

        private void EditUserClick(object sender, EventArgs e)
        {
            Editor editor = new Editor(this);
            editor.ShowDialog();
            if (editor.DialogResult == DialogResult.OK)
            {
                peopType_comboBox.Items.Clear();
                using (StreamReader reader = new StreamReader("categories.txt"))
                {
                    string temp = null;
                    peopType_comboBox.Items.Add("\n");
                    while ((temp = reader.ReadLine()) != null)
                        peopType_comboBox.Items.Add(temp);
                }
            }
        }

        private void KeyPressCheck(object sender, KeyPressEventArgs e)
        {
            char symb = e.KeyChar;

            if (!char.IsDigit(symb) && symb != 8 && symb != 46)
                e.Handled = true;
        }

        private void DoneBtnClick(object sener, EventArgs e)
        {
            if (name_textBox.Text == "")
                MessageBox.Show("Укажите ФИО!", "Warning!");
            else if (phoneNum_textBox.Text == "" && mobPhoneNum_textBox.Text == "" && email_textBox.Text == "" && adress_textBox.Text == "")
                MessageBox.Show("Укажите что-то из этого: стационарный/мобильный номер, электронный/домашний адрес.", "Warning!");
            else
            {
                f1.SetData(name_textBox.Text, phoneNum_textBox.Text, mobPhoneNum_textBox.Text, birthday_textBox.Text, email_textBox.Text, peopType_comboBox.Text, adress_textBox.Text, remark_textBox.Text);
                DialogResult = DialogResult.OK;
                Hide();
            }

        }

        private void FormLoad()
        {
            try
            {
                using (StreamReader reader = new StreamReader("categories.txt"))
                {
                    string temp = null;
                    peopType_comboBox.Items.Add("\n");
                    while ((temp = reader.ReadLine()) != null)
                        peopType_comboBox.Items.Add(temp);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void FormClose(object sender, FormClosingEventArgs e)
        {
            f1.Enabled = true;
        }
        
    }
}
