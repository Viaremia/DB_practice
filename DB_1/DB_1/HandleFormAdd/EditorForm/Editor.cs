using System;
using System.Windows.Forms;
using System.IO;

namespace DB_1
{
    public partial class Editor : Form
    {
        HandleFormAdd hf;

        public Editor()
        {
            InitializeComponent();
            FormLoad();
        }

        public Editor(HandleFormAdd handleForm)
        {
            InitializeComponent();
            FormLoad();
            hf = handleForm;
            hf.Enabled = false;
            
        }

        private void OkBtnClick(object sender, EventArgs e)
        {
            using (StreamWriter writer = new StreamWriter("categories.txt", false))
            {
                string[] temp = richTextBox.Text.Split('\n');
                for(int i = 0; i < temp.Length; i++)
                    writer.WriteLine(temp[i]);
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void FormLoad()
        {
            using (StreamReader reader = new StreamReader("categories.txt"))
            {
                string temp = null;
                while ((temp = reader.ReadLine()) != null)
                    richTextBox.Text += temp + "\n";
            }
        }

        private void FormClose(object sender, FormClosingEventArgs e)
        {
            hf.Enabled = true;
        }
    }
}
