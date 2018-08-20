using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFLabelMaker.Class;

namespace WPFLabelMaker.pages
{
    /// <summary>
    /// NormalPage.xaml 的互動邏輯
    /// </summary>
    public partial class NormalPage : NotifyUserControl
    {
        public CompleteTypes m_AllTypes;

        private PrintDocument m_Pd1;
        private PrintDocument m_Pd2;
        private PrintDocument m_Pd3;
        private PrintDocument m_Pd4;
        private PrintDocument m_Pd5;
        private PrintDocument m_Pd6;
        private PrintDocument m_Pd7;
        private PrintDocument m_PdOneMore;

        private string companyStr;
        private string locationstr;
        private int location;

        public PrintSetting m_PrinterSetting;

        public NormalPage()
        {
            InitializeComponent();
            SetupAll();
            this.DataContext = this;
            m_PrinterSetting = new PrintSetting();
            gridTypes.DataContext = m_AllTypes;

            //設定combotextfreeinput items欄位
            FreeTypeDataLoader ft = new FreeTypeDataLoader();
            if (ft.AllTypes != null)
            {
                foreach (string type in ft.AllTypes)
                {
                    string item = type;
                    if (item != string.Empty)
                    {
                        comboBoxFreeInput.Items.Add(item);
                    }
                }
            }
        }


