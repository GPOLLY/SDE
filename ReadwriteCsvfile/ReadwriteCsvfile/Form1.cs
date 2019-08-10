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


namespace ReadwriteCsvfile
{
    public partial class Form1 : Form
    {
        public Form1()
            
        
        {
            InitializeComponent();
        }
        //Read csv file
        public DataTable ReadCsv(string fileName)
        {
            DataTable dt = new DataTable("Data");
             using(OleDbConnection cn =new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=\""+
                Path.GetDirectoryName(fileName) + "\";Extended Properties='text;HDR=yes;FMT=Delimited(,)';"))
            {
                //Execute select query
                using (OleDbCommand cmd = new OleDbCommand(string.Format("select *from [{0}]", new FileInfo(fileName).Name), cn))
                {
                    cn.Open();
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(cmd))
                    {
                        adapter.Fill(dt);

                        
                       

                    }
                }
            }
             return dt;
        }

       
        
        private void btn_upload_Click(object sender, EventArgs e)
        {

             try
            { 
                

                 //Open file dialog, allows you to select a csv file
                using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "CSV|*.csv", ValidateNames = true, Multiselect = false })
                {
                    if (ofd.ShowDialog() == DialogResult.OK)
                        dataGridView1.DataSource = ReadCsv(ofd.FileName);
                        
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //close button
        private void btnclose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

       
