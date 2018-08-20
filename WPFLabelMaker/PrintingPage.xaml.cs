using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Windows;
using WPFLabelMaker.Class;

namespace WPFLabelMaker
{
    /// <summary>
    /// PrintingPage.xaml 的互動邏輯
    /// </summary>
    public partial class PrintingPage : Window, INotifyPropertyChanged
    {
        private PrintDocument m_Pd1;
        private PrintDocument m_Pd2;
        private PrintDocument m_Pd3;
        private PrintDocument m_Pd4;
        private PrintDocument m_Pd5;
        private PrintDocument m_Pd6;
        private PrintDocument m_Pd7;
        private PrintDocument m_PdOneMore;

        private PrintSetting m_PrinterSetting;

        public PrintingPage()
        {
            InitializeComponent();
        }

        public PrintingPage(string companyname, CompleteTypes alltypes, PrintSetting printersetting)
        {
            InitializeComponent();

            CompanyName = companyname;
            AllTypes = alltypes;

            int heightindex = AllTypes.GetTypesNumber();
            this.Height = (heightindex + 1) * 58 + 72;

            this.DataContext = this;

            //Setup Print 
            m_PrinterSetting = printersetting;
            string printerName = printersetting.PrinterName;


            if (AllTypes.Type1.IsChecked && int.Parse(AllTypes.Type1.Number) > 0)
            {
                m_Pd1 = new PrintDocument();
                m_Pd1.PrintPage += pd1_PrintPage;
                m_Pd1.PrinterSettings.Copies = short.Parse(AllTypes.Type1.Number);
                if (printerName != string.Empty)
                    m_Pd1.PrinterSettings.PrinterName = printerName;
            }

            if (AllTypes.Type2.IsChecked && int.Parse(AllTypes.Type2.Number) > 0)
            {
                m_Pd2 = new PrintDocument();
                m_Pd2.PrintPage += pd2_PrintPage;
                m_Pd2.PrinterSettings.Copies = short.Parse(AllTypes.Type2.Number);
                if (printerName != string.Empty)
                    m_Pd2.PrinterSettings.PrinterName = printerName;
            }

            if (AllTypes.Type3.IsChecked && int.Parse(AllTypes.Type3.Number) > 0)
            {
                m_Pd3 = new PrintDocument();
                m_Pd3.PrintPage += pd3_PrintPage;
                m_Pd3.PrinterSettings.Copies = short.Parse(AllTypes.Type3.Number);
                if (printerName != string.Empty)
                    m_Pd3.PrinterSettings.PrinterName = printerName;
            }

            if (AllTypes.Type4.IsChecked && int.Parse(AllTypes.Type4.Number) > 0)
            {
                m_Pd4 = new PrintDocument();
                m_Pd4.PrintPage += pd4_PrintPage;
                m_Pd4.PrinterSettings.Copies = short.Parse(AllTypes.Type4.Number);
                if (printerName != string.Empty)
                    m_Pd4.PrinterSettings.PrinterName = printerName;
            }

            if (AllTypes.Type5.IsChecked && int.Parse(AllTypes.Type5.Number) > 0)
            {
                m_Pd5 = new PrintDocument();
                m_Pd5.PrintPage += pd5_PrintPage;
                m_Pd5.PrinterSettings.Copies = short.Parse(AllTypes.Type5.Number);
                if (printerName != string.Empty)
                    m_Pd5.PrinterSettings.PrinterName = printerName;
            }

            if (AllTypes.Type6.IsChecked && int.Parse(AllTypes.Type6.Number) > 0)
            {
                m_Pd6 = new PrintDocument();
                m_Pd6.PrintPage += pd6_PrintPage;
                m_Pd6.PrinterSettings.Copies = short.Parse(AllTypes.Type6.Number);
                if (printerName != string.Empty)
                    m_Pd6.PrinterSettings.PrinterName = printerName;
            }

            if (AllTypes.Type7.IsChecked && AllTypes.Type7.Name != string.Empty && int.Parse(AllTypes.Type7.Number) > 0)
            {
                m_Pd7 = new PrintDocument();
                m_Pd7.PrintPage += pd7_PrintPage;
                m_Pd7.PrinterSettings.Copies = short.Parse(AllTypes.Type7.Number);
                if (printerName != string.Empty)
                    m_Pd7.PrinterSettings.PrinterName = printerName;
            }

            if (AllTypes.Addone)
            {
                m_PdOneMore = new PrintDocument();
                m_PdOneMore.PrintPage += pdOneMore_PrintPage;
                m_PdOneMore.PrinterSettings.Copies = 1;
                if (printerName != string.Empty)
                    m_PdOneMore.PrinterSettings.PrinterName = printerName;
            }
        }

