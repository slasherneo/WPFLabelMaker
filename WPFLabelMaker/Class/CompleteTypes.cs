using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFLabelMaker.Class
{
    public class CompleteTypes : INotifyBassClass
    {
        public SelectedType Type1 { get; set; }

        public SelectedType Type2 { get; set; }

        public SelectedType Type3 { get; set; }

        public SelectedType Type4 { get; set; }

        public SelectedType Type5 { get; set; }

        public SelectedType Type6 { get; set; }

        public SelectedType Type7 { get; set; }

        public int Sum
        {
            get { return GetSum(); }
        }

        public int GetSum()
        {
            int sum = 0;
            if (Type1.IsChecked)
                sum += int.Parse(Type1.Number);
            if (Type2.IsChecked)
                sum += int.Parse(Type2.Number);
            if (Type3.IsChecked)
                sum += int.Parse(Type3.Number);
            if (Type4.IsChecked)
                sum += int.Parse(Type4.Number);
            if (Type5.IsChecked)
                sum += int.Parse(Type5.Number);
            if (Type6.IsChecked)
                sum += int.Parse(Type6.Number);
            if (Type7.IsChecked && Type7.Name != string.Empty)
                sum += int.Parse(Type7.Number);
            if (addOne)
                sum++;
            return sum;
        }

        public int GetTypesNumber()
        {
            int sum = 0;
            if (Type1.IsChecked && int.Parse(Type1.Number) > 0)
                sum++;
            if (Type2.IsChecked && int.Parse(Type2.Number) > 0)
                sum++;
            if (Type3.IsChecked && int.Parse(Type3.Number) > 0)
                sum++;
            if (Type4.IsChecked && int.Parse(Type4.Number) > 0)
                sum++;
            if (Type5.IsChecked && int.Parse(Type5.Number) > 0)
                sum++;
            if (Type6.IsChecked && int.Parse(Type6.Number) > 0)
                sum++;
            if (Type7.IsChecked && Type7.Name != string.Empty && int.Parse(Type7.Number) > 0)
                sum++;
            if (addOne)
                sum++;

            return sum;
        }

        private bool addOne = true;
        public bool Addone
        {
            get { return addOne; }
            set
            {
                SetProperty(ref addOne, value);
            }
        }

    }
}
