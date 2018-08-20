using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace MemoryMaster
{
    public partial class DataViewer : Form
    {
        public DataViewer()
        {
            InitializeComponent();
        }
        public DataViewer(string pp)
        {
            InitializeComponent();
            dbsavePath = pp;
            System.Windows.Forms.FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                dbsavePath = fbd.SelectedPath;
                if (File.Exists(fbd.SelectedPath + "\\mcfg.dat"))
                {
                    label5.Text = "DB SAVE PATH: [SELECTED]";
                    panel1.Visible = true;
                    MemoryMaster mm = new MemoryMaster(dbsavePath);
                    Database db = mm.GetAllRecords();
                    string cnm = "";
                    foreach (var i in db.columns)
                    {
                        cnm += (" " + i);
                    }
                    label6.Text = "COLUMNS:" + cnm;
                    tabControl1.SelectedTab = tabControl1.TabPages[1];
                }
                else
                {
                    MessageBox.Show("Path:\"" + dbsavePath + "\" is not a correct MemoryMasterDB path", "MemoryMaster");

                }
            }
        }
        string dbsavePath = "";
        private void button1_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                label2.Text = "DB SAVE PATH: [SELECTED]";
                dbsavePath = fbd.SelectedPath;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(dbsavePath) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("YOU MUST ENTER ALL KEYWORDS", "MemoryMaster");
            }
            else
            {
                Directory.CreateDirectory(dbsavePath);
                MemoryMaster mm = new MemoryMaster(dbsavePath);
                mm.CreateDB(textBox2.Text.Split(',').ToList<string>());
                tabControl1.SelectedTab = tabControl1.TabPages[1];
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                dbsavePath = fbd.SelectedPath;
                if (File.Exists(fbd.SelectedPath + "\\mcfg.dat"))
                {
                    label5.Text = "DB SAVE PATH: [SELECTED]";
                    panel1.Visible =true;
                    MemoryMaster mm = new MemoryMaster(dbsavePath);
                    Database db = mm.GetAllRecords();
                    string cnm = "";
                    foreach(var i in db.columns)
                    {
                        cnm += (" " + i);
                    }
                    label6.Text = "COLUMNS:"+cnm;
                    tabControl1.SelectedTab = tabControl1.TabPages[1];
                }
                else
                {
                    MessageBox.Show("Path:\"" + dbsavePath + "\" is not a correct MemoryMasterDB path", "MemoryMaster");

                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(dbsavePath))
            {
                if (File.Exists(dbsavePath + "\\mcfg.dat"))
                {
                    listView1.Clear();
                    MemoryMaster mm = new MemoryMaster(dbsavePath);
                    Database db = mm.GetAllRecords();
                    foreach (var i in db.columns)
                    {
                        listView1.Columns.Add(i);
                    }
                    for (int i = 0; i < db.records.Count; i++)
                    {
                        var doc = db.records[i];
                        ListViewItem listViewItem = new ListViewItem();
                        int time = 0;
                        foreach (var j in doc.Values)
                        {
                            if (time == 0)
                            {
                                listViewItem = listView1.Items.Add(j);
                            }
                            else
                            {
                                listViewItem.SubItems.Add(j);
                            }
                            time++;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Path:\"" + dbsavePath + "\" is not a correct MemoryMasterDB path", "MemoryMaster");

                }
            }
            listView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            List<string> rec = textBox3.Text.Split(',').ToList<string>();
            MemoryMaster mm = new MemoryMaster(dbsavePath);
            Database db = mm.GetAllRecords();
            if(rec.Count==db.columns.Count)
            {
                mm.AddRecord(rec); tabControl1.SelectedTab = tabControl1.TabPages[1];
            }
            else
            {
                MessageBox.Show("INPUT ERROR", "MemoryMaster");
            }
            textBox3.Text = "";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            MemoryMaster mm = new MemoryMaster(dbsavePath);
            Dictionary<string, string> dd = new Dictionary<string, string>();
            dd.Add(textBox6.Text, textBox7.Text);
            mm.UpdateRecord(textBox4.Text, textBox5.Text,dd );
            tabControl1.SelectedTab = tabControl1.TabPages[1];
        }

        private void button7_Click(object sender, EventArgs e)
        {
            MemoryMaster mm = new MemoryMaster(dbsavePath);
            mm.DeleteRecord(textBox4.Text, textBox5.Text);
            tabControl1.SelectedTab = tabControl1.TabPages[1];
        }

        private void button8_Click(object sender, EventArgs e)
        {
            MemoryMaster mm = new MemoryMaster(dbsavePath);
            mm.CreateOrReplaceColumns(textBox8.Text.Split(',').ToList<string>());
        }

        private void tabPage2_Paint(object sender, PaintEventArgs e)
        {
            button4_Click(sender, e);
        }

        private void DataViewer_Load(object sender, EventArgs e)
        {

        }
    }
}
