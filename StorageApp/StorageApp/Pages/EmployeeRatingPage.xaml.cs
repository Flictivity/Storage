using System.Linq;
using System.Windows.Controls;

namespace StorageApp.Pages
{
    /// <summary>
    /// Interaction logic for EmployeeRatingPage.xaml
    /// </summary>
    public partial class EmployeeRatingPage : Page
    {
        public EmployeeRatingPage()
        {
            InitializeComponent();

            var currentEmp = App.Context.Работники
                .FirstOrDefault(x => x.ID_пользователя == App.CurrentUser.ID_пользователя);
            var res = App.Context.Рейтинг.Where(x => x.ID_работника != currentEmp.ID_работника).ToList();

            lvEmployees.ItemsSource = res;
        }
    }
}
