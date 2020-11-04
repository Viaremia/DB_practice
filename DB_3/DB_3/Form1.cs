using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using System.Data.OleDb;

namespace DB_3
{
    public partial class Form1 : Form
    {
        OleDbConnection connection = null;
        DataTable tables = null;
        List<string> columnsDateType = new List<string> { };
        List<string> columnsNames = new List<string> { };

        public Form1()
        {
            InitializeComponent();
        }
        
        private void DeleteBtnClick(object sender, EventArgs e)
        {
            if (connection == null || connection.State == ConnectionState.Closed)
                return;

            foreach (DataGridViewRow row in dataGridView.SelectedRows)
            {
                int id = int.Parse(row.Cells[0].Value.ToString());
                dataGridView.Rows.RemoveAt(row.Index);

                using (OleDbCommand command = new OleDbCommand($"DELETE FROM [{comboBoxTables.Text}] WHERE Id = {id}", connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        private void UpdateBtnclick(object sender, EventArgs e)
        {
            if (connection == null || connection.State == ConnectionState.Closed)
                return;
            string query = $"UPDATE {comboBoxTables.Text} SET ";
            query += textBoxValues.Text;
            query += " WHERE " + textBoxWhere.Text + ";";

            using (OleDbCommand command = new OleDbCommand(query, connection))
            {
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Attention!");
                }
            }

            string slct = $"SELECT * FROM {comboBoxTables.Text}";
            DataLoad(slct);
        }

        private void AddBtnClick(object sender, EventArgs e)
        {
            if (connection == null || connection.State == ConnectionState.Closed)
                return;

            string colNames = null;
            for (int i = 0; i != columnsNames.Count; i++)
            {
                colNames += $"[{columnsNames[i]}]";
                if (columnsNames.Count - i != 1)
                    colNames += ",";
            }

            string query = $"INSERT INTO {comboBoxTables.Text} ({colNames}) VALUES (";
            InsertFunc(ref query);
            query = query.Remove(query.Length - 2);
            query += ");";

            using (OleDbCommand command = new OleDbCommand(query, connection))
            {
                try
                {
                    int i = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void InsertFunc(ref string query)
        { 
            for (int i = 0; i != columnsNames.Count; i++)
            {
                string cell = null;
                try
                {
                    cell = dataGridView[columnsNames[i], dataGridView.RowCount - 2].Value.ToString();
                }
                catch (NullReferenceException ex)
                {
                    if (columnsNames[i] == "Id")
                    {
                        query += dataGridView.RowCount - 1 + ", ";
                        continue;
                    }
                    cell = null;
                    query += "NULL, ";
                    continue;
                }

                if (columnsDateType[i] == "Int32" || columnsDateType[i] == "Double")
                {
                    cell = cell.Replace(',', '.');
                    query += cell + ", ";
                }
                else if (columnsDateType[i] == "DateTime")
                {
                    string[] temp = null;
                    string tempStr = null;

                    temp = cell.Split('.');

                    tempStr = temp[0];
                    temp[0] = temp[1];
                    temp[1] = tempStr;

                    cell = null;
                    cell += "#";
                    for (int j = 0; j < 2; j++)
                    {
                        cell += temp[j] + "/";
                    }
                    cell += temp[2] + "#, ";
                    query += cell;
                }
                else if (columnsDateType[i] == "Boolean")
                    query += cell + ", ";
                else
                    query += "'" + cell + "', ";
            }
        }

        private void SelectionBtnClick(object sender, EventArgs e)
        {
            if (connection == null || connection.State == ConnectionState.Closed)
                return;

            string query = $"SELECT * FROM {comboBoxTables.Text}";

            if (textBoxWhere.Text != "")
                query += $" WHERE {textBoxWhere.Text};";
            else
                query += ";";
             
            DataLoad(query);
                    
        }

        private void LastBtnClick(object sender, EventArgs e)
        {
            try
            {
                dataGridView.ClearSelection();
                int count = dataGridView.Rows.Count - 2;
                dataGridView.Rows[count].Selected = true;
            }
            catch 
            {
                return;
            }
            
        }

        private void FirstBtnClick(object sender, EventArgs e)
        {
            try
            {
                dataGridView.ClearSelection();
                dataGridView.Rows[0].Selected = true;
            }
            catch
            {
                return;
            }
        }

        private void TablesDropDownClosed(object sender, EventArgs e)
        {
            dataGridView.Columns.Clear();
            using (OleDbCommand command = new OleDbCommand($"SELECT * FROM {comboBoxTables.Text} WHERE 1 = 0", connection))
            {
                OleDbDataReader reader = null;
                DataTable table = null;
                try
                {
                    reader = command.ExecuteReader();
                    table = reader.GetSchemaTable();
                    
                }
                catch
                {
                    return;
                }
                foreach (DataRow row in table.Rows)
                {
                    string columnName = row.Field<string>("ColumnName");
                    Type columnDT = row.Field<Type>("DataType");
                    dataGridView.Columns.Add(columnName.ToLower(), columnName);
                    columnsNames.Add(columnName);
                    columnsDateType.Add(columnDT.Name);
                }
            }

            DataLoad($"SELECT * FROM {comboBoxTables.Text};");
        }

        private void DataLoad(string query)
        {
            using (OleDbCommand command = new OleDbCommand(query, connection))
            {
                OleDbDataReader reader = null;
                try
                {
                    reader = command.ExecuteReader();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Attention!");
                    return;
                }
                dataGridView.Rows.Clear();
                while (reader.Read())
                {
                    List<string> tempList = new List<string> { };
                    int count = reader.FieldCount;
                    for (int i = 0; i != count; i++)
                        tempList.Add(reader[i].ToString());
                    dataGridView.Rows.Add(tempList.ToArray());
                }
                dataGridView.Sort(dataGridView.Columns[0], ListSortDirection.Ascending);
            }
        }

        private void TablesDropDown(object sender, EventArgs e)
        {
            comboBoxTables.Items.Clear();
            try
            {
                tables = connection.GetSchema("Tables", new string[] { null, null, null, "TABLE" });
            }
            catch
            {
                return;
            }
            foreach (DataRow row in tables.Rows)
            {
                string tableName = row["TABLE_NAME"].ToString();
                comboBoxTables.Items.Add(tableName);
            }
        }

        private void ConnectDBClick(object sender, EventArgs e)
        {
            string connectionString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = ";

            using (OpenFileDialog fd = new OpenFileDialog())
            {
                fd.Filter = "Access file (*.accdb)|*.accdb";
                if (fd.ShowDialog() == DialogResult.Cancel)
                    return;
                connectionString += fd.FileName + ";";
            }

            if (connection != null && connection.State == ConnectionState.Open)
            {
                connection.Close();
                connection.ConnectionString = connectionString;
            }
            else
                connection = new OleDbConnection(connectionString);
            connection.Open();
                
        }

        private void FormClose(object sender, FormClosingEventArgs e)
        {
            connection.Close();
        }

        private void ExitClick(object sender, EventArgs e)
        {
            Close();
        }

        private void Question1MouseClick(object sender, MouseEventArgs e)
        {
            MessageBoxIcon icon = MessageBoxIcon.Information;
            MessageBoxButtons button = MessageBoxButtons.OK;
            MessageBox.Show("New values. Examples of use:\n1. [Table_name] = 'something'\n" +
                "2. [Table_name] = something (without quotes for numbers)\n", "How it work?", button, icon);
        }

        private void Question2MouseClick(object sender, MouseEventArgs e)
        {
            MessageBoxIcon icon = MessageBoxIcon.Information;
            MessageBoxButtons button = MessageBoxButtons.OK;
            MessageBox.Show("Your condition. Examples of use:\n1. [Table_name] = 'something'\n" +
                "2. [Table_name] = something (without quotes for numbers)\n" +
                "3. [Table_name] = 'something' AND [Table_name] = 'something'", "How it work?", button, icon);
        }

        private void Question3MouseClick(object sender, MouseEventArgs e)
        {
            MessageBoxIcon icon = MessageBoxIcon.Information;
            MessageBoxButtons button = MessageBoxButtons.OK;
            MessageBox.Show("For inserting of new values, add it in the last row of data grid. And click add.", "How it work?", button, icon);
        }
    }
}
