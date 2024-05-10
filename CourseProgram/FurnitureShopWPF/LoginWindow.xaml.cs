using FurnitureDBLibrary.DataAccess;
using FurnitureDBLibrary.UserModels;
using System;
using System.Collections.Generic;
using System.Windows;

namespace FurnitureShopWPF
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private UserController _userController = new UserController();
        private List<User> _users;

        public LoginWindow()
        {
            try
            {                
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
                if (_users.Find(u => u.UserName == UserNameTextBox.Text && u.Password == Passwordbox.Password) != null)
                {
                    _users = _userController.Read();
                    User currentUser = _users.Find(u => u.UserName == UserNameTextBox.Text && u.Password == Passwordbox.Password);
                    MainWindow mainWindow = new MainWindow(currentUser);
                    this.Close();
                    mainWindow.Show();
                }
                else
                    throw new Exception();

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
