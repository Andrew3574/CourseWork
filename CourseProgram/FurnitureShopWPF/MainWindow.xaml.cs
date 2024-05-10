using FurnitureDBLibrary;
using FurnitureDBLibrary.DataAccess;
using FurnitureDBLibrary.Models;
using FurnitureDBLibrary.Models.CurrentFurnitures;
using FurnitureDBLibrary.Models.Furnitures;
using FurnitureDBLibrary.Models.FurnitureTypes;
using FurnitureDBLibrary.UserModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Windows;
using System.Windows.Controls;


namespace FurnitureShopWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly User _user;
        Furniture _currentFurniture;
        FurnitureSet _currentFurnitureSet;
        List<Furniture> _furnitures;
         List<Furniture> _furnitureList = new List<Furniture>();
        List<Manufacturer> _manufacturers;
        List<FurnitureType> _furnitureTypes;
        List<FurnitureSet> _furnitureSets;
        List<SetItems> _furnitureSetItems;
        SetItems _currentFurnitureSetItems;
        List<Sale> _sales;      
        bool isReLogined = false;

        readonly SaleController _saleController = new SaleController();
        readonly FurnitureSetItemController _furnitureSetItemController = new FurnitureSetItemController();
        readonly ManufacturerController _manufacturerController = new ManufacturerController();
        readonly FurnitureTypeController _furnitureTypeController = new FurnitureTypeController();
        readonly FurnitureController _furnitureController = new FurnitureController();


        public MainWindow(User user)
        {
            try
            {
                _user = user;
                InitializeComponent();
                switch (_user.RoleName.ToLower())
                {
                    case "admin":
                        InitializeAdminInterface();
                        break;

                    case "salesman":
                        InitializeSalesmanInterface();
                        break;

                    case "manager":
                        InitializeManagerInterface();
                        break;

                    default:
                        throw new Exception();
                }
                FurnitureSetListBox.Visibility = Visibility.Hidden;
                FurnitureSetStackPanel.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            FurnitureSetListBox.Visibility = Visibility.Hidden;
            FurnitureSetStackPanel.Visibility = Visibility.Hidden;
        }

        public void FurnitureListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BuyFurnitureButton.IsEnabled == false)
                BuyFurnitureButton.IsEnabled = true;

            if (FurnitureListBox.SelectedItem != null)
            {
                _currentFurniture = (Furniture)FurnitureListBox.Items[FurnitureListBox.SelectedIndex];
               
                NameTextBox.Text = _currentFurniture.FurnitureName;
                PriceTextBox.Text = _currentFurniture.GetRetailPrice().ToString("#.00");
                ManufacturerTextBox.Text = _currentFurniture.ManufacturerName;
                TypeTextBox.Text = _currentFurniture.TypeName;
                QuantityTextBox.Text = _currentFurniture.FurnitureQuantity.ToString();

            }
        }

        private void BuyButton_Click(object sender, RoutedEventArgs e)
        {
            _furnitureList.Add(_currentFurniture);

            --_currentFurniture.FurnitureQuantity;
        }

        private void LoginWindow_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            isReLogined = true;
            this.Close();
            loginWindow.Show();

        }

        private void FurnitureButton_Click(object sender, RoutedEventArgs e)
        {
            FurnitureListBox.Visibility = Visibility.Visible;
            FurnitureStackPanel.Visibility = Visibility.Visible;
            FurnitureSetListBox.Visibility = Visibility.Hidden;
            FurnitureSetStackPanel.Visibility = Visibility.Hidden;

            _furnitures = _furnitureController.Read();

            FurnitureButton.IsEnabled = false;
            FurnitureSetButton.IsEnabled = true;

            FurnitureListBox.ItemsSource = _furnitures;

        }

        private void FurnitureSetButton_Click(object sender, RoutedEventArgs e)
        {
            FurnitureSetController furnitureSetController = new FurnitureSetController();
            FurnitureSetListBox.Visibility = Visibility.Visible;
            FurnitureSetStackPanel.Visibility = Visibility.Visible;
            FurnitureListBox.Visibility = Visibility.Hidden;
            FurnitureStackPanel.Visibility = Visibility.Hidden;
            BuyFurnitureButton.IsEnabled = false;

            _furnitureSets = furnitureSetController.Read();
            _furnitureSetItems = _furnitureSetItemController.Read();
            _furnitures = _furnitureController.Read();

            FurnitureButton.IsEnabled = true;
            FurnitureSetButton.IsEnabled = false;

            FurnitureSetListBox.ItemsSource = _furnitureSets;

        }

        private void FurnitureSetListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BuyFurnitureSetButton.IsEnabled == false)
                BuyFurnitureSetButton.IsEnabled = true;

            if (FurnitureSetListBox.SelectedItem != null)
            {
                try
                {
                    _furnitureSetItems = _furnitureSetItemController.Read();
                    decimal totalCost = 0;
                    _currentFurnitureSet = (FurnitureSet)FurnitureSetListBox.Items[FurnitureSetListBox.SelectedIndex];
                    _currentFurnitureSetItems = _furnitureSetItems.Find(items => items.SetName == _currentFurnitureSet.FurnitureSetName);
                    if (_currentFurnitureSetItems.FurnitureList != null)
                    {
                        foreach (var item in _currentFurnitureSetItems.FurnitureList)
                            totalCost += item.GetRetailPrice();

                        SetItemNameListBox.ItemsSource = _currentFurnitureSetItems.FurnitureList;
                        TotalCostTextBlock.Text = "Общая стоимость: "+ totalCost.ToString("#.00");
                    }
                    else
                        throw new Exception();
                    
                }
                catch (Exception)
                {

                    MessageBox.Show("Данный гарнитур неукомплектован");
                }
                
            }
        }

        private void BuySetButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (Furniture furnitureSetItem in _currentFurnitureSetItems.FurnitureList)
            {
                _furnitureList.Add(furnitureSetItem);
            }
            
        }

        private void InitializeAdminInterface()
        {
            MainTabItem.Visibility = Visibility.Visible;
            ReportsTabItem.Visibility = Visibility.Visible;
            SalesTabItem.Visibility = Visibility.Visible;
        }

        private void InitializeManagerInterface()
        {
            MainTabItem.Visibility = Visibility.Visible;
            ReportsTabItem.Visibility = Visibility.Visible;
        }

        private void InitializeSalesmanInterface()
        {
            MainTabItem.Visibility = Visibility.Visible;
            SalesTabItem.Visibility = Visibility.Visible;
        }

        private void AddFurnitureButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {                
                Furniture furniture;
                Manufacturer curManufacturer = _manufacturers.Find(m => m.ManufacturerName == ManufacturerComboBox.Items[ManufacturerComboBox.SelectedIndex].ToString());
                FurnitureType curFurnitureType = _furnitureTypes.Find(t => t.TypeName == FurnitureTypesComboBox.Items[FurnitureTypesComboBox.SelectedIndex].ToString());
                
                switch (FurnitureVarietyComboBox.Items[FurnitureVarietyComboBox.SelectedIndex].ToString().ToLower())
                {
                    case "стул":
                        switch (FurnitureTypesComboBox.Items[FurnitureTypesComboBox.SelectedIndex].ToString().ToLower())
                        {
                            case "кухонная":
                                furniture = new KitchenChair(FurnitureNameTextBox.Text, Convert.ToDecimal(FurniturePriceTextBox.Text), Convert.ToInt32(FurnitureQuantityTextBox.Text));
                                _furnitureController.Create(furniture);
                                break;
                            case "офисная":
                                furniture = new OfficeChair(FurnitureNameTextBox.Text, Convert.ToDecimal(FurniturePriceTextBox.Text), Convert.ToInt32(FurnitureQuantityTextBox.Text));
                                _furnitureController.Create(furniture);
                                break;
                        }
                        break;

                    case "стол":
                        switch (FurnitureTypesComboBox.Items[FurnitureTypesComboBox.SelectedIndex].ToString().ToLower())
                        {
                            case "кухонная":
                                furniture = new KitchenTable(FurnitureNameTextBox.Text, Convert.ToDecimal(FurniturePriceTextBox.Text), Convert.ToInt32(FurnitureQuantityTextBox.Text));
                                _furnitureController.Create(furniture);
                                break;
                            case "офисная":
                                furniture = new OfficeTable(FurnitureNameTextBox.Text, Convert.ToDecimal(FurniturePriceTextBox.Text), Convert.ToInt32(FurnitureQuantityTextBox.Text));
                                _furnitureController.Create(furniture);
                                break;
                        }
                        break;

                    case "шкаф":
                        switch (FurnitureTypesComboBox.Items[FurnitureTypesComboBox.SelectedIndex].ToString().ToLower())
                        {
                            case "спальная":
                                furniture = new BedroomCloset(FurnitureNameTextBox.Text, Convert.ToDecimal(FurniturePriceTextBox.Text), Convert.ToInt32(FurnitureQuantityTextBox.Text));
                                _furnitureController.Create(furniture);
                                break;
                            case "офисная":
                                furniture = new OfficeCloset(FurnitureNameTextBox.Text, Convert.ToDecimal(FurniturePriceTextBox.Text), Convert.ToInt32(FurnitureQuantityTextBox.Text));
                                _furnitureController.Create(furniture);
                                break;
                        }
                        break;

                    case "диван":
                        switch (FurnitureTypesComboBox.Items[FurnitureTypesComboBox.SelectedIndex].ToString().ToLower())
                        {
                            case "гостинная":
                                furniture = new LoungeSofa(FurnitureNameTextBox.Text, Convert.ToDecimal(FurniturePriceTextBox.Text), Convert.ToInt32(FurnitureQuantityTextBox.Text));
                                _furnitureController.Create(furniture);
                                break;
                        }
                        break;

                    default:
                        throw new Exception("Магазин не продает данный тип мебели");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка ввода параметров товара");
            }
        }

        private void UpdateFurnitureButton_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                _currentFurniture = (Furniture)FurnitureListBox.Items[FurnitureListBox.SelectedIndex];

                _currentFurniture.FurnitureQuantity = Convert.ToInt32(FurnitureQuantityTextBox.Text);
                _furnitureController.Update(_currentFurniture);
            }
            catch (Exception)
            {
                
                MessageBox.Show("Ошибка указания товара!");
            }
        }


        private void DeleteFurnitureButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _currentFurniture = (Furniture)FurnitureListBox.Items[FurnitureListBox.SelectedIndex];

                _furnitureController.Delete(_currentFurniture);
            }
            catch (Exception)
            {

                MessageBox.Show("Ошибка указания товара!");
            }
        }


        private void RefreshFurnitureLBButton_Click(object sender, RoutedEventArgs e)
        {
            _furnitures = _furnitureController.Read();

            FurnitureListBox.ItemsSource = _furnitures;
        }

        private void CreateReportButton_Click(object sender, RoutedEventArgs e)
        {
            _sales = _saleController.Read();

            ReportListBox.ItemsSource = _sales;
        }


        private void ReportByManufacturer_Click(object sender, RoutedEventArgs e)
        {
            if (ManufacturerName.SelectedIndex != -1)
            {
                _sales = _saleController.Read();
                string manufacturerName = ManufacturerName.Items[ManufacturerName.SelectedIndex].ToString();
                var salesByManufacturer = _sales.FindAll(sale => sale.ManufacturerName == manufacturerName);

                ReportListBox.ItemsSource = salesByManufacturer;
            }
            else
                MessageBox.Show("Необходимо выбрать производителя");

        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Contains(SalesTabItem))
            {
                _furnitures = _furnitureController.Read();
                _manufacturers = _manufacturerController.Read();
                _furnitureTypes = _furnitureTypeController.Read();
                ManufacturerComboBox.ItemsSource = _manufacturers;
                FurnitureTypesComboBox.ItemsSource = _furnitureTypes;
                FurnitureVarietyComboBox.ItemsSource = _furnitureController.GetVarieties(_furnitures).Distinct();

                if (FurnitureListBox.SelectedIndex != -1)
                {
                    _currentFurniture = (Furniture)FurnitureListBox.Items[FurnitureListBox.SelectedIndex];
                    FurnitureNameTextBox.Text = _currentFurniture.FurnitureName;
                    FurniturePriceTextBox.Text = _currentFurniture.FurniturePrice.ToString();
                    FurnitureQuantityTextBox.Text = _currentFurniture.FurnitureQuantity.ToString();
                }
            }

            if (e.AddedItems.Contains(ReportsTabItem))
            {
                ManufacturerName.ItemsSource = _manufacturerController.Read();
            }
        }

        private void XMLReportButton_Click(object sender, RoutedEventArgs e)
        {
            _sales = _saleController.Read();
            _saleController.GenerateXMLReport(_sales);

            MessageBox.Show("Отчет успешно сформирован в XML формате");

        }

        private void CartButton_Click(object sender, RoutedEventArgs e)
        {
            CartWindow cartWindow = new CartWindow(_furnitureList);
            cartWindow.Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!isReLogined)
                DBConnection.GetInstance.CloseConnection();
        }       
    }
}
