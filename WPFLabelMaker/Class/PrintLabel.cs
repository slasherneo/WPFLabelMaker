using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFLabelMaker.Class
{
    class PrintLabel
    {
        private PrintDocument m_Pd1;
        private string company;
        private string location ;
        private NTItem data;
        private PrintSetting m_PrinterSetting;

        public PrintLabel(short copyes, string _company, string _location,NTItem _data)
        {
            m_Pd1 = new PrintDocument();
            m_PrinterSetting = new PrintSetting();
            m_Pd1.PrintPage += pd1_PrintPage;
            m_Pd1.PrinterSettings.Copies = copyes;
            company = _company;
            location = _location;
            data = _data;
            m_Pd1.PrinterSettings.PrinterName = m_PrinterSetting.PrinterName;
            m_Pd1.Print();

        }

        void pd1_PrintPage(object sender, PrintPageEventArgs e)
        {
            DrawBlock(e.Graphics);
        }
        

        private void DrawBlock(Graphics g)
        {
            Font drawFont1 = new Font("新細明體", 28);
            Font drawFont3 = new Font("新細明體", 20);
            Font drawFont2 = new Font("新細明體", 15);
            SolidBrush drawBrush = new SolidBrush(System.Drawing.Color.Black);

            if (data != null)
            {
                if (location != null && location != string.Empty)
                    g.DrawString(location + "  " + data.StartTime, drawFont2, drawBrush, new PointF(5, 6));

                g.DrawString(data.Matel +" " +data.Tile.ToString("0.0"), drawFont3, drawBrush, new PointF(2, 67));

                g.DrawString(data.Sn.ToString("0000"), drawFont3, drawBrush, new PointF(170, 6));

                g.DrawString(data.SizeShort.ToString() + " X " + data.SizeLong.ToString() + " 共" + data.Number, drawFont3, drawBrush, new PointF(2, 98));
            }else
            {
                if (location != null && location != string.Empty)
                    g.DrawString(location, drawFont3, drawBrush, new PointF(2, 6));
            }

            g.DrawString(company, drawFont1, drawBrush, new PointF(2, 30));

        }
    }
}
