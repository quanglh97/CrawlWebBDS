using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrawlWebBDS
{
    public partial class Form1 : Form
    {
        //[DllImport("User32.dll", CharSet = CharSet.Auto)]
        //public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        //[DllImport("User32.dll")]
        //private static extern IntPtr GetWindowDC(IntPtr hWnd);

        //protected override void WndProc(ref Message m)
        //{
        //    base.WndProc(ref m);
        //    const int WM_NCPAINT = 0x85;
        //    if (m.Msg == WM_NCPAINT)
        //    {
        //        IntPtr hdc = GetWindowDC(m.HWnd);
        //        if ((int)hdc != 0)
        //        {
        //            Graphics g = Graphics.FromHdc(hdc);
        //            g.FillRectangle(Brushes.Green, new Rectangle(0, -40, 4800, 50));
        //            g.Flush();
        //            ReleaseDC(m.HWnd, hdc);
        //        }
        //    }


        //}


        //Lienhechinhchu.com
        BackgroundWorker m_oWorker_LHCC_COM = new BackgroundWorker();
        BackgroundWorker m_autoWorker_LHCC_COM = new BackgroundWorker();

        //batdongsan44.com
        BackgroundWorker m_oWorker_BDS44 = new BackgroundWorker();
        BackgroundWorker m_autoWorker_BDS44 = new BackgroundWorker();

        //salenha.com
        BackgroundWorker m_oWorker_SALENHA = new BackgroundWorker();
        BackgroundWorker m_autoWorker_SALENHA = new BackgroundWorker();

        //salenha_vip.com
        BackgroundWorker m_oWorker_SALENHA_VIP = new BackgroundWorker();
        BackgroundWorker m_autoWorker_SALENHA_VIP = new BackgroundWorker();

        //nguonchinhchu.com
        BackgroundWorker m_oWorker_NCC = new BackgroundWorker();
        BackgroundWorker m_autoWorker_NCC = new BackgroundWorker();

        //nguonnhachinhchu.com
        BackgroundWorker m_oWorker_NNCC = new BackgroundWorker();
        BackgroundWorker m_autoWorker_NNCC = new BackgroundWorker();

        //tinhchinhchu.vn
        BackgroundWorker m_oWorker_TCC = new BackgroundWorker();
        BackgroundWorker m_autoWorker_TCC = new BackgroundWorker();

        //nguonnhamoi
        BackgroundWorker m_oWorker_NNM = new BackgroundWorker();
        BackgroundWorker m_autoWorker_NNM = new BackgroundWorker();


        BackgroundWorker statusWorker = new BackgroundWorker();

        public Form1()
        {
            InitializeComponent();

            this.FormClosing += FormMain_FormClosing;

            statusWorker.DoWork += myBackgroundWorker_DoWork_Status;
            statusWorker.ProgressChanged += myBackgroundWorker_ProgressChanged_Status;
            statusWorker.WorkerReportsProgress = true;
            statusWorker.WorkerSupportsCancellation = true;

            //Lienhechinhchu.com
            m_oWorker_LHCC_COM.DoWork += myBackgroundWorker_DoWork_LHCC_COM;
            m_oWorker_LHCC_COM.ProgressChanged += myBackgroundWorker_ProgressChanged_LHCC_COM;
            m_oWorker_LHCC_COM.RunWorkerCompleted += myBackgroundWorker_RunWorkerCompleted_LHCC_COM;
            m_oWorker_LHCC_COM.WorkerReportsProgress = true;
            m_oWorker_LHCC_COM.WorkerSupportsCancellation = true;

            m_autoWorker_LHCC_COM.DoWork += myBackgroundAutoWorker_DoWork_LHCC_COM;
            m_autoWorker_LHCC_COM.ProgressChanged += myBackgroundAutoWorker_ProgressChanged_LHCC_COM;
            m_autoWorker_LHCC_COM.RunWorkerCompleted += myBackgroundAutoWorker_RunWorkerCompleted_LHCC_COM;
            m_autoWorker_LHCC_COM.WorkerReportsProgress = true;
            m_autoWorker_LHCC_COM.WorkerSupportsCancellation = true;

            autoStartTime_LHCC_COM.ShowUpDown = true;
            autoStopTime_LHCC_COM.ShowUpDown = true;


            //batdongsan44.com
            m_oWorker_BDS44.DoWork += myBackgroundWorker_DoWork_BDS44;
            m_oWorker_BDS44.ProgressChanged += myBackgroundWorker_ProgressChanged_BDS44;
            m_oWorker_BDS44.RunWorkerCompleted += myBackgroundWorker_RunWorkerCompleted_BDS44;
            m_oWorker_BDS44.WorkerReportsProgress = true;
            m_oWorker_BDS44.WorkerSupportsCancellation = true;

            m_autoWorker_BDS44.DoWork += myBackgroundAutoWorker_DoWork_BDS44;
            m_autoWorker_BDS44.ProgressChanged += myBackgroundAutoWorker_ProgressChanged_BDS44;
            m_autoWorker_BDS44.RunWorkerCompleted += myBackgroundAutoWorker_RunWorkerCompleted_BDS44;
            m_autoWorker_BDS44.WorkerReportsProgress = true;
            m_autoWorker_BDS44.WorkerSupportsCancellation = true;

            autoStartTime_BDS44.ShowUpDown = true;
            autoStopTime_BDS44.ShowUpDown = true;

            //salenha.com
            m_oWorker_SALENHA.DoWork += myBackgroundWorker_DoWork_SALENHA;
            m_oWorker_SALENHA.ProgressChanged += myBackgroundWorker_ProgressChanged_SALENHA;
            m_oWorker_SALENHA.RunWorkerCompleted += myBackgroundWorker_RunWorkerCompleted_SALENHA;
            m_oWorker_SALENHA.WorkerReportsProgress = true;
            m_oWorker_SALENHA.WorkerSupportsCancellation = true;

            m_autoWorker_SALENHA.DoWork += myBackgroundAutoWorker_DoWork_SALENHA;
            m_autoWorker_SALENHA.ProgressChanged += myBackgroundAutoWorker_ProgressChanged_SALENHA;
            m_autoWorker_SALENHA.RunWorkerCompleted += myBackgroundAutoWorker_RunWorkerCompleted_SALENHA;
            m_autoWorker_SALENHA.WorkerReportsProgress = true;
            m_autoWorker_SALENHA.WorkerSupportsCancellation = true;

            autoStartTime_SALENHA.ShowUpDown = true;
            autoStopTime_SALENHA.ShowUpDown = true;

            //SALENHA_VIP.com
            m_oWorker_SALENHA_VIP.DoWork += myBackgroundWorker_DoWork_SALENHA_VIP;
            m_oWorker_SALENHA_VIP.ProgressChanged += myBackgroundWorker_ProgressChanged_SALENHA_VIP;
            m_oWorker_SALENHA_VIP.RunWorkerCompleted += myBackgroundWorker_RunWorkerCompleted_SALENHA_VIP;
            m_oWorker_SALENHA_VIP.WorkerReportsProgress = true;
            m_oWorker_SALENHA_VIP.WorkerSupportsCancellation = true;

            m_autoWorker_SALENHA_VIP.DoWork += myBackgroundAutoWorker_DoWork_SALENHA_VIP;
            m_autoWorker_SALENHA_VIP.ProgressChanged += myBackgroundAutoWorker_ProgressChanged_SALENHA_VIP;
            m_autoWorker_SALENHA_VIP.RunWorkerCompleted += myBackgroundAutoWorker_RunWorkerCompleted_SALENHA_VIP;
            m_autoWorker_SALENHA_VIP.WorkerReportsProgress = true;
            m_autoWorker_SALENHA_VIP.WorkerSupportsCancellation = true;

            autoStartTime_SALENHA_VIP.ShowUpDown = true;
            autoStopTime_SALENHA_VIP.ShowUpDown = true;

            //NCC.com
            m_oWorker_NCC.DoWork += myBackgroundWorker_DoWork_NCC;
            m_oWorker_NCC.ProgressChanged += myBackgroundWorker_ProgressChanged_NCC;
            m_oWorker_NCC.RunWorkerCompleted += myBackgroundWorker_RunWorkerCompleted_NCC;
            m_oWorker_NCC.WorkerReportsProgress = true;
            m_oWorker_NCC.WorkerSupportsCancellation = true;

            m_autoWorker_NCC.DoWork += myBackgroundAutoWorker_DoWork_NCC;
            m_autoWorker_NCC.ProgressChanged += myBackgroundAutoWorker_ProgressChanged_NCC;
            m_autoWorker_NCC.RunWorkerCompleted += myBackgroundAutoWorker_RunWorkerCompleted_NCC;
            m_autoWorker_NCC.WorkerReportsProgress = true;
            m_autoWorker_NCC.WorkerSupportsCancellation = true;

            autoStartTime_NCC.ShowUpDown = true;
            autoStopTime_NCC.ShowUpDown = true;

            //nguonnhachinhchu.com
            m_oWorker_NNCC.DoWork += myBackgroundWorker_DoWork_NNCC;
            m_oWorker_NNCC.ProgressChanged += myBackgroundWorker_ProgressChanged_NNCC;
            m_oWorker_NNCC.RunWorkerCompleted += myBackgroundWorker_RunWorkerCompleted_NNCC;
            m_oWorker_NNCC.WorkerReportsProgress = true;
            m_oWorker_NNCC.WorkerSupportsCancellation = true;

            m_autoWorker_NNCC.DoWork += myBackgroundAutoWorker_DoWork_NNCC;
            m_autoWorker_NNCC.ProgressChanged += myBackgroundAutoWorker_ProgressChanged_NNCC;
            m_autoWorker_NNCC.RunWorkerCompleted += myBackgroundAutoWorker_RunWorkerCompleted_NNCC;
            m_autoWorker_NNCC.WorkerReportsProgress = true;
            m_autoWorker_NNCC.WorkerSupportsCancellation = true;

            autoStartTime_NNCC.ShowUpDown = true;
            autoStopTime_NNCC.ShowUpDown = true;

            //tinchinhchu.net.vn
            m_oWorker_TCC.DoWork += myBackgroundWorker_DoWork_TCC;
            m_oWorker_TCC.ProgressChanged += myBackgroundWorker_ProgressChanged_TCC;
            m_oWorker_TCC.RunWorkerCompleted += myBackgroundWorker_RunWorkerCompleted_TCC;
            m_oWorker_TCC.WorkerReportsProgress = true;
            m_oWorker_TCC.WorkerSupportsCancellation = true;

            m_autoWorker_TCC.DoWork += myBackgroundAutoWorker_DoWork_TCC;
            m_autoWorker_TCC.ProgressChanged += myBackgroundAutoWorker_ProgressChanged_TCC;
            m_autoWorker_TCC.RunWorkerCompleted += myBackgroundAutoWorker_RunWorkerCompleted_TCC;
            m_autoWorker_TCC.WorkerReportsProgress = true;
            m_autoWorker_TCC.WorkerSupportsCancellation = true;

            autoStartTime_TCC.ShowUpDown = true;
            autoStopTime_TCC.ShowUpDown = true;

            //nguonnhamoi
            m_oWorker_NNM.DoWork += myBackgroundWorker_DoWork_NNM;
            m_oWorker_NNM.ProgressChanged += myBackgroundWorker_ProgressChanged_NNM;
            m_oWorker_NNM.RunWorkerCompleted += myBackgroundWorker_RunWorkerCompleted_NNM;
            m_oWorker_NNM.WorkerReportsProgress = true;
            m_oWorker_NNM.WorkerSupportsCancellation = true;

            m_autoWorker_NNM.DoWork += myBackgroundAutoWorker_DoWork_NNM;
            m_autoWorker_NNM.ProgressChanged += myBackgroundAutoWorker_ProgressChanged_NNM;
            m_autoWorker_NNM.RunWorkerCompleted += myBackgroundAutoWorker_RunWorkerCompleted_NNM;
            m_autoWorker_NNM.WorkerReportsProgress = true;
            m_autoWorker_NNM.WorkerSupportsCancellation = true;

            autoStartTime_NNM.ShowUpDown = true;
            autoStopTime_NNM.ShowUpDown = true;

            new Thread(new ParameterizedThreadStart(UpdateChromeDriver.CheckAnDownloadChromeDriver))
            {
                IsBackground = true,
            }.Start();


            try
            {
                statusWorker.RunWorkerAsync();

                defaultSetting_LHCC_COM();
                defaultSetting_BDS44();
                defaultSetting_SALENHA();
                defaultSetting_SALENHA_VIP();
                defaultSetting_NCC();
                defaultSetting_NNCC();
                defaultSetting_TCC();
                defaultSetting_NNM();
            }
            catch { }

            SetTabHeader(TabControl1.TabPages[0], Color.FromArgb(255, 66, 66, 66));
            for (int i = 1; i < TabControl1.TabPages.Count; i++)
            {
                SetTabHeader(TabControl1.TabPages[i], Color.FromArgb(255, 171, 171, 171));
            }

            this.TabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            this.TabControl1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl1_DrawItem);



        }
        private Dictionary<TabPage, Color> TabColors = new Dictionary<TabPage, Color>();
        private void SetTabHeader(TabPage page, Color color)
        {
            TabColors[page] = color;
            TabControl1.Invalidate();
        }
        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            var g = e.Graphics;
            var text = this.TabControl1.TabPages[e.Index].Text;
            var sizeText = g.MeasureString(text, this.TabControl1.Font);

            var x = e.Bounds.Left + 3;
            var y = e.Bounds.Top + (e.Bounds.Height - sizeText.Height) / 2;

            //g.DrawString(text, this.tabControl1.Font, Brushes.White, x, y);
            //e.DrawBackground();
            using (Brush br = new SolidBrush(TabColors[TabControl1.TabPages[e.Index]]))
            {
                e.Graphics.FillRectangle(br, e.Bounds);
                SizeF sz = e.Graphics.MeasureString(TabControl1.TabPages[e.Index].Text, e.Font);
                e.Graphics.DrawString(TabControl1.TabPages[e.Index].Text, e.Font, Brushes.White, e.Bounds.Left + (e.Bounds.Width - sz.Width) / 2, e.Bounds.Top + (e.Bounds.Height - sz.Height) / 2 + 1);

                Rectangle rect = e.Bounds;
                rect.Offset(0, 1);
                rect.Inflate(0, -1);
                e.Graphics.DrawRectangle(Pens.DarkGray, rect);
                e.DrawFocusRectangle();
            }
        }

        private void tabControl1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            SetTabHeader(TabControl1.TabPages[TabControl1.SelectedIndex], Color.FromArgb(255, 66, 66, 66));
            for (int i = 0; i < TabControl1.TabPages.Count; i++)
            {
                if (i != TabControl1.SelectedIndex)
                    SetTabHeader(TabControl1.TabPages[i], Color.FromArgb(255, 171, 171, 171));
            }
        }

        void FormMain_FormClosing(Object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.WindowsShutDown) return;

            else if (e.CloseReason == CloseReason.UserClosing)
            {
                // Assume that X has been clicked and act accordingly.
                // Confirm user wants to close
                switch (MessageBox.Show(this, "BẠN CÓ CHẮC MUỐN TẮT KHÔNG?", "BẠN VẪN MUỐN TĂT À?", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    //Stay on this form
                    case DialogResult.No:
                        e.Cancel = true;
                        this.Hide();
                        notifyIcon.Visible = true;
                        break;
                    default:
                        ProcessHelper.KillAllProcessTree("chromedriver");
                        break;
                }
            }
        }

        private void guideBtn_Click(object sender, EventArgs e)
        {
            GuideForm guideForm = new GuideForm();
            guideForm.ShowDialog();
        }

        void defaultSetting_LHCC_COM()
        {
            dataGridViewNotification_LHCC_COM.RowHeadersVisible = false;
            var pathStringFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LHCC_COM");
            if (!Directory.Exists(pathStringFolder))
            {
                Directory.CreateDirectory(pathStringFolder);
            }


            var fileSetting = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LHCC_COM", "Setting.txt");
            var fileStatus = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LHCC_COM", "status.txt");
            if (File.Exists(fileStatus))
            {
                var data = File.ReadAllText(fileStatus);
                StatusTBox_LHCC_COM.Text = data;
            }

            var fileStatusAuto = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LHCC_COM", "status_auto.txt");
            if (File.Exists(fileStatusAuto))
            {
                var data = File.ReadAllText(fileStatusAuto);
                autoStatusTBox_LHCC_COM.Text = data;
            }
            //var fileSetting = AppDomain.CurrentDomain.BaseDirectory + "Setting.txt";
            if (File.Exists(fileSetting))
            {
                var data = File.ReadAllText(fileSetting);
                var eleData = data.Split('|');
                //tudong
                autoFromPage_LHCC_COM.Value = Int32.Parse(eleData[0]);
                autoToPage_LHCC_COM.Value = Int32.Parse(eleData[1]);
                autoStartTime_LHCC_COM.Value = DateTime.Parse(eleData[2]);
                autoStopTime_LHCC_COM.Value = DateTime.Parse(eleData[3]);
                if (eleData[4] == "1")
                    monday_LHCC_COM.Checked = true;
                else
                    monday_LHCC_COM.Checked = false;
                if (eleData[5] == "1")
                    tuesday_LHCC_COM.Checked = true;
                else
                    tuesday_LHCC_COM.Checked = false;
                if (eleData[6] == "1")
                    wenesday_LHCC_COM.Checked = true;
                else
                    wenesday_LHCC_COM.Checked = false;
                if (eleData[7] == "1")
                    thusday_LHCC_COM.Checked = true;
                else
                    thusday_LHCC_COM.Checked = false;
                if (eleData[8] == "1")
                    friday_LHCC_COM.Checked = true;
                else
                    friday_LHCC_COM.Checked = false;
                if (eleData[9] == "1")
                    satuday_LHCC_COM.Checked = true;
                else
                    satuday_LHCC_COM.Checked = false;
                if (eleData[10] == "1")
                    sunday_LHCC_COM.Checked = true;
                else
                    sunday_LHCC_COM.Checked = false;

                autoTimeGetNew_LHCC_COM.Value = Int32.Parse(eleData[11]);
                checkLoginTime_LHCC_COM.Value = Int32.Parse(eleData[12]);
                if (eleData[13] == "1")
                    autoHNCBox_LHCC_COM.Checked = true;
                else
                    autoHNCBox_LHCC_COM.Checked = false;
                if (eleData[14] == "1")
                    autoHCMCBox_LHCC_COM.Checked = true;
                else
                    autoHCMCBox_LHCC_COM.Checked = false;

                //thu cong
                scrapeToPage_LHCC_COM.Value = Int32.Parse(eleData[15]);
                scrapeFromPage_LHCC_COM.Value = Int32.Parse(eleData[16]);
                if (eleData[17] == "1")
                    HNCBox_LHCC_COM.Checked = true;
                else
                    HNCBox_LHCC_COM.Checked = false;
                if (eleData[18] == "1")
                    HCMCBox_LHCC_COM.Checked = true;
                else
                    HCMCBox_LHCC_COM.Checked = false;
                //Cai dat chung
                timeBetweenNewFrom_LHCC_COM.Value = Int32.Parse(eleData[19]);
                timeBetweenNewTo_LHCC_COM.Value = Int32.Parse(eleData[20]);
                linkAPITBox1_LHCC_COM.Text = eleData[21];
                linkAPITBox2_LHCC_COM.Text = eleData[22];

                userHNTBox_LHCC_COM.Text = eleData[23];
                passHNTBox_LHCC_COM.Text = eleData[24];

                userHCMTBox_LHCC_COM.Text = eleData[25];
                passHCMTBox_LHCC_COM.Text = eleData[26];

                if (eleData[27] == "1")
                    TKHNCBox_LHCC_COM.Checked = true;
                else
                    TKHNCBox_LHCC_COM.Checked = false;
                if (eleData[28] == "1")
                    TKHCMCBox_LHCC_COM.Checked = true;
                else
                    TKHCMCBox_LHCC_COM.Checked = false;

                CrawlLienHeChinhChu.linkAPI1 = linkAPITBox1_LHCC_COM.Text;
                CrawlLienHeChinhChu.linkAPI2 = linkAPITBox2_LHCC_COM.Text;
                //scrape
                CrawlLienHeChinhChu.fromPage = (int)scrapeFromPage_LHCC_COM.Value;
                CrawlLienHeChinhChu.toPage = (int)scrapeToPage_LHCC_COM.Value;
                CrawlLienHeChinhChu.timeBetweenNewFrom = (int)timeBetweenNewFrom_LHCC_COM.Value;
                CrawlLienHeChinhChu.timeBetweenNewTo = (int)timeBetweenNewTo_LHCC_COM.Value;
                //auto


            }

        }

        //Lienhechinhchu.com//Lienhechinhchu.com//Lienhechinhchu.com//Lienhechinhchu.com//Lienhechinhchu.com//Lienhechinhchu.com
        void myBackgroundWorker_DoWork_Status(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if (CrawlLienHeChinhChu.runScrape || CrawlLienHeChinhChu.runAuto)
                {
                    if (CrawlLienHeChinhChu.runScrape || CrawlLienHeChinhChu.runningAuto)
                        statusWorker.ReportProgress(1, "true");
                    else
                        statusWorker.ReportProgress(1, "true2");
                }
                else
                {
                    statusWorker.ReportProgress(1, "false");
                }

                if (CrawlBDS44.runScrape || CrawlBDS44.runAuto)
                {
                    if (CrawlBDS44.runScrape || CrawlBDS44.runningAuto)
                        statusWorker.ReportProgress(2, "true");
                    else
                        statusWorker.ReportProgress(2, "true2");
                }
                else
                {
                    statusWorker.ReportProgress(2, "false");
                }

                if (CrawlSALENHA.runScrape || CrawlSALENHA.runAuto)
                {
                    if (CrawlSALENHA.runScrape || CrawlSALENHA.runningAuto)
                        statusWorker.ReportProgress(3, "true");
                    else
                        statusWorker.ReportProgress(3, "true2");
                }
                else
                {
                    statusWorker.ReportProgress(3, "false");
                }

                if (CrawlSALENHA_VIP.runScrape || CrawlSALENHA_VIP.runAuto)
                {
                    if (CrawlSALENHA_VIP.runScrape || CrawlSALENHA_VIP.runningAuto)
                        statusWorker.ReportProgress(4, "true");
                    else
                        statusWorker.ReportProgress(4, "true2");
                }
                else
                {
                    statusWorker.ReportProgress(4, "false");
                }

                if (CrawlNCC.runScrape || CrawlNCC.runAuto)
                {
                    if (CrawlNCC.runScrape || CrawlNCC.runningAuto)
                        statusWorker.ReportProgress(5, "true");
                    else
                        statusWorker.ReportProgress(5, "true2");
                }
                else
                {
                    statusWorker.ReportProgress(5, "false");
                }

                if (CrawlNNCC.runScrape || CrawlNNCC.runAuto)
                {
                    if (CrawlNNCC.runScrape || CrawlNNCC.runningAuto)
                        statusWorker.ReportProgress(6, "true");
                    else
                        statusWorker.ReportProgress(6, "true2");
                }
                else
                {
                    statusWorker.ReportProgress(6, "false");
                }

                if (CrawlTCC.runScrape || CrawlTCC.runAuto)
                {
                    if (CrawlTCC.runScrape || CrawlTCC.runningAuto)
                        statusWorker.ReportProgress(7, "true");
                    else
                        statusWorker.ReportProgress(7, "true2");
                }
                else
                {
                    statusWorker.ReportProgress(7, "false");
                }

                if (CrawlNNM.runScrape || CrawlNNM.runAuto)
                {
                    if (CrawlNNM.runScrape || CrawlNNM.runningAuto)
                        statusWorker.ReportProgress(8, "true");
                    else
                        statusWorker.ReportProgress(8, "true2");
                }
                else
                {
                    statusWorker.ReportProgress(8, "false");
                }

                Thread.Sleep(3000);
            }
        }
        void myBackgroundWorker_ProgressChanged_Status(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 1)
            {
                var status = e.UserState.ToString();
                if (status == "true")
                {
                    LLCCStatusBtn.BackColor = Color.White;
                }
                else if (status == "true2")
                {
                    LLCCStatusBtn.BackColor = Color.Blue;
                }
                else
                {
                    LLCCStatusBtn.BackColor = Color.Red;
                }
            }
            if (e.ProgressPercentage == 2)
            {
                var status = e.UserState.ToString();
                if (status == "true")
                {
                    BDS44StatusBtn.BackColor = Color.White;
                }
                else if (status == "true2")
                {
                    BDS44StatusBtn.BackColor = Color.Blue;
                }
                else
                {
                    BDS44StatusBtn.BackColor = Color.Red;
                }
            }
            if (e.ProgressPercentage == 3)
            {
                var status = e.UserState.ToString();
                if (status == "true")
                {
                    SAStatusBtn.BackColor = Color.White;
                }
                else if (status == "true2")
                {
                    SAStatusBtn.BackColor = Color.Blue;
                }
                else
                {
                    SAStatusBtn.BackColor = Color.Red;
                }
            }
            if (e.ProgressPercentage == 4)
            {
                var status = e.UserState.ToString();
                if (status == "true")
                {
                    SAVipStatusBtn.BackColor = Color.White;
                }
                else if (status == "true2")
                {
                    SAVipStatusBtn.BackColor = Color.Blue;
                }
                else
                {
                    SAVipStatusBtn.BackColor = Color.Red;
                }
            }
            if (e.ProgressPercentage == 5)
            {
                var status = e.UserState.ToString();
                if (status == "true")
                {
                    NCCStatusBtn.BackColor = Color.White;
                }
                else if (status == "true2")
                {
                    NCCStatusBtn.BackColor = Color.Blue;
                }
                else
                {
                    NCCStatusBtn.BackColor = Color.Red;
                }
            }
            if (e.ProgressPercentage == 6)
            {
                var status = e.UserState.ToString();
                if (status == "true")
                {
                    NNCCStatusBtn.BackColor = Color.White;
                }
                else if (status == "true2")
                {
                    NNCCStatusBtn.BackColor = Color.Blue;
                }
                else
                {
                    NNCCStatusBtn.BackColor = Color.Red;
                }
            }
            if (e.ProgressPercentage == 7)
            {
                var status = e.UserState.ToString();
                if (status == "true")
                {
                    TCCStatusBtn.BackColor = Color.White;
                }
                else if (status == "true2")
                {
                    TCCStatusBtn.BackColor = Color.Blue;
                }
                else
                {
                    TCCStatusBtn.BackColor = Color.Red;
                }
            }
            if (e.ProgressPercentage == 8)
            {
                var status = e.UserState.ToString();
                if (status == "true")
                {
                    NNMStatusBtn.BackColor = Color.White;
                }
                else if (status == "true2")
                {
                    NNMStatusBtn.BackColor = Color.Blue;
                }
                else
                {
                    NNMStatusBtn.BackColor = Color.Red;
                }
            }
        }

        void myBackgroundWorker_DoWork_LHCC_COM(object sender, DoWorkEventArgs e)
        {
            CrawlLienHeChinhChu.runScrape = true;
            CrawlLienHeChinhChu.ScrapeRunCrawl(m_oWorker_LHCC_COM);
        }

        void myBackgroundAutoWorker_DoWork_LHCC_COM(object sender, DoWorkEventArgs e)
        {
            CrawlLienHeChinhChu.runAuto = true;
            CrawlLienHeChinhChu.AutoRunCrawl(m_autoWorker_LHCC_COM);
        }



        void myBackgroundWorker_ProgressChanged_LHCC_COM(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 0)
            {
                StatusTBox_LHCC_COM.Text = "đang chạy";
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin thủ công";
                var url = e.UserState.ToString();
                string[] row1 = new string[] { dataGridViewNotification_LHCC_COM.Rows.Count.ToString(), "Server 1", "xong", time, type, url };
                StatusTBox_LHCC_COM.Text = "đang chạy, đã đẩy thành công: " + dataGridViewNotification_LHCC_COM.Rows.Count.ToString();
                dataGridViewNotification_LHCC_COM.Rows.Insert(0, row1);
                dataGridViewNotification_LHCC_COM.Rows[0].DefaultCellStyle.ForeColor = Color.Black;
                //dataGridViewNotification.Rows.Add(row1);

            }
            else if (e.ProgressPercentage == 1)
            {
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin thủ công";
                var url = e.UserState.ToString();
                string[] row1 = new string[] { dataGridViewNotification_LHCC_COM.Rows.Count.ToString(), "Server 1", "lỗi", time, type, url };
                dataGridViewNotification_LHCC_COM.Rows.Insert(0, row1);
                dataGridViewNotification_LHCC_COM.Rows[0].DefaultCellStyle.ForeColor = Color.Red;
                //dataGridViewNotification.Rows.Add(row1);
            }
            else if (e.ProgressPercentage == 2)
            {
                var dt = DateTime.Now;
                StatusTBox_LHCC_COM.Text = "chạy xong thủ công lúc: " + dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                scrapeStartBtn_LHCC_COM.Enabled = true;
                scrapeStopBtn_LHCC_COM.Enabled = false;
                var pathStringFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LHCC_COM");
                if (!Directory.Exists(pathStringFolder))
                {
                    Directory.CreateDirectory(pathStringFolder);
                }

                var pathStatus = System.IO.Path.Combine(pathStringFolder, "status.txt");
                File.WriteAllText(pathStatus, StatusTBox_LHCC_COM.Text);
            }
            else if (e.ProgressPercentage == 3)
            {
                StatusTBox_LHCC_COM.Text = "đang chạy";
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin thủ công";
                var url = e.UserState.ToString();

                string[] row1 = new string[] { dataGridViewNotification_LHCC_COM.Rows.Count.ToString(), "Server 2", "xong", time, type, url };
                StatusTBox_LHCC_COM.Text = "đang chạy, đã đẩy thành công: " + dataGridViewNotification_LHCC_COM.Rows.Count.ToString();
                //dataGridViewNotification.Rows.Add(row1);
                dataGridViewNotification_LHCC_COM.Rows.Insert(0, row1);
                dataGridViewNotification_LHCC_COM.Rows[0].DefaultCellStyle.ForeColor = Color.Black;

            }
            else if (e.ProgressPercentage == 4)
            {
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin thủ công";
                var url = e.UserState.ToString();
                string[] row1 = new string[] { dataGridViewNotification_LHCC_COM.Rows.Count.ToString(), "Server 2", "lỗi", time, type, url };
                //dataGridViewNotification.Rows.Add(row1);
                dataGridViewNotification_LHCC_COM.Rows.Insert(0, row1);
                dataGridViewNotification_LHCC_COM.Rows[0].DefaultCellStyle.ForeColor = Color.Red;
            }
            else if (e.ProgressPercentage == 6)
            {
                StatusTBox_LHCC_COM.Text = "Đang đợi login";
            }
        }

        void myBackgroundAutoWorker_ProgressChanged_LHCC_COM(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 0)
            {
                autoStatusTBox_LHCC_COM.Text = "đang chạy";
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin tự động";
                var url = e.UserState.ToString();
                if (url.Contains("đang chạy"))
                    return;
                string[] row1 = new string[] { dataGridViewNotification_LHCC_COM.Rows.Count.ToString(), "Server 1", "xong", time, type, url };
                autoStatusTBox_LHCC_COM.Text = "đang chạy, đã đẩy thành công: " + dataGridViewNotification_LHCC_COM.Rows.Count.ToString();
                //dataGridViewNotification.Rows.Add(row1);
                dataGridViewNotification_LHCC_COM.Rows.Insert(0, row1);
                dataGridViewNotification_LHCC_COM.Rows[0].DefaultCellStyle.ForeColor = Color.Green;


            }
            else if (e.ProgressPercentage == 1)
            {
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin tự động";
                var url = e.UserState.ToString();
                string[] row1 = new string[] { dataGridViewNotification_LHCC_COM.Rows.Count.ToString(), "Server 1", "lỗi", time, type, url };
                //dataGridViewNotification.Rows.Add(row1);
                dataGridViewNotification_LHCC_COM.Rows.Insert(0, row1);
                dataGridViewNotification_LHCC_COM.Rows[0].DefaultCellStyle.ForeColor = Color.Red;
            }
            else if (e.ProgressPercentage == 2)
            {
                var dt = DateTime.Now;
                autoStatusTBox_LHCC_COM.Text = "chạy xong tự động lúc: " + dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                autoStartBtn_LHCC_COM.Enabled = true;
                autoStopBtn_LHCC_COM.Enabled = false;
                var pathStringFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LHCC_COM");
                if (!Directory.Exists(pathStringFolder))
                {
                    Directory.CreateDirectory(pathStringFolder);
                }

                var pathStatus = System.IO.Path.Combine(pathStringFolder, "status_auto.txt");
                File.WriteAllText(pathStatus, autoStatusTBox_LHCC_COM.Text);

            }
            else if (e.ProgressPercentage == 5)
            {
                autoStatusTBox_LHCC_COM.Text = e.UserState.ToString();
                autoStartBtn_LHCC_COM.Enabled = false;
                autoStopBtn_LHCC_COM.Enabled = true;
            }
            else if (e.ProgressPercentage == 3)
            {
                autoStatusTBox_LHCC_COM.Text = "đang chạy";
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin tự động";
                var url = e.UserState.ToString();
                string[] row1 = new string[] { dataGridViewNotification_LHCC_COM.Rows.Count.ToString(), "Server 2", "xong", time, type, url };
                autoStatusTBox_LHCC_COM.Text = "đang chạy, đã đẩy thành công: " + dataGridViewNotification_LHCC_COM.Rows.Count.ToString();
                //dataGridViewNotification.Rows.Add(row1);
                dataGridViewNotification_LHCC_COM.Rows.Insert(0, row1);
                dataGridViewNotification_LHCC_COM.Rows[0].DefaultCellStyle.ForeColor = Color.Blue;


            }
            else if (e.ProgressPercentage == 4)
            {
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin tự động";
                var url = e.UserState.ToString();
                string[] row1 = new string[] { dataGridViewNotification_LHCC_COM.Rows.Count.ToString(), "Server 2", "lỗi", time, type, url };
                //dataGridViewNotification.Rows.Add(row1);
                dataGridViewNotification_LHCC_COM.Rows.Insert(0, row1);
                dataGridViewNotification_LHCC_COM.Rows[0].DefaultCellStyle.ForeColor = Color.Red;
            }
            else if (e.ProgressPercentage == 6)
            {
                autoStatusTBox_LHCC_COM.Text = "Đang đợi login";
            }
        }

        void myBackgroundWorker_RunWorkerCompleted_LHCC_COM(object sender, RunWorkerCompletedEventArgs e)
        {
            scrapeStartBtn_LHCC_COM.Enabled = true;
            scrapeStopBtn_LHCC_COM.Enabled = false;

            CrawlLienHeChinhChu.runScrape = false;
            //MessageBox.Show(this, "DỪNG THÀNH CÔNG CHẠY THỦ CÔNG");
        }

        void myBackgroundAutoWorker_RunWorkerCompleted_LHCC_COM(object sender, RunWorkerCompletedEventArgs e)
        {
            autoStartBtn_LHCC_COM.Enabled = true;
            autoStopBtn_LHCC_COM.Enabled = false;
            //MessageBox.Show(this, "DỪNG THÀNH CÔNG CHẠY TỰ ĐỘNG");
            CrawlLienHeChinhChu.runAuto = false;
        }

        private void scrapeStartBtn_Click(object sender, EventArgs e)
        {
            scrapeStartBtn_LHCC_COM.Enabled = false;
            scrapeStopBtn_LHCC_COM.Enabled = true;

            CrawlLienHeChinhChu.linkAPI1 = linkAPITBox1_LHCC_COM.Text;
            CrawlLienHeChinhChu.linkAPI2 = linkAPITBox2_LHCC_COM.Text;
            //scrape
            CrawlLienHeChinhChu.fromPage = (int)scrapeFromPage_LHCC_COM.Value;
            CrawlLienHeChinhChu.toPage = (int)scrapeToPage_LHCC_COM.Value;
            CrawlLienHeChinhChu.timeBetweenNewFrom = (int)timeBetweenNewFrom_LHCC_COM.Value;
            CrawlLienHeChinhChu.timeBetweenNewTo = (int)timeBetweenNewTo_LHCC_COM.Value;
            CrawlLienHeChinhChu.tinh.Clear();
            if (HNCBox_LHCC_COM.Checked == true)
            {
                CrawlLienHeChinhChu.tinh.Add("Hà+Nội");
            }
            if (HCMCBox_LHCC_COM.Checked == true)
            {
                CrawlLienHeChinhChu.tinh.Add("TPHCM");
            }

            if (autoLoginCBox_LHCC.Checked)
            {
                CrawlLienHeChinhChu.autoLogin = true;
            }
            else
            {
                CrawlLienHeChinhChu.autoLogin = false;
            }

            CrawlLienHeChinhChu.runScrape = true;

            CrawlLienHeChinhChu.userHN = userHNTBox_LHCC_COM.Text;
            CrawlLienHeChinhChu.passHN = passHNTBox_LHCC_COM.Text;

            StatusTBox_LHCC_COM.Text = "bắt đầu chạy";

            m_oWorker_LHCC_COM.RunWorkerAsync();
        }

        private void scrapeStopBtn_Click(object sender, EventArgs e)
        {
            scrapeStopBtn_LHCC_COM.Enabled = false;
            CrawlLienHeChinhChu.runScrape = false;
            //ProcessHelper.KillAllProcessTree("chromedriver");
            m_oWorker_LHCC_COM.CancelAsync();
            m_oWorker_LHCC_COM.Dispose();
            StatusTBox_LHCC_COM.Text = "đang dừng chạy";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridViewNotification_LHCC_COM.Rows.Clear();
        }

        private void SaveSettingBtn_Click(object sender, EventArgs e)
        {
            //tudong
            var svAutoFromPage = autoFromPage_LHCC_COM.Value.ToString();
            var svAutoToPage = autoToPage_LHCC_COM.Value.ToString();
            var svautoStartTime = autoStartTime_LHCC_COM.Value.ToString();
            var svautoStopTime = autoStopTime_LHCC_COM.Value.ToString();
            var svT2 = monday_LHCC_COM.Checked ? "1" : "0";
            var svT3 = tuesday_LHCC_COM.Checked ? "1" : "0";
            var svT4 = wenesday_LHCC_COM.Checked ? "1" : "0";
            var svT5 = thusday_LHCC_COM.Checked ? "1" : "0";
            var svT6 = friday_LHCC_COM.Checked ? "1" : "0";
            var svT7 = satuday_LHCC_COM.Checked ? "1" : "0";
            var svCN = sunday_LHCC_COM.Checked ? "1" : "0";
            var svautoTimeGetNew = autoTimeGetNew_LHCC_COM.Value.ToString();
            var svcheckLoginTime = checkLoginTime_LHCC_COM.Value.ToString();
            var svautoHNCBox = autoHNCBox_LHCC_COM.Checked ? "1" : "0";
            var svautoHCMCBox = autoHCMCBox_LHCC_COM.Checked ? "1" : "0";
            //thu cong
            var svscrapeToPage = scrapeToPage_LHCC_COM.Value.ToString();
            var svscrapeFromPage = scrapeFromPage_LHCC_COM.Value.ToString();
            var svHNCBox = HNCBox_LHCC_COM.Checked ? "1" : "0";
            var svHCMCBox = HCMCBox_LHCC_COM.Checked ? "1" : "0";
            //Cai dat chung
            var svtimeBetweenNewFrom = timeBetweenNewFrom_LHCC_COM.Value.ToString();
            var svtimeBetweenNewTo = timeBetweenNewTo_LHCC_COM.Value.ToString();
            var svlinkAPITBox1 = linkAPITBox1_LHCC_COM.Text;
            var svlinkAPITBox2 = linkAPITBox2_LHCC_COM.Text;

            var userHN = userHNTBox_LHCC_COM.Text;
            var passHN = passHNTBox_LHCC_COM.Text;

            var userHCM = userHCMTBox_LHCC_COM.Text;
            var passHCM = userHCMTBox_LHCC_COM.Text;

            var TKHN = TKHNCBox_LHCC_COM.Checked ? "1" : "0";
            var TKHCM = TKHCMCBox_LHCC_COM.Checked ? "1" : "0";

            var data = svAutoFromPage + "|" + svAutoToPage + "|" + svautoStartTime + "|" + svautoStopTime + "|" + svT2 + "|" + svT3 + "|"
                + svT4 + "|" + svT5 + "|" + svT6 + "|" + svT7 + "|" + svCN + "|" + svautoTimeGetNew + "|"
                + svcheckLoginTime + "|" + svautoHNCBox + "|" + svautoHCMCBox + "|" + svscrapeToPage + "|" + svscrapeFromPage + "|" + svHNCBox + "|"
                + svHCMCBox + "|" + svtimeBetweenNewFrom + "|" + svtimeBetweenNewTo + "|" + svlinkAPITBox1 + "|" + svlinkAPITBox2 + "|"
                + userHN + "|" + passHN + "|" + userHCM + "|" + passHCM + "|" + TKHN + "|" + TKHCM;

            var pathStringFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "LHCC_COM");
            if (!Directory.Exists(pathStringFolder))
            {
                Directory.CreateDirectory(pathStringFolder);
            }

            var pathSetting = System.IO.Path.Combine(pathStringFolder, "Setting.txt");
            File.WriteAllText(pathSetting, data);
        }

        private void autoStartBtn_Click(object sender, EventArgs e)
        {
            autoStartBtn_LHCC_COM.Enabled = false;
            autoStopBtn_LHCC_COM.Enabled = true;

            CrawlLienHeChinhChu.linkAPI1 = linkAPITBox1_LHCC_COM.Text;
            CrawlLienHeChinhChu.linkAPI2 = linkAPITBox2_LHCC_COM.Text;
            //scrape
            CrawlLienHeChinhChu.autoFromPage = (int)autoFromPage_LHCC_COM.Value;
            CrawlLienHeChinhChu.autoToPage = (int)autoToPage_LHCC_COM.Value;
            CrawlLienHeChinhChu.timeBetweenNewFrom = (int)timeBetweenNewFrom_LHCC_COM.Value;
            CrawlLienHeChinhChu.timeBetweenNewTo = (int)timeBetweenNewTo_LHCC_COM.Value;
            CrawlLienHeChinhChu.autoTinh.Clear();
            if (autoHNCBox_LHCC_COM.Checked == true)
            {
                CrawlLienHeChinhChu.autoTinh.Add("Hà+Nội");
            }
            if (autoHCMCBox_LHCC_COM.Checked == true)
            {
                CrawlLienHeChinhChu.autoTinh.Add("TPHCM");
            }

            if (autoLoginCBox_LHCC.Checked)
            {
                CrawlLienHeChinhChu.autoLogin = true;
            }
            else
            {
                CrawlLienHeChinhChu.autoLogin = false;
            }

            CrawlLienHeChinhChu.beginTime = autoStartTime_LHCC_COM.Value;
            CrawlLienHeChinhChu.endTime = autoStopTime_LHCC_COM.Value;

            CrawlLienHeChinhChu.userHN = userHNTBox_LHCC_COM.Text;
            CrawlLienHeChinhChu.passHN = passHNTBox_LHCC_COM.Text;

            CrawlLienHeChinhChu.dayinWeek.Clear();
            if (monday_LHCC_COM.Checked)
                CrawlLienHeChinhChu.dayinWeek.Add((int)DayOfWeek.Monday);
            if (tuesday_LHCC_COM.Checked)
                CrawlLienHeChinhChu.dayinWeek.Add((int)DayOfWeek.Tuesday);
            if (wenesday_LHCC_COM.Checked)
                CrawlLienHeChinhChu.dayinWeek.Add((int)DayOfWeek.Wednesday);
            if (thusday_LHCC_COM.Checked)
                CrawlLienHeChinhChu.dayinWeek.Add((int)DayOfWeek.Thursday);
            if (friday_LHCC_COM.Checked)
                CrawlLienHeChinhChu.dayinWeek.Add((int)DayOfWeek.Friday);
            if (satuday_LHCC_COM.Checked)
                CrawlLienHeChinhChu.dayinWeek.Add((int)DayOfWeek.Saturday);
            if (sunday_LHCC_COM.Checked)
                CrawlLienHeChinhChu.dayinWeek.Add((int)DayOfWeek.Sunday);

            CrawlLienHeChinhChu.loopTime = (int)autoTimeGetNew_LHCC_COM.Value;




            autoStatusTBox_LHCC_COM.Text = "bắt đầu chạy";

            m_autoWorker_LHCC_COM.RunWorkerAsync();
        }

        private void autoStopBtn_Click(object sender, EventArgs e)
        {
            autoStopBtn_LHCC_COM.Enabled = false;
            CrawlLienHeChinhChu.runAuto = false;
            //ProcessHelper.KillAllProcessTree("chromedriver");
            m_autoWorker_LHCC_COM.CancelAsync();
            m_autoWorker_LHCC_COM.Dispose();
            //while (m_autoWorker.IsBusy)
            //{
            //    autoStatusTBox.Text = "đang dừng !!!!";
            //    Thread.Sleep(2 * 1000);
            //}
            autoStatusTBox_LHCC_COM.Text = "đang dừng chạy";
        }

        private void notifyIcon_Click(object sender, EventArgs e)
        {
            this.Show();

        }

        //batdongsan44.com//batdongsan44.com//batdongsan44.com//batdongsan44.com//batdongsan44.com//batdongsan44.com//batdongsan44.com
        void defaultSetting_BDS44()
        {
            dataGridViewNotification_BDS44.RowHeadersVisible = false;
            var pathStringFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BDS44");

            if (!Directory.Exists(pathStringFolder))
            {
                Directory.CreateDirectory(pathStringFolder);
            }
            var fileSetting = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BDS44", "Setting.txt");
            var fileStatus = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BDS44", "status.txt");
            if (File.Exists(fileStatus))
            {
                var data = File.ReadAllText(fileStatus);
                StatusTBox_BDS44.Text = data;
            }

            var fileStatusAuto = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BDS44", "status_auto.txt");
            if (File.Exists(fileStatusAuto))
            {
                var data = File.ReadAllText(fileStatusAuto);
                autoStatusTBox_BDS44.Text = data;
            }

            //var fileSetting = AppDomain.CurrentDomain.BaseDirectory + "Setting.txt";
            if (File.Exists(fileSetting))
            {
                var data = File.ReadAllText(fileSetting);
                var eleData = data.Split('|');
                //tudong
                autoFromPage_BDS44.Value = Int32.Parse(eleData[0]);
                autoToPage_BDS44.Value = Int32.Parse(eleData[1]);
                autoStartTime_BDS44.Value = DateTime.Parse(eleData[2]);
                autoStopTime_BDS44.Value = DateTime.Parse(eleData[3]);
                if (eleData[4] == "1")
                    monday_BDS44.Checked = true;
                else
                    monday_BDS44.Checked = false;
                if (eleData[5] == "1")
                    tuesday_BDS44.Checked = true;
                else
                    tuesday_BDS44.Checked = false;
                if (eleData[6] == "1")
                    wenesday_BDS44.Checked = true;
                else
                    wenesday_BDS44.Checked = false;
                if (eleData[7] == "1")
                    thusday_BDS44.Checked = true;
                else
                    thusday_BDS44.Checked = false;
                if (eleData[8] == "1")
                    friday_BDS44.Checked = true;
                else
                    friday_BDS44.Checked = false;
                if (eleData[9] == "1")
                    satuday_BDS44.Checked = true;
                else
                    satuday_BDS44.Checked = false;
                if (eleData[10] == "1")
                    sunday_BDS44.Checked = true;
                else
                    sunday_BDS44.Checked = false;

                autoTimeGetNew_BDS44.Value = Int32.Parse(eleData[11]);
                checkLoginTime_BDS44.Value = Int32.Parse(eleData[12]);

                //thu cong
                scrapeToPage_BDS44.Value = Int32.Parse(eleData[13]);
                scrapeFromPage_BDS44.Value = Int32.Parse(eleData[14]);

                //Cai dat chung
                timeBetweenNewFrom_BDS44.Value = Int32.Parse(eleData[15]);
                timeBetweenNewTo_BDS44.Value = Int32.Parse(eleData[16]);
                linkAPITBox1_BDS44.Text = eleData[17];
                linkAPITBox2_BDS44.Text = eleData[18];

                if (eleData[19] == "1")
                    TKHNCBox_BDS44.Checked = true;
                else
                    TKHNCBox_BDS44.Checked = false;
                if (eleData[20] == "1")
                    TKHCMCBox_BDS44.Checked = true;
                else
                    TKHCMCBox_BDS44.Checked = false;

                CrawlBDS44.linkAPI1 = linkAPITBox1_BDS44.Text;
                CrawlBDS44.linkAPI2 = linkAPITBox2_BDS44.Text;
                //scrape
                CrawlBDS44.fromPage = (int)scrapeFromPage_BDS44.Value;
                CrawlBDS44.toPage = (int)scrapeToPage_BDS44.Value;
                CrawlBDS44.timeBetweenNewFrom = (int)timeBetweenNewFrom_BDS44.Value;
                CrawlBDS44.timeBetweenNewTo = (int)timeBetweenNewTo_BDS44.Value;
                //auto
            }
        }

        void myBackgroundWorker_DoWork_BDS44(object sender, DoWorkEventArgs e)
        {
            CrawlBDS44.runScrape = true;
            CrawlBDS44.ScrapeRunCrawl(m_oWorker_BDS44);
        }

        void myBackgroundAutoWorker_DoWork_BDS44(object sender, DoWorkEventArgs e)
        {
            CrawlBDS44.runAuto = true;
            CrawlBDS44.AutoRunCrawl(m_autoWorker_BDS44);
        }



        void myBackgroundWorker_ProgressChanged_BDS44(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 0)
            {
                StatusTBox_BDS44.Text = "đang chạy";
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin thủ công";
                var url = e.UserState.ToString();

                string[] row1 = new string[] { dataGridViewNotification_BDS44.Rows.Count.ToString(), "Server 1", "xong", time, type, url };
                StatusTBox_BDS44.Text = "đang chạy, đã đẩy thành công: " + dataGridViewNotification_BDS44.Rows.Count.ToString();
                dataGridViewNotification_BDS44.Rows.Insert(0, row1);
                dataGridViewNotification_BDS44.Rows[0].DefaultCellStyle.ForeColor = Color.Black;
                //dataGridViewNotification.Rows.Add(row1);

            }
            else if (e.ProgressPercentage == 1)
            {
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin thủ công";
                var url = e.UserState.ToString();
                string[] row1 = new string[] { dataGridViewNotification_BDS44.Rows.Count.ToString(), "Server 1", "lỗi", time, type, url };
                dataGridViewNotification_BDS44.Rows.Insert(0, row1);
                dataGridViewNotification_BDS44.Rows[0].DefaultCellStyle.ForeColor = Color.Red;
                //dataGridViewNotification.Rows.Add(row1);
            }
            else if (e.ProgressPercentage == 2)
            {
                var dt = DateTime.Now;
                StatusTBox_BDS44.Text = "chạy xong thủ công lúc: " + dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                scrapeStartBtn_BDS44.Enabled = true;
                scrapeStopBtn_BDS44.Enabled = false;
                var pathStringFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BDS44");
                if (!Directory.Exists(pathStringFolder))
                {
                    Directory.CreateDirectory(pathStringFolder);
                }

                var pathStatus = System.IO.Path.Combine(pathStringFolder, "status.txt");
                File.WriteAllText(pathStatus, StatusTBox_BDS44.Text);
            }
            else if (e.ProgressPercentage == 3)
            {
                StatusTBox_BDS44.Text = "đang chạy";
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin thủ công";
                var url = e.UserState.ToString();

                string[] row1 = new string[] { dataGridViewNotification_BDS44.Rows.Count.ToString(), "Server 2", "xong", time, type, url };
                StatusTBox_BDS44.Text = "đang chạy, đã đẩy thành công: " + dataGridViewNotification_BDS44.Rows.Count.ToString();
                //dataGridViewNotification.Rows.Add(row1);
                dataGridViewNotification_BDS44.Rows.Insert(0, row1);
                dataGridViewNotification_BDS44.Rows[0].DefaultCellStyle.ForeColor = Color.Black;
            }
            else if (e.ProgressPercentage == 4)
            {
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin thủ công";
                var url = e.UserState.ToString();
                string[] row1 = new string[] { dataGridViewNotification_BDS44.Rows.Count.ToString(), "Server 2", "lỗi", time, type, url };
                //dataGridViewNotification.Rows.Add(row1);
                dataGridViewNotification_BDS44.Rows.Insert(0, row1);
                dataGridViewNotification_BDS44.Rows[0].DefaultCellStyle.ForeColor = Color.Red;
            }
            else if (e.ProgressPercentage == 6)
            {
                StatusTBox_BDS44.Text = "Đang đợi login";
            }
        }

        void myBackgroundAutoWorker_ProgressChanged_BDS44(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 0)
            {
                autoStatusTBox_BDS44.Text = "đang chạy";
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin tự động";
                var url = e.UserState.ToString();
                if (url.Contains("đang chạy"))
                    return;
                string[] row1 = new string[] { dataGridViewNotification_BDS44.Rows.Count.ToString(), "Server 1", "xong", time, type, url };
                autoStatusTBox_BDS44.Text = "đang chạy, đã đẩy thành công: " + dataGridViewNotification_BDS44.Rows.Count.ToString();
                //dataGridViewNotification.Rows.Add(row1);
                dataGridViewNotification_BDS44.Rows.Insert(0, row1);
                dataGridViewNotification_BDS44.Rows[0].DefaultCellStyle.ForeColor = Color.Green;


            }
            else if (e.ProgressPercentage == 1)
            {
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin tự động";
                var url = e.UserState.ToString();
                string[] row1 = new string[] { dataGridViewNotification_BDS44.Rows.Count.ToString(), "Server 1", "lỗi", time, type, url };
                //dataGridViewNotification.Rows.Add(row1);
                dataGridViewNotification_BDS44.Rows.Insert(0, row1);
                dataGridViewNotification_BDS44.Rows[0].DefaultCellStyle.ForeColor = Color.Red;
            }
            else if (e.ProgressPercentage == 2)
            {
                var dt = DateTime.Now;
                autoStatusTBox_BDS44.Text = "chạy xong tự động lúc: " + dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                autoStartBtn_BDS44.Enabled = true;
                autoStopBtn_BDS44.Enabled = false;
                var pathStringFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BDS44");
                if (!Directory.Exists(pathStringFolder))
                {
                    Directory.CreateDirectory(pathStringFolder);
                }

                var pathStatus = System.IO.Path.Combine(pathStringFolder, "status_auto.txt");
                File.WriteAllText(pathStatus, autoStatusTBox_BDS44.Text);
            }
            else if (e.ProgressPercentage == 5)
            {
                autoStatusTBox_BDS44.Text = e.UserState.ToString();
                autoStartBtn_BDS44.Enabled = false;
                autoStopBtn_BDS44.Enabled = true;
            }
            else if (e.ProgressPercentage == 3)
            {
                autoStatusTBox_BDS44.Text = "đang chạy";
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin tự động";
                var url = e.UserState.ToString();
                string[] row1 = new string[] { dataGridViewNotification_BDS44.Rows.Count.ToString(), "Server 2", "xong", time, type, url };
                autoStatusTBox_BDS44.Text = "đang chạy, đã đẩy thành công: " + dataGridViewNotification_BDS44.Rows.Count.ToString();
                //dataGridViewNotification.Rows.Add(row1);
                dataGridViewNotification_BDS44.Rows.Insert(0, row1);
                dataGridViewNotification_BDS44.Rows[0].DefaultCellStyle.ForeColor = Color.Blue;


            }
            else if (e.ProgressPercentage == 4)
            {
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin tự động";
                var url = e.UserState.ToString();
                string[] row1 = new string[] { dataGridViewNotification_BDS44.Rows.Count.ToString(), "Server 2", "lỗi", time, type, url };
                //dataGridViewNotification.Rows.Add(row1);
                dataGridViewNotification_BDS44.Rows.Insert(0, row1);
                dataGridViewNotification_BDS44.Rows[0].DefaultCellStyle.ForeColor = Color.Red;
            }
            else if (e.ProgressPercentage == 6)
            {
                autoStatusTBox_BDS44.Text = "Đang đợi login";
            }
        }

        void myBackgroundWorker_RunWorkerCompleted_BDS44(object sender, RunWorkerCompletedEventArgs e)
        {
            scrapeStartBtn_BDS44.Enabled = true;
            scrapeStopBtn_BDS44.Enabled = false;
            //MessageBox.Show(this, "DỪNG THÀNH CÔNG CHẠY THỦ CÔNG");
            CrawlBDS44.runScrape = false;
        }

        void myBackgroundAutoWorker_RunWorkerCompleted_BDS44(object sender, RunWorkerCompletedEventArgs e)
        {
            autoStartBtn_BDS44.Enabled = true;
            autoStopBtn_BDS44.Enabled = false;
            //MessageBox.Show(this, "DỪNG THÀNH CÔNG CHẠY TỰ ĐỘNG");
            CrawlBDS44.runAuto = false;
        }

        private void scrapeStartBtn_BDS44_Click(object sender, EventArgs e)
        {
            scrapeStartBtn_BDS44.Enabled = false;
            scrapeStopBtn_BDS44.Enabled = true;

            CrawlBDS44.linkAPI1 = linkAPITBox1_BDS44.Text;
            CrawlBDS44.linkAPI2 = linkAPITBox2_BDS44.Text;
            //scrape
            CrawlBDS44.fromPage = (int)scrapeFromPage_BDS44.Value;
            CrawlBDS44.toPage = (int)scrapeToPage_BDS44.Value;
            CrawlBDS44.timeBetweenNewFrom = (int)timeBetweenNewFrom_BDS44.Value;
            CrawlBDS44.timeBetweenNewTo = (int)timeBetweenNewTo_BDS44.Value;
            CrawlBDS44.tinh.Clear();
            if (TKHNCBox_BDS44.Checked == true)
            {
                CrawlBDS44.tinh.Add("Hà+Nội");
            }
            if (TKHCMCBox_BDS44.Checked == true)
            {
                CrawlBDS44.tinh.Add("TPHCM");
            }

            CrawlBDS44.runScrape = true;

            StatusTBox_BDS44.Text = "bắt đầu chạy";

            m_oWorker_BDS44.RunWorkerAsync();
        }

        private void scrapeStopBtn_BDS44_Click(object sender, EventArgs e)
        {
            scrapeStopBtn_BDS44.Enabled = false;
            CrawlBDS44.runScrape = false;
            //ProcessHelper.KillAllProcessTree("chromedriver");
            m_oWorker_BDS44.CancelAsync();
            m_oWorker_BDS44.Dispose();
            StatusTBox_BDS44.Text = "đang dừng chạy";
        }

        private void autoStartBtn_BDS44_Click(object sender, EventArgs e)
        {
            autoStartBtn_BDS44.Enabled = false;
            autoStopBtn_BDS44.Enabled = true;

            CrawlBDS44.linkAPI1 = linkAPITBox1_BDS44.Text;
            CrawlBDS44.linkAPI2 = linkAPITBox2_BDS44.Text;
            //scrape
            CrawlBDS44.autoFromPage = (int)autoFromPage_BDS44.Value;
            CrawlBDS44.autoToPage = (int)autoToPage_BDS44.Value;
            CrawlBDS44.timeBetweenNewFrom = (int)timeBetweenNewFrom_BDS44.Value;
            CrawlBDS44.timeBetweenNewTo = (int)timeBetweenNewTo_BDS44.Value;
            CrawlBDS44.autoTinh.Clear();
            if (TKHNCBox_BDS44.Checked == true)
            {
                CrawlBDS44.autoTinh.Add("Hà+Nội");
            }
            if (TKHCMCBox_BDS44.Checked == true)
            {
                CrawlBDS44.autoTinh.Add("TPHCM");
            }

            CrawlBDS44.beginTime = autoStartTime_BDS44.Value;
            CrawlBDS44.endTime = autoStopTime_BDS44.Value;

            CrawlBDS44.dayinWeek.Clear();
            if (monday_BDS44.Checked)
                CrawlBDS44.dayinWeek.Add((int)DayOfWeek.Monday);
            if (tuesday_BDS44.Checked)
                CrawlBDS44.dayinWeek.Add((int)DayOfWeek.Tuesday);
            if (wenesday_BDS44.Checked)
                CrawlBDS44.dayinWeek.Add((int)DayOfWeek.Wednesday);
            if (thusday_BDS44.Checked)
                CrawlBDS44.dayinWeek.Add((int)DayOfWeek.Thursday);
            if (friday_BDS44.Checked)
                CrawlBDS44.dayinWeek.Add((int)DayOfWeek.Friday);
            if (satuday_BDS44.Checked)
                CrawlBDS44.dayinWeek.Add((int)DayOfWeek.Saturday);
            if (sunday_BDS44.Checked)
                CrawlBDS44.dayinWeek.Add((int)DayOfWeek.Sunday);

            CrawlBDS44.loopTime = (int)autoTimeGetNew_BDS44.Value;

            autoStatusTBox_BDS44.Text = "bắt đầu chạy";

            m_autoWorker_BDS44.RunWorkerAsync();
        }

        private void autoStopBtn_BDS44_Click(object sender, EventArgs e)
        {
            autoStopBtn_BDS44.Enabled = false;
            CrawlBDS44.runAuto = false;
            //ProcessHelper.KillAllProcessTree("chromedriver");
            m_autoWorker_BDS44.CancelAsync();
            m_autoWorker_BDS44.Dispose();
            //while (m_autoWorker.IsBusy)
            //{
            //    autoStatusTBox.Text = "đang dừng !!!!";
            //    Thread.Sleep(2 * 1000);
            //}
            autoStatusTBox_BDS44.Text = "đang dừng chạy";
        }

        private void DeleteNotificationBtn_BDS44_Click(object sender, EventArgs e)
        {
            dataGridViewNotification_BDS44.Rows.Clear();
        }

        private void SaveSettingBtn_BDS44_Click(object sender, EventArgs e)
        {
            //tudong
            var svAutoFromPage = autoFromPage_BDS44.Value.ToString();
            var svAutoToPage = autoToPage_BDS44.Value.ToString();
            var svautoStartTime = autoStartTime_BDS44.Value.ToString();
            var svautoStopTime = autoStopTime_BDS44.Value.ToString();
            var svT2 = monday_BDS44.Checked ? "1" : "0";
            var svT3 = tuesday_BDS44.Checked ? "1" : "0";
            var svT4 = wenesday_BDS44.Checked ? "1" : "0";
            var svT5 = thusday_BDS44.Checked ? "1" : "0";
            var svT6 = friday_BDS44.Checked ? "1" : "0";
            var svT7 = satuday_BDS44.Checked ? "1" : "0";
            var svCN = sunday_BDS44.Checked ? "1" : "0";
            var svautoTimeGetNew = autoTimeGetNew_BDS44.Value.ToString();
            var svcheckLoginTime = checkLoginTime_BDS44.Value.ToString();

            //thu cong
            var svscrapeToPage = scrapeToPage_BDS44.Value.ToString();
            var svscrapeFromPage = scrapeFromPage_BDS44.Value.ToString();
            var svHNCBox = TKHNCBox_BDS44.Checked ? "1" : "0";
            var svHCMCBox = TKHCMCBox_BDS44.Checked ? "1" : "0";
            //Cai dat chung
            var svtimeBetweenNewFrom = timeBetweenNewFrom_BDS44.Value.ToString();
            var svtimeBetweenNewTo = timeBetweenNewTo_BDS44.Value.ToString();
            var svlinkAPITBox1 = linkAPITBox1_BDS44.Text;
            var svlinkAPITBox2 = linkAPITBox2_BDS44.Text;

            var data = svAutoFromPage + "|" + svAutoToPage + "|" + svautoStartTime + "|" + svautoStopTime + "|" + svT2 + "|" + svT3 + "|"
                + svT4 + "|" + svT5 + "|" + svT6 + "|" + svT7 + "|" + svCN + "|" + svautoTimeGetNew + "|"
                + svcheckLoginTime + "|" + svscrapeToPage + "|" + svscrapeFromPage
                + "|" + svtimeBetweenNewFrom + "|" + svtimeBetweenNewTo + "|" + svlinkAPITBox1 + "|" + svlinkAPITBox2
                + "|" + svHNCBox + "|" + svHCMCBox;

            var pathStringFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BDS44");
            if (!Directory.Exists(pathStringFolder))
            {
                Directory.CreateDirectory(pathStringFolder);
            }

            var pathSetting = System.IO.Path.Combine(pathStringFolder, "Setting.txt");
            File.WriteAllText(pathSetting, data);
        }

        //salenha.com//salenha.com//salenha.com//salenha.com//salenha.com//salenha.com//salenha.com//salenha.com//salenha.com//salenha.com//salenha.com
        void defaultSetting_SALENHA()
        {
            dataGridViewNotification_SALENHA.RowHeadersVisible = false;
            var pathStringFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SALENHA");

            if (!Directory.Exists(pathStringFolder))
            {
                Directory.CreateDirectory(pathStringFolder);
            }
            var fileSetting = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SALENHA", "Setting.txt");
            var fileStatus = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SALENHA", "status.txt");
            if (File.Exists(fileStatus))
            {
                var data = File.ReadAllText(fileStatus);
                StatusTBox_SALENHA.Text = data;
            }
            var fileStatusAuto = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SALENHA", "status_auto.txt");
            if (File.Exists(fileStatusAuto))
            {
                var data = File.ReadAllText(fileStatusAuto);
                autoStatusTBox_SALENHA.Text = data;
            }
            //var fileSetting = AppDomain.CurrentDomain.BaseDirectory + "Setting.txt";
            if (File.Exists(fileSetting))
            {
                var data = File.ReadAllText(fileSetting);
                var eleData = data.Split('|');
                //tudong
                autoFromPage_SALENHA.Value = Int32.Parse(eleData[0]);
                autoToPage_SALENHA.Value = Int32.Parse(eleData[1]);
                autoStartTime_SALENHA.Value = DateTime.Parse(eleData[2]);
                autoStopTime_SALENHA.Value = DateTime.Parse(eleData[3]);
                if (eleData[4] == "1")
                    monday_SALENHA.Checked = true;
                else
                    monday_SALENHA.Checked = false;
                if (eleData[5] == "1")
                    tuesday_SALENHA.Checked = true;
                else
                    tuesday_SALENHA.Checked = false;
                if (eleData[6] == "1")
                    wenesday_SALENHA.Checked = true;
                else
                    wenesday_SALENHA.Checked = false;
                if (eleData[7] == "1")
                    thusday_SALENHA.Checked = true;
                else
                    thusday_SALENHA.Checked = false;
                if (eleData[8] == "1")
                    friday_SALENHA.Checked = true;
                else
                    friday_SALENHA.Checked = false;
                if (eleData[9] == "1")
                    satuday_SALENHA.Checked = true;
                else
                    satuday_SALENHA.Checked = false;
                if (eleData[10] == "1")
                    sunday_SALENHA.Checked = true;
                else
                    sunday_SALENHA.Checked = false;

                autoTimeGetNew_SALENHA.Value = Int32.Parse(eleData[11]);
                checkLoginTime_SALENHA.Value = Int32.Parse(eleData[12]);
                if (eleData[13] == "1")
                    autoBanCBox_SALENHA.Checked = true;
                else
                    autoBanCBox_SALENHA.Checked = false;
                if (eleData[14] == "1")
                    autoThueCBox_SALENHA.Checked = true;
                else
                    autoThueCBox_SALENHA.Checked = false;

                //thu cong
                scrapeToPage_SALENHA.Value = Int32.Parse(eleData[15]);
                scrapeFromPage_SALENHA.Value = Int32.Parse(eleData[16]);
                if (eleData[17] == "1")
                    BanCBox_SALENHA.Checked = true;
                else
                    BanCBox_SALENHA.Checked = false;
                if (eleData[18] == "1")
                    ThueCBox_SALENHA.Checked = true;
                else
                    ThueCBox_SALENHA.Checked = false;
                //Cai dat chung
                timeBetweenNewFrom_SALENHA.Value = Int32.Parse(eleData[19]);
                timeBetweenNewTo_SALENHA.Value = Int32.Parse(eleData[20]);
                linkAPITBox1_SALENHA.Text = eleData[21];
                linkAPITBox2_SALENHA.Text = eleData[22];

                if (eleData[23] == "1")
                    TKHNCBox_SALENHA.Checked = true;
                else
                    TKHNCBox_SALENHA.Checked = false;
                if (eleData[24] == "1")
                    TKHCMCBox_SALENHA.Checked = true;
                else
                    TKHCMCBox_SALENHA.Checked = false;

                userHNTBox_SALENHA.Text = eleData[25];
                passHNTBox_SALENHA.Text = eleData[26];

                userHCMTBox_SALENHA.Text = eleData[25];
                passHCMTBox_SALENHA.Text = eleData[26];

                CrawlSALENHA.linkAPI1 = linkAPITBox1_SALENHA.Text;
                CrawlSALENHA.linkAPI2 = linkAPITBox2_SALENHA.Text;
                //scrape
                CrawlSALENHA.fromPage = (int)scrapeFromPage_SALENHA.Value;
                CrawlSALENHA.toPage = (int)scrapeToPage_SALENHA.Value;
                CrawlSALENHA.timeBetweenNewFrom = (int)timeBetweenNewFrom_SALENHA.Value;
                CrawlSALENHA.timeBetweenNewTo = (int)timeBetweenNewTo_SALENHA.Value;
                //auto


            }

        }

        void myBackgroundWorker_DoWork_SALENHA(object sender, DoWorkEventArgs e)
        {
            CrawlSALENHA.runScrape = true;
            //CrawlSALENHA.m_oWorker = m_oWorker_SALENHA;
            CrawlSALENHA.ScrapeRunCrawl(m_oWorker_SALENHA);
        }

        void myBackgroundAutoWorker_DoWork_SALENHA(object sender, DoWorkEventArgs e)
        {
            CrawlSALENHA.runAuto = true;
            //CrawlSALENHA.m_oWorker = m_oWorker_SALENHA;
            CrawlSALENHA.AutoRunCrawl(m_autoWorker_SALENHA);
        }



        void myBackgroundWorker_ProgressChanged_SALENHA(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 0)
            {
                StatusTBox_SALENHA.Text = "đang chạy";
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin thủ công";
                var url = e.UserState.ToString();

                string[] row1 = new string[] { dataGridViewNotification_SALENHA.Rows.Count.ToString(), "Server 1", "xong", time, type, url };
                StatusTBox_SALENHA.Text = "đang chạy, đã đẩy thành công: " + dataGridViewNotification_SALENHA.Rows.Count.ToString();
                dataGridViewNotification_SALENHA.Rows.Insert(0, row1);
                dataGridViewNotification_SALENHA.Rows[0].DefaultCellStyle.ForeColor = Color.Black;
                //dataGridViewNotification.Rows.Add(row1);

            }
            else if (e.ProgressPercentage == 1)
            {
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin thủ công";
                var url = e.UserState.ToString();
                string[] row1 = new string[] { dataGridViewNotification_SALENHA.Rows.Count.ToString(), "Server 1", "lỗi", time, type, url };
                dataGridViewNotification_SALENHA.Rows.Insert(0, row1);
                dataGridViewNotification_SALENHA.Rows[0].DefaultCellStyle.ForeColor = Color.Red;
                //dataGridViewNotification.Rows.Add(row1);
            }
            else if (e.ProgressPercentage == 2)
            {
                var dt = DateTime.Now;
                StatusTBox_SALENHA.Text = "chạy xong thủ công lúc: " + dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                scrapeStartBtn_SALENHA.Enabled = true;
                scrapeStopBtn_SALENHA.Enabled = false;
                var pathStringFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SALENHA");
                if (!Directory.Exists(pathStringFolder))
                {
                    Directory.CreateDirectory(pathStringFolder);
                }

                var pathStatus = System.IO.Path.Combine(pathStringFolder, "status.txt");
                File.WriteAllText(pathStatus, StatusTBox_SALENHA.Text);
            }
            else if (e.ProgressPercentage == 3)
            {
                StatusTBox_SALENHA.Text = "đang chạy";
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin thủ công";
                var url = e.UserState.ToString();

                string[] row1 = new string[] { dataGridViewNotification_SALENHA.Rows.Count.ToString(), "Server 2", "xong", time, type, url };
                StatusTBox_SALENHA.Text = "đang chạy, đã đẩy thành công: " + dataGridViewNotification_SALENHA.Rows.Count.ToString();
                //dataGridViewNotification.Rows.Add(row1);
                dataGridViewNotification_SALENHA.Rows.Insert(0, row1);
                dataGridViewNotification_SALENHA.Rows[0].DefaultCellStyle.ForeColor = Color.Black;

            }
            else if (e.ProgressPercentage == 4)
            {
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin thủ công";
                var url = e.UserState.ToString();
                string[] row1 = new string[] { dataGridViewNotification_SALENHA.Rows.Count.ToString(), "Server 2", "lỗi", time, type, url };
                //dataGridViewNotification.Rows.Add(row1);
                dataGridViewNotification_SALENHA.Rows.Insert(0, row1);
                dataGridViewNotification_LHCC_COM.Rows[0].DefaultCellStyle.ForeColor = Color.Red;
            }
            else if (e.ProgressPercentage == 6)
            {
                StatusTBox_SALENHA.Text = "Đang đợi login";
            }

            existSaleNhaCBox.Checked = Contances.bRMWordsSaleNha;
        }

        void myBackgroundAutoWorker_ProgressChanged_SALENHA(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 0)
            {
                autoStatusTBox_SALENHA.Text = "đang chạy";
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin tự động";
                var url = e.UserState.ToString();
                if (url.Contains("đang chạy"))
                    return;
                string[] row1 = new string[] { dataGridViewNotification_SALENHA.Rows.Count.ToString(), "Server 1", "xong", time, type, url };
                autoStatusTBox_SALENHA.Text = "đang chạy, đã đẩy thành công: " + dataGridViewNotification_SALENHA.Rows.Count.ToString();
                //dataGridViewNotification.Rows.Add(row1);
                dataGridViewNotification_SALENHA.Rows.Insert(0, row1);
                dataGridViewNotification_SALENHA.Rows[0].DefaultCellStyle.ForeColor = Color.Green;


            }
            else if (e.ProgressPercentage == 1)
            {
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin tự động";
                var url = e.UserState.ToString();
                string[] row1 = new string[] { dataGridViewNotification_SALENHA.Rows.Count.ToString(), "Server 1", "lỗi", time, type, url };
                //dataGridViewNotification.Rows.Add(row1);
                dataGridViewNotification_SALENHA.Rows.Insert(0, row1);
                dataGridViewNotification_SALENHA.Rows[0].DefaultCellStyle.ForeColor = Color.Red;
            }
            else if (e.ProgressPercentage == 2)
            {
                var dt = DateTime.Now;
                autoStatusTBox_SALENHA.Text = "chạy xong tự động lúc: " + dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                autoStartBtn_SALENHA.Enabled = true;
                autoStopBtn_SALENHA.Enabled = false;
                var pathStringFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SALENHA");
                if (!Directory.Exists(pathStringFolder))
                {
                    Directory.CreateDirectory(pathStringFolder);
                }

                var pathStatus = System.IO.Path.Combine(pathStringFolder, "status_auto.txt");
                File.WriteAllText(pathStatus, autoStatusTBox_SALENHA.Text);
            }
            else if (e.ProgressPercentage == 5)
            {
                autoStatusTBox_SALENHA.Text = e.UserState.ToString();
                autoStartBtn_SALENHA.Enabled = false;
                autoStopBtn_SALENHA.Enabled = true;
            }
            else if (e.ProgressPercentage == 3)
            {
                autoStatusTBox_SALENHA.Text = "đang chạy";
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin tự động";
                var url = e.UserState.ToString();
                string[] row1 = new string[] { dataGridViewNotification_SALENHA.Rows.Count.ToString(), "Server 2", "xong", time, type, url };
                autoStatusTBox_SALENHA.Text = "đang chạy, đã đẩy thành công: " + dataGridViewNotification_SALENHA.Rows.Count.ToString();
                //dataGridViewNotification.Rows.Add(row1);
                dataGridViewNotification_SALENHA.Rows.Insert(0, row1);
                dataGridViewNotification_SALENHA.Rows[0].DefaultCellStyle.ForeColor = Color.Blue;


            }
            else if (e.ProgressPercentage == 4)
            {
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin tự động";
                var url = e.UserState.ToString();
                string[] row1 = new string[] { dataGridViewNotification_SALENHA.Rows.Count.ToString(), "Server 2", "lỗi", time, type, url };
                //dataGridViewNotification.Rows.Add(row1);
                dataGridViewNotification_SALENHA.Rows.Insert(0, row1);
                dataGridViewNotification_SALENHA.Rows[0].DefaultCellStyle.ForeColor = Color.Red;
            }
            else if (e.ProgressPercentage == 6)
            {
                autoStatusTBox_SALENHA.Text = "Đang đợi login";
            }

            existSaleNhaCBox.Checked = Contances.bRMWordsSaleNha;
        }

        void myBackgroundWorker_RunWorkerCompleted_SALENHA(object sender, RunWorkerCompletedEventArgs e)
        {
            scrapeStartBtn_SALENHA.Enabled = true;
            scrapeStopBtn_SALENHA.Enabled = false;
            //MessageBox.Show(this, "DỪNG THÀNH CÔNG CHẠY THỦ CÔNG");
            CrawlSALENHA.runScrape = false;
        }

        void myBackgroundAutoWorker_RunWorkerCompleted_SALENHA(object sender, RunWorkerCompletedEventArgs e)
        {
            autoStartBtn_SALENHA.Enabled = true;
            autoStopBtn_SALENHA.Enabled = false;
            //MessageBox.Show(this, "DỪNG THÀNH CÔNG CHẠY TỰ ĐỘNG");
            CrawlSALENHA.runAuto = false;
        }

        private void DeleteNotificationBtn_SALENHA_Click(object sender, EventArgs e)
        {
            dataGridViewNotification_SALENHA.Rows.Clear();
        }

        private void scrapeStartBtn_SALENHA_Click(object sender, EventArgs e)
        {
            scrapeStartBtn_SALENHA.Enabled = false;
            scrapeStopBtn_SALENHA.Enabled = true;

            CrawlSALENHA.userHN = userHNTBox_SALENHA.Text;
            CrawlSALENHA.passHN = passHNTBox_SALENHA.Text;

            CrawlSALENHA.userHCM = userHCMTBox_SALENHA.Text;
            CrawlSALENHA.passHCM = passHCMTBox_SALENHA.Text;

            CrawlSALENHA.linkAPI1 = linkAPITBox1_SALENHA.Text;
            CrawlSALENHA.linkAPI2 = linkAPITBox2_SALENHA.Text;
            //scrape
            CrawlSALENHA.fromPage = (int)scrapeFromPage_SALENHA.Value;
            CrawlSALENHA.toPage = (int)scrapeToPage_SALENHA.Value;
            CrawlSALENHA.timeBetweenNewFrom = (int)timeBetweenNewFrom_SALENHA.Value;
            CrawlSALENHA.timeBetweenNewTo = (int)timeBetweenNewTo_SALENHA.Value;
            CrawlSALENHA.crawl_ban_salenha = false;
            CrawlSALENHA.crawl_thue_salenha = false;
            if (BanCBox_SALENHA.Checked == true)
            {
                CrawlSALENHA.crawl_ban_salenha = true;
            }
            if (ThueCBox_SALENHA.Checked == true)
            {
                CrawlSALENHA.crawl_thue_salenha = true;
            }

            CrawlSALENHA.tinh.Clear();
            if (TKHNCBox_SALENHA.Checked)
            {
                CrawlSALENHA.tinh.Add("HN");
            }
            if (TKHCMCBox_SALENHA.Checked)
            {
                CrawlSALENHA.tinh.Add("HCM");
            }

            CrawlSALENHA.runScrape = true;

            StatusTBox_SALENHA.Text = "bắt đầu chạy";

            m_oWorker_SALENHA.RunWorkerAsync();
        }

        private void scrapeStopBtn_SALENHA_Click(object sender, EventArgs e)
        {
            scrapeStopBtn_SALENHA.Enabled = false;
            CrawlSALENHA.runScrape = false;
            //ProcessHelper.KillAllProcessTree("chromedriver");
            m_oWorker_SALENHA.CancelAsync();
            m_oWorker_SALENHA.Dispose();
            StatusTBox_SALENHA.Text = "đang dừng chạy";
        }

        private void autoStartBtn_SALENHA_Click(object sender, EventArgs e)
        {
            autoStartBtn_SALENHA.Enabled = false;
            autoStopBtn_SALENHA.Enabled = true;

            CrawlSALENHA.userHN = userHNTBox_SALENHA.Text;
            CrawlSALENHA.passHN = passHNTBox_SALENHA.Text;

            CrawlSALENHA.userHCM = userHCMTBox_SALENHA.Text;
            CrawlSALENHA.passHCM = passHCMTBox_SALENHA.Text;

            CrawlSALENHA.linkAPI1 = linkAPITBox1_SALENHA.Text;
            CrawlSALENHA.linkAPI2 = linkAPITBox2_SALENHA.Text;
            //scrape
            CrawlSALENHA.autoFromPage = (int)autoFromPage_SALENHA.Value;
            CrawlSALENHA.autoToPage = (int)autoToPage_SALENHA.Value;
            CrawlSALENHA.timeBetweenNewFrom = (int)timeBetweenNewFrom_SALENHA.Value;
            CrawlSALENHA.timeBetweenNewTo = (int)timeBetweenNewTo_SALENHA.Value;

            CrawlSALENHA.auto_crawl_ban_salenha = false;
            CrawlSALENHA.auto_crawl_thue_salenha = false;
            if (autoBanCBox_SALENHA.Checked == true)
            {
                CrawlSALENHA.auto_crawl_ban_salenha = true;
            }
            if (autoThueCBox_SALENHA.Checked == true)
            {
                CrawlSALENHA.auto_crawl_thue_salenha = true;
            }

            CrawlSALENHA.autoTinh.Clear();
            if (TKHNCBox_SALENHA.Checked)
            {
                CrawlSALENHA.autoTinh.Add("HN");
            }
            if (TKHCMCBox_SALENHA.Checked)
            {
                CrawlSALENHA.autoTinh.Add("HCM");
            }

            CrawlSALENHA.beginTime = autoStartTime_SALENHA.Value;
            CrawlSALENHA.endTime = autoStopTime_SALENHA.Value;

            CrawlSALENHA.dayinWeek.Clear();
            if (monday_SALENHA.Checked)
                CrawlSALENHA.dayinWeek.Add((int)DayOfWeek.Monday);
            if (tuesday_SALENHA.Checked)
                CrawlSALENHA.dayinWeek.Add((int)DayOfWeek.Tuesday);
            if (wenesday_SALENHA.Checked)
                CrawlSALENHA.dayinWeek.Add((int)DayOfWeek.Wednesday);
            if (thusday_SALENHA.Checked)
                CrawlSALENHA.dayinWeek.Add((int)DayOfWeek.Thursday);
            if (friday_SALENHA.Checked)
                CrawlSALENHA.dayinWeek.Add((int)DayOfWeek.Friday);
            if (satuday_SALENHA.Checked)
                CrawlSALENHA.dayinWeek.Add((int)DayOfWeek.Saturday);
            if (sunday_SALENHA.Checked)
                CrawlSALENHA.dayinWeek.Add((int)DayOfWeek.Sunday);

            CrawlSALENHA.loopTime = (int)autoTimeGetNew_SALENHA.Value;

            autoStatusTBox_SALENHA.Text = "bắt đầu chạy";

            m_autoWorker_SALENHA.RunWorkerAsync();
        }

        private void autoStopBtn_SALENHA_Click(object sender, EventArgs e)
        {
            autoStopBtn_SALENHA.Enabled = false;
            CrawlSALENHA.runAuto = false;
            //ProcessHelper.KillAllProcessTree("chromedriver");
            m_autoWorker_SALENHA.CancelAsync();
            m_autoWorker_SALENHA.Dispose();
            //while (m_autoWorker.IsBusy)
            //{
            //    autoStatusTBox.Text = "đang dừng !!!!";
            //    Thread.Sleep(2 * 1000);
            //}
            autoStatusTBox_SALENHA.Text = "đang dừng chạy";
        }

        private void SaveSettingBtn_SALENHA_Click(object sender, EventArgs e)
        {
            //tudong
            var svAutoFromPage = autoFromPage_SALENHA.Value.ToString();
            var svAutoToPage = autoToPage_SALENHA.Value.ToString();
            var svautoStartTime = autoStartTime_SALENHA.Value.ToString();
            var svautoStopTime = autoStopTime_SALENHA.Value.ToString();
            var svT2 = monday_SALENHA.Checked ? "1" : "0";
            var svT3 = tuesday_SALENHA.Checked ? "1" : "0";
            var svT4 = wenesday_SALENHA.Checked ? "1" : "0";
            var svT5 = thusday_SALENHA.Checked ? "1" : "0";
            var svT6 = friday_SALENHA.Checked ? "1" : "0";
            var svT7 = satuday_SALENHA.Checked ? "1" : "0";
            var svCN = sunday_SALENHA.Checked ? "1" : "0";
            var svautoTimeGetNew = autoTimeGetNew_SALENHA.Value.ToString();
            var svcheckLoginTime = checkLoginTime_SALENHA.Value.ToString();
            var svautoHNCBox = autoBanCBox_SALENHA.Checked ? "1" : "0";
            var svautoHCMCBox = autoThueCBox_SALENHA.Checked ? "1" : "0";
            //thu cong
            var svscrapeToPage = scrapeToPage_SALENHA.Value.ToString();
            var svscrapeFromPage = scrapeFromPage_SALENHA.Value.ToString();
            var svHNCBox = BanCBox_SALENHA.Checked ? "1" : "0";
            var svHCMCBox = ThueCBox_SALENHA.Checked ? "1" : "0";
            //Cai dat chung
            var svtimeBetweenNewFrom = timeBetweenNewFrom_SALENHA.Value.ToString();
            var svtimeBetweenNewTo = timeBetweenNewTo_SALENHA.Value.ToString();
            var svlinkAPITBox1 = linkAPITBox1_SALENHA.Text;
            var svlinkAPITBox2 = linkAPITBox2_SALENHA.Text;

            var TKHN = TKHNCBox_SALENHA.Checked ? "1" : "0";
            var TKHCM = TKHCMCBox_SALENHA.Checked ? "1" : "0";

            var userHN = userHNTBox_SALENHA.Text;
            var passHN = passHNTBox_SALENHA.Text;

            var userHCM = userHCMTBox_SALENHA.Text;
            var passHCM = passHCMTBox_SALENHA.Text;


            var data = svAutoFromPage + "|" + svAutoToPage + "|" + svautoStartTime + "|" + svautoStopTime + "|" + svT2 + "|" + svT3 + "|"
                + svT4 + "|" + svT5 + "|" + svT6 + "|" + svT7 + "|" + svCN + "|" + svautoTimeGetNew + "|"
                + svcheckLoginTime + "|" + svautoHNCBox + "|" + svautoHCMCBox + "|" + svscrapeToPage + "|" + svscrapeFromPage + "|" + svHNCBox + "|"
                + svHCMCBox + "|" + svtimeBetweenNewFrom + "|" + svtimeBetweenNewTo + "|" + svlinkAPITBox1 + "|" + svlinkAPITBox2
                + "|" + TKHN + "|" + TKHCM
                + "|" + userHN + "|" + passHN
                + "|" + userHCM + "|" + passHCM;

            var pathStringFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SALENHA");
            if (!Directory.Exists(pathStringFolder))
            {
                Directory.CreateDirectory(pathStringFolder);
            }

            var pathSetting = System.IO.Path.Combine(pathStringFolder, "Setting.txt");
            File.WriteAllText(pathSetting, data);
        }

        //SALENHA_VIP.com//SALENHA_VIP.com//SALENHA_VIP.com//SALENHA_VIP.com//SALENHA_VIP.com//SALENHA_VIP.com//SALENHA_VIP.com//SALENHA_VIP.com//SALENHA_VIP.com//SALENHA_VIP.com//SALENHA_VIP.com
        void defaultSetting_SALENHA_VIP()
        {
            dataGridViewNotification_SALENHA_VIP.RowHeadersVisible = false;
            var pathStringFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SALENHA_VIP");

            if (!Directory.Exists(pathStringFolder))
            {
                Directory.CreateDirectory(pathStringFolder);
            }
            var fileSetting = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SALENHA_VIP", "Setting.txt");
            var fileStatus = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SALENHA_VIP", "status.txt");
            if (File.Exists(fileStatus))
            {
                var data = File.ReadAllText(fileStatus);
                StatusTBox_SALENHA_VIP.Text = data;
            }
            var fileStatusAuto = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SALENHA_VIP", "status_auto.txt");
            if (File.Exists(fileStatusAuto))
            {
                var data = File.ReadAllText(fileStatusAuto);
                autoStatusTBox_SALENHA_VIP.Text = data;
            }

            //var fileSetting = AppDomain.CurrentDomain.BaseDirectory + "Setting.txt";
            if (File.Exists(fileSetting))
            {
                var data = File.ReadAllText(fileSetting);
                var eleData = data.Split('|');
                //tudong
                autoFromPage_SALENHA_VIP.Value = Int32.Parse(eleData[0]);
                autoToPage_SALENHA_VIP.Value = Int32.Parse(eleData[1]);
                autoStartTime_SALENHA_VIP.Value = DateTime.Parse(eleData[2]);
                autoStopTime_SALENHA_VIP.Value = DateTime.Parse(eleData[3]);
                if (eleData[4] == "1")
                    monday_SALENHA_VIP.Checked = true;
                else
                    monday_SALENHA_VIP.Checked = false;
                if (eleData[5] == "1")
                    tuesday_SALENHA_VIP.Checked = true;
                else
                    tuesday_SALENHA_VIP.Checked = false;
                if (eleData[6] == "1")
                    wenesday_SALENHA_VIP.Checked = true;
                else
                    wenesday_SALENHA_VIP.Checked = false;
                if (eleData[7] == "1")
                    thusday_SALENHA_VIP.Checked = true;
                else
                    thusday_SALENHA_VIP.Checked = false;
                if (eleData[8] == "1")
                    friday_SALENHA_VIP.Checked = true;
                else
                    friday_SALENHA_VIP.Checked = false;
                if (eleData[9] == "1")
                    satuday_SALENHA_VIP.Checked = true;
                else
                    satuday_SALENHA_VIP.Checked = false;
                if (eleData[10] == "1")
                    sunday_SALENHA_VIP.Checked = true;
                else
                    sunday_SALENHA_VIP.Checked = false;

                autoTimeGetNew_SALENHA_VIP.Value = Int32.Parse(eleData[11]);
                checkLoginTime_SALENHA_VIP.Value = Int32.Parse(eleData[12]);
                if (eleData[13] == "1")
                    autoBanCBox_SALENHA_VIP.Checked = true;
                else
                    autoBanCBox_SALENHA_VIP.Checked = false;
                if (eleData[14] == "1")
                    autoThueCBox_SALENHA_VIP.Checked = true;
                else
                    autoThueCBox_SALENHA_VIP.Checked = false;

                //thu cong
                scrapeToPage_SALENHA_VIP.Value = Int32.Parse(eleData[15]);
                scrapeFromPage_SALENHA_VIP.Value = Int32.Parse(eleData[16]);
                if (eleData[17] == "1")
                    banCBox_SALENHA_VIP.Checked = true;
                else
                    banCBox_SALENHA_VIP.Checked = false;
                if (eleData[18] == "1")
                    ThueCBox_SALENHA_VIP.Checked = true;
                else
                    ThueCBox_SALENHA_VIP.Checked = false;
                //Cai dat chung
                timeBetweenNewFrom_SALENHA_VIP.Value = Int32.Parse(eleData[19]);
                timeBetweenNewTo_SALENHA_VIP.Value = Int32.Parse(eleData[20]);
                linkAPITBox1_SALENHA_VIP.Text = eleData[21];
                linkAPITBox2_SALENHA_VIP.Text = eleData[22];

                if (eleData[23] == "1")
                    TKHNCBox_SALENHA_VIP.Checked = true;
                else
                    TKHNCBox_SALENHA_VIP.Checked = false;
                if (eleData[24] == "1")
                    TKHCMCBox_SALENHA_VIP.Checked = true;
                else
                    TKHCMCBox_SALENHA_VIP.Checked = false;

                CrawlSALENHA_VIP.linkAPI1 = linkAPITBox1_SALENHA_VIP.Text;
                CrawlSALENHA_VIP.linkAPI2 = linkAPITBox2_SALENHA_VIP.Text;
                //scrape
                CrawlSALENHA_VIP.fromPage = (int)scrapeFromPage_SALENHA_VIP.Value;
                CrawlSALENHA_VIP.toPage = (int)scrapeToPage_SALENHA_VIP.Value;
                CrawlSALENHA_VIP.timeBetweenNewFrom = (int)timeBetweenNewFrom_SALENHA_VIP.Value;
                CrawlSALENHA_VIP.timeBetweenNewTo = (int)timeBetweenNewTo_SALENHA_VIP.Value;
                //auto


            }

        }

        void myBackgroundWorker_DoWork_SALENHA_VIP(object sender, DoWorkEventArgs e)
        {
            CrawlSALENHA_VIP.runScrape = true;
            CrawlSALENHA_VIP.ScrapeRunCrawl(m_oWorker_SALENHA_VIP);
        }

        void myBackgroundAutoWorker_DoWork_SALENHA_VIP(object sender, DoWorkEventArgs e)
        {
            CrawlSALENHA_VIP.runAuto = true;
            CrawlSALENHA_VIP.AutoRunCrawl(m_autoWorker_SALENHA_VIP);
        }



        void myBackgroundWorker_ProgressChanged_SALENHA_VIP(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 0)
            {
                StatusTBox_SALENHA_VIP.Text = "đang chạy";
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin thủ công";
                var url = e.UserState.ToString();

                string[] row1 = new string[] { dataGridViewNotification_SALENHA_VIP.Rows.Count.ToString(), "Server 1", "xong", time, type, url };
                StatusTBox_SALENHA_VIP.Text = "đang chạy, đã đẩy thành công: " + dataGridViewNotification_SALENHA_VIP.Rows.Count.ToString();
                dataGridViewNotification_SALENHA_VIP.Rows.Insert(0, row1);
                dataGridViewNotification_SALENHA_VIP.Rows[0].DefaultCellStyle.ForeColor = Color.Black;
            }
            else if (e.ProgressPercentage == 1)
            {
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin thủ công";
                var url = e.UserState.ToString();
                string[] row1 = new string[] { dataGridViewNotification_SALENHA_VIP.Rows.Count.ToString(), "Server 1", "lỗi", time, type, url };
                dataGridViewNotification_SALENHA_VIP.Rows.Insert(0, row1);
                dataGridViewNotification_SALENHA_VIP.Rows[0].DefaultCellStyle.ForeColor = Color.Red;
                //dataGridViewNotification.Rows.Add(row1);
            }
            else if (e.ProgressPercentage == 2)
            {
                var dt = DateTime.Now;
                StatusTBox_SALENHA_VIP.Text = "chạy xong thủ công lúc: " + dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                scrapeStartBtn_SALENHA_VIP.Enabled = true;
                scrapeStopBtn_SALENHA_VIP.Enabled = false;
                var pathStringFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SALENHA_VIP");
                if (!Directory.Exists(pathStringFolder))
                {
                    Directory.CreateDirectory(pathStringFolder);
                }

                var pathStatus = System.IO.Path.Combine(pathStringFolder, "status.txt");
                File.WriteAllText(pathStatus, StatusTBox_SALENHA_VIP.Text);
            }
            else if (e.ProgressPercentage == 3)
            {
                StatusTBox_SALENHA_VIP.Text = "đang chạy";
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin thủ công";
                var url = e.UserState.ToString();

                string[] row1 = new string[] { dataGridViewNotification_SALENHA_VIP.Rows.Count.ToString(), "Server 2", "xong", time, type, url };
                StatusTBox_SALENHA_VIP.Text = "đang chạy, đã đẩy thành công: " + dataGridViewNotification_SALENHA_VIP.Rows.Count.ToString();
                //dataGridViewNotification.Rows.Add(row1);
                dataGridViewNotification_SALENHA_VIP.Rows.Insert(0, row1);
                dataGridViewNotification_SALENHA_VIP.Rows[0].DefaultCellStyle.ForeColor = Color.Black;

            }
            else if (e.ProgressPercentage == 4)
            {
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin thủ công";
                var url = e.UserState.ToString();
                string[] row1 = new string[] { dataGridViewNotification_SALENHA_VIP.Rows.Count.ToString(), "Server 2", "lỗi", time, type, url };
                //dataGridViewNotification.Rows.Add(row1);
                dataGridViewNotification_SALENHA_VIP.Rows.Insert(0, row1);
                dataGridViewNotification_SALENHA_VIP.Rows[0].DefaultCellStyle.ForeColor = Color.Red;
            }
            else if (e.ProgressPercentage == 6)
            {
                StatusTBox_SALENHA_VIP.Text = "Đang đợi login";
            }
        }

        void myBackgroundAutoWorker_ProgressChanged_SALENHA_VIP(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 0)
            {
                autoStatusTBox_SALENHA_VIP.Text = "đang chạy";
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin tự động";
                var url = e.UserState.ToString();
                if (url.Contains("đang chạy"))
                    return;
                string[] row1 = new string[] { dataGridViewNotification_SALENHA_VIP.Rows.Count.ToString(), "Server 1", "xong", time, type, url };
                autoStatusTBox_SALENHA_VIP.Text = "đang chạy, đã đẩy thành công: " + dataGridViewNotification_SALENHA_VIP.Rows.Count.ToString();
                //dataGridViewNotification.Rows.Add(row1);
                dataGridViewNotification_SALENHA_VIP.Rows.Insert(0, row1);
                dataGridViewNotification_SALENHA_VIP.Rows[0].DefaultCellStyle.ForeColor = Color.Green;


            }
            else if (e.ProgressPercentage == 1)
            {
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin tự động";
                var url = e.UserState.ToString();
                string[] row1 = new string[] { dataGridViewNotification_SALENHA_VIP.Rows.Count.ToString(), "Server 1", "lỗi", time, type, url };
                //dataGridViewNotification.Rows.Add(row1);
                dataGridViewNotification_SALENHA_VIP.Rows.Insert(0, row1);
                dataGridViewNotification_SALENHA_VIP.Rows[0].DefaultCellStyle.ForeColor = Color.Red;
            }
            else if (e.ProgressPercentage == 2)
            {
                var dt = DateTime.Now;
                autoStatusTBox_SALENHA_VIP.Text = "chạy xong tự động lúc: " + dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                autoStartBtn_SALENHA_VIP.Enabled = true;
                autoStopBtn_SALENHA_VIP.Enabled = false;
                var pathStringFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SALENHA_VIP");
                if (!Directory.Exists(pathStringFolder))
                {
                    Directory.CreateDirectory(pathStringFolder);
                }

                var pathStatus = System.IO.Path.Combine(pathStringFolder, "status_auto.txt");
                File.WriteAllText(pathStatus, autoStatusTBox_SALENHA_VIP.Text);
            }
            else if (e.ProgressPercentage == 5)
            {
                autoStatusTBox_SALENHA_VIP.Text = e.UserState.ToString();
                autoStartBtn_SALENHA_VIP.Enabled = false;
                autoStopBtn_SALENHA_VIP.Enabled = true;
            }
            else if (e.ProgressPercentage == 3)
            {
                autoStatusTBox_SALENHA_VIP.Text = "đang chạy";
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin tự động";
                var url = e.UserState.ToString();
                string[] row1 = new string[] { dataGridViewNotification_SALENHA_VIP.Rows.Count.ToString(), "Server 2", "xong", time, type, url };
                autoStatusTBox_SALENHA_VIP.Text = "đang chạy, đã đẩy thành công: " + dataGridViewNotification_SALENHA_VIP.Rows.Count.ToString();
                //dataGridViewNotification.Rows.Add(row1);
                dataGridViewNotification_SALENHA_VIP.Rows.Insert(0, row1);
                dataGridViewNotification_SALENHA_VIP.Rows[0].DefaultCellStyle.ForeColor = Color.Blue;


            }
            else if (e.ProgressPercentage == 4)
            {
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin tự động";
                var url = e.UserState.ToString();
                string[] row1 = new string[] { dataGridViewNotification_SALENHA_VIP.Rows.Count.ToString(), "Server 2", "lỗi", time, type, url };
                //dataGridViewNotification.Rows.Add(row1);
                dataGridViewNotification_SALENHA_VIP.Rows.Insert(0, row1);
                dataGridViewNotification_SALENHA_VIP.Rows[0].DefaultCellStyle.ForeColor = Color.Red;
            }
            else if (e.ProgressPercentage == 6)
            {
                autoStatusTBox_SALENHA_VIP.Text = "Đang đợi login";
            }
        }

        void myBackgroundWorker_RunWorkerCompleted_SALENHA_VIP(object sender, RunWorkerCompletedEventArgs e)
        {
            scrapeStartBtn_SALENHA_VIP.Enabled = true;
            scrapeStopBtn_SALENHA_VIP.Enabled = false;
            //MessageBox.Show(this, "DỪNG THÀNH CÔNG CHẠY THỦ CÔNG");
            CrawlSALENHA_VIP.runScrape = false;
        }

        void myBackgroundAutoWorker_RunWorkerCompleted_SALENHA_VIP(object sender, RunWorkerCompletedEventArgs e)
        {
            autoStartBtn_SALENHA_VIP.Enabled = true;
            autoStopBtn_SALENHA_VIP.Enabled = false;
            //MessageBox.Show(this, "DỪNG THÀNH CÔNG CHẠY TỰ ĐỘNG");
            CrawlSALENHA_VIP.runAuto = false;

        }

        private void scrapeStartBtn_SALENHA_VIP_Click(object sender, EventArgs e)
        {
            scrapeStartBtn_SALENHA_VIP.Enabled = false;
            scrapeStopBtn_SALENHA_VIP.Enabled = true;

            CrawlSALENHA_VIP.linkAPI1 = linkAPITBox1_SALENHA_VIP.Text;
            CrawlSALENHA_VIP.linkAPI2 = linkAPITBox2_SALENHA_VIP.Text;
            //scrape
            CrawlSALENHA_VIP.fromPage = (int)scrapeFromPage_SALENHA_VIP.Value;
            CrawlSALENHA_VIP.toPage = (int)scrapeToPage_SALENHA_VIP.Value;
            CrawlSALENHA_VIP.timeBetweenNewFrom = (int)timeBetweenNewFrom_SALENHA_VIP.Value;
            CrawlSALENHA_VIP.timeBetweenNewTo = (int)timeBetweenNewTo_SALENHA_VIP.Value;
            CrawlSALENHA_VIP.tinh.Clear();
            CrawlSALENHA_VIP.crawl_ban_salenha_vip = false;
            CrawlSALENHA_VIP.crawl_thue_salenha_vip = false;
            if (banCBox_SALENHA_VIP.Checked == true)
            {
                CrawlSALENHA_VIP.crawl_ban_salenha_vip = true;
            }
            if (ThueCBox_SALENHA_VIP.Checked == true)
            {
                CrawlSALENHA_VIP.crawl_thue_salenha_vip = true;
            }

            CrawlSALENHA_VIP.tinh.Clear();
            if (TKHNCBox_SALENHA_VIP.Checked)
            {
                CrawlSALENHA_VIP.tinh.Add("HN");
            }
            if (TKHCMCBox_SALENHA_VIP.Checked)
            {
                CrawlSALENHA_VIP.tinh.Add("HCM");
            }

            CrawlSALENHA_VIP.runScrape = true;
            StatusTBox_SALENHA_VIP.Text = "bắt đầu chạy";

            m_oWorker_SALENHA_VIP.RunWorkerAsync();
        }

        private void scrapeStopBtn_SALENHA_VIP_Click(object sender, EventArgs e)
        {
            scrapeStopBtn_SALENHA_VIP.Enabled = false;
            CrawlSALENHA_VIP.runScrape = false;
            m_oWorker_SALENHA_VIP.CancelAsync();
            m_oWorker_SALENHA_VIP.Dispose();
            StatusTBox_SALENHA_VIP.Text = "đang dừng chạy";
        }

        private void autoStartBtn_SALENHA_VIP_Click(object sender, EventArgs e)
        {
            autoStartBtn_SALENHA_VIP.Enabled = false;
            autoStopBtn_SALENHA_VIP.Enabled = true;

            CrawlSALENHA_VIP.linkAPI1 = linkAPITBox1_SALENHA_VIP.Text;
            CrawlSALENHA_VIP.linkAPI2 = linkAPITBox2_SALENHA_VIP.Text;
            //scrape
            CrawlSALENHA_VIP.autoFromPage = (int)autoFromPage_SALENHA_VIP.Value;
            CrawlSALENHA_VIP.autoToPage = (int)autoToPage_SALENHA_VIP.Value;
            CrawlSALENHA_VIP.timeBetweenNewFrom = (int)timeBetweenNewFrom_SALENHA_VIP.Value;
            CrawlSALENHA_VIP.timeBetweenNewTo = (int)timeBetweenNewTo_SALENHA_VIP.Value;

            CrawlSALENHA_VIP.auto_crawl_ban_salenha_vip = false;
            CrawlSALENHA_VIP.auto_crawl_thue_salenha_vip = false;
            if (autoBanCBox_SALENHA_VIP.Checked == true)
            {
                CrawlSALENHA_VIP.auto_crawl_ban_salenha_vip = true;
            }
            if (autoThueCBox_SALENHA_VIP.Checked == true)
            {
                CrawlSALENHA_VIP.auto_crawl_thue_salenha_vip = true;
            }

            CrawlSALENHA_VIP.autoTinh.Clear();
            if (TKHNCBox_SALENHA_VIP.Checked)
            {
                CrawlSALENHA_VIP.autoTinh.Add("HN");
            }
            if (TKHCMCBox_SALENHA_VIP.Checked)
            {
                CrawlSALENHA_VIP.autoTinh.Add("HCM");
            }

            CrawlSALENHA_VIP.beginTime = autoStartTime_SALENHA_VIP.Value;
            CrawlSALENHA_VIP.endTime = autoStopTime_SALENHA_VIP.Value;

            CrawlSALENHA_VIP.dayinWeek.Clear();
            if (monday_SALENHA_VIP.Checked)
                CrawlSALENHA_VIP.dayinWeek.Add((int)DayOfWeek.Monday);
            if (tuesday_SALENHA_VIP.Checked)
                CrawlSALENHA_VIP.dayinWeek.Add((int)DayOfWeek.Tuesday);
            if (wenesday_SALENHA_VIP.Checked)
                CrawlSALENHA_VIP.dayinWeek.Add((int)DayOfWeek.Wednesday);
            if (thusday_SALENHA_VIP.Checked)
                CrawlSALENHA_VIP.dayinWeek.Add((int)DayOfWeek.Thursday);
            if (friday_SALENHA_VIP.Checked)
                CrawlSALENHA_VIP.dayinWeek.Add((int)DayOfWeek.Friday);
            if (satuday_SALENHA_VIP.Checked)
                CrawlSALENHA_VIP.dayinWeek.Add((int)DayOfWeek.Saturday);
            if (sunday_SALENHA_VIP.Checked)
                CrawlSALENHA_VIP.dayinWeek.Add((int)DayOfWeek.Sunday);

            CrawlSALENHA_VIP.loopTime = (int)autoTimeGetNew_SALENHA_VIP.Value;

            autoStatusTBox_SALENHA_VIP.Text = "bắt đầu chạy";

            m_autoWorker_SALENHA_VIP.RunWorkerAsync();
        }

        private void autoStopBtn_SALENHA_VIP_Click(object sender, EventArgs e)
        {
            autoStopBtn_SALENHA_VIP.Enabled = false;
            CrawlSALENHA_VIP.runAuto = false;
            //ProcessHelper.KillAllProcessTree("chromedriver");
            m_autoWorker_SALENHA_VIP.CancelAsync();
            m_autoWorker_SALENHA_VIP.Dispose();
            //while (m_autoWorker.IsBusy)
            //{
            //    autoStatusTBox.Text = "đang dừng !!!!";
            //    Thread.Sleep(2 * 1000);
            //}
            autoStatusTBox_SALENHA_VIP.Text = "đang dừng chạy";
        }

        private void DeleteNotificationBtn_SALENHA_VIP_Click(object sender, EventArgs e)
        {
            dataGridViewNotification_SALENHA_VIP.Rows.Clear();
        }

        private void SaveSettingBtn_SALENHA_VIP_Click(object sender, EventArgs e)
        {
            //tudong
            var svAutoFromPage = autoFromPage_SALENHA_VIP.Value.ToString();
            var svAutoToPage = autoToPage_SALENHA_VIP.Value.ToString();
            var svautoStartTime = autoStartTime_SALENHA_VIP.Value.ToString();
            var svautoStopTime = autoStopTime_SALENHA_VIP.Value.ToString();
            var svT2 = monday_SALENHA_VIP.Checked ? "1" : "0";
            var svT3 = tuesday_SALENHA_VIP.Checked ? "1" : "0";
            var svT4 = wenesday_SALENHA_VIP.Checked ? "1" : "0";
            var svT5 = thusday_SALENHA_VIP.Checked ? "1" : "0";
            var svT6 = friday_SALENHA_VIP.Checked ? "1" : "0";
            var svT7 = satuday_SALENHA_VIP.Checked ? "1" : "0";
            var svCN = sunday_SALENHA_VIP.Checked ? "1" : "0";
            var svautoTimeGetNew = autoTimeGetNew_SALENHA_VIP.Value.ToString();
            var svcheckLoginTime = checkLoginTime_SALENHA_VIP.Value.ToString();
            var svautoHNCBox = autoBanCBox_SALENHA_VIP.Checked ? "1" : "0";
            var svautoHCMCBox = autoThueCBox_SALENHA_VIP.Checked ? "1" : "0";
            //thu cong
            var svscrapeToPage = scrapeToPage_SALENHA_VIP.Value.ToString();
            var svscrapeFromPage = scrapeFromPage_SALENHA_VIP.Value.ToString();
            var svHNCBox = banCBox_SALENHA_VIP.Checked ? "1" : "0";
            var svHCMCBox = ThueCBox_SALENHA_VIP.Checked ? "1" : "0";
            //Cai dat chung
            var svtimeBetweenNewFrom = timeBetweenNewFrom_SALENHA_VIP.Value.ToString();
            var svtimeBetweenNewTo = timeBetweenNewTo_SALENHA_VIP.Value.ToString();
            var svlinkAPITBox1 = linkAPITBox1_SALENHA_VIP.Text;
            var svlinkAPITBox2 = linkAPITBox2_SALENHA_VIP.Text;

            var tkHN = TKHNCBox_SALENHA_VIP.Checked ? "1" : "0";
            var tkHCM = TKHCMCBox_SALENHA_VIP.Checked ? "1" : "0";

            var data = svAutoFromPage + "|" + svAutoToPage + "|" + svautoStartTime + "|" + svautoStopTime + "|" + svT2 + "|" + svT3 + "|"
                + svT4 + "|" + svT5 + "|" + svT6 + "|" + svT7 + "|" + svCN + "|" + svautoTimeGetNew + "|"
                + svcheckLoginTime + "|" + svautoHNCBox + "|" + svautoHCMCBox + "|" + svscrapeToPage + "|" + svscrapeFromPage + "|" + svHNCBox + "|"
                + svHCMCBox + "|" + svtimeBetweenNewFrom + "|" + svtimeBetweenNewTo + "|" + svlinkAPITBox1 + "|" + svlinkAPITBox2
                 + "|" + tkHN + "|" + tkHCM;

            var pathStringFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SALENHA_VIP");
            if (!Directory.Exists(pathStringFolder))
            {
                Directory.CreateDirectory(pathStringFolder);
            }

            var pathSetting = System.IO.Path.Combine(pathStringFolder, "Setting.txt");
            File.WriteAllText(pathSetting, data);
        }

        //NCC.com//NCC.com//NCC.com//NCC.com//NCC.com//NCC.com//NCC.com//NCC.com//NCC.com//NCC.com//NCC.com
        void defaultSetting_NCC()
        {
            dataGridViewNotification_NCC.RowHeadersVisible = false;
            var pathStringFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NCC");

            if (!Directory.Exists(pathStringFolder))
            {
                Directory.CreateDirectory(pathStringFolder);
            }
            var fileSetting = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NCC", "Setting.txt");
            var fileStatus = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NCC", "status.txt");
            if (File.Exists(fileStatus))
            {
                var data = File.ReadAllText(fileStatus);
                StatusTBox_NCC.Text = data;
            }

            var fileStatusAuto = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NCC", "status_auto.txt");
            if (File.Exists(fileStatusAuto))
            {
                var data = File.ReadAllText(fileStatusAuto);
                autoStatusTBox_NCC.Text = data;
            }

            //var fileSetting = AppDomain.CurrentDomain.BaseDirectory + "Setting.txt";
            if (File.Exists(fileSetting))
            {
                var data = File.ReadAllText(fileSetting);
                var eleData = data.Split('|');
                //tudong
                autoFromPage_NCC.Value = Int32.Parse(eleData[0]);
                autoToPage_NCC.Value = Int32.Parse(eleData[1]);
                autoStartTime_NCC.Value = DateTime.Parse(eleData[2]);
                autoStopTime_NCC.Value = DateTime.Parse(eleData[3]);
                if (eleData[4] == "1")
                    monday_NCC.Checked = true;
                else
                    monday_NCC.Checked = false;
                if (eleData[5] == "1")
                    tuesday_NCC.Checked = true;
                else
                    tuesday_NCC.Checked = false;
                if (eleData[6] == "1")
                    wenesday_NCC.Checked = true;
                else
                    wenesday_NCC.Checked = false;
                if (eleData[7] == "1")
                    thusday_NCC.Checked = true;
                else
                    thusday_NCC.Checked = false;
                if (eleData[8] == "1")
                    friday_NCC.Checked = true;
                else
                    friday_NCC.Checked = false;
                if (eleData[9] == "1")
                    satuday_NCC.Checked = true;
                else
                    satuday_NCC.Checked = false;
                if (eleData[10] == "1")
                    sunday_NCC.Checked = true;
                else
                    sunday_NCC.Checked = false; https://nguonchinhchu.vn/api/import-crawl-post/tcc

                autoTimeGetNew_NCC.Value = Int32.Parse(eleData[11]);
                checkLoginTime_NCC.Value = Int32.Parse(eleData[12]);
                //thu cong
                scrapeToPage_NCC.Value = Int32.Parse(eleData[13]);
                scrapeFromPage_NCC.Value = Int32.Parse(eleData[14]);
                //Cai dat chung
                timeBetweenNewFrom_NCC.Value = Int32.Parse(eleData[15]);
                timeBetweenNewTo_NCC.Value = Int32.Parse(eleData[16]);
                linkAPITBox1_NCC.Text = eleData[17];
                linkAPITBox2_NCC.Text = eleData[18];
                if (eleData[19] == "1")
                    TKHNCBox_NCC.Checked = true;
                else
                    TKHNCBox_NCC.Checked = false;

                if (eleData[20] == "1")
                    TKHCMCBox_NCC.Checked = true;
                else
                    TKHCMCBox_NCC.Checked = false;


                CrawlNCC.linkAPI1 = linkAPITBox1_NCC.Text;
                CrawlNCC.linkAPI2 = linkAPITBox2_NCC.Text;
                //scrape
                CrawlNCC.fromPage = (int)scrapeFromPage_NCC.Value;
                CrawlNCC.toPage = (int)scrapeToPage_NCC.Value;
                CrawlNCC.timeBetweenNewFrom = (int)timeBetweenNewFrom_NCC.Value;
                CrawlNCC.timeBetweenNewTo = (int)timeBetweenNewTo_NCC.Value;
                //auto
            }

        }

        void myBackgroundWorker_DoWork_NCC(object sender, DoWorkEventArgs e)
        {
            CrawlNCC.runScrape = true;
            CrawlNCC.ScrapeRunCrawl(m_oWorker_NCC);
        }

        void myBackgroundAutoWorker_DoWork_NCC(object sender, DoWorkEventArgs e)
        {
            CrawlNCC.runAuto = true;
            CrawlNCC.AutoRunCrawl(m_autoWorker_NCC);
        }



        void myBackgroundWorker_ProgressChanged_NCC(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 0)
            {
                StatusTBox_NCC.Text = "đang chạy";
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin thủ công";
                var url = e.UserState.ToString();

                string[] row1 = new string[] { dataGridViewNotification_NCC.Rows.Count.ToString(), "Server 1", "xong", time, type, url };
                StatusTBox_NCC.Text = "đang chạy, đã đẩy thành công: " + dataGridViewNotification_NCC.Rows.Count.ToString();
                dataGridViewNotification_NCC.Rows.Insert(0, row1);
                dataGridViewNotification_NCC.Rows[0].DefaultCellStyle.ForeColor = Color.Black;

            }
            else if (e.ProgressPercentage == 1)
            {
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin thủ công";
                var url = e.UserState.ToString();
                string[] row1 = new string[] { dataGridViewNotification_NCC.Rows.Count.ToString(), "Server 1", "lỗi", time, type, url };
                dataGridViewNotification_NCC.Rows.Insert(0, row1);
                dataGridViewNotification_NCC.Rows[0].DefaultCellStyle.ForeColor = Color.Red;
                //dataGridViewNotification.Rows.Add(row1);
            }
            else if (e.ProgressPercentage == 2)
            {
                var dt = DateTime.Now;
                StatusTBox_NCC.Text = "chạy xong thủ công lúc: " + dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                scrapeStartBtn_NCC.Enabled = true;
                scrapeStopBtn_NCC.Enabled = false;
                var pathStringFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NCC");
                if (!Directory.Exists(pathStringFolder))
                {
                    Directory.CreateDirectory(pathStringFolder);
                }

                var pathStatus = System.IO.Path.Combine(pathStringFolder, "status.txt");
                File.WriteAllText(pathStatus, StatusTBox_NCC.Text);
            }
            else if (e.ProgressPercentage == 3)
            {
                StatusTBox_NCC.Text = "đang chạy";
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin thủ công";
                var url = e.UserState.ToString();

                string[] row1 = new string[] { dataGridViewNotification_NCC.Rows.Count.ToString(), "Server 2", "xong", time, type, url };
                StatusTBox_NCC.Text = "đang chạy, đã đẩy thành công: " + dataGridViewNotification_NCC.Rows.Count.ToString();
                //dataGridViewNotification.Rows.Add(row1);
                dataGridViewNotification_NCC.Rows.Insert(0, row1);
                dataGridViewNotification_NCC.Rows[0].DefaultCellStyle.ForeColor = Color.Black;

            }
            else if (e.ProgressPercentage == 4)
            {
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin thủ công";
                var url = e.UserState.ToString();
                string[] row1 = new string[] { dataGridViewNotification_NCC.Rows.Count.ToString(), "Server 2", "lỗi", time, type, url };
                //dataGridViewNotification.Rows.Add(row1);
                dataGridViewNotification_NCC.Rows.Insert(0, row1);
                dataGridViewNotification_NCC.Rows[0].DefaultCellStyle.ForeColor = Color.Red;
            }
            else if (e.ProgressPercentage == 6)
            {
                StatusTBox_NCC.Text = "Đang đợi login";
            }
        }

        void myBackgroundAutoWorker_ProgressChanged_NCC(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 0)
            {
                autoStatusTBox_NCC.Text = "đang chạy";
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin tự động";
                var url = e.UserState.ToString();
                if (url.Contains("đang chạy"))
                    return;
                string[] row1 = new string[] { dataGridViewNotification_NCC.Rows.Count.ToString(), "Server 1", "xong", time, type, url };
                autoStatusTBox_NCC.Text = "đang chạy, đã đẩy thành công: " + dataGridViewNotification_NCC.Rows.Count.ToString();
                //dataGridViewNotification.Rows.Add(row1);
                dataGridViewNotification_NCC.Rows.Insert(0, row1);
                dataGridViewNotification_NCC.Rows[0].DefaultCellStyle.ForeColor = Color.Green;


            }
            else if (e.ProgressPercentage == 1)
            {
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin tự động";
                var url = e.UserState.ToString();
                string[] row1 = new string[] { dataGridViewNotification_NCC.Rows.Count.ToString(), "Server 1", "lỗi", time, type, url };
                //dataGridViewNotification.Rows.Add(row1);
                dataGridViewNotification_NCC.Rows.Insert(0, row1);
                dataGridViewNotification_NCC.Rows[0].DefaultCellStyle.ForeColor = Color.Red;

            }
            else if (e.ProgressPercentage == 2)
            {
                var dt = DateTime.Now;
                autoStatusTBox_NCC.Text = "chạy xong tự động lúc: " + dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                autoStartBtn_NCC.Enabled = true;
                autoStopBtn_NCC.Enabled = false;

                var pathStringFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NCC");
                if (!Directory.Exists(pathStringFolder))
                {
                    Directory.CreateDirectory(pathStringFolder);
                }

                var pathStatus = System.IO.Path.Combine(pathStringFolder, "status_auto.txt");
                File.WriteAllText(pathStatus, autoStatusTBox_NCC.Text);
            }
            else if (e.ProgressPercentage == 5)
            {
                autoStatusTBox_NCC.Text = e.UserState.ToString();
                autoStartBtn_NCC.Enabled = false;
                autoStopBtn_NCC.Enabled = true;
            }
            else if (e.ProgressPercentage == 3)
            {
                autoStatusTBox_NCC.Text = "đang chạy";
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin tự động";
                var url = e.UserState.ToString();
                string[] row1 = new string[] { dataGridViewNotification_NCC.Rows.Count.ToString(), "Server 2", "xong", time, type, url };
                autoStatusTBox_NCC.Text = "đang chạy, đã đẩy thành công: " + dataGridViewNotification_NCC.Rows.Count.ToString();
                //dataGridViewNotification.Rows.Add(row1);
                dataGridViewNotification_NCC.Rows.Insert(0, row1);
                dataGridViewNotification_NCC.Rows[0].DefaultCellStyle.ForeColor = Color.Blue;


            }
            else if (e.ProgressPercentage == 4)
            {
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin tự động";
                var url = e.UserState.ToString();
                string[] row1 = new string[] { dataGridViewNotification_NCC.Rows.Count.ToString(), "Server 2", "lỗi", time, type, url };
                //dataGridViewNotification.Rows.Add(row1);
                dataGridViewNotification_NCC.Rows.Insert(0, row1);
                dataGridViewNotification_NCC.Rows[0].DefaultCellStyle.ForeColor = Color.Red;

            }
            else if (e.ProgressPercentage == 6)
            {
                autoStatusTBox_NCC.Text = "Đang đợi login";
            }
        }

        void myBackgroundWorker_RunWorkerCompleted_NCC(object sender, RunWorkerCompletedEventArgs e)
        {
            scrapeStartBtn_NCC.Enabled = true;
            scrapeStopBtn_NCC.Enabled = false;
            //MessageBox.Show(this, "DỪNG THÀNH CÔNG CHẠY THỦ CÔNG");
            CrawlNCC.runScrape = false;
        }

        void myBackgroundAutoWorker_RunWorkerCompleted_NCC(object sender, RunWorkerCompletedEventArgs e)
        {
            autoStartBtn_NCC.Enabled = true;
            autoStopBtn_NCC.Enabled = false;
            //MessageBox.Show(this, "DỪNG THÀNH CÔNG CHẠY TỰ ĐỘNG");
            CrawlNCC.runAuto = false;
        }

        private void scrapeStartBtn_NCC_Click(object sender, EventArgs e)
        {
            scrapeStartBtn_NCC.Enabled = false;
            scrapeStopBtn_NCC.Enabled = true;

            CrawlNCC.linkAPI1 = linkAPITBox1_NCC.Text;
            CrawlNCC.linkAPI2 = linkAPITBox2_NCC.Text;
            //scrape
            CrawlNCC.fromPage = (int)scrapeFromPage_NCC.Value;
            CrawlNCC.toPage = (int)scrapeToPage_NCC.Value;
            CrawlNCC.timeBetweenNewFrom = (int)timeBetweenNewFrom_NCC.Value;
            CrawlNCC.timeBetweenNewTo = (int)timeBetweenNewTo_NCC.Value;
            CrawlNCC.tinh.Clear();
            if (TKHNCBox_NCC.Checked == true)
            {
                CrawlNCC.tinh.Add("Hà+Nội");
            }
            if (TKHCMCBox_NCC.Checked == true)
            {
                CrawlNCC.tinh.Add("TPHCM");
            }

            CrawlNCC.runScrape = true;

            StatusTBox_NCC.Text = "bắt đầu chạy";

            m_oWorker_NCC.RunWorkerAsync();
        }

        private void scrapeStopBtn_NCC_Click(object sender, EventArgs e)
        {
            scrapeStopBtn_NCC.Enabled = false;
            CrawlNCC.runScrape = false;
            //ProcessHelper.KillAllProcessTree("chromedriver");
            m_oWorker_NCC.CancelAsync();
            m_oWorker_NCC.Dispose();
            StatusTBox_NCC.Text = "đang dừng chạy";
        }

        private void autoStartBtn_NCC_Click(object sender, EventArgs e)
        {
            autoStartBtn_NCC.Enabled = false;
            autoStopBtn_NCC.Enabled = true;

            CrawlNCC.linkAPI1 = linkAPITBox1_NCC.Text;
            CrawlNCC.linkAPI2 = linkAPITBox2_NCC.Text;
            //scrape
            CrawlNCC.autoFromPage = (int)autoFromPage_NCC.Value;
            CrawlNCC.autoToPage = (int)autoToPage_NCC.Value;
            CrawlNCC.timeBetweenNewFrom = (int)timeBetweenNewFrom_NCC.Value;
            CrawlNCC.timeBetweenNewTo = (int)timeBetweenNewTo_NCC.Value;
            CrawlNCC.autoTinh.Clear();
            if (TKHNCBox_NCC.Checked == true)
            {
                CrawlNCC.autoTinh.Add("Hà+Nội");
            }
            if (TKHCMCBox_NCC.Checked == true)
            {
                CrawlNCC.autoTinh.Add("TPHCM");
            }

            CrawlNCC.beginTime = autoStartTime_NCC.Value;
            CrawlNCC.endTime = autoStopTime_NCC.Value;

            CrawlNCC.dayinWeek.Clear();
            if (monday_NCC.Checked)
                CrawlNCC.dayinWeek.Add((int)DayOfWeek.Monday);
            if (tuesday_NCC.Checked)
                CrawlNCC.dayinWeek.Add((int)DayOfWeek.Tuesday);
            if (wenesday_NCC.Checked)
                CrawlNCC.dayinWeek.Add((int)DayOfWeek.Wednesday);
            if (thusday_NCC.Checked)
                CrawlNCC.dayinWeek.Add((int)DayOfWeek.Thursday);
            if (friday_NCC.Checked)
                CrawlNCC.dayinWeek.Add((int)DayOfWeek.Friday);
            if (satuday_NCC.Checked)
                CrawlNCC.dayinWeek.Add((int)DayOfWeek.Saturday);
            if (sunday_NCC.Checked)
                CrawlNCC.dayinWeek.Add((int)DayOfWeek.Sunday);

            CrawlNCC.loopTime = (int)autoTimeGetNew_NCC.Value;

            autoStatusTBox_NCC.Text = "bắt đầu chạy";

            m_autoWorker_NCC.RunWorkerAsync();
        }

        private void autoStopBtn_NCC_Click(object sender, EventArgs e)
        {
            autoStopBtn_NCC.Enabled = false;
            CrawlNCC.runAuto = false;
            //ProcessHelper.KillAllProcessTree("chromedriver");
            m_autoWorker_NCC.CancelAsync();
            m_autoWorker_NCC.Dispose();
            //while (m_autoWorker.IsBusy)
            //{
            //    autoStatusTBox.Text = "đang dừng !!!!";
            //    Thread.Sleep(2 * 1000);
            //}
            autoStatusTBox_NCC.Text = "đang dừng chạy";
        }

        private void DeleteNotificationBtn_NCC_Click(object sender, EventArgs e)
        {
            dataGridViewNotification_NCC.Rows.Clear();
        }

        private void SaveSettingBtn_NCC_Click(object sender, EventArgs e)
        {
            //tudong
            var svAutoFromPage = autoFromPage_NCC.Value.ToString();
            var svAutoToPage = autoToPage_NCC.Value.ToString();
            var svautoStartTime = autoStartTime_NCC.Value.ToString();
            var svautoStopTime = autoStopTime_NCC.Value.ToString();
            var svT2 = monday_NCC.Checked ? "1" : "0";
            var svT3 = tuesday_NCC.Checked ? "1" : "0";
            var svT4 = wenesday_NCC.Checked ? "1" : "0";
            var svT5 = thusday_NCC.Checked ? "1" : "0";
            var svT6 = friday_NCC.Checked ? "1" : "0";
            var svT7 = satuday_NCC.Checked ? "1" : "0";
            var svCN = sunday_NCC.Checked ? "1" : "0";
            var svautoTimeGetNew = autoTimeGetNew_NCC.Value.ToString();
            var svcheckLoginTime = checkLoginTime_NCC.Value.ToString();
            //thu cong
            var svscrapeToPage = scrapeToPage_NCC.Value.ToString();
            var svscrapeFromPage = scrapeFromPage_NCC.Value.ToString();
            //Cai dat chung
            var svtimeBetweenNewFrom = timeBetweenNewFrom_NCC.Value.ToString();
            var svtimeBetweenNewTo = timeBetweenNewTo_NCC.Value.ToString();
            var svlinkAPITBox1 = linkAPITBox1_NCC.Text;
            var svlinkAPITBox2 = linkAPITBox2_NCC.Text;

            var TKHN = TKHNCBox_NCC.Checked ? "1" : "0";
            var TKHCM = TKHCMCBox_NCC.Checked ? "1" : "0";


            var data = svAutoFromPage + "|" + svAutoToPage + "|" + svautoStartTime + "|" + svautoStopTime + "|" + svT2 + "|" + svT3 + "|"
                + svT4 + "|" + svT5 + "|" + svT6 + "|" + svT7 + "|" + svCN + "|" + svautoTimeGetNew + "|"
                + svcheckLoginTime + "|" + svscrapeToPage + "|" + svscrapeFromPage + "|" + svtimeBetweenNewFrom
                + "|" + svtimeBetweenNewTo + "|" + svlinkAPITBox1 + "|" + svlinkAPITBox2
                + "|" + TKHN + "|" + TKHCM;

            var pathStringFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NCC");
            if (!Directory.Exists(pathStringFolder))
            {
                Directory.CreateDirectory(pathStringFolder);
            }

            var pathSetting = System.IO.Path.Combine(pathStringFolder, "Setting.txt");
            File.WriteAllText(pathSetting, data);
        }

        //NNCC.com//NNCC.com//NNCC.com//NNCC.com//NNCC.com//NNCC.com//NNCC.com//NNCC.com//NNCC.com//NNCC.com//NNCC.com
        void defaultSetting_NNCC()
        {
            dataGridViewNotification_NNCC.RowHeadersVisible = false;
            var pathStringFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NNCC");

            if (!Directory.Exists(pathStringFolder))
            {
                Directory.CreateDirectory(pathStringFolder);
            }
            var fileSetting = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NNCC", "Setting.txt");
            var fileStatus = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NNCC", "status.txt");
            if (File.Exists(fileStatus))
            {
                var data = File.ReadAllText(fileStatus);
                StatusTBox_NNCC.Text = data;
            }

            var fileStatusAuto = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NNCC", "status_auto.txt");
            if (File.Exists(fileStatusAuto))
            {
                var data = File.ReadAllText(fileStatusAuto);
                autoStatusTBox_NNCC.Text = data;
            }

            //var fileSetting = AppDomain.CurrentDomain.BaseDirectory + "Setting.txt";
            if (File.Exists(fileSetting))
            {
                var data = File.ReadAllText(fileSetting);
                var eleData = data.Split('|');
                //tudong
                autoFromPage_NNCC.Value = Int32.Parse(eleData[0]);
                autoToPage_NNCC.Value = Int32.Parse(eleData[1]);
                autoStartTime_NNCC.Value = DateTime.Parse(eleData[2]);
                autoStopTime_NNCC.Value = DateTime.Parse(eleData[3]);
                if (eleData[4] == "1")
                    monday_NNCC.Checked = true;
                else
                    monday_NNCC.Checked = false;
                if (eleData[5] == "1")
                    tuesday_NNCC.Checked = true;
                else
                    tuesday_NNCC.Checked = false;
                if (eleData[6] == "1")
                    wenesday_NNCC.Checked = true;
                else
                    wenesday_NNCC.Checked = false;
                if (eleData[7] == "1")
                    thusday_NNCC.Checked = true;
                else
                    thusday_NNCC.Checked = false;
                if (eleData[8] == "1")
                    friday_NNCC.Checked = true;
                else
                    friday_NNCC.Checked = false;
                if (eleData[9] == "1")
                    satuday_NNCC.Checked = true;
                else
                    satuday_NNCC.Checked = false;
                if (eleData[10] == "1")
                    sunday_NNCC.Checked = true;
                else
                    sunday_NNCC.Checked = false;

                autoTimeGetNew_NNCC.Value = Int32.Parse(eleData[11]);
                checkLoginTime_NNCC.Value = Int32.Parse(eleData[12]);

                //thu cong
                scrapeToPage_NNCC.Value = Int32.Parse(eleData[13]);
                scrapeFromPage_NNCC.Value = Int32.Parse(eleData[14]);

                //Cai dat chung
                timeBetweenNewFrom_NNCC.Value = Int32.Parse(eleData[15]);
                timeBetweenNewTo_NNCC.Value = Int32.Parse(eleData[16]);
                linkAPITBox1_NNCC.Text = eleData[17];
                linkAPITBox2_NNCC.Text = eleData[18];

                userHNTBox_NNCC.Text = eleData[19];
                passHNTBox_NNCC.Text = eleData[20];

                userHCMTBox_NNCC.Text = eleData[21];
                passHCMTBox_NNCC.Text = eleData[22];

                if (eleData[23] == "1")
                    TKHNCBox_NNCC.Checked = true;
                else
                    TKHNCBox_NNCC.Checked = false;
                if (eleData[24] == "1")
                    TKHCMCBox_NNCC.Checked = true;
                else
                    TKHCMCBox_NNCC.Checked = false;

                CrawlNNCC.linkAPI1 = linkAPITBox1_NNCC.Text;
                CrawlNNCC.linkAPI2 = linkAPITBox2_NNCC.Text;
                //scrape
                CrawlNNCC.fromPage = (int)scrapeFromPage_NNCC.Value;
                CrawlNNCC.toPage = (int)scrapeToPage_NNCC.Value;
                CrawlNNCC.timeBetweenNewFrom = (int)timeBetweenNewFrom_NNCC.Value;
                CrawlNNCC.timeBetweenNewTo = (int)timeBetweenNewTo_NNCC.Value;
                //auto
            }

        }

        void myBackgroundWorker_DoWork_NNCC(object sender, DoWorkEventArgs e)
        {
            CrawlNNCC.runScrape = true;
            CrawlNNCC.ScrapeRunCrawl(m_oWorker_NNCC);
        }

        void myBackgroundAutoWorker_DoWork_NNCC(object sender, DoWorkEventArgs e)
        {
            CrawlNNCC.runAuto = true;
            CrawlNNCC.AutoRunCrawl(m_autoWorker_NNCC);
        }



        void myBackgroundWorker_ProgressChanged_NNCC(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 0)
            {
                StatusTBox_NNCC.Text = "đang chạy";
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin thủ công";
                var url = e.UserState.ToString();

                string[] row1 = new string[] { dataGridViewNotification_NNCC.Rows.Count.ToString(), "Server 1", "xong", time, type, url };
                StatusTBox_NNCC.Text = "đang chạy, đã đẩy thành công: " + dataGridViewNotification_NNCC.Rows.Count.ToString();
                dataGridViewNotification_NNCC.Rows.Insert(0, row1);
                dataGridViewNotification_NNCC.Rows[0].DefaultCellStyle.ForeColor = Color.Black;

            }
            else if (e.ProgressPercentage == 1)
            {
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin thủ công";
                var url = e.UserState.ToString();
                string[] row1 = new string[] { dataGridViewNotification_NNCC.Rows.Count.ToString(), "Server 1", "lỗi", time, type, url };
                dataGridViewNotification_NNCC.Rows.Insert(0, row1);
                dataGridViewNotification_NNCC.Rows[0].DefaultCellStyle.ForeColor = Color.Red;
                //dataGridViewNotification.Rows.Add(row1);
            }
            else if (e.ProgressPercentage == 2)
            {
                var dt = DateTime.Now;
                StatusTBox_NNCC.Text = "chạy xong thủ công lúc: " + dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                scrapeStartBtn_NNCC.Enabled = true;
                scrapeStopBtn_NNCC.Enabled = false;
                var pathStringFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NNCC");
                if (!Directory.Exists(pathStringFolder))
                {
                    Directory.CreateDirectory(pathStringFolder);
                }

                var pathStatus = System.IO.Path.Combine(pathStringFolder, "status.txt");
                File.WriteAllText(pathStatus, StatusTBox_NNCC.Text);
            }
            else if (e.ProgressPercentage == 3)
            {
                StatusTBox_NNCC.Text = "đang chạy";
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin thủ công";
                var url = e.UserState.ToString();

                string[] row1 = new string[] { dataGridViewNotification_NNCC.Rows.Count.ToString(), "Server 2", "xong", time, type, url };
                StatusTBox_NNCC.Text = "đang chạy, đã đẩy thành công: " + dataGridViewNotification_NNCC.Rows.Count.ToString();
                //dataGridViewNotification.Rows.Add(row1);
                dataGridViewNotification_NNCC.Rows.Insert(0, row1);
                dataGridViewNotification_NNCC.Rows[0].DefaultCellStyle.ForeColor = Color.Black;

            }
            else if (e.ProgressPercentage == 4)
            {
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin thủ công";
                var url = e.UserState.ToString();
                string[] row1 = new string[] { dataGridViewNotification_NNCC.Rows.Count.ToString(), "Server 2", "lỗi", time, type, url };
                //dataGridViewNotification.Rows.Add(row1);
                dataGridViewNotification_NNCC.Rows.Insert(0, row1);
                dataGridViewNotification_NNCC.Rows[0].DefaultCellStyle.ForeColor = Color.Red;
            }
            else if (e.ProgressPercentage == 6)
            {
                StatusTBox_NNCC.Text = "Đang đợi login";
            }
        }

        void myBackgroundAutoWorker_ProgressChanged_NNCC(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 0)
            {
                autoStatusTBox_NNCC.Text = "đang chạy";
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin tự động";
                var url = e.UserState.ToString();
                if (url.Contains("đang chạy"))
                    return;
                string[] row1 = new string[] { dataGridViewNotification_NNCC.Rows.Count.ToString(), "Server 1", "xong", time, type, url };
                autoStatusTBox_NNCC.Text = "đang chạy, đã đẩy thành công: " + dataGridViewNotification_NNCC.Rows.Count.ToString();
                //dataGridViewNotification.Rows.Add(row1);
                dataGridViewNotification_NNCC.Rows.Insert(0, row1);
                dataGridViewNotification_NNCC.Rows[0].DefaultCellStyle.ForeColor = Color.Green;


            }
            else if (e.ProgressPercentage == 1)
            {
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin tự động";
                var url = e.UserState.ToString();
                string[] row1 = new string[] { dataGridViewNotification_NNCC.Rows.Count.ToString(), "Server 1", "lỗi", time, type, url };
                //dataGridViewNotification.Rows.Add(row1);
                dataGridViewNotification_NNCC.Rows.Insert(0, row1);
                dataGridViewNotification_NNCC.Rows[0].DefaultCellStyle.ForeColor = Color.Red;

            }
            else if (e.ProgressPercentage == 2)
            {
                var dt = DateTime.Now;
                autoStatusTBox_NNCC.Text = "chạy xong tự động lúc: " + dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                autoStartBtn_NNCC.Enabled = true;
                autoStopBtn_NNCC.Enabled = false;

                var pathStringFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NNCC");
                if (!Directory.Exists(pathStringFolder))
                {
                    Directory.CreateDirectory(pathStringFolder);
                }

                var pathStatus = System.IO.Path.Combine(pathStringFolder, "status_auto.txt");
                File.WriteAllText(pathStatus, autoStatusTBox_NNCC.Text);
            }
            else if (e.ProgressPercentage == 5)
            {
                autoStatusTBox_NNCC.Text = e.UserState.ToString();
                autoStartBtn_NNCC.Enabled = false;
                autoStopBtn_NNCC.Enabled = true;
            }
            else if (e.ProgressPercentage == 3)
            {
                autoStatusTBox_NNCC.Text = "đang chạy";
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin tự động";
                var url = e.UserState.ToString();
                string[] row1 = new string[] { dataGridViewNotification_NNCC.Rows.Count.ToString(), "Server 2", "xong", time, type, url };
                autoStatusTBox_NNCC.Text = "đang chạy, đã đẩy thành công: " + dataGridViewNotification_NNCC.Rows.Count.ToString();
                //dataGridViewNotification.Rows.Add(row1);
                dataGridViewNotification_NNCC.Rows.Insert(0, row1);
                dataGridViewNotification_NNCC.Rows[0].DefaultCellStyle.ForeColor = Color.Blue;


            }
            else if (e.ProgressPercentage == 4)
            {
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin tự động";
                var url = e.UserState.ToString();
                string[] row1 = new string[] { dataGridViewNotification_NNCC.Rows.Count.ToString(), "Server 2", "lỗi", time, type, url };
                //dataGridViewNotification.Rows.Add(row1);
                dataGridViewNotification_NNCC.Rows.Insert(0, row1);
                dataGridViewNotification_NNCC.Rows[0].DefaultCellStyle.ForeColor = Color.Red;

            }
            else if (e.ProgressPercentage == 6)
            {
                autoStatusTBox_NNCC.Text = "Đang đợi login";
            }
        }

        void myBackgroundWorker_RunWorkerCompleted_NNCC(object sender, RunWorkerCompletedEventArgs e)
        {
            scrapeStartBtn_NNCC.Enabled = true;
            scrapeStopBtn_NNCC.Enabled = false;
            //MessageBox.Show(this, "DỪNG THÀNH CÔNG CHẠY THỦ CÔNG");
            CrawlNNCC.runScrape = false;
        }

        void myBackgroundAutoWorker_RunWorkerCompleted_NNCC(object sender, RunWorkerCompletedEventArgs e)
        {
            autoStartBtn_NNCC.Enabled = true;
            autoStopBtn_NNCC.Enabled = false;
            //MessageBox.Show(this, "DỪNG THÀNH CÔNG CHẠY TỰ ĐỘNG");
            CrawlNNCC.runAuto = false;
        }

        private void scrapeStartBtn_NNCC_Click(object sender, EventArgs e)
        {
            scrapeStartBtn_NNCC.Enabled = false;
            scrapeStopBtn_NNCC.Enabled = true;

            CrawlNNCC.userHN = userHNTBox_NNCC.Text;
            CrawlNNCC.passHN = passHNTBox_NNCC.Text;

            CrawlNNCC.userHCM = userHCMTBox_NNCC.Text;
            CrawlNNCC.passHCM = passHCMTBox_NNCC.Text;

            CrawlNNCC.linkAPI1 = linkAPITBox1_NNCC.Text;
            CrawlNNCC.linkAPI2 = linkAPITBox2_NNCC.Text;
            //scrape
            CrawlNNCC.fromPage = (int)scrapeFromPage_NNCC.Value;
            CrawlNNCC.toPage = (int)scrapeToPage_NNCC.Value;
            CrawlNNCC.timeBetweenNewFrom = (int)timeBetweenNewFrom_NNCC.Value;
            CrawlNNCC.timeBetweenNewTo = (int)timeBetweenNewTo_NNCC.Value;
            CrawlNNCC.tinh.Clear();
            if (TKHNCBox_NNCC.Checked)
            {
                CrawlNNCC.tinh.Add("HN");
            }
            if (TKHCMCBox_NNCC.Checked)
            {
                CrawlNNCC.tinh.Add("HCM");
            }

            if (layXuoiRBtn_NNCC.Checked)
            {
                CrawlNNCC.layXuoiMode = true;
            }
            else
            {
                CrawlNNCC.layXuoiMode = false;
            }


            CrawlNNCC.runScrape = true;
            if (AutoLoginCBox_NNCC.Checked)
                CrawlNNCC.autoLogin = true;
            else
                CrawlNNCC.autoLogin = false;


            StatusTBox_NNCC.Text = "bắt đầu chạy";

            m_oWorker_NNCC.RunWorkerAsync();
        }

        private void scrapeStopBtn_NNCC_Click(object sender, EventArgs e)
        {
            scrapeStopBtn_NNCC.Enabled = false;
            CrawlNNCC.runScrape = false;
            //ProcessHelper.KillAllProcessTree("chromedriver");
            m_oWorker_NNCC.CancelAsync();
            m_oWorker_NNCC.Dispose();
            StatusTBox_NNCC.Text = "đang dừng chạy";
        }

        private void autoStartBtn_NNCC_Click(object sender, EventArgs e)
        {
            autoStartBtn_NNCC.Enabled = false;
            autoStopBtn_NNCC.Enabled = true;


            CrawlNNCC.userHN = userHNTBox_NNCC.Text;
            CrawlNNCC.passHN = passHNTBox_NNCC.Text;

            CrawlNNCC.userHCM = userHCMTBox_NNCC.Text;
            CrawlNNCC.passHCM = passHCMTBox_NNCC.Text;

            CrawlNNCC.linkAPI1 = linkAPITBox1_NNCC.Text;
            CrawlNNCC.linkAPI2 = linkAPITBox2_NNCC.Text;
            //scrape
            CrawlNNCC.autoFromPage = (int)autoFromPage_NNCC.Value;
            CrawlNNCC.autoToPage = (int)autoToPage_NNCC.Value;
            CrawlNNCC.timeBetweenNewFrom = (int)timeBetweenNewFrom_NNCC.Value;
            CrawlNNCC.timeBetweenNewTo = (int)timeBetweenNewTo_NNCC.Value;
            CrawlNNCC.autoTinh.Clear();
            if (TKHNCBox_NNCC.Checked)
            {
                CrawlNNCC.autoTinh.Add("HN");
            }
            if (TKHCMCBox_NNCC.Checked)
            {
                CrawlNNCC.autoTinh.Add("HCM");
            }
            if (layXuoiRBtn_NNCC.Checked)
            {
                CrawlNNCC.layXuoiMode = true;
            }
            else
            {
                CrawlNNCC.layXuoiMode = false;
            }



            CrawlNNCC.beginTime = autoStartTime_NNCC.Value;
            CrawlNNCC.endTime = autoStopTime_NNCC.Value;

            CrawlNNCC.dayinWeek.Clear();
            if (monday_NNCC.Checked)
                CrawlNNCC.dayinWeek.Add((int)DayOfWeek.Monday);
            if (tuesday_NNCC.Checked)
                CrawlNNCC.dayinWeek.Add((int)DayOfWeek.Tuesday);
            if (wenesday_NNCC.Checked)
                CrawlNNCC.dayinWeek.Add((int)DayOfWeek.Wednesday);
            if (thusday_NNCC.Checked)
                CrawlNNCC.dayinWeek.Add((int)DayOfWeek.Thursday);
            if (friday_NNCC.Checked)
                CrawlNNCC.dayinWeek.Add((int)DayOfWeek.Friday);
            if (satuday_NNCC.Checked)
                CrawlNNCC.dayinWeek.Add((int)DayOfWeek.Saturday);
            if (sunday_NNCC.Checked)
                CrawlNNCC.dayinWeek.Add((int)DayOfWeek.Sunday);

            CrawlNNCC.loopTime = (int)autoTimeGetNew_NNCC.Value;

            if (AutoLoginCBox_NNCC.Checked)
                CrawlNNCC.autoLogin = true;
            else
                CrawlNNCC.autoLogin = false;

            autoStatusTBox_NNCC.Text = "bắt đầu chạy";

            m_autoWorker_NNCC.RunWorkerAsync();
        }

        private void autoStopBtn_NNCC_Click(object sender, EventArgs e)
        {
            autoStopBtn_NNCC.Enabled = false;
            CrawlNNCC.runAuto = false;
            //ProcessHelper.KillAllProcessTree("chromedriver");
            m_autoWorker_NNCC.CancelAsync();
            m_autoWorker_NNCC.Dispose();
            //while (m_autoWorker.IsBusy)
            //{
            //    autoStatusTBox.Text = "đang dừng !!!!";
            //    Thread.Sleep(2 * 1000);
            //}
            autoStatusTBox_NNCC.Text = "đang dừng chạy";
        }

        private void DeleteNotificationBtn_NNCC_Click(object sender, EventArgs e)
        {
            dataGridViewNotification_NNCC.Rows.Clear();
        }

        private void SaveSettingBtn_NNCC_Click(object sender, EventArgs e)
        {
            //tudong
            var svAutoFromPage = autoFromPage_NNCC.Value.ToString();
            var svAutoToPage = autoToPage_NNCC.Value.ToString();
            var svautoStartTime = autoStartTime_NNCC.Value.ToString();
            var svautoStopTime = autoStopTime_NNCC.Value.ToString();
            var svT2 = monday_NNCC.Checked ? "1" : "0";
            var svT3 = tuesday_NNCC.Checked ? "1" : "0";
            var svT4 = wenesday_NNCC.Checked ? "1" : "0";
            var svT5 = thusday_NNCC.Checked ? "1" : "0";
            var svT6 = friday_NNCC.Checked ? "1" : "0";
            var svT7 = satuday_NNCC.Checked ? "1" : "0";
            var svCN = sunday_NNCC.Checked ? "1" : "0";
            var svautoTimeGetNew = autoTimeGetNew_NNCC.Value.ToString();
            var svcheckLoginTime = checkLoginTime_NNCC.Value.ToString();
            //var svautoHNCBox = autoHNCBox_NNCC.Checked ? "1" : "0";
            //var svautoHCMCBox = autoHCMCBox_NNCC.Checked ? "1" : "0";
            //thu cong
            var svscrapeToPage = scrapeToPage_NNCC.Value.ToString();
            var svscrapeFromPage = scrapeFromPage_NNCC.Value.ToString();
            //var svHNCBox = HNCBox_NNCC.Checked ? "1" : "0";
            //var svHCMCBox = HCMCBox_NNCC.Checked ? "1" : "0";
            //Cai dat chung
            var svtimeBetweenNewFrom = timeBetweenNewFrom_NNCC.Value.ToString();
            var svtimeBetweenNewTo = timeBetweenNewTo_NNCC.Value.ToString();
            var svlinkAPITBox1 = linkAPITBox1_NNCC.Text;
            var svlinkAPITBox2 = linkAPITBox2_NNCC.Text;

            var userHN = userHNTBox_NNCC.Text;
            var passHN = passHNTBox_NNCC.Text;

            var userHCM = userHCMTBox_NNCC.Text;
            var passHCM = passHCMTBox_NNCC.Text;

            var TKHN = TKHNCBox_NNCC.Checked ? "1" : "0";
            var TKHCM = TKHCMCBox_NNCC.Checked ? "1" : "0";

            var data = svAutoFromPage + "|" + svAutoToPage + "|" + svautoStartTime + "|" + svautoStopTime + "|" + svT2 + "|" + svT3 + "|"
                + svT4 + "|" + svT5 + "|" + svT6 + "|" + svT7 + "|" + svCN + "|" + svautoTimeGetNew + "|"
                + svcheckLoginTime + "|" + svscrapeToPage + "|" + svscrapeFromPage + "|" + svtimeBetweenNewFrom + "|" + svtimeBetweenNewTo + "|" + svlinkAPITBox1 + "|" + svlinkAPITBox2
                + "|" + userHN + "|" + passHN + "|" + userHCM + "|" + passHCM
                + "|" + TKHN + "|" + TKHCM;

            var pathStringFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NNCC");
            if (!Directory.Exists(pathStringFolder))
            {
                Directory.CreateDirectory(pathStringFolder);
            }

            var pathSetting = System.IO.Path.Combine(pathStringFolder, "Setting.txt");
            File.WriteAllText(pathSetting, data);
        }
        //TCC.VN//TCC.VN//TCC.VN//TCC.VN//TCC.VN//TCC.VN//TCC.VN//TCC.VN//TCC.VN//TCC.VN//TCC.VN//TCC.VN//TCC.VN//TCC.VN//TCC.VN//TCC.VN//TCC.VN//TCC.VN
        void defaultSetting_TCC()
        {
            dataGridViewNotification_TCC.RowHeadersVisible = false;
            var pathStringFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TCC");

            if (!Directory.Exists(pathStringFolder))
            {
                Directory.CreateDirectory(pathStringFolder);
            }
            var fileSetting = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TCC", "Setting.txt");
            var fileStatus = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TCC", "status.txt");
            var keyCaptchaPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "keyCaptcha.txt");
            if (File.Exists(fileStatus))
            {
                var data = File.ReadAllText(fileStatus);
                StatusTBox_TCC.Text = data;
            }

            if (File.Exists(keyCaptchaPath))
            {
                CaptchaHelper.apiKeyCaptcha = File.ReadAllText(keyCaptchaPath).Trim();
            }

            var fileStatusAuto = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TCC", "status_auto.txt");
            if (File.Exists(fileStatusAuto))
            {
                var data = File.ReadAllText(fileStatusAuto);
                autoStatusTBox_TCC.Text = data;
            }

            //var fileSetting = AppDomain.CurrentDomain.BaseDirectory + "Setting.txt";
            if (File.Exists(fileSetting))
            {
                var data = File.ReadAllText(fileSetting);
                var eleData = data.Split('|');
                //tudong
                autoFromPage_TCC.Value = Int32.Parse(eleData[0]);
                autoToPage_TCC.Value = Int32.Parse(eleData[1]);
                autoStartTime_TCC.Value = DateTime.Parse(eleData[2]);
                autoStartTime_TCC.Value = DateTime.Parse(eleData[3]);
                if (eleData[4] == "1")
                    monday_TCC.Checked = true;
                else
                    monday_TCC.Checked = false;
                if (eleData[5] == "1")
                    tuesday_TCC.Checked = true;
                else
                    tuesday_TCC.Checked = false;
                if (eleData[6] == "1")
                    wenesday_TCC.Checked = true;
                else
                    wenesday_TCC.Checked = false;
                if (eleData[7] == "1")
                    thusday_TCC.Checked = true;
                else
                    thusday_TCC.Checked = false;
                if (eleData[8] == "1")
                    friday_TCC.Checked = true;
                else
                    friday_TCC.Checked = false;
                if (eleData[9] == "1")
                    satuday_TCC.Checked = true;
                else
                    satuday_TCC.Checked = false;
                if (eleData[10] == "1")
                    sunday_TCC.Checked = true;
                else
                    sunday_TCC.Checked = false;

                autoTimeGetNew_TCC.Value = Int32.Parse(eleData[11]);
                checkLoginTime_TCC.Value = Int32.Parse(eleData[12]);

                //thu cong
                scrapeToPage_TCC.Value = Int32.Parse(eleData[13]);
                scrapeFromPage_TCC.Value = Int32.Parse(eleData[14]);

                //Cai dat chung
                timeBetweenNewFrom_TCC.Value = Int32.Parse(eleData[15]);
                timeBetweenNewTo_TCC.Value = Int32.Parse(eleData[16]);
                linkAPITBox1_TCC.Text = eleData[17];
                linkAPITBox2_TCC.Text = eleData[18];

                userHNTBox_TCC.Text = eleData[19];
                passHNTBox_TCC.Text = eleData[20];

                userHCMTBox_TCC.Text = eleData[21];
                passHCMTBox_TCC.Text = eleData[22];

                if (eleData[23] == "1")
                    TKHNCBox_TCC.Checked = true;
                else
                    TKHNCBox_TCC.Checked = false;
                if (eleData[24] == "1")
                    TKHCMCBox_TCC.Checked = true;
                else
                    TKHCMCBox_TCC.Checked = false;

                if (eleData[25] == "1")
                    layXuoiRBtn_TCC.Checked = true;
                else
                    layNguocRBtn_TCC.Checked = true;

                CrawlTCC.linkAPI1 = linkAPITBox1_TCC.Text;
                CrawlTCC.linkAPI2 = linkAPITBox2_TCC.Text;
                //scrape
                CrawlTCC.fromPage = (int)scrapeFromPage_TCC.Value;
                CrawlTCC.toPage = (int)scrapeToPage_TCC.Value;
                CrawlTCC.timeBetweenNewFrom = (int)timeBetweenNewFrom_TCC.Value;
                CrawlTCC.timeBetweenNewTo = (int)timeBetweenNewTo_TCC.Value;
                //auto
            }

        }

        void myBackgroundWorker_DoWork_TCC(object sender, DoWorkEventArgs e)
        {
            CrawlTCC.runScrape = true;
            CrawlTCC.ScrapeRunCrawl(m_oWorker_TCC);
        }

        void myBackgroundAutoWorker_DoWork_TCC(object sender, DoWorkEventArgs e)
        {
            CrawlTCC.runAuto = true;
            CrawlTCC.AutoRunCrawl(m_autoWorker_TCC);
        }



        void myBackgroundWorker_ProgressChanged_TCC(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 0)
            {
                StatusTBox_TCC.Text = "đang chạy";
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin thủ công";
                var url = e.UserState.ToString();

                string[] row1 = new string[] { dataGridViewNotification_TCC.Rows.Count.ToString(), "Server 1", "xong", time, type, url };
                StatusTBox_TCC.Text = "đang chạy, đã đẩy thành công: " + dataGridViewNotification_TCC.Rows.Count.ToString();
                dataGridViewNotification_TCC.Rows.Insert(0, row1);
                dataGridViewNotification_TCC.Rows[0].DefaultCellStyle.ForeColor = Color.Black;

            }
            else if (e.ProgressPercentage == 1)
            {
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin thủ công";
                var url = e.UserState.ToString();
                string[] row1 = new string[] { dataGridViewNotification_TCC.Rows.Count.ToString(), "Server 1", "lỗi", time, type, url };
                dataGridViewNotification_TCC.Rows.Insert(0, row1);
                dataGridViewNotification_TCC.Rows[0].DefaultCellStyle.ForeColor = Color.Red;
                //dataGridViewNotification.Rows.Add(row1);
            }
            else if (e.ProgressPercentage == 2)
            {
                var dt = DateTime.Now;
                StatusTBox_TCC.Text = "chạy xong thủ công lúc: " + dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                scrapeStartBtn_TCC.Enabled = true;
                scrapeStopBtn_TCC.Enabled = false;
                var pathStringFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TCC");
                if (!Directory.Exists(pathStringFolder))
                {
                    Directory.CreateDirectory(pathStringFolder);
                }

                var pathStatus = System.IO.Path.Combine(pathStringFolder, "status.txt");
                File.WriteAllText(pathStatus, StatusTBox_TCC.Text);
            }
            else if (e.ProgressPercentage == 3)
            {
                StatusTBox_TCC.Text = "đang chạy";
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin thủ công";
                var url = e.UserState.ToString();

                string[] row1 = new string[] { dataGridViewNotification_TCC.Rows.Count.ToString(), "Server 2", "xong", time, type, url };
                StatusTBox_TCC.Text = "đang chạy, đã đẩy thành công: " + dataGridViewNotification_TCC.Rows.Count.ToString();
                //dataGridViewNotification.Rows.Add(row1);
                dataGridViewNotification_TCC.Rows.Insert(0, row1);
                dataGridViewNotification_TCC.Rows[0].DefaultCellStyle.ForeColor = Color.Black;

            }
            else if (e.ProgressPercentage == 4)
            {
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin thủ công";
                var url = e.UserState.ToString();
                string[] row1 = new string[] { dataGridViewNotification_TCC.Rows.Count.ToString(), "Server 2", "lỗi", time, type, url };
                //dataGridViewNotification.Rows.Add(row1);
                dataGridViewNotification_TCC.Rows.Insert(0, row1);
                dataGridViewNotification_TCC.Rows[0].DefaultCellStyle.ForeColor = Color.Red;
            }
            else if (e.ProgressPercentage == 6)
            {
                StatusTBox_TCC.Text = "Đang đợi login";
            }
        }

        void myBackgroundAutoWorker_ProgressChanged_TCC(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 0)
            {
                autoStatusTBox_TCC.Text = "đang chạy";
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin tự động";
                var url = e.UserState.ToString();
                if (url.Contains("đang chạy"))
                    return;
                string[] row1 = new string[] { dataGridViewNotification_TCC.Rows.Count.ToString(), "Server 1", "xong", time, type, url };
                autoStatusTBox_TCC.Text = "đang chạy, đã đẩy thành công: " + dataGridViewNotification_TCC.Rows.Count.ToString();
                //dataGridViewNotification.Rows.Add(row1);
                dataGridViewNotification_TCC.Rows.Insert(0, row1);
                dataGridViewNotification_TCC.Rows[0].DefaultCellStyle.ForeColor = Color.Green;


            }
            else if (e.ProgressPercentage == 1)
            {
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin tự động";
                var url = e.UserState.ToString();
                string[] row1 = new string[] { dataGridViewNotification_TCC.Rows.Count.ToString(), "Server 1", "lỗi", time, type, url };
                //dataGridViewNotification.Rows.Add(row1);
                dataGridViewNotification_TCC.Rows.Insert(0, row1);
                dataGridViewNotification_TCC.Rows[0].DefaultCellStyle.ForeColor = Color.Red;

            }
            else if (e.ProgressPercentage == 2)
            {
                var dt = DateTime.Now;
                autoStatusTBox_TCC.Text = "chạy xong tự động lúc: " + dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                autoStartBtn_TCC.Enabled = true;
                autoStopBtn_TCC.Enabled = false;

                var pathStringFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TCC");
                if (!Directory.Exists(pathStringFolder))
                {
                    Directory.CreateDirectory(pathStringFolder);
                }

                var pathStatus = System.IO.Path.Combine(pathStringFolder, "status_auto.txt");
                File.WriteAllText(pathStatus, autoStatusTBox_TCC.Text);
            }
            else if (e.ProgressPercentage == 5)
            {
                autoStatusTBox_TCC.Text = e.UserState.ToString();
                autoStartBtn_TCC.Enabled = false;
                autoStopBtn_TCC.Enabled = true;
            }
            else if (e.ProgressPercentage == 3)
            {
                autoStatusTBox_TCC.Text = "đang chạy";
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin tự động";
                var url = e.UserState.ToString();
                string[] row1 = new string[] { dataGridViewNotification_TCC.Rows.Count.ToString(), "Server 2", "xong", time, type, url };
                autoStatusTBox_TCC.Text = "đang chạy, đã đẩy thành công: " + dataGridViewNotification_TCC.Rows.Count.ToString();
                //dataGridViewNotification.Rows.Add(row1);
                dataGridViewNotification_TCC.Rows.Insert(0, row1);
                dataGridViewNotification_TCC.Rows[0].DefaultCellStyle.ForeColor = Color.Blue;


            }
            else if (e.ProgressPercentage == 4)
            {
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin tự động";
                var url = e.UserState.ToString();
                string[] row1 = new string[] { dataGridViewNotification_TCC.Rows.Count.ToString(), "Server 2", "lỗi", time, type, url };
                //dataGridViewNotification.Rows.Add(row1);
                dataGridViewNotification_TCC.Rows.Insert(0, row1);
                dataGridViewNotification_TCC.Rows[0].DefaultCellStyle.ForeColor = Color.Red;

            }
            else if (e.ProgressPercentage == 6)
            {
                autoStatusTBox_TCC.Text = "Đang đợi login";
            }
        }

        void myBackgroundWorker_RunWorkerCompleted_TCC(object sender, RunWorkerCompletedEventArgs e)
        {
            scrapeStartBtn_TCC.Enabled = true;
            scrapeStopBtn_TCC.Enabled = false;
            //MessageBox.Show(this, "DỪNG THÀNH CÔNG CHẠY THỦ CÔNG");
            CrawlTCC.runScrape = false;
        }

        void myBackgroundAutoWorker_RunWorkerCompleted_TCC(object sender, RunWorkerCompletedEventArgs e)
        {
            autoStartBtn_TCC.Enabled = true;
            autoStopBtn_TCC.Enabled = false;
            //MessageBox.Show(this, "DỪNG THÀNH CÔNG CHẠY TỰ ĐỘNG");
            CrawlTCC.runAuto = false;
        }

        private void scrapeStartBtn_TCC_Click(object sender, EventArgs e)
        {
            scrapeStartBtn_TCC.Enabled = false;
            scrapeStopBtn_TCC.Enabled = true;
            if (layXuoiRBtn_TCC.Checked)
            {
                CrawlTCC.layXuoiMode = true;
            }
            else
            {
                CrawlTCC.layXuoiMode = false;
            }

            CrawlTCC.userHN = userHNTBox_TCC.Text;
            CrawlTCC.passHN = passHNTBox_TCC.Text;

            CrawlTCC.userHCM = userHCMTBox_TCC.Text;
            CrawlTCC.passHCM = passHCMTBox_TCC.Text;

            CrawlTCC.linkAPI1 = linkAPITBox1_TCC.Text;
            CrawlTCC.linkAPI2 = linkAPITBox2_TCC.Text;
            //scrape
            CrawlTCC.fromPage = (int)scrapeFromPage_TCC.Value;
            CrawlTCC.toPage = (int)scrapeToPage_TCC.Value;
            CrawlTCC.timeBetweenNewFrom = (int)timeBetweenNewFrom_TCC.Value;
            CrawlTCC.timeBetweenNewTo = (int)timeBetweenNewTo_TCC.Value;
            CrawlTCC.tinh.Clear();
            if (TKHNCBox_TCC.Checked)
            {
                CrawlTCC.tinh.Add("HN");
            }
            if (TKHCMCBox_TCC.Checked)
            {
                CrawlTCC.tinh.Add("HCM");
            }

            CrawlTCC.runScrape = true;

            if (AutoLoginCBox_TCC.Checked)
            {
                CrawlTCC.autoLogin = true;
            }
            else
            {
                CrawlTCC.autoLogin = false;
            }

            StatusTBox_TCC.Text = "bắt đầu chạy";

            m_oWorker_TCC.RunWorkerAsync();
        }

        private void scrapeStopBtn_TCC_Click(object sender, EventArgs e)
        {
            scrapeStopBtn_TCC.Enabled = false;
            CrawlTCC.runScrape = false;
            //ProcessHelper.KillAllProcessTree("chromedriver");
            m_oWorker_TCC.CancelAsync();
            m_oWorker_TCC.Dispose();
            StatusTBox_TCC.Text = "đang dừng chạy";
        }

        private void autoStartBtn_TCC_Click(object sender, EventArgs e)
        {
            autoStartBtn_TCC.Enabled = false;
            autoStopBtn_TCC.Enabled = true;

            if (layXuoiRBtn_TCC.Checked)
            {
                CrawlTCC.layXuoiMode = true;
            }
            else
            {
                CrawlTCC.layXuoiMode = false;
            }

            CrawlTCC.userHN = userHNTBox_TCC.Text;
            CrawlTCC.passHN = passHNTBox_TCC.Text;

            CrawlTCC.userHCM = userHCMTBox_TCC.Text;
            CrawlTCC.passHCM = passHCMTBox_TCC.Text;

            CrawlTCC.linkAPI1 = linkAPITBox1_TCC.Text;
            CrawlTCC.linkAPI2 = linkAPITBox2_TCC.Text;
            //scrape
            CrawlTCC.autoFromPage = (int)autoFromPage_TCC.Value;
            CrawlTCC.autoToPage = (int)autoToPage_TCC.Value;
            CrawlTCC.timeBetweenNewFrom = (int)timeBetweenNewFrom_TCC.Value;
            CrawlTCC.timeBetweenNewTo = (int)timeBetweenNewTo_TCC.Value;
            CrawlTCC.autoTinh.Clear();
            if (TKHNCBox_TCC.Checked)
            {
                CrawlTCC.autoTinh.Add("HN");
            }
            if (TKHCMCBox_TCC.Checked)
            {
                CrawlTCC.autoTinh.Add("HCM");
            }

            CrawlTCC.beginTime = autoStartTime_TCC.Value;
            CrawlTCC.endTime = autoStopTime_TCC.Value;

            CrawlTCC.dayinWeek.Clear();
            if (monday_TCC.Checked)
                CrawlTCC.dayinWeek.Add((int)DayOfWeek.Monday);
            if (tuesday_TCC.Checked)
                CrawlTCC.dayinWeek.Add((int)DayOfWeek.Tuesday);
            if (wenesday_TCC.Checked)
                CrawlTCC.dayinWeek.Add((int)DayOfWeek.Wednesday);
            if (thusday_TCC.Checked)
                CrawlTCC.dayinWeek.Add((int)DayOfWeek.Thursday);
            if (friday_TCC.Checked)
                CrawlTCC.dayinWeek.Add((int)DayOfWeek.Friday);
            if (satuday_TCC.Checked)
                CrawlTCC.dayinWeek.Add((int)DayOfWeek.Saturday);
            if (sunday_TCC.Checked)
                CrawlTCC.dayinWeek.Add((int)DayOfWeek.Sunday);

            CrawlTCC.loopTime = (int)autoTimeGetNew_TCC.Value;

            if (AutoLoginCBox_TCC.Checked)
            {
                CrawlTCC.autoLogin = true;
            }
            else
            {
                CrawlTCC.autoLogin = false;
            }

            autoStatusTBox_TCC.Text = "bắt đầu chạy";

            m_autoWorker_TCC.RunWorkerAsync();
        }

        private void autoStopBtn_TCC_Click(object sender, EventArgs e)
        {
            autoStopBtn_TCC.Enabled = false;
            CrawlTCC.runAuto = false;
            //ProcessHelper.KillAllProcessTree("chromedriver");
            m_autoWorker_TCC.CancelAsync();
            m_autoWorker_TCC.Dispose();
            //while (m_autoWorker.IsBusy)
            //{
            //    autoStatusTBox.Text = "đang dừng !!!!";
            //    Thread.Sleep(2 * 1000);
            //}
            autoStatusTBox_TCC.Text = "đang dừng chạy";
        }

        private void DeleteNotificationBtn_TCC_Click(object sender, EventArgs e)
        {
            dataGridViewNotification_TCC.Rows.Clear();
        }

        private void SaveSettingBtn_TCC_Click(object sender, EventArgs e)
        {
            //tudong
            var svAutoFromPage = autoFromPage_TCC.Value.ToString();
            var svAutoToPage = autoToPage_TCC.Value.ToString();
            var svautoStartTime = autoStartTime_TCC.Value.ToString();
            var svautoStopTime = autoStartTime_TCC.Value.ToString();
            var svT2 = monday_TCC.Checked ? "1" : "0";
            var svT3 = tuesday_TCC.Checked ? "1" : "0";
            var svT4 = wenesday_TCC.Checked ? "1" : "0";
            var svT5 = thusday_TCC.Checked ? "1" : "0";
            var svT6 = friday_TCC.Checked ? "1" : "0";
            var svT7 = satuday_TCC.Checked ? "1" : "0";
            var svCN = sunday_TCC.Checked ? "1" : "0";
            var svautoTimeGetNew = autoTimeGetNew_TCC.Value.ToString();
            var svcheckLoginTime = checkLoginTime_TCC.Value.ToString();
            //var svautoHNCBox = autoHNCBox_TCC.Checked ? "1" : "0";
            //var svautoHCMCBox = autoHCMCBox_TCC.Checked ? "1" : "0";
            //thu cong
            var svscrapeToPage = scrapeToPage_TCC.Value.ToString();
            var svscrapeFromPage = scrapeFromPage_TCC.Value.ToString();
            //var svHNCBox = HNCBox_TCC.Checked ? "1" : "0";
            //var svHCMCBox = HCMCBox_TCC.Checked ? "1" : "0";
            //Cai dat chung
            var svtimeBetweenNewFrom = timeBetweenNewFrom_TCC.Value.ToString();
            var svtimeBetweenNewTo = timeBetweenNewTo_TCC.Value.ToString();
            var svlinkAPITBox1 = linkAPITBox1_TCC.Text;
            var svlinkAPITBox2 = linkAPITBox2_TCC.Text;

            var userHN = userHNTBox_TCC.Text;
            var passHN = passHNTBox_TCC.Text;

            var userHCM = userHCMTBox_TCC.Text;
            var passHCM = passHCMTBox_TCC.Text;

            var TKHN = TKHNCBox_TCC.Checked ? "1" : "0";
            var TKHCM = TKHCMCBox_TCC.Checked ? "1" : "0";
            var layxuoi = layXuoiRBtn_TCC.Checked ? "1" : "0";

            var data = svAutoFromPage + "|" + svAutoToPage + "|" + svautoStartTime + "|" + svautoStopTime + "|" + svT2 + "|" + svT3 + "|"
                + svT4 + "|" + svT5 + "|" + svT6 + "|" + svT7 + "|" + svCN + "|" + svautoTimeGetNew + "|"
                + svcheckLoginTime + "|" + svscrapeToPage + "|" + svscrapeFromPage + "|" + svtimeBetweenNewFrom + "|" + svtimeBetweenNewTo + "|" + svlinkAPITBox1 + "|" + svlinkAPITBox2
                + "|" + userHN + "|" + passHN + "|" + userHCM + "|" + passHCM
                + "|" + TKHN + "|" + TKHCM + "|" + layxuoi;

            var pathStringFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TCC");
            if (!Directory.Exists(pathStringFolder))
            {
                Directory.CreateDirectory(pathStringFolder);
            }

            var pathSetting = System.IO.Path.Combine(pathStringFolder, "Setting.txt");
            File.WriteAllText(pathSetting, data);
        }
        //NNM.VN//NNM.VN//NNM.VN//NNM.VN//NNM.VN//NNM.VN//NNM.VN//NNM.VN//NNM.VN//NNM.VN//NNM.VN//NNM.VN//NNM.VN//NNM.VN//NNM.VN//NNM.VN//NNM.VN//NNM.VN
        void defaultSetting_NNM()
        {
            dataGridViewNotification_NNM.RowHeadersVisible = false;
            var pathStringFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NNM");

            if (!Directory.Exists(pathStringFolder))
            {
                Directory.CreateDirectory(pathStringFolder);
            }
            var fileSetting = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NNM", "Setting.txt");
            var fileStatus = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NNM", "status.txt");
            var keyCaptchaPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "keyCaptcha.txt");
            if (File.Exists(fileStatus))
            {
                var data = File.ReadAllText(fileStatus);
                StatusTBox_NNM.Text = data;
            }

            if (File.Exists(keyCaptchaPath))
            {
                CaptchaHelper.apiKeyCaptcha = File.ReadAllText(keyCaptchaPath).Trim();
            }

            var fileStatusAuto = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NNM", "status_auto.txt");
            if (File.Exists(fileStatusAuto))
            {
                var data = File.ReadAllText(fileStatusAuto);
                autoStatusTBox_NNM.Text = data;
            }

            //var fileSetting = AppDomain.CurrentDomain.BaseDirectory + "Setting.txt";
            if (File.Exists(fileSetting))
            {
                var data = File.ReadAllText(fileSetting);
                var eleData = data.Split('|');
                //tudong
                autoFromPage_NNM.Value = Int32.Parse(eleData[0]);
                autoToPage_NNM.Value = Int32.Parse(eleData[1]);
                autoStartTime_NNM.Value = DateTime.Parse(eleData[2]);
                autoStartTime_NNM.Value = DateTime.Parse(eleData[3]);
                if (eleData[4] == "1")
                    monday_NNM.Checked = true;
                else
                    monday_NNM.Checked = false;
                if (eleData[5] == "1")
                    tuesday_NNM.Checked = true;
                else
                    tuesday_NNM.Checked = false;
                if (eleData[6] == "1")
                    wenesday_NNM.Checked = true;
                else
                    wenesday_NNM.Checked = false;
                if (eleData[7] == "1")
                    thusday_NNM.Checked = true;
                else
                    thusday_NNM.Checked = false;
                if (eleData[8] == "1")
                    friday_NNM.Checked = true;
                else
                    friday_NNM.Checked = false;
                if (eleData[9] == "1")
                    satuday_NNM.Checked = true;
                else
                    satuday_NNM.Checked = false;
                if (eleData[10] == "1")
                    sunday_NNM.Checked = true;
                else
                    sunday_NNM.Checked = false;

                autoTimeGetNew_NNM.Value = Int32.Parse(eleData[11]);
                checkLoginTime_NNM.Value = Int32.Parse(eleData[12]);

                //thu cong
                scrapeToPage_NNM.Value = Int32.Parse(eleData[13]);
                scrapeFromPage_NNM.Value = Int32.Parse(eleData[14]);

                //Cai dat chung
                timeBetweenNewFrom_NNM.Value = Int32.Parse(eleData[15]);
                timeBetweenNewTo_NNM.Value = Int32.Parse(eleData[16]);
                linkAPITBox1_NNM.Text = eleData[17];
                linkAPITBox2_NNM.Text = eleData[18];

                userHNTBox_NNM.Text = eleData[19];
                passHNTBox_NNM.Text = eleData[20];

                userHCMTBox_NNM.Text = eleData[21];
                passHCMTBox_NNM.Text = eleData[22];

                if (eleData[23] == "1")
                    TKHNCBox_NNM.Checked = true;
                else
                    TKHNCBox_NNM.Checked = false;
                if (eleData[24] == "1")
                    TKHCMCBox_NNM.Checked = true;
                else
                    TKHCMCBox_NNM.Checked = false;

                if (eleData[25] == "1")
                    layXuoiRBtn_NNM.Checked = true;
                else
                    layNguocRBtn_NNM.Checked = true;

                CrawlNNM.linkAPI1 = linkAPITBox1_NNM.Text;
                CrawlNNM.linkAPI2 = linkAPITBox2_NNM.Text;
                //scrape
                CrawlNNM.fromPage = (int)scrapeFromPage_NNM.Value;
                CrawlNNM.toPage = (int)scrapeToPage_NNM.Value;
                CrawlNNM.timeBetweenNewFrom = (int)timeBetweenNewFrom_NNM.Value;
                CrawlNNM.timeBetweenNewTo = (int)timeBetweenNewTo_NNM.Value;
                //auto
            }

        }

        void myBackgroundWorker_DoWork_NNM(object sender, DoWorkEventArgs e)
        {
            CrawlNNM.runScrape = true;
            CrawlNNM.ScrapeRunCrawl(m_oWorker_NNM);
        }

        void myBackgroundAutoWorker_DoWork_NNM(object sender, DoWorkEventArgs e)
        {
            CrawlNNM.runAuto = true;
            CrawlNNM.AutoRunCrawl(m_autoWorker_NNM);
        }



        void myBackgroundWorker_ProgressChanged_NNM(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 0)
            {
                StatusTBox_NNM.Text = "đang chạy";
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin thủ công";
                var url = e.UserState.ToString();

                string[] row1 = new string[] { dataGridViewNotification_NNM.Rows.Count.ToString(), "Server 1", "xong", time, type, url };
                StatusTBox_NNM.Text = "đang chạy, đã đẩy thành công: " + dataGridViewNotification_NNM.Rows.Count.ToString();
                dataGridViewNotification_NNM.Rows.Insert(0, row1);
                dataGridViewNotification_NNM.Rows[0].DefaultCellStyle.ForeColor = Color.Black;

            }
            else if (e.ProgressPercentage == 1)
            {
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin thủ công";
                var url = e.UserState.ToString();
                string[] row1 = new string[] { dataGridViewNotification_NNM.Rows.Count.ToString(), "Server 1", "lỗi", time, type, url };
                dataGridViewNotification_NNM.Rows.Insert(0, row1);
                dataGridViewNotification_NNM.Rows[0].DefaultCellStyle.ForeColor = Color.Red;
                //dataGridViewNotification.Rows.Add(row1);
            }
            else if (e.ProgressPercentage == 2)
            {
                var dt = DateTime.Now;
                StatusTBox_NNM.Text = "chạy xong thủ công lúc: " + dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                scrapeStartBtn_NNM.Enabled = true;
                scrapeStopBtn_NNM.Enabled = false;
                var pathStringFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NNM");
                if (!Directory.Exists(pathStringFolder))
                {
                    Directory.CreateDirectory(pathStringFolder);
                }

                var pathStatus = System.IO.Path.Combine(pathStringFolder, "status.txt");
                File.WriteAllText(pathStatus, StatusTBox_NNM.Text);
            }
            else if (e.ProgressPercentage == 3)
            {
                StatusTBox_NNM.Text = "đang chạy";
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin thủ công";
                var url = e.UserState.ToString();

                string[] row1 = new string[] { dataGridViewNotification_NNM.Rows.Count.ToString(), "Server 2", "xong", time, type, url };
                StatusTBox_NNM.Text = "đang chạy, đã đẩy thành công: " + dataGridViewNotification_NNM.Rows.Count.ToString();
                //dataGridViewNotification.Rows.Add(row1);
                dataGridViewNotification_NNM.Rows.Insert(0, row1);
                dataGridViewNotification_NNM.Rows[0].DefaultCellStyle.ForeColor = Color.Black;

            }
            else if (e.ProgressPercentage == 4)
            {
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin thủ công";
                var url = e.UserState.ToString();
                string[] row1 = new string[] { dataGridViewNotification_NNM.Rows.Count.ToString(), "Server 2", "lỗi", time, type, url };
                //dataGridViewNotification.Rows.Add(row1);
                dataGridViewNotification_NNM.Rows.Insert(0, row1);
                dataGridViewNotification_NNM.Rows[0].DefaultCellStyle.ForeColor = Color.Red;
            }
            else if (e.ProgressPercentage == 6)
            {
                StatusTBox_NNM.Text = "Đang đợi login";
            }

            existNNMCBox.Checked = Contances.bRMWordsNNM;
        }

        void myBackgroundAutoWorker_ProgressChanged_NNM(object sender, ProgressChangedEventArgs e)
        {
            if (e.ProgressPercentage == 0)
            {
                autoStatusTBox_NNM.Text = "đang chạy";
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin tự động";
                var url = e.UserState.ToString();
                if (url.Contains("đang chạy"))
                    return;
                string[] row1 = new string[] { dataGridViewNotification_NNM.Rows.Count.ToString(), "Server 1", "xong", time, type, url };
                autoStatusTBox_NNM.Text = "đang chạy, đã đẩy thành công: " + dataGridViewNotification_NNM.Rows.Count.ToString();
                //dataGridViewNotification.Rows.Add(row1);
                dataGridViewNotification_NNM.Rows.Insert(0, row1);
                dataGridViewNotification_NNM.Rows[0].DefaultCellStyle.ForeColor = Color.Green;


            }
            else if (e.ProgressPercentage == 1)
            {
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin tự động";
                var url = e.UserState.ToString();
                string[] row1 = new string[] { dataGridViewNotification_NNM.Rows.Count.ToString(), "Server 1", "lỗi", time, type, url };
                //dataGridViewNotification.Rows.Add(row1);
                dataGridViewNotification_NNM.Rows.Insert(0, row1);
                dataGridViewNotification_NNM.Rows[0].DefaultCellStyle.ForeColor = Color.Red;

            }
            else if (e.ProgressPercentage == 2)
            {
                var dt = DateTime.Now;
                autoStatusTBox_NNM.Text = "chạy xong tự động lúc: " + dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                autoStartBtn_NNM.Enabled = true;
                autoStopBtn_NNM.Enabled = false;

                var pathStringFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NNM");
                if (!Directory.Exists(pathStringFolder))
                {
                    Directory.CreateDirectory(pathStringFolder);
                }

                var pathStatus = System.IO.Path.Combine(pathStringFolder, "status_auto.txt");
                File.WriteAllText(pathStatus, autoStatusTBox_NNM.Text);
            }
            else if (e.ProgressPercentage == 5)
            {
                autoStatusTBox_NNM.Text = e.UserState.ToString();
                autoStartBtn_NNM.Enabled = false;
                autoStopBtn_NNM.Enabled = true;
            }
            else if (e.ProgressPercentage == 3)
            {
                autoStatusTBox_NNM.Text = "đang chạy";
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin tự động";
                var url = e.UserState.ToString();
                string[] row1 = new string[] { dataGridViewNotification_NNM.Rows.Count.ToString(), "Server 2", "xong", time, type, url };
                autoStatusTBox_NNM.Text = "đang chạy, đã đẩy thành công: " + dataGridViewNotification_NNM.Rows.Count.ToString();
                //dataGridViewNotification.Rows.Add(row1);
                dataGridViewNotification_NNM.Rows.Insert(0, row1);
                dataGridViewNotification_NNM.Rows[0].DefaultCellStyle.ForeColor = Color.Blue;


            }
            else if (e.ProgressPercentage == 4)
            {
                var dt = DateTime.Now;
                var time = dt.ToShortTimeString() + " - " + dt.ToShortDateString();
                var type = "đẩy tin tự động";
                var url = e.UserState.ToString();
                string[] row1 = new string[] { dataGridViewNotification_NNM.Rows.Count.ToString(), "Server 2", "lỗi", time, type, url };
                //dataGridViewNotification.Rows.Add(row1);
                dataGridViewNotification_NNM.Rows.Insert(0, row1);
                dataGridViewNotification_NNM.Rows[0].DefaultCellStyle.ForeColor = Color.Red;

            }
            else if (e.ProgressPercentage == 6)
            {
                autoStatusTBox_NNM.Text = "Đang đợi login";
            }

            existNNMCBox.Checked = Contances.bRMWordsNNM;
        }

        void myBackgroundWorker_RunWorkerCompleted_NNM(object sender, RunWorkerCompletedEventArgs e)
        {
            scrapeStartBtn_NNM.Enabled = true;
            scrapeStopBtn_NNM.Enabled = false;
            //MessageBox.Show(this, "DỪNG THÀNH CÔNG CHẠY THỦ CÔNG");
            CrawlNNM.runScrape = false;
        }

        void myBackgroundAutoWorker_RunWorkerCompleted_NNM(object sender, RunWorkerCompletedEventArgs e)
        {
            autoStartBtn_NNM.Enabled = true;
            autoStopBtn_NNM.Enabled = false;
            //MessageBox.Show(this, "DỪNG THÀNH CÔNG CHẠY TỰ ĐỘNG");
            CrawlNNM.runAuto = false;
        }

        private void scrapeStartBtn_NNM_Click(object sender, EventArgs e)
        {
            scrapeStartBtn_NNM.Enabled = false;
            scrapeStopBtn_NNM.Enabled = true;
            if (layXuoiRBtn_NNM.Checked)
            {
                CrawlNNM.layXuoiMode = true;
            }
            else
            {
                CrawlNNM.layXuoiMode = false;
            }

            CrawlNNM.userHN = userHNTBox_NNM.Text;
            CrawlNNM.passHN = passHNTBox_NNM.Text;

            CrawlNNM.userHCM = userHCMTBox_NNM.Text;
            CrawlNNM.passHCM = passHCMTBox_NNM.Text;

            CrawlNNM.linkAPI1 = linkAPITBox1_NNM.Text;
            CrawlNNM.linkAPI2 = linkAPITBox2_NNM.Text;
            //scrape
            CrawlNNM.fromPage = (int)scrapeFromPage_NNM.Value;
            CrawlNNM.toPage = (int)scrapeToPage_NNM.Value;
            CrawlNNM.timeBetweenNewFrom = (int)timeBetweenNewFrom_NNM.Value;
            CrawlNNM.timeBetweenNewTo = (int)timeBetweenNewTo_NNM.Value;
            CrawlNNM.tinh.Clear();
            if (TKHNCBox_NNM.Checked)
            {
                CrawlNNM.tinh.Add("HN");
            }
            if (TKHCMCBox_NNM.Checked)
            {
                CrawlNNM.tinh.Add("HCM");
            }

            CrawlNNM.runScrape = true;

            if (AutoLoginCBox_NNM.Checked)
            {
                CrawlNNM.autoLogin = true;
            }
            else
            {
                CrawlNNM.autoLogin = false;
            }

            StatusTBox_NNM.Text = "bắt đầu chạy";

            m_oWorker_NNM.RunWorkerAsync();
        }

        private void scrapeStopBtn_NNM_Click(object sender, EventArgs e)
        {
            scrapeStopBtn_NNM.Enabled = false;
            CrawlNNM.runScrape = false;
            //ProcessHelper.KillAllProcessTree("chromedriver");
            m_oWorker_NNM.CancelAsync();
            m_oWorker_NNM.Dispose();
            StatusTBox_NNM.Text = "đang dừng chạy";
        }

        private void autoStartBtn_NNM_Click(object sender, EventArgs e)
        {
            autoStartBtn_NNM.Enabled = false;
            autoStopBtn_NNM.Enabled = true;

            if (layXuoiRBtn_NNM.Checked)
            {
                CrawlNNM.layXuoiMode = true;
            }
            else
            {
                CrawlNNM.layXuoiMode = false;
            }

            CrawlNNM.userHN = userHNTBox_NNM.Text;
            CrawlNNM.passHN = passHNTBox_NNM.Text;

            CrawlNNM.userHCM = userHCMTBox_NNM.Text;
            CrawlNNM.passHCM = passHCMTBox_NNM.Text;

            CrawlNNM.linkAPI1 = linkAPITBox1_NNM.Text;
            CrawlNNM.linkAPI2 = linkAPITBox2_NNM.Text;
            //scrape
            CrawlNNM.autoFromPage = (int)autoFromPage_NNM.Value;
            CrawlNNM.autoToPage = (int)autoToPage_NNM.Value;
            CrawlNNM.timeBetweenNewFrom = (int)timeBetweenNewFrom_NNM.Value;
            CrawlNNM.timeBetweenNewTo = (int)timeBetweenNewTo_NNM.Value;
            CrawlNNM.autoTinh.Clear();
            if (TKHNCBox_NNM.Checked)
            {
                CrawlNNM.autoTinh.Add("HN");
            }
            if (TKHCMCBox_NNM.Checked)
            {
                CrawlNNM.autoTinh.Add("HCM");
            }

            CrawlNNM.beginTime = autoStartTime_NNM.Value;
            CrawlNNM.endTime = autoStopTime_NNM.Value;

            CrawlNNM.dayinWeek.Clear();
            if (monday_NNM.Checked)
                CrawlNNM.dayinWeek.Add((int)DayOfWeek.Monday);
            if (tuesday_NNM.Checked)
                CrawlNNM.dayinWeek.Add((int)DayOfWeek.Tuesday);
            if (wenesday_NNM.Checked)
                CrawlNNM.dayinWeek.Add((int)DayOfWeek.Wednesday);
            if (thusday_NNM.Checked)
                CrawlNNM.dayinWeek.Add((int)DayOfWeek.Thursday);
            if (friday_NNM.Checked)
                CrawlNNM.dayinWeek.Add((int)DayOfWeek.Friday);
            if (satuday_NNM.Checked)
                CrawlNNM.dayinWeek.Add((int)DayOfWeek.Saturday);
            if (sunday_NNM.Checked)
                CrawlNNM.dayinWeek.Add((int)DayOfWeek.Sunday);

            CrawlNNM.loopTime = (int)autoTimeGetNew_NNM.Value;

            if (AutoLoginCBox_NNM.Checked)
            {
                CrawlNNM.autoLogin = true;
            }
            else
            {
                CrawlNNM.autoLogin = false;
            }

            autoStatusTBox_NNM.Text = "bắt đầu chạy";

            m_autoWorker_NNM.RunWorkerAsync();
        }

        private void autoStopBtn_NNM_Click(object sender, EventArgs e)
        {
            autoStopBtn_NNM.Enabled = false;
            CrawlNNM.runAuto = false;
            //ProcessHelper.KillAllProcessTree("chromedriver");
            m_autoWorker_NNM.CancelAsync();
            m_autoWorker_NNM.Dispose();
            //while (m_autoWorker.IsBusy)
            //{
            //    autoStatusTBox.Text = "đang dừng !!!!";
            //    Thread.Sleep(2 * 1000);
            //}
            autoStatusTBox_NNM.Text = "đang dừng chạy";
        }

        private void DeleteNotificationBtn_NNM_Click(object sender, EventArgs e)
        {
            dataGridViewNotification_NNM.Rows.Clear();
        }

        private void SaveSettingBtn_NNM_Click(object sender, EventArgs e)
        {
            //tudong
            var svAutoFromPage = autoFromPage_NNM.Value.ToString();
            var svAutoToPage = autoToPage_NNM.Value.ToString();
            var svautoStartTime = autoStartTime_NNM.Value.ToString();
            var svautoStopTime = autoStartTime_NNM.Value.ToString();
            var svT2 = monday_NNM.Checked ? "1" : "0";
            var svT3 = tuesday_NNM.Checked ? "1" : "0";
            var svT4 = wenesday_NNM.Checked ? "1" : "0";
            var svT5 = thusday_NNM.Checked ? "1" : "0";
            var svT6 = friday_NNM.Checked ? "1" : "0";
            var svT7 = satuday_NNM.Checked ? "1" : "0";
            var svCN = sunday_NNM.Checked ? "1" : "0";
            var svautoTimeGetNew = autoTimeGetNew_NNM.Value.ToString();
            var svcheckLoginTime = checkLoginTime_NNM.Value.ToString();
            //var svautoHNCBox = autoHNCBox_NNM.Checked ? "1" : "0";
            //var svautoHCMCBox = autoHCMCBox_NNM.Checked ? "1" : "0";
            //thu cong
            var svscrapeToPage = scrapeToPage_NNM.Value.ToString();
            var svscrapeFromPage = scrapeFromPage_NNM.Value.ToString();
            //var svHNCBox = HNCBox_NNM.Checked ? "1" : "0";
            //var svHCMCBox = HCMCBox_NNM.Checked ? "1" : "0";
            //Cai dat chung
            var svtimeBetweenNewFrom = timeBetweenNewFrom_NNM.Value.ToString();
            var svtimeBetweenNewTo = timeBetweenNewTo_NNM.Value.ToString();
            var svlinkAPITBox1 = linkAPITBox1_NNM.Text;
            var svlinkAPITBox2 = linkAPITBox2_NNM.Text;

            var userHN = userHNTBox_NNM.Text;
            var passHN = passHNTBox_NNM.Text;

            var userHCM = userHCMTBox_NNM.Text;
            var passHCM = passHCMTBox_NNM.Text;

            var TKHN = TKHNCBox_NNM.Checked ? "1" : "0";
            var TKHCM = TKHCMCBox_NNM.Checked ? "1" : "0";
            var layxuoi = layXuoiRBtn_NNM.Checked ? "1" : "0";

            var data = svAutoFromPage + "|" + svAutoToPage + "|" + svautoStartTime + "|" + svautoStopTime + "|" + svT2 + "|" + svT3 + "|"
                + svT4 + "|" + svT5 + "|" + svT6 + "|" + svT7 + "|" + svCN + "|" + svautoTimeGetNew + "|"
                + svcheckLoginTime + "|" + svscrapeToPage + "|" + svscrapeFromPage + "|" + svtimeBetweenNewFrom + "|" + svtimeBetweenNewTo + "|" + svlinkAPITBox1 + "|" + svlinkAPITBox2
                + "|" + userHN + "|" + passHN + "|" + userHCM + "|" + passHCM
                + "|" + TKHN + "|" + TKHCM + "|" + layxuoi;

            var pathStringFolder = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "NNM");
            if (!Directory.Exists(pathStringFolder))
            {
                Directory.CreateDirectory(pathStringFolder);
            }

            var pathSetting = System.IO.Path.Combine(pathStringFolder, "Setting.txt");
            File.WriteAllText(pathSetting, data);
        }

        private void RemoveWordBtn_Click(object sender, EventArgs e)
        {
            RemoveWordsForm rmForm = new RemoveWordsForm("salenha");
            rmForm.ShowDialog();
        }

        private void RemoveWordNNMBtn_Click(object sender, EventArgs e)
        {
            RemoveWordsForm rmForm = new RemoveWordsForm("nguonnhamoi");
            rmForm.ShowDialog();
        }
    }
}
