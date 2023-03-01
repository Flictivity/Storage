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
    /// Interaction logic for TasksPage.xaml
    /// </summary>
    public partial class TasksPage : Page
    {
        public TasksPage()
        {
            InitializeComponent();

            var tasks = App.Context.Задачи.ToList();
            lvInProgress.ItemsSource = tasks.Where(x => !x.Состояние_работы).ToList();
            lvFinished.ItemsSource = tasks.Where(x => x.Состояние_работы).ToList();
        }

        private void lvFinished_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowTaskInfo((Задачи)lvFinished.SelectedItem);
        }


        private void ShowTaskInfo(Задачи task)
        {
            NavigationService.Navigate( new TaskPage(task));
        }

        private void lvInProgress_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowTaskInfo((Задачи)lvInProgress.SelectedItem);
        }
    }
}
