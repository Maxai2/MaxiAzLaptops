using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace MaxiAzLaptops
{
    /// <summary>
    /// Interaction logic for AddEditWin.xaml
    /// </summary>
    public partial class AddEditWin : Window
    {
        public Laptop laptop { get; set; }

        public AddEditWin(Laptop laptop = null)
        {
            InitializeComponent();

            this.laptop = laptop;

            if (laptop != null)
            {
                tbName.Text = laptop.Name;
                tbOS.Text = laptop.OS;
                tbRAM.Text = laptop.RAM;
                tbHDD.Text = laptop.HDD;
                tbScrSize.Text = laptop.ScreenSize;
                tbOldP.Text = laptop.OldPrice;
                tbNewP.Text = laptop.NewPrice;
                tbVebCam.Text = laptop.VebCam;
                tbImagePath.Text = laptop.ImageName;
            }
        }

        private void ButtonOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JPG file|*.jpg|PNG file|*.png";

            if (openFileDialog.ShowDialog() == true)
            {
                tbImagePath.Text = openFileDialog.FileName;
            }
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            if (tbName.Text == "" && tbImagePath.Text == "")
            {
                DialogResult = false;
                Close();
            }
            else
            {
                if (laptop == null)
                {
                    laptop = new Laptop
                    {
                        Name = tbName.Text,
                        OS = tbOS.Text,
                        RAM = tbRAM.Text,
                        HDD = tbHDD.Text,
                        ScreenSize = tbScrSize.Text,
                        VebCam = tbVebCam.Text,
                        OldPrice = tbOldP.Text,
                        NewPrice = tbNewP.Text,
                        ImageName = tbImagePath.Text
                    };
                }
                else
                {
                    laptop.Name = tbName.Text;
                    laptop.OS = tbOS.Text;
                    laptop.RAM = tbRAM.Text;
                    laptop.HDD = tbHDD.Text;
                    laptop.ScreenSize = tbScrSize.Text;
                    laptop.OldPrice = tbOldP.Text;
                    laptop.NewPrice = tbNewP.Text;
                    laptop.VebCam = tbVebCam.Text;
                    laptop.ImageName = tbImagePath.Text;
                }

                DialogResult = true;

                Close();
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //private ICommand applyCommand;
        //public ICommand ApplyCommand
        //{
        //    get
        //    {
        //        if (applyCommand is null)
        //        {
        //            applyCommand = new RelayCommand(
        //                (param) =>
        //                {

        //                }
        //        }

        //        return applyCommand;
        //    }

        //}

    }
}
