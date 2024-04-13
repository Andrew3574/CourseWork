using FurnitureDBLibrary.DataAccess;
using FurnitureDBLibrary.Models;
using FurnitureDBLibrary.UserModels;
using System;
using System.Collections.Generic;
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

namespace FurnitureShopWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private User _user;
        private Furniture _currentFurniture;
        private FurnitureSet _currentFurnitureSet;
        private FurnitureSetItem _currentFurnitureSetItem;
        private List<Furniture> _furnitures;
        private List<Manufacturer> _manufacturers;
        private List<FurnitureType> _furnitureTypes;
        private List<FurnitureSet> _furnitureSets;
        private List<FurnitureSetItem> _furnitureSetItems;

        private FurnitureSetItemController _furnitureSetItemController = new FurnitureSetItemController();
        private FurnitureSetController _furnitureSetController = new FurnitureSetController();
        private ManufacturerController _manufacturerController = new ManufacturerController();
        private FurnitureTypeController _furnitureTypeController = new FurnitureTypeController();
        private FurnitureController _furnitureController = new FurnitureController();

        
        public MainWindow(User user)
        {
            try
            {
                _user = user;
                InitializeComponent();
                ManufacturerComboBox.ItemsSource = _manufacturers;
                FurnitureTypesComboBox.ItemsSource = _furnitureTypes;
                switch (_user.RoleId)
                {
                    case 1:
                        InitializeAdminInterface();
                        InitializeManagerInterface();
                        InitializeSalesmanInterface();
                        break;

                    case 2:
                        InitializeManagerInterface();
                        InitializeSalesmanInterface();
                        break;

                    case 3:
                        InitializeSalesmanInterface();
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
            ManufacturerComboBox.ItemsSource = _manufacturerController.Read();
            FurnitureTypesComboBox.ItemsSource = _furnitureTypeController.Read();
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
                if (quantity > 0)
                {
                    QuantityTextBox.Text = _currentFurniture.FurnitureQuantity--.ToString();
                    _furnitureController.Update(_currentFurniture);
                }
                else
                    throw new Exception();
            }
            catch (Exception)
            {
                MessageBox.Show("На данный момент товара нет!");
            }
            

        }

        private void LoginWindow_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
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
                //foreach()
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void InitializeAdminInterface()
        {

        }

        private void InitializeManagerInterface()
        {

        }

        private void InitializeSalesmanInterface()
        {

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
            catch (Exception ex)
            {
                //"Ошибка ввода параметров товара!"
                MessageBox.Show(ex.Message);
            }
        }
    }
}