        void pd1_PrintPage(object sender, PrintPageEventArgs e)
        {
            DrawBlock(e.Graphics, m_AllTypes.Type1);
        }

        void pd2_PrintPage(object sender, PrintPageEventArgs e)
        {
            DrawBlock(e.Graphics, m_AllTypes.Type2);
        }

        void pd3_PrintPage(object sender, PrintPageEventArgs e)
        {
            DrawBlock(e.Graphics, m_AllTypes.Type3);
        }

        void pd4_PrintPage(object sender, PrintPageEventArgs e)
        {
            DrawBlock(e.Graphics, m_AllTypes.Type4);
        }

        void pd5_PrintPage(object sender, PrintPageEventArgs e)
        {
            DrawBlock(e.Graphics, m_AllTypes.Type5);
        }

        void pd6_PrintPage(object sender, PrintPageEventArgs e)
        {
            DrawBlock(e.Graphics, m_AllTypes.Type6);
        }

        void pd7_PrintPage(object sender, PrintPageEventArgs e)
        {
            DrawBlock(e.Graphics, m_AllTypes.Type7);
        }

        void pdOneMore_PrintPage(object sender, PrintPageEventArgs e)
        {
            DrawBlock(e.Graphics, null);
        }

        //執行列印
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (m_Pd1 != null)
                m_Pd1.Print();
            if (m_Pd2 != null)
                m_Pd2.Print();
            if (m_Pd3 != null)
                m_Pd3.Print();
            if (m_Pd4 != null)
                m_Pd4.Print();
            if (m_Pd5 != null)
                m_Pd5.Print();
            if (m_Pd6 != null)
                m_Pd6.Print();
            if (m_Pd7 != null)
                m_Pd7.Print();
            if (m_PdOneMore != null)
                m_PdOneMore.Print();
        }


        //產生標籤圖形與文字 printdocument draw on graphic
        private void DrawBlock(Graphics g, SelectedType type)
        {

            Font drawFont1 = new Font("新細明體", m_PrinterSetting.Font1);
            Font drawFont2 = new Font("新細明體", m_PrinterSetting.Font2);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            Char[] words = CompanyName.ToCharArray();
            if (words.Length == 2)
            {
                g.DrawString(words[0].ToString(), drawFont1, drawBrush, new PointF((m_PrinterSetting.Font1XLoc * 2 + m_PrinterSetting.Font1XSplit) / 2, m_PrinterSetting.Font1YLoc));
                g.DrawString(words[1].ToString(), drawFont1, drawBrush, new PointF((m_PrinterSetting.Font1XLoc * 2 + 3 * m_PrinterSetting.Font1XSplit) / 2, m_PrinterSetting.Font1YLoc));
            }
            else if (words.Length > 2)
            {
                g.DrawString(words[0].ToString(), drawFont1, drawBrush, new PointF(m_PrinterSetting.Font1XLoc, m_PrinterSetting.Font1YLoc));
                g.DrawString(words[1].ToString(), drawFont1, drawBrush, new PointF(m_PrinterSetting.Font1XLoc + m_PrinterSetting.Font1XSplit, m_PrinterSetting.Font1YLoc));
                g.DrawString(words[2].ToString(), drawFont1, drawBrush, new PointF(m_PrinterSetting.Font1XLoc + 2 * m_PrinterSetting.Font1XSplit, m_PrinterSetting.Font1YLoc));
            }

            if (type != null)
            {        
               g.DrawString(type.Name, drawFont2, drawBrush, new PointF(m_PrinterSetting.Font2XLoc, m_PrinterSetting.Font2YLoc));               
            }
        }

        private string m_CompanyName;
        public string CompanyName
        {
            get { return m_CompanyName; }
            set
            {
                m_CompanyName = value;
                OnPropertyChanged("CompanyName");
            }
        }

        private CompleteTypes m_AllTypes;
        public CompleteTypes AllTypes
        {
            get { return m_AllTypes; }
            set
            {
                m_AllTypes = value;
                OnPropertyChanged("AllTypes");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }


    }
}
