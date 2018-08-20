using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// ListPage.xaml 的互動邏輯
    /// </summary>
    public partial class ListPage : NotifyUserControl
    {

        public PrintSetting m_PrinterSetting;


        private PrintDocument m_PdForList;
        private ResultItem m_PrintingItem;
        private DBLoadUtility m_DBload = new DBLoadUtility();
        private PrintDocument m_PdOneMore;



        private ObservableCollection<ResultItem> m_Results;


        private string tab2companyStr;
        private string tab2locationstr;

        public ListPage()
        {
            InitializeComponent();

            m_PrinterSetting = new PrintSetting(); 
           
            //設定商品單號搜尋系統
            m_Results = new ObservableCollection<ResultItem>();
            ListBox_SearchResult.ItemsSource = m_Results;
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

        #region 單號搜尋使用

        //搜尋銷單單號使用
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchData();
        }

        //貼上直接執行
        private void Paste_Click(object sender, RoutedEventArgs e)
        {
            TextBox_SearchNumber.Clear();
            TextBox_SearchNumber.Paste();
            SearchData();
        }

        private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SearchData();
            }
        }

        private void SearchData()
        {
            //清除所有現有資料
            Result_CompanyName.Text = string.Empty;
            selectAll.IsChecked = false;
            tab2locationstr = null;
            m_Results.Clear();

            //TODO進行資料搜尋
            List<ResultItem> tempdata = m_DBload.GetSearchData(TextBox_SearchNumber.Text);
            if (tempdata.Count > 0)
            {
                Result_CompanyName.Text = tempdata[0].Company;
                if (tempdata[0].CompanyId.IndexOf('3') == 0)
                    tab2locationstr = "蘆洲";
                else if (tempdata[0].CompanyId.IndexOf('Z') == 0)
                    tab2locationstr = "內湖";
                else
                    tab2locationstr = "石牌";

            }
            foreach (ResultItem temp in tempdata)
            {
                temp.Name = ClearOtherWords(temp.Name);
                if (temp.ItemID.Contains("ZA") || temp.ItemID == string.Empty || temp.ItemID.Contains("F-") || temp.ItemID.Contains("X-") || temp.ItemID == "X"
                    || temp.ItemID.Contains("H"))
                {
                    if (temp.ItemID.Contains("F-"))
                    {
                        string[] splittemp = temp.ItemID.Split('-');
                        if (splittemp.Length == 2)
                        {

                            int i = 0;
                            Int32.TryParse(splittemp[1], out i);
                            if (i >= 10)
                                continue;
                        }
                    }
                    m_Results.Add(temp);
                }
            }

        }

        private string ClearOtherWords(string input)
        {

            //清除MM
            input = input.Replace("mm", "").ToLower();

            while (input.Contains("  "))
            {
                input = input.Replace("  ", " ");
            }


            return input;
        }

        //選擇清單使用
        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (selectAll.IsChecked.HasValue && selectAll.IsChecked.Value)
                ListBox_SearchResult.SelectAll();
            else
                ListBox_SearchResult.UnselectAll();
        }

        //清除選擇的動作
        private void OnUncheckItem(object sender, RoutedEventArgs e)
        {
            selectAll.IsChecked = false;
        }

        //清單列印指令
        private void ListButton_Click(object sender, RoutedEventArgs e)
        {
            tab2companyStr = Result_CompanyName.Text;
            StringBuilder sb = new StringBuilder();
            if (tab2companyStr == string.Empty)
            {
                sb.AppendLine("請重新輸入訂單編號!");
            }
            if (SumAllResults() <= 0)
            {
                sb.AppendLine("沒有列印數量!");
            }
            if (sb.Length > 0)
            {
                MessageBox.Show(sb.ToString(), "注意", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (ListBox_SearchResult.SelectedItems != null)
            {
                string printerName = m_PrinterSetting.PrinterName;
                foreach (ResultItem item in ListBox_SearchResult.SelectedItems)
                {
                    m_PdForList = new PrintDocument();
                    int i = 0;
                    Int32.TryParse(item.Number, out i);
                    if (i > 0)
                    {
                        m_PdForList.PrintPage += pdForListe_PrintPage;
                        m_PrintingItem = item;
                        m_PdForList.PrinterSettings.Copies = (short)i;
                        if (printerName != string.Empty)
                            m_PdForList.PrinterSettings.PrinterName = printerName;

                        m_PdForList.Print();
                    }
                }
                if (Result_Addone.IsChecked.HasValue && Result_Addone.IsChecked.Value)
                {
                    m_PdOneMore = new PrintDocument();
                    m_PdOneMore.PrintPage += pdOneMore_PrintPage;
                    m_PdOneMore.PrinterSettings.Copies = 1;
                    if (printerName != string.Empty)
                        m_PdOneMore.PrinterSettings.PrinterName = printerName;
                    m_PdOneMore.Print();
                }
            }
        }

        void pdOneMore_PrintPage(object sender, PrintPageEventArgs e)
        {
            DrawBlock(e.Graphics, null);
        }

        private void DrawBlock(Graphics g, SelectedType type)
        {

            Font drawFont1 = new Font("新細明體", m_PrinterSetting.Font1);
            Font drawFont3 = new Font("新細明體", 15);
            Font drawFont2 = new Font("新細明體", m_PrinterSetting.Font2);
            SolidBrush drawBrush = new SolidBrush(System.Drawing.Color.Black);

            if (tab2locationstr != null)
                g.DrawString(tab2locationstr, drawFont3, drawBrush, new PointF(5, 5));

            g.DrawString(tab2companyStr, drawFont1, drawBrush, new PointF(m_PrinterSetting.Font1XLoc + 30, m_PrinterSetting.Font1YLoc));

            if (type != null)
            {
                g.DrawString(type.Name, drawFont2, drawBrush, new PointF(m_PrinterSetting.Font2XLoc, m_PrinterSetting.Font2YLoc));
            }
        }


        private int SumAllResults()
        {
            int sum = 0;
            foreach (ResultItem item in ListBox_SearchResult.SelectedItems)
            {
                int i = 0;
                Int32.TryParse(item.Number, out i);
                sum = sum + i;
            }
            if (Result_Addone.IsChecked.HasValue && Result_Addone.IsChecked.Value)
            {
                sum++;
            }
            return sum;
        }

        void pdForListe_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            Font drawFont1 = new Font("新細明體", m_PrinterSetting.SCFont1);
            Font drawFont2 = new Font("新細明體", m_PrinterSetting.SCFont2);
            Font drawFont3 = new Font("新細明體", m_PrinterSetting.SCFont3);
            Font drawFont4 = new Font("新細明體", 15);
            SolidBrush drawBrush = new SolidBrush(System.Drawing.Color.Black);

            if (tab2locationstr != null)
                g.DrawString(tab2locationstr, drawFont4, drawBrush, new PointF(5, 3));

            g.DrawString(tab2companyStr, drawFont1, drawBrush, new PointF(m_PrinterSetting.SCFont1XLoc + 30, m_PrinterSetting.SCFont1YLoc));


            if (m_PrintingItem != null && m_PrintingItem.Name != null)
            {
                string[] items = SplitData(m_PrintingItem.Name);
                if (items.Length == 1)
                {
                    items[0] = items[0].Replace("ST", "").Replace("錏", "").Replace("電動", "");
                    g.DrawString(items[0], drawFont2, drawBrush, new PointF(m_PrinterSetting.SCFont2XLoc, m_PrinterSetting.SCFont2YLoc));
                }
                else
                {
                    if (items.Length >= 2 && items[0] != string.Empty)
                    {
                        items[0] = items[0].Replace("ST", "").Replace("錏", "").Replace("電動", "");
                        g.DrawString(items[0], drawFont2, drawBrush, new PointF(m_PrinterSetting.SCFont2XLoc, m_PrinterSetting.SCFont2YLoc));
                    }
                    if (items.Length >= 2 && items[1] != string.Empty)
                    {
                        items[1] = items[1].Replace("ST", "").Replace("錏", "").Replace("電動", "");
                        g.DrawString(items[1], drawFont3, drawBrush, new PointF(m_PrinterSetting.SCFont3XLoc, m_PrinterSetting.SCFont3YLoc));
                    }
                }

            }
        }

        private string[] SplitData(string input)
        {
            string[] temp = input.Split(' ');
            if (temp.Length == 2)
            {
                string[] check = temp[1].Split('*');
                if (check.Length == 3)
                    return temp;
            }
            else if (temp.Length > 2)
            {
                string[] check = temp[temp.Length - 1].Split('*');
                if (check.Length == 3)
                {
                    string[] output = new string[2];
                    for (int i = 0; i < temp.Length - 1; i++)
                    {
                        output[0] = output[0] + temp[i];
                    }
                    output[1] = temp[temp.Length - 1];
                    return output;
                }
            }
            return temp;
        }

        #endregion
    }
}
