﻿using Microsoft.Win32;
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

        public ObservableCollection<Laptop> SearchContainer;

        public ObservableCollection<string> listForSearch { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;

            openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML file (*.xml)|*.xml";

            saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML file (*.xml)|*.xml";

            Goods = new ObservableCollection<Laptop>();

            SearchContainer = new ObservableCollection<Laptop>();

            listForSearch = new ObservableCollection<string>()
            {
                "Name",
                "OS",
                "RAM",
                "HDD",
                "Screen size",
                "Veb cam",
                "Old price",
                "New price"
            };

        }
        //-------------------------------------------
        private void MenuExit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are u sure?", "", MessageBoxButton.YesNo);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    this.Close();
                    break;
                case MessageBoxResult.No:
                    break;
            }
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
            int num = lbItems.SelectedIndex;

            var item = Goods[num];

            var win = new AddEditWin(item) { Owner = this };

            if (win.ShowDialog() == true)
            {
                Goods[num] = win.laptop;
            }
        }
        //-------------------------------------------
        private void MenuDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are u sure?", "", MessageBoxButton.YesNo);

            switch (result)
            {
                case MessageBoxResult.Yes:
                    Goods.RemoveAt(lbItems.SelectedIndex);
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }
        //-------------------------------------------
        void Save(bool saveAs)
        {
            XDocument xdocWrite = xdocWrite = new XDocument(new XElement("Laptops")); //new XDeclaration("1.0", "utf-8", "yes")

            if (saveAs)
            {

                if (saveFileDialog.ShowDialog() == true)
                {

                    foreach (var item in Goods)
                    {
                        xdocWrite.Root.Add(new XElement("laptop", new XElement("Name", item.Name),     // new XAttribute("Id", item.Id)
                                                                new XElement("OS", item.OS),
                                                                new XElement("RAM", item.RAM),
                                                                new XElement("HDD", item.HDD),
                                                                new XElement("ScreenSize", item.ScreenSize),
                                                                new XElement("VebCam", item.VebCam),
                                                                new XElement("OldPrice", item.OldPrice),
                                                                new XElement("NewPrice", item.NewPrice),
                                                                new XElement("ImageName", item.ImageName)
                                                                ));
                    }

                    CurrentDir = saveFileDialog.FileName;

                    xdocWrite.Save(saveFileDialog.FileName);
                }
            }
            else
            {
                foreach (var item in Goods)
                {
                    xdocWrite.Root.Add(new XElement("laptop", new XElement("Name", item.Name),     // new XAttribute("Id", item.Id)
                                                            new XElement("OS", item.OS),
                                                            new XElement("RAM", item.RAM),
                                                            new XElement("HDD", item.HDD),
                                                            new XElement("ScreenSize", item.ScreenSize),
                                                            new XElement("VebCam", item.VebCam),
                                                            new XElement("OldPrice", item.OldPrice),
                                                            new XElement("NewPrice", item.NewPrice),
                                                            new XElement("ImageName", item.ImageName)
                                                            ));
                }

                xdocWrite.Save(CurrentDir);
            }
        }
        //-------------------------------------------
        private void MenuSave_Click(object sender, RoutedEventArgs e)
        {
            if (Goods.Count != 0)
            {
                if (CurrentDir != "")
                    Save(true);
                else
                    Save(false);
            }
            else
                MessageBox.Show("No Laptop to save!");
        }
        //-------------------------------------------
        private void MenuSaveAs_Click(object sender, RoutedEventArgs e)
        {
            if (Goods.Count != 0)
                Save(true);
            else
                MessageBox.Show("No Laptop to save!");
        }
        //-------------------------------------------
        private void MenuOpen_Click(object sender, RoutedEventArgs e)
        {
            if (Goods.Count != 0)
            {
                if (CurrentDir != "")
                    Save(false);
                else
                    Save(true);

                Goods.Clear();
            }

            if (openFileDialog.ShowDialog() == true)
            {
                CurrentDir = openFileDialog.FileName;

                XDocument xdocRead = XDocument.Load(CurrentDir);

                foreach (XElement item in xdocRead.Root.Elements())
                {
                    Laptop laptop = new Laptop()
                    {
                        Name = item.Element("Name").Value,                 /* XElement("Name", item.Name*/
                        OS = item.Element("OS").Value,                     /*Element("OS", item.OS),*/
                        RAM = item.Element("RAM").Value,                   /*Element("RAM", item.RAM),*/
                        HDD = item.Element("HDD").Value,                   /*Element("HDD", item.HDD)),*/
                        ScreenSize = item.Element("ScreenSize").Value,     /*Element("ScreenSize", item.*/
                        VebCam = item.Element("VebCam").Value,             /*Element("VebCam", item.VebC*/
                        OldPrice = item.Element("OldPrice").Value,         /*Element("OldPrice", item.Ol*/
                        NewPrice = item.Element("NewPrice").Value,         /*Element("NewPrice", item.Ne*/
                        ImageName = item.Element("ImageName").Value        /*Element("ImageName", item.I*/
                    };

                    Goods.Add(laptop);
                }
            }
        }
        //-------------------------------------------
        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            if (Goods.Count == 0)
            {
                tbSearch.IsChecked = false;
                return;
            }

            if (tbSearch.IsChecked == true)
            {
                SearchContainer.Clear();

                int length = Goods.Count;

                foreach (var item in Goods)
                {
                    switch (cbFindParam.SelectedItem)
                    {
                        case "Name":
                            if (item.Name.Contains(tbSearchText.Text))
                                SearchContainer.Add(item);
                            break;
                        case "OS":
                            if (item.OS.Contains(tbSearchText.Text))
                                SearchContainer.Add(item);
                            break; 
                        case "RAM":
                            if (item.RAM.Contains(tbSearchText.Text))
                                SearchContainer.Add(item);
                            break;
                        case "HDD":
                            if (item.HDD.Contains(tbSearchText.Text))
                                SearchContainer.Add(item);
                            break;
                        case "Screen size":
                            if (item.ScreenSize.Contains(tbSearchText.Text))
                                SearchContainer.Add(item);
                            break;
                        case "Old price":
                            if (item.OldPrice.Contains(tbSearchText.Text))
                                SearchContainer.Add(item);
                            break;
                        case "New price":
                            if (item.NewPrice.Contains(tbSearchText.Text))
                                SearchContainer.Add(item);
                            break;
                        case "Veb cam":
                            if (item.VebCam.Contains(tbSearchText.Text))
                                SearchContainer.Add(item);
                            break;
                    }
                }

                lbItems.ItemsSource = SearchContainer;
            }
            else
            {
                lbItems.ItemsSource = Goods;
            }

            //lbItems.Items.Refresh();
        }
        //-------------------------------------------
    }
}
//-------------------------------------------