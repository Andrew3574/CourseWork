using FurnitureDBLibrary;
using FurnitureDBLibrary.DataAccess;
using FurnitureDBLibrary.Models;
using FurnitureDBLibrary.Models.CurrentFurnitures;
using FurnitureDBLibrary.UserModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace FurnitureShopWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly User _user;
        private Furniture _currentFurniture;
        private FurnitureSetItem _currentFurnitureSetItem;
        private Sale _currentSale;
        private List<Furniture> _furnitures;
        private List<Manufacturer> _manufacturers;
        private List<FurnitureType> _furnitureTypes;
        private List<FurnitureSetItem> _furnitureSetItems;
        private List<Sale> _sales;
        private List<string> varietyList = new List<string>();        
        bool isReLogined = false;

        private readonly SaleController _saleController = new SaleController();
        private readonly FurnitureSetItemController _furnitureSetItemController = new FurnitureSetItemController();
        private readonly ManufacturerController _manufacturerController = new ManufacturerController();
        private readonly FurnitureTypeController _furnitureTypeController = new FurnitureTypeController();
        private readonly FurnitureController _furnitureController = new FurnitureController();


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

            if (FurnitureListBox.SelectedItem != null /*&& FurnitureListBox.SelectedItem is Furniture == true*/)
            {
                _currentFurniture = (Furniture)FurnitureListBox.Items[FurnitureListBox.SelectedIndex];
               
                NameTextBox.Text = _currentFurniture.FurnitureName;
                PriceTextBox.Text = _currentFurniture.FurniturePrice.ToString();
                ManufacturerTextBox.Text = _currentFurniture.FurnitureManufacturer.ManufacturerName;
                TypeTextBox.Text = _currentFurniture.FurnitureType.TypeName;
                QuantityTextBox.Text = _currentFurniture.FurnitureQuantity.ToString();

            }
        }

        private void BuyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int quantity = Convert.ToInt32(QuantityTextBox.Text);
                _sales = _saleController.Read();

                if (quantity > 0)
                {
                    _currentFurniture.FurnitureQuantity--;
                    QuantityTextBox.Text = _currentFurniture.FurnitureQuantity.ToString();
                    _furnitureController.Update(_currentFurniture);

                    Sale curSale = _sales.Find(sale => _currentFurniture.FurnitureName == sale.FurnitureName && sale.SaleDate == DateTime.Today);

                    if (curSale == null)
                    {
                        _currentSale = new Sale(_currentFurniture.FurnitureName,_currentFurniture.FurniturePrice,_currentFurniture.FurnitureType,_currentFurniture.FurnitureManufacturer, 1, DateTime.Today);
                        _saleController.Create(_currentSale);
                    }
                    else
                    {
                        _currentSale = new Sale(_currentFurniture.FurnitureName, _currentFurniture.FurniturePrice, _currentFurniture.FurnitureType, _currentFurniture.FurnitureManufacturer, curSale.FurnitureSaledQuantity + 1, DateTime.Today);
                        _saleController.Update(_currentSale);
                    }

                }
                else
                    MessageBox.Show("На данный момент товара нет!");
            }
            catch (InvalidOperationException)
            {
                _currentSale = new Sale(_currentFurniture.FurnitureName, _currentFurniture.FurniturePrice, _currentFurniture.FurnitureType, _currentFurniture.FurnitureManufacturer, 1, DateTime.Today);
                _saleController.Create(_currentSale);
            }

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

            List<FurnitureSet> furnitureSets = furnitureSetController.Read();
            _furnitureSetItems = _furnitureSetItemController.Read();
            _furnitures = _furnitureController.Read();

            FurnitureButton.IsEnabled = true;
            FurnitureSetButton.IsEnabled = false;

            FurnitureSetListBox.ItemsSource = furnitureSets;


        }

        private void FurnitureSetListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BuyFurnitureSetButton.IsEnabled == false)
                BuyFurnitureSetButton.IsEnabled = true;

            if (FurnitureSetListBox.SelectedItem != null /*&& FurnitureSetListBox.SelectedItem is FurnitureSet == true*/)
            {
                _currentFurnitureSetItem = (FurnitureSetItem)FurnitureSetListBox.Items[FurnitureSetListBox.SelectedIndex];

                SetItemNameTextBlock.Text = _currentFurnitureSetItem.FurnitureSetName;

            }
        }

        private void BuySetButton_Click(object sender, RoutedEventArgs e)
        {
            /*try
            {
                _sales = _saleController.Read();
                _currentFurnitureSet = (FurnitureSet)FurnitureSetListBox.Items[FurnitureSetListBox.SelectedIndex];
                var currentSetItems = _furnitureSetItems.Where(item => item.FurnitureSetId == _currentFurnitureSet.FurnitureSetId).ToList();
                foreach (FurnitureSetItem furnitureSetItem in currentSetItems)
                {
                    _currentFurniture = _furnitures.Where(furniture => furniture.FurnitureId == furnitureSetItem.FurnitureId).First();
                    if (_currentFurniture.FurnitureQuantity > 0)
                    {
                        _currentFurniture.FurnitureQuantity--;
                        _furnitureController.Update(_currentFurniture);

                        Sale curSale = _sales.Where(sale => _currentFurniture.FurnitureId == sale.FurnitureId && sale.SaleDate == DateTime.Today).First();

                        _currentSale = new Sale(curSale.SaleId, _currentFurniture.FurnitureId, curSale.FurnitureSaledQuantity + 1, DateTime.Today);
                        _saleController.Update(_currentSale);
                    }
                    else
                        throw new ArgumentException();

                }
            }
            catch (InvalidOperationException)
            {
                _currentSale = new Sale(_sales.Count + 1, _currentFurniture.FurnitureId, 1, DateTime.Today);
                _saleController.Create(_currentSale);
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Покупка на данный момент невозможна!");
            }*/
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
                //-----------

                Furniture furniture;
                Manufacturer curManufacturer = _manufacturers.Find(m => m.ManufacturerName == ManufacturerComboBox.Items[ManufacturerComboBox.SelectedIndex].ToString());
                FurnitureType curFurnitureType = _furnitureTypes.Find(t => t.TypeName == FurnitureTypesComboBox.Items[FurnitureTypesComboBox.SelectedIndex].ToString());
                
                switch (FurnitureVarietyComboBox.Items[FurnitureVarietyComboBox.SelectedIndex].ToString().ToLower())
                {
                    case "стул":
                        furniture = new Chair(FurnitureNameTextBox.Text,Convert.ToDecimal(FurniturePriceTextBox.Text),Convert.ToInt32(FurnitureQuantityTextBox.Text), curFurnitureType, curManufacturer);
                        break;

                    case "стол":
                        furniture = new Table(FurnitureNameTextBox.Text, Convert.ToDecimal(FurniturePriceTextBox.Text), Convert.ToInt32(FurnitureQuantityTextBox.Text), curFurnitureType, curManufacturer);
                        break;

                    case "диван":
                        furniture = new Sofa(FurnitureNameTextBox.Text, Convert.ToDecimal(FurniturePriceTextBox.Text), Convert.ToInt32(FurnitureQuantityTextBox.Text), curFurnitureType, curManufacturer);
                        break;

                    case "шкаф":
                        furniture = new Closet(FurnitureNameTextBox.Text, Convert.ToDecimal(FurniturePriceTextBox.Text), Convert.ToInt32(FurnitureQuantityTextBox.Text), curFurnitureType, curManufacturer);
                        break;

                    default:
                        throw new Exception();
                }
                _furnitureController.Create(furniture);
            }
            catch (Exception)
            {
                MessageBox.Show("Ошибка ввода параметров товара");
            }
        }

        private void UpdateFurnitureButton_Click(object sender, RoutedEventArgs e)
        {
            Furniture furniture;

            try
            {
                _currentFurniture = (Furniture)FurnitureListBox.Items[FurnitureListBox.SelectedIndex];
                /*
                                if (FurnitureTypesComboBox.SelectedIndex == -1 || FurnitureTypesComboBox.SelectedItem == null)
                                    FurnitureTypesComboBox.SelectedIndex = _currentFurniture.FurnitureTypeId - 1;

                                if (ManufacturerComboBox.SelectedIndex == -1 || ManufacturerComboBox.SelectedItem == null)
                                    ManufacturerComboBox.SelectedIndex = _currentFurniture.FurnitureManufacturerId - 1;*/

                _currentFurniture.FurnitureQuantity = Convert.ToInt32(FurnitureQuantityTextBox.Text);
                _furnitureController.Update(_currentFurniture);
            }
            catch (Exception ex)
            {
                //"Ошибка указания товара!"
                MessageBox.Show(ex.Message);
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
                var salesByManufacturer = _sales.FindAll(sale => sale.FurnitureManufacturer.ManufacturerName == manufacturerName);

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
                foreach(Furniture furn in _furnitures)
                    varietyList.Add(furn.FurnitureVariety);

                FurnitureVarietyComboBox.ItemsSource = varietyList;
                _manufacturers = _manufacturerController.Read();
                _furnitureTypes = _furnitureTypeController.Read();
                ManufacturerComboBox.ItemsSource = _manufacturers;
                FurnitureTypesComboBox.ItemsSource = _furnitureTypes;

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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!isReLogined)
                DBConnection.GetInstance.CloseConnection();
        }

        private void BuyFurnitureSetButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
