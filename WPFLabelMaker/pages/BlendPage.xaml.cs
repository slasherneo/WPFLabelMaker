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
    /// BlendPage.xaml 的互動邏輯
    /// </summary>
    public partial class BlendPage : NotifyUserControl
    {
        private ObservableCollection<NTItem> m_NTList;
        private int locations;

        private string locationstr;
        private string companyStr;
    


        public BlendPage()
        {
            InitializeComponent();
            this.DataContext = this;
            //設定新設選單
            m_NTList = new ObservableCollection<NTItem>();
            m_NTList.CollectionChanged +=m_NTList_CollectionChanged;
            ListBox_NTList.ItemsSource = m_NTList;
        }

        private void m_NTList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            //處理新增檔案
            if (e.OldItems == null && e.NewItems != null)
            {
                    if (m_NTList.Count == 1)
                    {
                        DateTime dt = DateTime.Now;
                        m_NTList[m_NTList.Count - 1].StartTime = dt.ToString("MMdd");
                    }
                    if (m_NTList.Count > 1)
                    {
                        m_NTList[m_NTList.Count - 1].Sn = m_NTList[m_NTList.Count - 2].Sn+1;
                        m_NTList[m_NTList.Count - 1].StartTime = m_NTList[m_NTList.Count - 2].StartTime;
                        m_NTList[m_NTList.Count - 1].Matel = m_NTList[m_NTList.Count - 2].Matel;
                        m_NTList[m_NTList.Count - 1].Tile = m_NTList[m_NTList.Count - 2].Tile;
                    }               
            }
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

        private void SRadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            locations = int.Parse(rb.DataContext.ToString());
        }



        private CompanyName groupcompany;
        public CompanyName GroupCompany
        {
            get { return groupcompany; }
            set
            {
                groupcompany = value;
                if (groupcompany != null)
                {
                    if (groupcompany.ID != null)
                    {
                        if (groupcompany.ID.IndexOf('3') == 0)
                            Lou_Radio.IsChecked = true;
                        else if (groupcompany.ID.IndexOf('Z') == 0)
                            Nah_Radio.IsChecked = true;
                        else
                            Shin_Radio.IsChecked = true;
                    }
                }
                SetProperty(ref groupcompany, value);
            }
        }

        private string groupcompanyname;
        public string GroupCompanyName
        {
            get { return groupcompanyname; }
            set
            {
                SetProperty(ref groupcompanyname, value);
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            m_NTList.Clear();
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            locationstr = null;
            StringBuilder sb = new StringBuilder();
            if (GroupCompany == null && GroupCompanyName == null)
            {
                sb.AppendLine("請輸入公司名稱!");
            }
            if (m_NTList.Count <= 0)
            {
                sb.AppendLine("沒有填寫內容!");
            }
            if (sb.Length > 0)
            {
                MessageBox.Show(sb.ToString(), "注意", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (GroupCompany != null)
            {
                companyStr = GroupCompany.Name;
                switch (locations)
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
                companyStr = GroupCompanyName.Trim();
            }

            foreach(NTItem item in m_NTList)
            {
                PrintLabel pl = null; 
                if(item.Number>0)
                    pl = new PrintLabel((short)item.Number, companyStr, locationstr, item);
            }
            if((bool)Result_Addone.IsChecked)
            {
                PrintLabel pl = new PrintLabel((short)1, companyStr, locationstr, null);
            }
        }

        void m_PdList_PrintPage(object sender, PrintPageEventArgs e)
        {
            throw new NotImplementedException();
        }

        

    }
}
