using Microsoft.VisualBasic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace WPFLabelMaker.controls
{
    /// <summary>
    /// UserControl1.xaml 的互動邏輯
    /// </summary>
    public partial class AutoComboxCompany : UserControl, INotifyPropertyChanged
    {
        public AutoComboxCompany()
        {
            InitializeComponent();
            NTCompanyName.ItemsSource = new CompanyList();
            NTCompanyName.ItemFilter = (search, item) =>
            {
                CompanyName company = item as CompanyName;
                if (company != null)
                {
                    string filter = Strings.StrConv(search, VbStrConv.Narrow, 0).ToLower().Trim();
                    return company.ID.ToLower().Contains(filter) || company.Name.Contains(filter);
                }
                return false;
            };
        }

        private void CBCompanyName_DropDownClosing(object sender, RoutedPropertyChangingEventArgs<bool> e)
        {

            AutoCompleteBox aCBox = sender as AutoCompleteBox;
            if (aCBox != null && aCBox.SelectedItem == null)
            {
                aCBox.SelectedItem = (aCBox.ItemsSource as CompanyList).GetCompanyByIDOrName(Strings.StrConv(aCBox.SearchText, VbStrConv.Narrow, 0).ToLower().Trim()); ;
            }
        }

        public static DependencyProperty CompanyItemProperty = DependencyProperty.Register(
           "Company", typeof(CompanyName), typeof(AutoComboxCompany));


        public CompanyName Company
        {
            get
            {
                return (CompanyName)GetValue(CompanyItemProperty);
            }
            set
            {
                DbSetValue(CompanyItemProperty, value, "Company");
            }


        }

        public static DependencyProperty ComapnyNameItemProperty = DependencyProperty.Register(
           "CompanyName", typeof(string), typeof(AutoComboxCompany));


        public string CompanyName
        {
            get
            {
                return (string)GetValue(ComapnyNameItemProperty);
            }
            set
            {
                DbSetValue(ComapnyNameItemProperty, value, "Company");
            }


        }

        private void DbSetValue(DependencyProperty property, object value, string item)
        {
            SetValue(property, value);
            OnPropertyChanged(item);
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
