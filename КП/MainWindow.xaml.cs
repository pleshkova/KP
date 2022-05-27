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

namespace КП
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnVoity_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UsersDataContext db = new UsersDataContext(Properties.Settings.Default.DbConnect);
                var userLogin = (from users in db.User where users.Логин == txtLogin.Text select users).ToArray();
                var userPass = (from users in db.User where users.Пароль == txtPassword.Password select users).ToArray();
                try
                {
                    if (txtLogin.Text == userLogin[0].Логин && txtPassword.Password == userPass[0].Пароль)
                    {
                        Olimp olimp = new Olimp();
                        olimp.Show();
                        this.Close();
                    }
                }
                catch { MessageBox.Show("Введите корректные данные"); }
            }
            catch { MessageBox.Show("Ошибка сединения"); }
            

        }

        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            Reg reg = new Reg();
            reg.Show();
            this.Close();
        }
    }
}
