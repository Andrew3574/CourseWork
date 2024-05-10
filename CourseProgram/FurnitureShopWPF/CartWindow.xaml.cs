using FurnitureDBLibrary.DataAccess;
using FurnitureDBLibrary.Models;
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

namespace FurnitureShopWPF
{
    /// <summary>
    /// Логика взаимодействия для CartWindow.xaml
    /// </summary>
    public partial class CartWindow : Window
    {
        List<Furniture> furnitureList = new List<Furniture>();
        List<Sale> saleList = new List<Sale>();
        List<Sale> currentSaleList = new List<Sale>();
        ObservableCollection<Sale> sales;
        Sale currentSale;
        Sale selectedSale;
        decimal totalCost = 0;

        SaleController SaleController = new SaleController();
        FurnitureController FurnitureController = new FurnitureController();

        public CartWindow(List<Furniture> _furnitureList)
        {
            saleList = SaleController.Read();
            InitializeComponent();
            currentSaleList = new List<Sale>();
            furnitureList.AddRange(_furnitureList);
            foreach (var item in furnitureList)
            {
                if (item.FurnitureQuantity > 0)
                {
                    Sale sale = currentSaleList.Find(s => s.FurnitureName == item.FurnitureName);
                    
                    
                    if (sale == null)
                    {
                        currentSale = new Sale(item.FurnitureName, item.GetRetailPrice(), item.TypeName, item.ManufacturerName, 1, DateTime.Today);
                        currentSaleList.Add(currentSale);
                    }
                    else
                    {
                        if (sale.FurnitureSaledQuantity != item.FurnitureQuantity)
                        {
                            currentSale = new Sale(item.FurnitureName, item.GetRetailPrice(), item.TypeName, item.ManufacturerName, ++sale.FurnitureSaledQuantity, DateTime.Today);
                        }
                    }

                    totalCost += currentSale.FurnitureRetailPrice;
                }
                else
                    continue;
                
            }

            CartTotalCostTextBlock.Text += totalCost.ToString();
            sales = new ObservableCollection<Sale>(currentSaleList);
            CartDisplay.ItemsSource = sales;
        }

        private void ReturnToMainButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BuyButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentSaleList.Count != 0 && furnitureList.Count != 0)
            {
                foreach (Sale sale in sales)
                {
                    var curFurniture = furnitureList.Find(f => f.FurnitureName == sale.FurnitureName);

                    if (sale.FurnitureSaledQuantity != 0)
                    {
                        FurnitureController.Update(curFurniture);

                        var curSale = saleList.Find(s => s.FurnitureName == sale.FurnitureName && s.SaleDate == sale.SaleDate);

                        if (curSale == null)
                        {
                            currentSale = new Sale(sale.FurnitureName, sale.FurnitureRetailPrice, sale.TypeName, sale.ManufacturerName, 1, DateTime.Today);
                            SaleController.Create(currentSale);
                        }
                        else
                        {
                            currentSale = new Sale(sale.FurnitureName, sale.FurnitureRetailPrice, sale.TypeName, sale.ManufacturerName, curSale.FurnitureSaledQuantity + sale.FurnitureSaledQuantity, DateTime.Today);
                            SaleController.Update(currentSale);

                        }
                    }
                    else
                    {
                        continue;
                    }

                }

                MessageBox.Show("Покупка успешно завершена");

            }
            else
                MessageBox.Show("Корзина покупок пуста");
        }

        private void CartDisplay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedSale = (Sale)CartDisplay.Items[CartDisplay.SelectedIndex];
        }

        private void RemoveCartItem_Click(object sender, RoutedEventArgs e)
        {
            totalCost = 0;
            CartTotalCostTextBlock.Text = "";

            if (CartDisplay.SelectedIndex != -1)
            {
                if (selectedSale.FurnitureSaledQuantity != 0)
                {
                    selectedSale.FurnitureSaledQuantity = --selectedSale.FurnitureSaledQuantity;

                    CartDisplay.Items.Refresh();
                }

                foreach (var item in currentSaleList)
                {
                    Sale sale = currentSaleList.Find(s => s.FurnitureName == item.FurnitureName);

                    totalCost += sale.FurnitureRetailPrice * sale.FurnitureSaledQuantity;
                }

                CartTotalCostTextBlock.Text += totalCost.ToString("0.00");
            }
        }
    }
}
