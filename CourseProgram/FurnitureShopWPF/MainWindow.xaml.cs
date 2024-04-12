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
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        public void MainDataListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BuyButton.IsEnabled == false)
                BuyButton.IsEnabled = true;

            if (MainDataListBox.SelectedItem != null && MainDataListBox.SelectedItem is Furniture == true)
            {
                _currentFurniture = (Furniture)MainDataListBox.Items[MainDataListBox.SelectedIndex];
                var curFurniture = _furnitureController.GetCurrentFurnitureInfo(_currentFurniture, _furnitures, _manufacturers, _furnitureTypes);
                
                NameTextBox.Text = curFurniture[0];
                PriceTextBox.Text = curFurniture[1];
                ManufacturerTextBox.Text = curFurniture[2];
                TypeTextBox.Text = curFurniture[3];
                QuantityTextBox.Text = curFurniture[4];

            }
            else if(MainDataListBox.SelectedItem != null && MainDataListBox.SelectedItem is FurnitureSetItem == true)
            {
                //вывод в левый столбец FurnitureSet, а справа - информация о FurnitureSetItem'ах

                /*_currentFurnitureSetItem = (FurnitureSetItem)MainDataListBox.Items[MainDataListBox.SelectedIndex];
                var _curSetItem = _furnitureSetItemController.GetCurrentFurnitureSetInfo*/
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
            _furnitures = _furnitureController.Read();
            _manufacturers = _manufacturerController.Read();
            _furnitureTypes = _furnitureTypeController.Read();

            MainDataListBox.ItemsSource = _furnitures;
            FurnitureButton.IsEnabled = false;
            FurnitureSetButton.IsEnabled = true;
        }

        private void FurnitureSetButton_Click(object sender, RoutedEventArgs e)
        {
            _furnitureSets = _furnitureSetController.Read();
            _furnitureSetItems = _furnitureSetItemController.Read();

            MainDataListBox.ItemsSource = _furnitureSetItems;
            FurnitureButton.IsEnabled = true;
            FurnitureSetButton.IsEnabled = false;
        }
    }
}