        private void SetupAll()
        {
            m_AllTypes = new CompleteTypes();

            m_AllTypes.Type1 = new SelectedType { Name = "壓", IsChecked = false, Number = "0" };
            m_AllTypes.Type2 = new SelectedType { Name = "角", IsChecked = false, Number = "0" };
            m_AllTypes.Type3 = new SelectedType { Name = "平", IsChecked = false, Number = "0" };
            m_AllTypes.Type4 = new SelectedType { Name = "花", IsChecked = false, Number = "0" };
            m_AllTypes.Type5 = new SelectedType { Name = "連体", IsChecked = false, Number = "0" };
            m_AllTypes.Type6 = new SelectedType { Name = "串", IsChecked = false, Number = "0" };
            m_AllTypes.Type7 = new SelectedType { Name = string.Empty, IsChecked = false, Number = "0" };

        }


        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            //只可以輸入數字
            if ((e.Key < Key.D0 || e.Key > Key.D9) && (e.Key < Key.NumPad0 || e.Key > Key.NumPad9) && e.Key != Key.Back && e.Key != Key.Delete
                && e.Key != Key.Left && e.Key != Key.Right && e.Key != Key.Tab && !(e.Key == Key.ImeProcessed && (e.ImeProcessedKey >= Key.NumPad0 || e.ImeProcessedKey <= Key.NumPad9)))
            {
                e.Handled = true;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            locationstr = null;
            StringBuilder sb = new StringBuilder();
            if (SingleCompany == null && SingleCompanyName == string.Empty)
            {
                sb.AppendLine("請輸入公司名稱!");
            }
            if (m_AllTypes.GetSum() <= 0)
            {
                sb.AppendLine("沒有列印數量!");
            }
            if (sb.Length > 0)
            {
                MessageBox.Show(sb.ToString(), "注意", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (SingleCompany != null)
            {
                companyStr = SingleCompany.Name;
                switch (location)
                {
                    case 0:
                        locationstr = "石牌";
                        break;
                    case 1:
                        locationstr = "蘆洲";
                        break;
                    case 2:
                        locationstr = "內湖";
                        break;
                }


            }
            else
            {
                companyStr = SingleCompanyName.Trim();
            }

            //開始列印  


            //PrintingPage printpage = new PrintingPage(companystr, m_AllTypes, m_PrinterSetting);
            //printpage.Show();

            m_Pd1 = m_Pd2 = m_Pd3 = m_Pd4 = m_Pd5 = m_Pd6 = m_Pd7 = m_PdOneMore = null;

            StartPrint();

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


        private void PreViewButton_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            if (SingleCompany == null && SingleCompanyName == string.Empty)
            {
                sb.AppendLine("請輸入公司名稱!");
            }
            if (m_AllTypes.GetSum() <= 0)
            {
                sb.AppendLine("沒有列印數量!");
            }
            if (sb.Length > 0)
            {
                MessageBox.Show(sb.ToString(), "注意", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            string companystr;
            if (SingleCompany != null)
            {
                companystr = SingleCompany.Name;
            }
            else
            {
                companystr = SingleCompanyName.Trim();
            }

            //開始列印  
            PrintingPage printpage = new PrintingPage(companystr, m_AllTypes, m_PrinterSetting);
            printpage.Show();
        }

        private void StartPrint()
        {
            //Setup Print 
            string printerName = m_PrinterSetting.PrinterName;


            if (m_AllTypes.Type1.IsChecked && int.Parse(m_AllTypes.Type1.Number) > 0)
            {
                m_Pd1 = new PrintDocument();
                m_Pd1.PrintPage += pd1_PrintPage;
                m_Pd1.PrinterSettings.Copies = short.Parse(m_AllTypes.Type1.Number);
                if (printerName != string.Empty)
                    m_Pd1.PrinterSettings.PrinterName = printerName;
            }

            if (m_AllTypes.Type2.IsChecked && int.Parse(m_AllTypes.Type2.Number) > 0)
            {
                m_Pd2 = new PrintDocument();
                m_Pd2.PrintPage += pd2_PrintPage;
                m_Pd2.PrinterSettings.Copies = short.Parse(m_AllTypes.Type2.Number);
                if (printerName != string.Empty)
                    m_Pd2.PrinterSettings.PrinterName = printerName;
            }

            if (m_AllTypes.Type3.IsChecked && int.Parse(m_AllTypes.Type3.Number) > 0)
            {
                m_Pd3 = new PrintDocument();
                m_Pd3.PrintPage += pd3_PrintPage;
                m_Pd3.PrinterSettings.Copies = short.Parse(m_AllTypes.Type3.Number);
                if (printerName != string.Empty)
                    m_Pd3.PrinterSettings.PrinterName = printerName;
            }

            if (m_AllTypes.Type4.IsChecked && int.Parse(m_AllTypes.Type4.Number) > 0)
            {
                m_Pd4 = new PrintDocument();
                m_Pd4.PrintPage += pd4_PrintPage;
                m_Pd4.PrinterSettings.Copies = short.Parse(m_AllTypes.Type4.Number);
                if (printerName != string.Empty)
                    m_Pd4.PrinterSettings.PrinterName = printerName;
            }

            if (m_AllTypes.Type5.IsChecked && int.Parse(m_AllTypes.Type5.Number) > 0)
            {
                m_Pd5 = new PrintDocument();
                m_Pd5.PrintPage += pd5_PrintPage;
                m_Pd5.PrinterSettings.Copies = short.Parse(m_AllTypes.Type5.Number);
                if (printerName != string.Empty)
                    m_Pd5.PrinterSettings.PrinterName = printerName;
            }

            if (m_AllTypes.Type6.IsChecked && int.Parse(m_AllTypes.Type6.Number) > 0)
            {
                m_Pd6 = new PrintDocument();
                m_Pd6.PrintPage += pd6_PrintPage;
                m_Pd6.PrinterSettings.Copies = short.Parse(m_AllTypes.Type6.Number);
                if (printerName != string.Empty)
                    m_Pd6.PrinterSettings.PrinterName = printerName;
            }

            if (m_AllTypes.Type7.IsChecked && m_AllTypes.Type7.Name != string.Empty && int.Parse(m_AllTypes.Type7.Number) > 0)
            {
                m_Pd7 = new PrintDocument();
                m_Pd7.PrintPage += pd7_PrintPage;
                m_Pd7.PrinterSettings.Copies = short.Parse(m_AllTypes.Type7.Number);
                if (printerName != string.Empty)
                    m_Pd7.PrinterSettings.PrinterName = printerName;
            }

            if (m_AllTypes.Addone)
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

        //產生標籤圖形與文字 printdocument draw on graphic
        private void DrawBlock(Graphics g, SelectedType type)
        {

            Font drawFont1 = new Font("新細明體", m_PrinterSetting.Font1);
            Font drawFont3 = new Font("新細明體", 15);
            Font drawFont2 = new Font("新細明體", m_PrinterSetting.Font2);
            SolidBrush drawBrush = new SolidBrush(System.Drawing.Color.Black);

            if (locationstr != null)
                g.DrawString(locationstr, drawFont3, drawBrush, new PointF(5, 5));

            g.DrawString(companyStr, drawFont1, drawBrush, new PointF(m_PrinterSetting.Font1XLoc + 30, m_PrinterSetting.Font1YLoc));

            if (type != null)
            {
                g.DrawString(type.Name, drawFont2, drawBrush, new PointF(m_PrinterSetting.Font2XLoc, m_PrinterSetting.Font2YLoc));
            }
        }

        private void SRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            location = int.Parse(rb.DataContext.ToString());
        }


        private CompanyName singlecompany;
        public CompanyName SingleCompany
        {
            get { return singlecompany; }
            set
            {
                singlecompany = value;
                if (singlecompany != null)
                {
                    if (singlecompany.ID != null)
                    {
                        if (singlecompany.ID.IndexOf('3') == 0)
                            Lou_Radio.IsChecked = true;
                        else if (singlecompany.ID.IndexOf('Z') == 0)
                            Nah_Radio.IsChecked = true;
                        else
                            Shin_Radio.IsChecked = true;
                    }
                }
                SetProperty(ref singlecompany, value);
            }
        }

        private string singlecompanyname = "";
        public string SingleCompanyName
        {
            get { return singlecompanyname; }
            set
            {
                SetProperty(ref singlecompanyname, value);
            }
        }

    }
}
