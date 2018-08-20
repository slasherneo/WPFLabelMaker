using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFLabelMaker.Class
{
    public class ResultItem : INotifyBassClass
    {
        private string company;
        public string Company
        {
            get { return company; }
            set
            {
                SetProperty(ref company, value);
            }
        }

        public string CompanyId { get; set; }

        private string itemid;
        public string ItemID
        {
            get { return itemid; }
            set
            {
                SetProperty(ref itemid, value);
            }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                SetProperty(ref name, value);
            }
        }

        public String GetFirstName()
        {
            string[] splits = name.Split(' ');
            if (splits != null && splits.Length > 2)
                return splits[0];
            else
                return null;
        }


        public String GetSecondName()
        {
            string[] splits = name.Split(' ');
            if (splits != null && splits.Length > 2)
                return splits[1];
            else
                return null;
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
