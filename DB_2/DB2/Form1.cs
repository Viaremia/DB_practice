using System;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb;

namespace DB2
{
    public partial class Form1 : Form
    {
        private OleDbConnection connection = null;
        public Form1()
        {
            InitializeComponent();
            FormLoad();
        }

        private void ConnectDBClick(object sender, EventArgs e)
        {
            string connectionString = $"Provider = Microsoft.Jet.OLEDB.4.0; Data Source = ";

            using (OpenFileDialog fd = new OpenFileDialog())
            {
                fd.Filter = "Access file(*.mdb)|*.mdb";
                if (fd.ShowDialog() == DialogResult.Cancel)
                    return;

                connectionString += fd.FileName + ";";
            }

            connection = new OleDbConnection(connectionString);
            connection.Open();
        }

        private void CreateClick(object sender, EventArgs e)
        {
            richTextBox.Visible = true;
        }

        private void OpenClick(object sender, EventArgs e)
        {
            using (StreamReader sr = new StreamReader("Query.txt"))
            {
                richTextBox.Text = sr.ReadToEnd();
            }
        }

        private void SaveClicl(object sender, EventArgs e)
        {
            using (StreamWriter sw = new StreamWriter("Query.txt", false))
            {
                sw.WriteLine(richTextBox.Text);
            }
        }

        private void GenerateBtnClick(object sender, EventArgs e)
        {
            GeneratorStage1();
            GeneratorStage2();
            int index = richTextBox.Text.Length - 2;
            char c = richTextBox.Text[index];
            if (richTextBox.Text[index] == ',')
            {
                richTextBox.Text = richTextBox.Text.Remove(index, 1);
                richTextBox.Text += ";";
                return;
            }

            if (richTextBox.Text[index] != ';')
                richTextBox.Text += ";";
        }

        private void GeneratorStage1()
        {
            string[] keyWords = new string[] { "FROM", "UPDATE", "TABLE" };
            string tempText = richTextBox.Text;
            foreach (string s in keyWords)
            {
                int i = tempText.IndexOf(s, 0);
                if (i == -1)
                    goto point1;
                i += s.Length;
                if (s == "TABLE")
                {
                    tempText = tempText.Insert(i, " [Table_name]");
                    goto point1;
                }
                tempText = tempText.Insert(i, $" [{comboBoxTable.Text}]");
            point1:
                richTextBox.Text = tempText;
            }
        }

        private void GeneratorStage2()
        {
            string[] keyWords = new string[] { "SET", "WHERE" };
            string tempText = richTextBox.Text;
            
            foreach(string s in keyWords)
            {
                int i = tempText.IndexOf(s, 0);
                if (i == -1)
                    goto point1;
                i += s.Length;
                int index = 0;
                if (s == "SET")
                {
                     index = tempText.IndexOf("WHERE", 0);
                    if (index != -1)
                        index = index - i;
                }

                string subStr = tempText.Substring(i, index);
                string[] spliter = subStr.Split(' ');
                subStr = null;
                for (int j = 0; j < spliter.Length; j++)
                {
                    if (spliter[j] != "")
                        tempText = tempText.Replace(spliter[j], spliter[j] + " = \'your_text\',");
                }
            point1:
                richTextBox.Text = tempText;
            }
        }
        // изм значения в полях таблицы
        private void TableFieldSlctValChenged(object sender, EventArgs e)
        {
            if (richTextBox.Visible == false || comboBoxTableField.Text == "")
                return;
            if (comboBoxTableField.Text == "All")
            {
                richTextBox.Text += "* ";
                return;
            }
            richTextBox.Text += comboBoxTableField.Text + " "; 
        }

        // изм значения в запросах
        private void QueryTypeSlctValChenged(object sender, EventArgs e)
        {
            if (richTextBox.Visible == false || comboBoxQueryType.Text == "")
                return;

            richTextBox.Text += comboBoxQueryType.Text + " ";
        }

        // выпадение запросов
        private void QueryTypeDrpDwn(object sender, EventArgs e)
        {
            try
            {
                comboBoxQueryType.Items.Clear();
                using (StreamReader sr = new StreamReader("QueryTypes.txt"))
                {
                    string val = null;
                    while ((val = sr.ReadLine()) != null)
                        comboBoxQueryType.Items.Add(val);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // выпадение таблицы
        private void TableDrpDwn(object sender, EventArgs e)
        {
            if (connection == null)
                return;

            comboBoxTable.Items.Clear();
            DataTable tables = connection.GetSchema("Tables", new string[] { null, null, null, "TABLE" });
            foreach (DataRow row in tables.Rows)
            {
                string tableName = row["TABLE_NAME"].ToString();
                comboBoxTable.Items.Add(tableName);
            }
        }

        // выпадение полей таблицы
        private void TableFieldDrpDwn(object sender, EventArgs e)
        {
            if (comboBoxTable.Text == "")
                return;

            comboBoxTableField.Items.Clear();
            using (OleDbCommand command = new OleDbCommand($"SELECT * FROM {comboBoxTable.Text} WHERE 1 = 0", connection))
            {
                OleDbDataReader reader = command.ExecuteReader();
                DataTable table = reader.GetSchemaTable();
                foreach (DataRow t in table.Rows)
                {
                    string rowName = t.Field<string>("ColumnName");
                    comboBoxTableField.Items.Add(rowName);
                }
                comboBoxTableField.Items.Add("All");
                reader.Close();
            }
        }


        private void RedButtonClick(object sender, EventArgs e) 
        {
            richTextBox.Clear();
        }

        private void ExitClick(object sender, EventArgs e)
        {
            Close();
        }

        private void FormClose(object sender, FormClosingEventArgs e) 
        {
            connection.Close();
        }

        private void FormLoad()
        {
            richTextBox.Visible = false;
        }
        
    }
}
