using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.IO;
using System.Runtime.InteropServices;

namespace Discount
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public bool search;
        public bool st = true;
        public string file_name;
        public string file_name_out;
        public string data_source;

        //import dll from use configuration file
        [DllImport("kernel32.dll")]
        static extern uint GetPrivateProfileString(
        string lpAppName,
        string lpKeyName,
        string lpDefault,
        StringBuilder lpReturnedString,
        uint nSize,
        string lpFileName);

        private void loadconfig()
        {
            StringBuilder buffer = new StringBuilder(50,50);

            GetPrivateProfileString("SETTINGS", "file_name", "DISCCLI.DBF", buffer, 50, Environment.CurrentDirectory + "\\config.ini");

            file_name = buffer.ToString();

            GetPrivateProfileString("SETTINGS", "data_source", "", buffer, 50, Environment.CurrentDirectory + "\\config.ini");

            if (buffer.Length > 0)
                data_source = buffer.ToString();
            else
                data_source = Environment.CurrentDirectory;

            GetPrivateProfileString("SETTINGS", "file_name_out", "out", buffer, 50, Environment.CurrentDirectory + "\\config.ini");

            if (buffer.Length > 0)
                file_name_out = buffer.ToString();
            else
                file_name_out = "out";

        }

        private void load_dbf()
        {
            dataGridView1.Rows.Clear();

            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = "Provider=vfpoledb.1;Data Source=" + data_source + ";Collating Sequence=MACHINE;CODEPAGE=866";
            conn.Open();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;
            
            if(file_name == "DISCCLI.DBF")
                cmd.CommandText = "SELECT BARCODE,PERCENT FROM " + file_name + "";
            else
                cmd.CommandText = "SELECT BARCODE,XPERCENT FROM " + file_name + "";

            OleDbDataReader dr;

            try
            {
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    dataGridView1.Rows.Add(dr.GetValue(0), Convert.ToInt32(dr.GetValue(1)));
                }
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                //TODO предложить ввести путь к файлу 
                log_write(ex.Message, "Exception", "Exception");
                MessageBox.Show(ex.Message);
            }
            catch (System.Exception ex)
            {
                log_write(ex.Message, "Exception", "Exception");
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();

                //test this
                textbox_num.Focus();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            log_write("Приложение запущено", "INFO", "Discount");

            loadconfig();

            load_dbf();

            check_dbf();

        }

        private void textbox_num_TextChanged(object sender, EventArgs e)
        {
            int len = textbox_num.TextLength;

            if (len == 0 || !search )
                return;

            query(textbox_num.Text);

        }

        private void textbox_num_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                if (textbox_num.Text.Length - 1 < 0)
                    return;

                try
                {
                    query(textbox_num.Text.Substring(0, textbox_num.Text.Length - 1));
                }
                catch (System.Exception ex)
                {
                    log_write(ex.Message, "Exception", "Exception");
                }
                
            }

            else if (e.KeyCode == Keys.Enter)
                textbox_percent.Focus();

            else if (e.KeyCode == Keys.Escape)
            {
                textbox_num.Text = "";

                dataGridView1.Rows.Clear();
                load_dbf();
            }
        }

        private void textbox_num_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == '!')
                search = true;
            else
                search = false;
        }

        private void query(string bar)
        {
            dataGridView1.Rows.Clear();

            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = "Provider=vfpoledb.1;Data Source=" + data_source + ";Collating Sequence=MACHINE;CODEPAGE=866";
            conn.Open();
            OleDbCommand cmd = new OleDbCommand();
            cmd.Connection = conn;

            if (file_name == "DISCCLI.DBF")
                cmd.CommandText = "SELECT BARCODE,PERCENT FROM " + file_name + " WHERE BARCODE LIKE '%" + bar + "%'";
            else
            {
                cmd.CommandText = "SELECT BARCODE,XPERCENT FROM " + file_name + " WHERE BARCODE LIKE '%" + bar + "%'";
            }

            OleDbDataReader dr;
            dr = cmd.ExecuteReader();

            //log_write(cmd.CommandText, "SELECT", "query_select");

            try
            {
                if (dr == null || !dr.HasRows)
                {
                    button_add.Enabled = true;
                }
                else
                {
                    button_add.Enabled = false;
                }


                while (dr.Read())
                {
                    dataGridView1.Rows.Add(dr.GetValue(0), Convert.ToInt32(dr.GetValue(1)));
                }
            }
            catch (System.Exception ex)
            {
                log_write(ex.Message, "Exception", "Exception");
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        private void log_write(string str,string reason,string logname)
        {
            string EntryTime = DateTime.Now.ToLongTimeString();             
            string EntryDate = DateTime.Today.ToShortDateString();
            string fileName = "log/" + EntryDate + "/" + logname + ".log";  //log + data +logname ? 

            if (!Directory.Exists(Environment.CurrentDirectory + "/log/" + EntryDate + "/"))
            {
                Directory.CreateDirectory((Environment.CurrentDirectory + "/log/" + EntryDate + "/"));
            }

            try
            {
                StreamWriter sw = new StreamWriter(fileName, true, System.Text.Encoding.UTF8);
                sw.WriteLine("["+EntryDate+"]["+EntryTime+"]["+reason+"]"+" "+str);
                sw.Close();
            }
            catch(Exception ex)
            {
                log_write(ex.Message, "Exception", "Exception");
                MessageBox.Show(ex.Message);
            }
        }

        private void check_dubl()
        {
            //TODO Проверку на дубликаты 
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            log_write("Приложение остановлено", "INFO", "Discount");
            Application.Exit();
        }

        private void button_insert_Click(object sender, EventArgs e)
        {
            check_dbf();

            disable_button();

            button_insert.ForeColor = Color.Red;

            foreach (DataGridViewRow dr in dataGridView1.SelectedRows)
            {
                string n = dr.Cells[0].Value.ToString().Replace(" ", "").Replace("!", "");
                UInt64 p;

                if (textbox_percent.Text.Length > 0)
                    p = Convert.ToUInt64(textbox_percent.Text);
                else
                    p = Convert.ToUInt64(dr.Cells[1].Value);

                if (n.Length < 13)
                {
                    string minN = n;
                    string maxN = n;

                    for (int i = n.Length; i < 13; i++)
                    {
                        minN += "0";
                        maxN += "9";
                    }

                    UInt64 mxN = Convert.ToUInt64(maxN);

                    progressBar1.Minimum = 0;
                    progressBar1.Maximum = Convert.ToInt32((Convert.ToUInt64(maxN) - Convert.ToUInt64(minN)));

                    for (UInt64 i = Convert.ToUInt64(minN); i <= mxN; i++)
                    {
                        //функция добавляющая новые значения к запросу 
                        insert_table(file_name_out, i.ToString(), p);

                        Application.DoEvents();

                        progressBar1.Value++;

                        if (progressBar1.Value == progressBar1.Maximum)
                            progressBar1.Value = 0;

                        //экстренный выход из цикла в случае ошибки
                        if (!st)
                            i = mxN;
                    }
                }
                else
                {
                    try
                    {
                        insert_table(file_name_out, n , Convert.ToUInt64(p));

                        dataGridView1.Rows.Clear();
                        load_dbf();
                    }
                    catch (System.Exception ex)
                    {
                        log_write(ex.Message, "Exception", "Exception");
                    }
                }
            }

            dataGridView1.Rows.Clear();
            load_dbf();

            enable_button();

            button_insert.ForeColor = Color.Green;

        }

        private void create_table(string name)
        {
            OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + data_source + ";Extended Properties=dBASE III;User ID=Admin;Password=");
            OleDbCommand create = conn.CreateCommand();

            try
            {
                conn.Open();

                //структура выходной таблицы 
                create.CommandText = "CREATE TABLE " + name + "(barcode Character , @percent INTEGER  )";

                create.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                log_write(ex.Message, "Exception", "Exception");          
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

        }
        private void insert_table(string name,string val1,UInt64 val2)
        {
            OleDbConnection conn = new OleDbConnection();               
            OleDbCommand cmd = new OleDbCommand();
           
            conn.ConnectionString = "Provider=vfpoledb;Data Source=" + data_source + ";Collating Sequence=MACHINE;CODEPAGE=866";

            cmd.Connection = conn;

            try
            {
                conn.Open();
                cmd.CommandText = "SELECT BARCODE,XPERCENT FROM " + name + " WHERE barcode = '" + val1 + "'";
                OleDbDataReader dr;

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    log_write("DELETE FROM " + name + " WHERE barcode = " + Convert.ToUInt64(dr.GetValue(0)) + " AND PERCENT = "+Convert.ToUInt64(dr.GetValue(1))+"", "SELECT", "backup");
                }

                if (!dr.IsClosed)
                    dr.Close();
            }
            catch (System.Exception ex)
            {
                log_write(ex.Message, "Exception", "Exception");
            }

            try
            {
                //зачистка дублирующих записей;
                //TODO компрессия
                cmd.CommandText = "DELETE FROM " + name + " WHERE barcode = '" + val1 + "'";
                log_write("DELETE FROM " + name + " WHERE barcode = '" + val1 + "'", "DELETE", "backup");

                cmd.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                log_write(ex.Message, "Exception", "Exception");
            }

            try
            {
                cmd.CommandText = "INSERT INTO " + name + " (barcode, xpercent) VALUES ('" + Convert.ToString(val1).Replace(" ","") + "'," + val2 + ")";
                log_write("INSERT INTO " + name + " (barcode, xpercent) VALUES (" + val1.Replace(" ", "") + "," + val2 + ")", "INSERT", "backup");

                cmd.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                label_status.Visible = true;
                label_status.ForeColor = Color.Red;
                label_status.Text = "Отказ!";
                timer_msg_clear.Enabled = true;

                log_write(ex.Message, "Exception", "Exception");

                MessageBox.Show(ex.Message);

                //для экстренного выхода из функции обозначаем статус операции
                st = false;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();

                if (progressBar1.Value == progressBar1.Maximum)
                {
                    clear_window();
                }

                if (st)
                {
                    label_status.ForeColor = Color.Green;
                    label_status.Visible = true; 
                    label_status.Text = "Успешно";
                    timer_msg_clear.Enabled = true;

                    textbox_num.Focus();
                }
            }
        }

        private void timer_msg_clear_Tick(object sender, EventArgs e)
        {
            label_status.Visible = false;
            timer_msg_clear.Enabled = false;
            label_status.Text = "";
            label_status.ForeColor = Color.Green;

            button_add.ForeColor = SystemColors.ControlText;
        }

        private void clear_window()
        {
            textbox_percent.Text = "";
            textbox_num.Text = "";
            dataGridView1.Rows.Clear();
            load_dbf();
        }

        private void check_dbf()
        {
            if (!File.Exists(Environment.CurrentDirectory +"\\"+ file_name_out+".dbf" ))
            {
                create_table(file_name_out);
                log_write("Создана таблица file_name_out", "CREATE", "Discount");
            }
        }

        private void button_add_Click(object sender, EventArgs e)
        {
            if (textbox_num.Text == "" || textbox_percent.Text == "")
            {
                MessageBox.Show("Введены не все данные для добавления записи!");
                return;
            }

            string message = "Добавить?";
            string caption = "Отмена";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;
            result = MessageBox.Show(message, caption, buttons);
            if (result == System.Windows.Forms.DialogResult.Yes) 
            {
                insert_table(file_name_out, textbox_num.Text, Convert.ToUInt64(textbox_percent.Text));
            }
        }

        private void textbox_percent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                textbox_num.Focus();
            }
            if (e.KeyCode == Keys.Enter)
            {
                if (textbox_num.Text == "")
                    textbox_num.Focus();
                if (textbox_percent.Text == "")
                    return;
                else
                {
                    button_add.ForeColor = Color.Green;
                    button_add.Focus();
                }
            }
        }

        private void date_backup()
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                file_name = "OUT.DBF";
                dataGridView1.Rows.Clear();

                load_dbf();

                check_dbf();
            }
            else
            {
                file_name = "DISCCLI.DBF";
                dataGridView1.Rows.Clear();

                load_dbf();

                check_dbf();
            }

        }

        private void button_delete_Click(object sender, EventArgs e)
        {
            string message = "Вы действительно хотите удалить выделеную запись?";
            string caption = "Отмена";
            MessageBoxButtons buttons = MessageBoxButtons.YesNo;
            DialogResult result;
            result = MessageBox.Show(message, caption, buttons);

            if (result != System.Windows.Forms.DialogResult.Yes)
                return;

            foreach (DataGridViewRow dr in dataGridView1.SelectedRows)
            {
                Application.DoEvents();

                string n = dr.Cells[0].Value.ToString().Replace(" ", "").Replace("!", "");
                UInt64 p;

                if (textbox_percent.Text.Length > 0)
                    p = Convert.ToUInt64(textbox_percent.Text);
                else
                    p = Convert.ToUInt64(dr.Cells[1].Value);

                if (n.Length < 13)
                {
                    string minN = n;
                    string maxN = n;

                    for (int i = n.Length; i < 13; i++)
                    {
                        minN += "0";
                        maxN += "9";
                    }

                    UInt64 mxN = Convert.ToUInt64(maxN);

                    progressBar1.Minimum = 0;
                    progressBar1.Maximum = Convert.ToInt32((Convert.ToUInt64(maxN) - Convert.ToUInt64(minN)));

                    for (UInt64 i = Convert.ToUInt64(minN); i <= mxN; i++)
                    {
                        //функция удаления строк  
                        delete_table(file_name_out, i.ToString(), p);

                        Application.DoEvents();

                        progressBar1.Value++;

                        //экстренный выход из цикла в случае ошибки
                        if (!st)
                            i = mxN;
                    }
                    progressBar1.Value = 0;               
                }
                else
                {
                    try
                    {
                        delete_table(file_name_out, n , p);
                    }
                    catch (System.Exception ex)
                    {
                        log_write(ex.Message, "Exception", "Exception");
                    }
                }   
            }  
        }

        private void delete_table(string name, string val1, UInt64 val2)
        {
            disable_button();

            OleDbConnection conn = new OleDbConnection();
            OleDbCommand cmd = new OleDbCommand();

            conn.ConnectionString = "Provider=vfpoledb;Data Source=" + data_source + ";Collating Sequence=MACHINE;CODEPAGE=866";

            cmd.Connection = conn;

            try
            {
                OleDbDataReader dr;

                conn.Open();
                cmd.CommandText = "SELECT BARCODE,XPERCENT FROM " + name + " WHERE barcode = '" + val1 + "'";

                dr = cmd.ExecuteReader();

                while (dr.Read())
                    log_write("BARCODE: "+ dr.GetString(0).Replace(" ", "") + " PERCENT: " + Convert.ToUInt64(dr.GetValue(1)), "SELECT", "backup");

                if (!dr.IsClosed)
                    dr.Close();
            }

            catch (System.Exception ex)
            {
                log_write(ex.Message, "Exception", "Exception");
                st = false;
            }

            try
            {
                cmd.CommandText = "DELETE FROM " + name + " WHERE barcode = '" + val1 + "'";
                log_write("DELETE FROM " + name + " WHERE barcode = '" + val1.Replace(" ","") + "'", "DELETE", "backup");

                cmd.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                log_write(ex.Message, "Exception", "Exception");
                st = false;
            }

            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();

                if (st)
                {
                    label_status.ForeColor = Color.Green;
                    label_status.Visible = true;
                    label_status.Text = "Успешно";
                    timer_msg_clear.Enabled = true;

                    textbox_num.Focus();
                }
                else     
                {
                    label_status.Visible = true;
                    label_status.ForeColor = Color.Red;
                    label_status.Text = "Отказ!";

                    timer_msg_clear.Enabled = true;
                }
            }

            if (progressBar1.Value == progressBar1.Maximum -1)
            {
                dataGridView1.Rows.Clear();
                load_dbf();

                enable_button();
            }
        }

        private void pack_dbf()
        {
            OleDbConnection conn = new OleDbConnection();
            OleDbCommand cmd = new OleDbCommand();

            conn.ConnectionString = "Provider=vfpoledb;Data Source=" + data_source + ";Collating Sequence=MACHINE;CODEPAGE=866";

            cmd.Connection = conn;

            try
            {
                OleDbDataReader dr;

                conn.Open();

                cmd.CommandText = "SELECT BARCODE,XPERCENT FROM " + file_name_out;

                dr = cmd.ExecuteReader();

                create_table("back_" + file_name_out);
                log_write("Создаем временную таблицу back_" + file_name_out, "CREATE", "Discount");

                progressBar1.Minimum = 0;
                progressBar1.Maximum = dr.RecordsAffected;

                disable_button();

                while (dr.Read())
                {
                    Application.DoEvents();

                    progressBar1.Value++;

                    insert_table("back_" + file_name_out,dr.GetString(0),Convert.ToUInt64(dr.GetValue(1)));
                }

                if (progressBar1.Value == progressBar1.Maximum)
                {
                    progressBar1.Value = 0;
                    enable_button();
                }

                if (conn.State == ConnectionState.Open)
                    conn.Close();

                dr.Close();

                if (File.Exists(Environment.CurrentDirectory + "\\" + file_name_out + ".dbf"))
                {
                    string EntryDate = DateTime.Today.ToShortDateString();
                    string EntryTime = DateTime.Now.ToLongTimeString().Replace(":","-")+"_";

                    if (!Directory.Exists(Environment.CurrentDirectory + "\\backup\\" + EntryDate + "\\"))
                    {
                        Directory.CreateDirectory(Environment.CurrentDirectory + "\\backup\\" + EntryDate + "\\");
                        log_write("Создаем папку " + Environment.CurrentDirectory + "\\backup\\" + EntryDate + "\\", "CREATE", "Discount");
                    }

                    File.Copy(data_source + "\\" + "back_" + file_name_out + ".dbf", Environment.CurrentDirectory + "\\backup\\" + EntryDate + "\\" + EntryTime + file_name_out + ".dbf");
                    log_write("Делаем копию back_" + file_name_out, "CREATE", "Discount");
                    File.Delete(Environment.CurrentDirectory + "\\" + file_name_out + ".dbf");
                    log_write("Удаляем оригинальный файл " + file_name_out, "DELETE", "Discount");
                }

                File.Move(data_source + "\\" + "back_" + file_name_out + ".dbf" , Environment.CurrentDirectory + "\\" + file_name_out + ".dbf");
                log_write("Подменяем оригинальный файл дубликатом " + file_name_out, "REPLACE", "Discount");
            }

            catch (System.Exception ex)
            {
                log_write(ex.Message, "Exception", "Exception");
            }

            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }

        }

        private void disable_button()
        {
            checkBox1.Enabled = false;
            button_delete.Enabled = false;
            button_insert.Enabled = false;
            button1.Enabled = false;
            textbox_num.Enabled = false;
            textbox_percent.Enabled = false;
        }
        private void enable_button()
        {
            checkBox1.Enabled = true;
            button_delete.Enabled = true;
            button_insert.Enabled = true;
            button1.Enabled = true;
            textbox_num.Enabled = true;
            textbox_percent.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pack_dbf();
        }

    }
}

