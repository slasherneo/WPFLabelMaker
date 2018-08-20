using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFLabelMaker.Class
{
    class NTItem : INotifyBassClass
    {
        private int sn;
        public int Sn
        {
            get { return sn; }
            set
            {
                SetProperty(ref sn, value);
            }
        }

        private string matel;
        public string Matel
        {
            get { return matel; }
            set
            {
                SetProperty(ref matel, value);
            }
        }

        private string startime;
        public string StartTime
        {
            get { return startime; }
            set
            {
                SetProperty(ref startime, value);
            }
        }

        private double tile;
        public double Tile
        {
            get { return tile; }
            set
            {
                SetProperty(ref tile, value);
            }
        }

        private double sizeshort;
        public double SizeShort
        {
            get { return sizeshort; }
            set
            {
                SetProperty(ref sizeshort, value);
            }
        }

        private double sizelong;
        public double SizeLong
        {
            get { return sizelong; }
            set
            {
                SetProperty(ref sizelong, value);
            }
        }

        private int number;
        public int Number
        {
            get { return number; }
            set
            {
                SetProperty(ref number, value);
            }
        }
    }
}
