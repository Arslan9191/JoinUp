using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JoinUp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Флаг, сигнализирующий о том какая кнопка была нажата
        /// </summary>
        public static char enterFlag;

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Функция загрузки страницы регистрации
        /// </summary>
        void loadRegPage()
        {
            MainFrame.Navigate(new RegPage());
            organizerButton.Visibility = Visibility.Collapsed;
            memberButton.Visibility = Visibility.Collapsed;
            titleApp.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Нажатие кнопки "Я - организатор"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PressOrganizerButt (object sender, RoutedEventArgs e)
        {
            enterFlag = 'o';
            loadRegPage();
        }

        /// <summary>
        /// Нажатие кнопки "Я - участник"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PressMemberButt(object sender, RoutedEventArgs e)
        {
            enterFlag = 'm';
            loadRegPage();
        }
    }
}