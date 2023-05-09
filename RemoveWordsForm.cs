using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrawlWebBDS
{
    public partial class RemoveWordsForm : Form
    {
        public string web = "";
        public RemoveWordsForm(string web)
        {
            InitializeComponent();
            this.web = web;
            getGuiData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(this.web == "salenha")
            {
                var data = RMWTBox.Text.Trim();
                Contances.lsRemoveWordsSaleNha = data.Split(new[] { "\r\n" }, StringSplitOptions.None).ToList();
            }
            else if(this.web == "nguonnhamoi")
            {
                var data = RMWTBox.Text.Trim();
                Contances.lsRemoveWordsNNM = data.Split(new[] { "\r\n" }, StringSplitOptions.None).ToList();
            }
            setGuideData();
        }

        public void setGuideData()
        {
            var data = RMWTBox.Text;
            if (this.web == "salenha")
            {
                
                var pathStringRMData = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RemoveSaleNhaData.txt");
                File.WriteAllText(pathStringRMData, data);
            }
            else if (this.web == "nguonnhamoi")
            {
                var pathStringRMData = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RemoveNNMData.txt");
                File.WriteAllText(pathStringRMData, data);
            }
        }

        public void getGuiData()
        {
            if (this.web == "salenha")
            {
                var pathStringRMData = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RemoveSaleNhaData.txt");
                if (File.Exists(pathStringRMData))
                {
                    var data = File.ReadAllText(pathStringRMData);
                    RMWTBox.Text = data;
                }
            }
            else if(this.web == "nguonnhamoi")
            {
                var pathStringRMData = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "RemoveNNMData.txt");
                if (File.Exists(pathStringRMData))
                {
                    var data = File.ReadAllText(pathStringRMData);
                    RMWTBox.Text = data;
                }
            }
            
        }

        
    }
}
