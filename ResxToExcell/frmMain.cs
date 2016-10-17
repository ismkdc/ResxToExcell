using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace ResxToExcell
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            ofd.Filter = "Resx files(*.resx) | *.resx";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                lstFiles.Items.AddRange(ofd.FileNames);
            }
        }

        private void btnBasla_Click(object sender, EventArgs e)
        {
            if (lstFiles.Items.Count == 0)
            {
                MessageBox.Show("Resx dosyalarını seçiniz");
                return;
            }
     
            foreach (string item in lstFiles.Items)
            {
                Resx.resx_List.Clear();
                XmlDocument doc = new XmlDocument();
                doc.Load(item);
                foreach (XmlNode node in doc.SelectNodes("root/data"))
                {
                    if (node.Attributes != null)
                    {
                        Resx r = new Resx();
                        r.FileName = item;
                        r.Name = node.Attributes["name"].Value;
                        r.Value = node.ChildNodes[1].InnerText;
                        Resx.resx_List.Add(r);
                    }
                }
                XLWorkbook workbook = new XLWorkbook();
                DataTable dt = new DataTable("datas");
                dt.Columns.Add("Name", typeof(string));
                dt.Columns.Add("Value", typeof(string));
                foreach (Resx myResx in Resx.resx_List)
                {
                    dt.Rows.Add(myResx.Name, myResx.Value);
                }
                workbook.Worksheets.Add(dt);
                workbook.SaveAs(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\sa" + @"\" + Path.GetFileNameWithoutExtension(new FileInfo(Resx.resx_List[0].FileName).Name) + ".xlsx");
            }
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\sa";
            Process.Start("explorer.exe",path);
            lstFiles.Items.Clear();
            MessageBox.Show("Done!");
    
        }
    }
}
