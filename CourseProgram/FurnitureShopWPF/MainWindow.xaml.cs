/*using FurnitureDBLibrary;
using FurnitureDBLibrary.DataAccess;
using FurnitureDBLibrary.Models;
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
        private FurnitureSet _currentFurnitureSet;
        private Sale _currentSale;
        private List<Furniture> _furnitures;
        private List<Manufacturer> _manufacturers;
        private List<FurnitureType> _furnitureTypes;
        private List<FurnitureSet> _furnitureSets;
        private List<FurnitureSetItem> _furnitureSetItems;
        private List<Sale> _sales;
        bool isReLogined=false;

        private readonly SaleController _saleController = new SaleController();
        private readonly FurnitureSetItemController _furnitureSetItemController = new FurnitureSetItemController();
        private readonly FurnitureSetController _furnitureSetController = new FurnitureSetController();
        private readonly ManufacturerController _manufacturerController = new ManufacturerController();
        private readonly FurnitureTypeController _furnitureTypeController = new FurnitureTypeController();
        private readonly FurnitureController _furnitureController = new FurnitureController();

        
        public MainWindow(User user)
        {
            try
            {
                _user = user;
                InitializeComponent();
                switch (_user.RoleId)
                {
                    case 1:
                        InitializeAdminInterface();
                        break;

                    case 2:
                        InitializeSalesmanInterface();                        
                        break;

                    case 3:
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

            if (FurnitureListBox.SelectedItem != null && FurnitureListBox.SelectedItem is Furniture == true)
            {
                _currentFurniture = (Furniture)FurnitureListBox.Items[FurnitureListBox.SelectedIndex];
                var curFurniture = _furnitureController.GetCurrentFurnitureInfo(_currentFurniture, _furnitures, _manufacturers, _furnitureTypes);
                
                NameTextBox.Text = curFurniture[0];
                PriceTextBox.Text = curFurniture[1];
                ManufacturerTextBox.Text = curFurniture[2];
                TypeTextBox.Text = curFurniture[3];
                QuantityTextBox.Text = curFurniture[4];

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

                    Sale curSale = _sales.Where(sale => _currentFurniture.FurnitureId == sale.FurnitureId && sale.SaleDate == DateTime.Today).First();

                    if (curSale == null)
                    {
                        _currentSale = new Sale(_sales.Count + 1, _currentFurniture.FurnitureId, 1, DateTime.Today);
                        _saleController.Create(_currentSale);
                    }
                    else
                    {
                        _currentSale = new Sale( curSale.SaleId, _currentFurniture.FurnitureId, curSale.FurnitureSaledQuantity + 1, DateTime.Today);
                        _saleController.Update(_currentSale);
                    }
                    
                }
                else
                    MessageBox.Show("На данный момент товара нет!");
            }
            catch (InvalidOperationException)
            {
                _currentSale = new Sale(_sales.Count + 1, _currentFurniture.FurnitureId, 1, DateTime.Today);
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
            _manufacturers = _manufacturerController.Read();
            _furnitureTypes = _furnitureTypeController.Read();

            FurnitureButton.IsEnabled = false;
            FurnitureSetButton.IsEnabled = true;

            FurnitureListBox.ItemsSource = _furnitures;

            
        }

        private void FurnitureSetButton_Click(object sender, RoutedEventArgs e)
        {
            FurnitureSetListBox.Visibility = Visibility.Visible;
            FurnitureSetStackPanel.Visibility = Visibility.Visible;
            FurnitureListBox.Visibility = Visibility.Hidden;
            FurnitureStackPanel.Visibility = Visibility.Hidden;
            BuyFurnitureButton.IsEnabled = false;

            _furnitureSets = _furnitureSetController.Read();
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

            if (FurnitureSetListBox.SelectedItem != null && FurnitureSetListBox.SelectedItem is FurnitureSet == true)
            {
                _currentFurnitureSet = (FurnitureSet)FurnitureSetListBox.Items[FurnitureSetListBox.SelectedIndex];
                var curSet = _furnitureSetController.GetInfo(_currentFurnitureSet,_furnitureSetItems,_furnitures);

                SetItemNameTextBlock.Text = curSet;


            }
        }

        private void BuySetButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _sales = _saleController.Read();
                _currentFurnitureSet = (FurnitureSet)FurnitureSetListBox.Items[FurnitureSetListBox.SelectedIndex];
                var currentSetItems = _furnitureSetItems.Where( item => item.FurnitureSetId == _currentFurnitureSet.FurnitureSetId ).ToList();
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
            MainTabItem.Visibility= Visibility.Visible;
            SalesTabItem.Visibility = Visibility.Visible;
        }

        private void AddFurnitureButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               
                int furnId = _furnitureController.Read().Count() + 1;
                short manufactId = Convert.ToInt16(FurnitureTypesComboBox.SelectedIndex + 1);
                short typeId = Convert.ToInt16(ManufacturerComboBox.SelectedIndex + 1);

                Furniture furniture = new Furniture
                    (furnId,
                    FurnitureNameTextBox.Text,
                    Convert.ToDecimal(FurniturePriceTextBox.Text),
                    Convert.ToInt32(FurnitureQuantityTextBox.Text),
                    manufactId,
                    typeId);
                    

                _furnitureController.Create(furniture);
                

            }
            catch (Exception )
            {
                MessageBox.Show("Ошибка ввода параметров товара!");
            }
        }

        private void UpdateFurnitureButton_Click(object sender, RoutedEventArgs e)
        {
            Furniture furniture;

            try
            {
                _currentFurniture = (Furniture)FurnitureListBox.Items[FurnitureListBox.SelectedIndex];

                if (FurnitureTypesComboBox.SelectedIndex == -1 || FurnitureTypesComboBox.SelectedItem == null)
                    FurnitureTypesComboBox.SelectedIndex = _currentFurniture.FurnitureTypeId - 1;

                if (ManufacturerComboBox.SelectedIndex == -1 || ManufacturerComboBox.SelectedItem == null)
                    ManufacturerComboBox.SelectedIndex = _currentFurniture.FurnitureManufacturerId - 1;

                int furnId = _furnitureController.Read().Count() + 1;
                short manufactId = Convert.ToInt16(FurnitureTypesComboBox.SelectedIndex + 1);
                short typeId = Convert.ToInt16(ManufacturerComboBox.SelectedIndex + 1);

                furniture = new Furniture(furnId, FurnitureNameTextBox.Text, Convert.ToDecimal(FurniturePriceTextBox.Text), Convert.ToInt32(FurnitureQuantityTextBox.Text), manufactId, typeId);

                _furnitureController.Update(furniture);
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
                Furniture furniture = _furnitureController.Read().Where(furn => FurnitureNameTextBox.Text == furn.FurnitureName).First();


                _furnitureController.Delete(furniture);
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
            _manufacturers = _manufacturerController.Read();
            _furnitureTypes= _furnitureTypeController.Read();
            _furnitures = _furnitureController.Read();

            ReportListBox.ItemsSource = _saleController.GetInfo(_sales,_furnitures,_manufacturers,_furnitureTypes);
        }


        private void ReportByManufacturer_Click(object sender, RoutedEventArgs e)
        {
            _sales = _saleController.Read();
            _manufacturers = _manufacturerController.Read();
            _furnitureTypes = _furnitureTypeController.Read();
            _furnitures = _furnitureController.Read();

            ReportListBox.ItemsSource = _saleController.GetInfoByManufacturer(ManufacturerName.Text,_sales, _furnitures, _manufacturers, _furnitureTypes);

        }
        
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Contains(SalesTabItem))
            {
                ManufacturerComboBox.ItemsSource = _manufacturerController.Read();
                FurnitureTypesComboBox.ItemsSource = _furnitureTypeController.Read();

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
            _manufacturers = _manufacturerController.Read();
            _furnitureTypes = _furnitureTypeController.Read();
            _furnitures = _furnitureController.Read();

            var info = _saleController.GetInfo(_sales, _furnitures, _manufacturers, _furnitureTypes);

            _saleController.GenerateXMLReport(info);

            MessageBox.Show("Отчет успешно сформирован в XML формате");

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if(!isReLogined)
                DBConnection.GetInstance.CloseConnection();
        }

        private void BuyFurnitureSetButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
*/