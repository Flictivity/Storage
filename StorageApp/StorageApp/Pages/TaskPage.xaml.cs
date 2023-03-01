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
    /// Interaction logic for TaskPage.xaml
    /// </summary>
    public partial class TaskPage : Page
    {
        public TaskPage(Задачи task)
        {
            InitializeComponent();


            cbAdmin.ItemsSource = App.Context.Работники.Where(x => x.Пользователи.ID_роли == 2).ToList();
            cbEmployee.ItemsSource = App.Context.Работники.Where(x => x.Пользователи.ID_роли == 1).ToList();

            tbName.Text = task.Название;
            tbDifficult.Text = task.Сложность_задачи.ToString();
            chbIsFinished.IsChecked = task.Состояние_работы;
            dpStartTime.SelectedDate = task.Начало_выполнения;
            dpEndDate.SelectedDate = task.Окончание_выполнения;
            tbEstimation.Text = task.Оценка_выполнения.ToString();
            cbAdmin.SelectedItem = task.Работники1;
            cbEmployee.SelectedItem = task.Работники;
            tbTimer.Text = task.Время_выполнения.ToString();
        }
    }
}
