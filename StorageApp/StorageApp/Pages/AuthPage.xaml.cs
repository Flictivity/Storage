using MaterialDesignThemes.Wpf;
using StorageApp.DataBase;
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

namespace StorageApp.Pages
{
    /// <summary>
    /// Interaction logic for AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var myMessageQueue = new SnackbarMessageQueue(TimeSpan.FromSeconds(2));
            MySnackbar.MessageQueue = myMessageQueue;

            try
            {
                var user = App.Context.Пользователи.FirstOrDefault(x => x.Почта == tbEmail.Text);
                if (user == null)
                {
                    myMessageQueue.Enqueue("Пользователь с такой почтой не найден!");
                    return;
                }
                if (user.Пароль != tbPassword.Text)
                {
                    myMessageQueue.Enqueue("Неверный пароль!");
                    return;
                }
                App.CurrentUser = user;
                NavigationService.Navigate(new AdminTasksPage(false));
            }
            catch
            {
                myMessageQueue.Enqueue("Произошла ошибка!");
                return;
            }
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegPage());
        }
    }
}
