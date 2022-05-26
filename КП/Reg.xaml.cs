using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace КП
{
    /// <summary>
    /// Логика взаимодействия для Reg.xaml
    /// </summary>
    public partial class Reg : Window
    {
        public Reg()
        {
            InitializeComponent();
        }

        private void BtnRegReg_Click(object sender, RoutedEventArgs e)
        {
            string s = "Пароль должен содержать ";
                //проверка пароля
                bool A, B, C, D;
                if (txtPasswordReg.Text.Length < 6) { s = s + "Минимум 6 символов,"; A = false; }
                else A = true;

                // проверка на Верхний регистр
                if (Regex.IsMatch(txtPasswordReg.Text, @"[A-Z]") || Regex.IsMatch(txtPasswordReg.Text, @"[А-Я]")) B = true;
                { s = s + "Минимум 1 прописную букву, "; B = false; }

                // проверка на число
                if (Regex.IsMatch(txtPasswordReg.Text, @"[0-9]")) C = true;
                else { s = s + "Минимум 1 цифру, "; C = false; }

                // проверка на знак
                if (Regex.IsMatch(txtPasswordReg.Text, @"[!@#$%^]")) D = true;
                else { s = s + "Минимум 1 один символ из набора:  ! @ # $ % ^"; D = false; }

            try
            {
                if (A && B && C && D)
                {
                    using (DataContext db = new DataContext(Properties.Settings.Default.DbConnect))
                    {
                        UsersDataContext dv = new UsersDataContext();
                        string log = txtLoginReg.Text;
                        string pas = txtPasswordReg.Text;
                        string fio = txtFIOReg.Text;
                        User user = new User();
                        user.Логин = log;
                        user.Пароль = pas;
                        user.ФИО = fio;
                        db.GetTable<User>().InsertOnSubmit(user);
                        db.SubmitChanges();
                        MessageBox.Show("Пользователь добавлен");
                    }
                    MainWindow main = new MainWindow();
                    main.Show();
                    this.Close();

                }
                else
                {
                    MessageBox.Show(s);
                }
            }

            catch
            {
                MessageBox.Show("Ошибка соединения");
            }
            
        }

        private void BtnRegOtm_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}
