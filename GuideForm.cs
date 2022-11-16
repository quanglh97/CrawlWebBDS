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
    public partial class GuideForm : Form
    {
        public GuideForm()
        {
            InitializeComponent();
            setGuiData();
        }

        public void getGuideData()
        {
            var data = infoTBox.Text;
            var pathStringGuideData = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GuideData.txt");
            File.WriteAllText(pathStringGuideData, data);
        }

        public void setGuiData()
        {
            var pathStringGuideData = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "GuideData.txt");
            if (File.Exists(pathStringGuideData))
            {
                var data = File.ReadAllText(pathStringGuideData);
                infoTBox.Text = data;
            }
        }

        private void saveGuideInfoBtn_Click(object sender, EventArgs e)
        {
            getGuideData();
        }
    }

    
}
