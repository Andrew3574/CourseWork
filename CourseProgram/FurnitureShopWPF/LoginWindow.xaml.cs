using FurnitureDBLibrary.DataAccess;
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
using System.Windows.Shapes;

namespace FurnitureShopWPF
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private UserController _userController;
        private List<User> _users;

        public LoginWindow()
        {
            try
            {
                _userController = new UserController();
                _users = _userController.Read();
                InitializeComponent();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                User currentUser = _userController.GetUser(UserNameTextBox.Text, PasswordTextBox.Text, _users);
                MainWindow mainWindow = new MainWindow(currentUser);
                this.Close();
                mainWindow.Show();
                  
            }
            catch (Exception)
            {
                MessageBox.Show("Некорректный ввод пользователя!");
            }           
            
        }

        private void GuestEnterButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.Show();
            
        }

        
    }
}
