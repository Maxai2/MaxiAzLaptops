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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
//-------------------------------------------
namespace MaxiAzLaptops
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Laptop> Goods { get; set; }

        string CurrentDir;

        OpenFileDialog openFileDialog;
        SaveFileDialog saveFileDialog;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML file (*.xml)|*.xml";

            saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML file (*.xml)|*.xml";

            Goods = new ObservableCollection<Laptop>();

            //Goods = new ObservableCollection<Laptop>()
            //{
            //    new Laptop()
            //    {
            //        Name = "Lenovo IP 110-15IBR 15.6\"",
            //        OS = "DOS", 
            //        RAM = "4 Gb",
            //        HDD = "500 Gb",
            //        ScreenSize = "15.6\"",
            //        VebCam = "have",
            //        OldPrice = "559.9₼",
            //        NewPrice = "529.9₼",
            //        ImageName = "LenovoLaptopPic.png"
            //    },

            //    new Laptop()
            //    {
            //        Name = "Acer EX2519-C298 15.6\"",
            //        OS = "DOS",
            //        RAM = "4 Gb",
            //        HDD = "500 Gb",
            //        ScreenSize = "15.6\"",
            //        VebCam = "have",
            //        OldPrice = "559.9₼",
            //        NewPrice = "529.9₼",
            //        ImageName = "AcerLaptopPic.png"
            //    }
            //};
        }
        //-------------------------------------------
        private void MenuExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        //-------------------------------------------
        private void MenuAdd_Click(object sender, RoutedEventArgs e)
        {
            var win = new AddEditWin() { Owner = this };

            if (win.ShowDialog() == true)
            {
                this.Goods.Add(win.laptop); 
            }
        }
        //-------------------------------------------
        private void MenuEdit_Click(object sender, RoutedEventArgs e)
        {
            var item = sender as Laptop;

            int num = (sender as ListBox).SelectedIndex;

            var win = new AddEditWin(item) { Owner = this };

            if (win.ShowDialog() == true)
            {
                var temp = win.laptop;

                Goods[num].Name = temp.Name;
                Goods[num].OS = temp.OS;
                Goods[num].RAM = temp.RAM;
                Goods[num].HDD = temp.HDD;
                Goods[num].ScreenSize = temp.ScreenSize;
                Goods[num].NewPrice = temp.NewPrice;
                Goods[num].OldPrice = temp.OldPrice;
                Goods[num].VebCam = temp.VebCam;
                Goods[num].ImageName = temp.ImageName;
            }
        }
        //-------------------------------------------
        private void MenuDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are u sure?", "", MessageBoxButton.YesNo);

            switch (result)
            {
                case MessageBoxResult.Yes:

                    break;
                case MessageBoxResult.None:
                case MessageBoxResult.No:
                    break;
            }
        }
        //-------------------------------------------
        void Save(bool save)
        {
            if (save)
            {
                if (saveFileDialog.ShowDialog() == true)
                {
                    XDocument xdocWrite = new XDocument(new XElement("Tasks")); //new XDeclaration("1.0", "utf-8", "yes")

                    foreach (var item in Goods)
                    {
                        xdocWrite.Root.Add(new XElement("task", new XElement("name", item.Name),     // new XAttribute("Id", item.Id)
                                                            new XElement("OS", item.OS),
                                                            new XElement("RAM", item.RAM),
                                                            new XElement("HDD", item.HDD)),
                                                            new XElement("Screen size", item.ScreenSize),
                                                            new XElement("Veb cam", item.VebCam),
                                                            new XElement("Old price", item.OldPrice),
                                                            new XElement("New price", item.NewPrice),
                                                            new XElement("ImageName", item.ImageName));
                    }

                    xdocWrite.Save(saveFileDialog.FileName);
                }

            }


        }
        //-------------------------------------------
        private void MenuSave_Click(object sender, RoutedEventArgs e)
        {

        }
        //-------------------------------------------
        private void MenuSaveAs_Click(object sender, RoutedEventArgs e)
        {

        }
        //-------------------------------------------
    }
}
//-------------------------------------------