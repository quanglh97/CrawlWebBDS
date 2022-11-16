using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrawlWebBDS
{
    public class MyTabControl : TabControl

    {

        public MyTabControl()

        {

            this.DrawMode = TabDrawMode.OwnerDrawFixed;

        }



        protected override void OnDrawItem(DrawItemEventArgs e)

        {

            using (SolidBrush br = new SolidBrush(Color.White))

            {

                e.Graphics.FillRectangle(br, e.Bounds);

            }



            Font font = e.Font;



            if (e.Index == this.SelectedIndex)

            {

                font = new Font(e.Font, FontStyle.Bold);

            }



            using (SolidBrush br = new SolidBrush(Color.Black))

            {

                StringFormat sf = new StringFormat();

                sf.LineAlignment = StringAlignment.Center;

                sf.Alignment = StringAlignment.Center;

                e.Graphics.DrawString(this.TabPages[e.Index].Text, font, br, e.Bounds, sf);

            }



            font.Dispose();

        }

    }
}
