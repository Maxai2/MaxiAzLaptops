using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
//-------------------------------------------
namespace MaxiAzLaptops
{
    public class Laptop : INotifyPropertyChanged
    {
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnChanged();
            }
        }

        private string os;
        public string OS
        {
            get { return os; }
            set
            {
                os = value;
                OnChanged();
            }
        }

        private string ram;
        public string RAM
        {
            get { return ram; }
            set
            {
                ram = value;
                OnChanged();
            }
        }

        private string hdd;
        public string HDD
        {
            get { return hdd; }
            set
            {
                hdd = value;
                OnChanged();
            }
        }

        private string screensize;
        public string ScreenSize
        {
            get { return screensize; }
            set
            {
                screensize = value;
                OnChanged();
            }
        }

        private string vebcam;
        public string VebCam
        {
            get { return vebcam; }
            set
            {
                vebcam = value;
                OnChanged();
            }
        }

        private string oldprice;
        public string OldPrice
        {
            get { return oldprice; }
            set
            {
                oldprice = value;
                OnChanged();
            }
        }

        private string newprice;
        public string NewPrice
        {
            get { return newprice; }
            set
            {
                newprice = value;
                OnChanged();
            }
        }

        private string imagename;
        public string ImageName
        {
            get { return imagename; }
            set
            {
                imagename = value;
                OnChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnChanged([CallerMemberName]string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
//-------------------------------------------