using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFLabelMaker.Class
{
    public class SelectedType : INotifyBassClass
    {
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                SetProperty(ref name,value);
            }
        }

        private bool isChecked;
        public bool IsChecked
        {
            get { return isChecked; }
            set
            {
                SetProperty(ref isChecked, value);
            }
        }

        private string number { get; set; }
        public string Number
        {
            get { return number; }
            set
            {
                int tempint = 0;
                if (value == string.Empty || !int.TryParse(value, out tempint))
                {
                    number = "0";
                }
                else
                {
                    number = value;
                }
                OnPropertyChanged("Number");
            }
        }
    }
}
