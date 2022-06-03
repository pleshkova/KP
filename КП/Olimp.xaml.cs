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
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using Microsoft.Office.Interop.Excel;
using Window = System.Windows.Window;
using System.Text.RegularExpressions;
using System.Data.Linq.SqlClient;

namespace КП
{
    /// <summary>
    /// Логика взаимодействия для Olimp.xaml
    /// </summary>
    public partial class Olimp : Window
    {
        public Olimp()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Update();
        }

        public void Update()
        {
            try
            {
                using (DataContext db = new DataContext(Properties.Settings.Default.DbConnect))
                {
                    Table<Uchastniky> uchastnikies = db.GetTable<Uchastniky>();
                    bdUchastnik.ItemsSource = uchastnikies;
                    Table<Olimpiadi> olimpiadis = db.GetTable<Olimpiadi>();
                    bdOlimpiadi.ItemsSource = olimpiadis;
                    Table<Uchastie> uchasties = db.GetTable<Uchastie>();
                    
                    bdUchastiya.ItemsSource = from a in uchasties
                                              join b in uchastnikies on a.Участник equals b.ID_участника
                                              join c in olimpiadis on a.Олимпиада equals c.ID_олимпиады
                                              select new { a.ID_участия, b.ФИО, c.Название, a.Баллы };

                    OlimpiadyDataContext dc = new OlimpiadyDataContext();
                    var sor = (from a in dc.Olimpiadi
                               select a.Название);
                    cmbOlimp.ItemsSource = sor;
                    cmbZapOlimp.ItemsSource = sor;

                    UchastnikiDataContext dc1 = new UchastnikiDataContext();
                    var uch = (from a in dc1.Uchastniky
                               select a.ФИО);
                    cmbFIO.ItemsSource = uch;
                    cmbZapUch.ItemsSource = uch;
                }
            }
            catch { MessageBox.Show("Ошибка соединения"); }
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tiUchastniki.IsSelected == false)
            {
                txtPoisk.Clear();
                BtnCanselUch_Click(null, null);
            }
            if (tiOlimp.IsSelected == false) BtnCanselOlimp_Click(null, null);
            if (tiUchastiya.IsSelected == false) BtnCanselUchast_Click(null, null);

        }


        //Вкладка Участники
        //Код для кнопки "Новый участник"
        private void BtnNewUchastnik_Click(object sender, RoutedEventArgs e)
        {
            txtFIO.IsEnabled = true;
            txtUchZav.IsEnabled = true;
            DateBith.IsEnabled = true;
            txtPhone.IsEnabled = true;
            btnCanselUch.IsEnabled = true;
            btnOKUchNew.Visibility = Visibility.Visible;
            btnOKUchRed.Visibility = Visibility.Hidden;
            btnOKUchNew.IsEnabled = true;
        }

        //Код для кнопки "Редактировать"
        private void BtnRedUch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (bdUchastnik.SelectedItem != null)
                {
                    UchastnikiDataContext db = new UchastnikiDataContext();
                    txtFIO.IsEnabled = true;
                    txtUchZav.IsEnabled = true;
                    txtPhone.IsEnabled = true;
                    btnCanselUch.IsEnabled = true;
                    btnOKUchNew.Visibility = Visibility.Hidden;
                    btnOKUchRed.Visibility = Visibility.Visible;
                    btnOKUchRed.IsEnabled = true;
                    object item = bdUchastnik.SelectedItem;
                    long vb = Convert.ToInt64((bdUchastnik.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                    txtFIO.Text = (from u in db.Uchastniky
                                   where u.ID_участника == vb
                                   select u.ФИО).FirstOrDefault();
                    txtUchZav.Text = (from u in db.Uchastniky
                                      where u.ID_участника == vb
                                      select u.Учебное_заведение).FirstOrDefault();
                    txtPhone.Text = (from u in db.Uchastniky
                                     where u.ID_участника == vb
                                     select u.Телефон).FirstOrDefault();
                    DateBith.Text = Convert.ToString((from u in db.Uchastniky
                                                      where u.ID_участника == vb
                                                      select u.Дата_рождения).FirstOrDefault());
                }
                else MessageBox.Show("Запись не выбрана");
            }
            catch { MessageBox.Show("Ошибка соединения"); }
        }

        //Код для кнопки отмена
        private void BtnCanselUch_Click(object sender, RoutedEventArgs e)
        {
            txtFIO.IsEnabled = false;
            txtUchZav.IsEnabled = false;
            txtPhone.IsEnabled = false;
            txtFIO.Clear();
            txtUchZav.Clear();
            DateBith.Text = "";
            txtPhone.Clear();
            btnCanselUch.IsEnabled = false;
            DateBith.IsEnabled = false;
            btnOKUchNew.IsEnabled = false;
            btnOKUchRed.IsEnabled = false;
        }

        //Код для сохранения новых данных
        private void BtnOKUchNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtFIO.Text.Length>0 && txtPhone.Text.Length>0 && DateBith.Text.Length>0 && txtUchZav.Text.Length>0)
                {
                    UchastnikiDataContext db = new UchastnikiDataContext();
                    Uchastniky uchastnik = new Uchastniky();
                    uchastnik.ФИО = txtFIO.Text;
                    uchastnik.Телефон = txtPhone.Text;
                    uchastnik.Дата_рождения = Convert.ToDateTime(DateBith.Text);
                    uchastnik.Учебное_заведение = txtUchZav.Text;
                    db.GetTable<Uchastniky>().InsertOnSubmit(uchastnik);
                    db.SubmitChanges();
                    Update();

                    MessageBox.Show("Добавлены новые данные");
                    txtFIO.IsEnabled = false;
                    txtUchZav.IsEnabled = false;
                    txtPhone.IsEnabled = false;
                    txtFIO.Clear();
                    txtUchZav.Clear();
                    DateBith.Text = "";
                    txtPhone.Clear();
                    btnCanselUch.IsEnabled = false;
                    btnOKUchNew.IsEnabled = false;
                    btnOKUchRed.IsEnabled = false;
                }
                else MessageBox.Show("Заполните все поля");
            }
            catch { MessageBox.Show("Ошибка соединения"); }            
        }

        //Код для сохранения отредактированных данных
        private void BtnOKUchRed_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtFIO.Text.Length > 0 && txtPhone.Text.Length > 0 && DateBith.Text.Length > 0 && txtUchZav.Text.Length > 0)
                {
                    object item = bdUchastnik.SelectedItem;
                    long vb = Convert.ToInt64((bdUchastnik.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                    UchastnikiDataContext db = new UchastnikiDataContext();
                    Uchastniky uch = db.Uchastniky.FirstOrDefault(uchid => uchid.ID_участника.Equals(vb));
                    uch.ФИО = txtFIO.Text;
                    uch.Учебное_заведение = txtUchZav.Text;
                    uch.Телефон = txtPhone.Text;
                    uch.Дата_рождения = Convert.ToDateTime(DateBith.Text);
                    var SelectQuery =
                        from a in db.GetTable<Uchastniky>()
                        select a;
                    db.SubmitChanges();
                    bdUchastnik.ItemsSource = SelectQuery;
                    MessageBox.Show("Данные изменены");

                    txtFIO.IsEnabled = false;
                    txtUchZav.IsEnabled = false;
                    txtPhone.IsEnabled = false;
                    txtFIO.Clear();
                    txtUchZav.Clear();
                    txtPhone.Clear();
                    DateBith.Text = "";
                    btnCanselUch.IsEnabled = false;
                    btnOKUchNew.IsEnabled = false;
                    btnOKUchRed.IsEnabled = false;
                }
                else MessageBox.Show("Заполните все поля");
            }
            catch { MessageBox.Show("Ошибка соединения"); }

        }

         //Код для кнопки "Удалить"
        private void BtnDelUch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (bdUchastnik.SelectedItem != null)
                {
                    UchastnikiDataContext db = new UchastnikiDataContext();
                    object item = bdUchastnik.SelectedItem;
                    long vb = Convert.ToInt64((bdUchastnik.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                    var uch = (from s in db.Uchastniky where s.ID_участника == vb select s).Single<Uchastniky>();
                    try
                    {
                        db.Uchastniky.DeleteOnSubmit(uch);
                        db.SubmitChanges();
                        Update();
                        MessageBox.Show("Данные удалены");
                    }
                    catch { MessageBox.Show("Вы не можете удалить этого участника, т.к. он зарегистрирован в участии"); }
                }
                else MessageBox.Show("Запись не выбрана");
            }
            catch { MessageBox.Show("Ошибка соединения"); }
        }

        //Код поиска участников по ФИО
        private void TxtPoisk_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                using (DataContext db = new DataContext(Properties.Settings.Default.DbConnect))
                {
                    UchastnikiDataContext dc1 = new UchastnikiDataContext();
                    if (txtPoisk.Text.Length > 0)
                    {
                        var uch = (from a in dc1.Uchastniky where SqlMethods.Like(a.ФИО, txtPoisk.Text + "%") select a).ToArray();
                        Table<Uchastniky> uchastnikies = db.GetTable<Uchastniky>();
                        bdUchastnik.ItemsSource = uch;
                    }
                    else Update();
                }
            }
            catch { MessageBox.Show("Ошибка соединения"); }
        }
        
        //Вкладка Соревнования
        private void BtnNewOlimp_Click(object sender, RoutedEventArgs e)
        {
            txtNazv.IsEnabled = true;
            dateProv.IsEnabled = true;
            txtKolvo.IsEnabled = true;
            btnCanselOlimp.IsEnabled = true;
            btnOKOlimpRed.Visibility = Visibility.Hidden;
            btnOKOlimpNew.Visibility = Visibility.Visible;
            btnOKOlimpNew.IsEnabled = true;
        }

        private void BtnRedOlimp_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                if (bdOlimpiadi.SelectedItem != null)
                {
                    txtNazv.IsEnabled = true;
                    txtKolvo.IsEnabled = true;
                    dateProv.IsEnabled = true;
                    btnCanselOlimp.IsEnabled = true;
                    btnOKOlimpNew.Visibility = Visibility.Hidden;
                    btnOKOlimpRed.Visibility = Visibility.Visible;
                    btnOKOlimpRed.IsEnabled = true;
                    OlimpiadyDataContext db = new OlimpiadyDataContext();
                    object item = bdOlimpiadi.SelectedItem;
                    long vb = Convert.ToInt64((bdOlimpiadi.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                    txtNazv.Text = (from u in db.Olimpiadi
                                    where u.ID_олимпиады == vb
                                    select u.Название).FirstOrDefault();
                    dateProv.Text = Convert.ToString((from u in db.Olimpiadi
                                                      where u.ID_олимпиады == vb
                                                      select u.Дата_проведения).FirstOrDefault());
                    txtKolvo.Text = Convert.ToString((from u in db.Olimpiadi
                                                      where u.ID_олимпиады == vb
                                                      select u.Количество_заданий).FirstOrDefault());
                }
                else MessageBox.Show("Запись не выбрана");
            }
            catch { MessageBox.Show("Ошибка соединения"); }
        }

        private void BtnCanselOlimp_Click(object sender, RoutedEventArgs e)
        {
            txtNazv.IsEnabled = false;
            dateProv.IsEnabled = false;
            txtKolvo.IsEnabled = false;
            txtKolvo.Clear();
            txtNazv.Clear();
            dateProv.Text = "";
            btnCanselOlimp.IsEnabled = false;
            btnOKOlimpNew.IsEnabled = false;
            btnOKOlimpRed.IsEnabled = false;
        }

        private void BtnOKOlimpNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtNazv.Text.Length>0 && dateProv.Text.Length>0 && txtKolvo.Text.Length>0)
                {
                    OlimpiadyDataContext db = new OlimpiadyDataContext();
                    Olimpiadi olimp = new Olimpiadi();
                    olimp.Название = txtNazv.Text;
                    olimp.Дата_проведения = Convert.ToDateTime(dateProv.Text);
                    olimp.Количество_заданий = Convert.ToInt32(txtKolvo.Text);
                    db.GetTable<Olimpiadi>().InsertOnSubmit(olimp);
                    db.SubmitChanges();
                    Update();

                    MessageBox.Show("Добавлены новые данные");
                    txtNazv.IsEnabled = false;
                    dateProv.IsEnabled = false;
                    txtKolvo.IsEnabled = false;
                    txtKolvo.Clear();
                    txtNazv.Clear();
                    dateProv.Text = "";
                    btnCanselOlimp.IsEnabled = false;
                    btnOKOlimpNew.IsEnabled = false;
                    btnOKOlimpRed.IsEnabled = false;
                }
                else MessageBox.Show("Заполните все поля");
            }
            catch { MessageBox.Show("Ошибка соединения"); }            
        }

        private void BtnOKOlimpRed_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (txtNazv.Text.Length > 0 && dateProv.Text.Length > 0 && txtKolvo.Text.Length>0)
                {
                    object item = bdOlimpiadi.SelectedItem;
                    long vb = Convert.ToInt64((bdOlimpiadi.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                    OlimpiadyDataContext db = new OlimpiadyDataContext();
                    Olimpiadi olimp = db.Olimpiadi.FirstOrDefault(uchid => uchid.ID_олимпиады.Equals(vb));
                    olimp.Название = txtNazv.Text;
                    olimp.Дата_проведения = Convert.ToDateTime(dateProv.Text);
                    olimp.Количество_заданий = Convert.ToInt32(txtKolvo.Text);
                    var SelectQuery =
                        from a in db.GetTable<Olimpiadi>()
                        select a;
                    db.SubmitChanges();
                    bdOlimpiadi.ItemsSource = SelectQuery;
                    MessageBox.Show("Данные изменены");
                    txtNazv.IsEnabled = false;
                    txtKolvo.IsEnabled = false;
                    dateProv.IsEnabled = false;
                    txtNazv.Clear();
                    txtKolvo.Clear();
                    dateProv.Text = "";
                    btnCanselOlimp.IsEnabled = false;
                    btnOKOlimpNew.IsEnabled = false;
                    btnOKOlimpRed.IsEnabled = false;
                }
                else MessageBox.Show("Заполните все поля");
            }
            catch { MessageBox.Show("Ошибка соединения"); }
        }

        private void BtnDelOlimp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (bdOlimpiadi.SelectedItem != null)
                {
                    OlimpiadyDataContext db = new OlimpiadyDataContext();
                    object item = bdOlimpiadi.SelectedItem;
                    long vb = Convert.ToInt64((bdOlimpiadi.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                    var uch = (from s in db.Olimpiadi where s.ID_олимпиады == vb select s).Single<Olimpiadi>();
                    try
                    {
                    db.Olimpiadi.DeleteOnSubmit(uch);
                    db.SubmitChanges();
                    Update();
                    MessageBox.Show("Данные удалены");
                    }
                    catch { MessageBox.Show("Вы не можете удалить эту олимпиаду, т.к. на неё уже зарегистрированно участие"); }
                }
                else MessageBox.Show("Запись не выбрана");
        }
            catch { MessageBox.Show("Ошибка соединения"); }
        }


        //Вкладка Участия
        private void BdUchastiya_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                object item = bdUchastiya.SelectedItem;
                if (item != null)
                {
                    long ID = Convert.ToInt64((bdUchastiya.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                    using (DataContext db = new DataContext(Properties.Settings.Default.DbConnect))
                    {
                        Table<Uchastniky> uchastnikies = db.GetTable<Uchastniky>();
                        Table<Uchastie> uchasties = db.GetTable<Uchastie>();
                        var infoUch = from a in uchastnikies
                                      join b in uchasties on a.ID_участника equals b.Участник
                                      where b.ID_участия == ID
                                      select new { a.ID_участника, a.ФИО, a.Учебное_заведение, a.Дата_рождения, a.Телефон };
                        bdInf.ItemsSource = infoUch;
                    }
                }
                Update();
            }
            catch { MessageBox.Show("Ошибка соединения"); }
        }
        
        private void BtnNewUchastie_Click(object sender, RoutedEventArgs e)
        {
            cmbFIO.IsEnabled = true;
            cmbOlimp.IsEnabled = true;
            txtBall.IsEnabled = true;
            btnOKUchastNew.IsEnabled = true;
            btnCanselUchast.IsEnabled = true;
        }

        private void BtnCanselUchast_Click(object sender, RoutedEventArgs e)
        {
            cmbFIO.SelectedValue="";
            cmbOlimp.SelectedValue = "";
            cmbOlimp.IsEnabled = false;
            cmbFIO.IsEnabled = false;
            txtBall.Clear();
            txtBall.IsEnabled = false;
            btnOKUchastNew.IsEnabled = false;
            btnCanselUchast.IsEnabled = false;
        }

        //Код сохранения нового участия
        private void BtnOKUchastNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cmbFIO.Text.Length>0 && cmbOlimp.Text.Length>0 && txtBall.Text.Length>0)
                {
                    string newUch = Convert.ToString(cmbFIO.SelectedItem);
                    UchastnikiDataContext dc = new UchastnikiDataContext();
                    var uch = (from a in dc.Uchastniky
                               where a.ФИО == newUch
                               select a).ToArray();
                    string newOl = Convert.ToString(cmbOlimp.SelectedItem);
                    OlimpiadyDataContext dc1 = new OlimpiadyDataContext();
                    var sor = (from a in dc1.Olimpiadi
                               where a.Название == newOl
                               select a).ToArray();
                    UchastiyaDataContext dc2 = new UchastiyaDataContext();
                    Uchastie uchastie = new Uchastie();
                    uchastie.Участник = uch[0].ID_участника;
                    uchastie.Олимпиада = sor[0].ID_олимпиады;
                    uchastie.Баллы = Convert.ToInt32(txtBall.Text);
                    dc2.GetTable<Uchastie>().InsertOnSubmit(uchastie);
                    dc2.SubmitChanges();
                    Update();

                    MessageBox.Show("Добавлены новые данные");

                    cmbFIO.SelectedValue = "";
                    cmbOlimp.SelectedValue = "";
                    cmbOlimp.IsEnabled = false;
                    cmbFIO.IsEnabled = false;
                    txtBall.Clear();
                    txtBall.IsEnabled = false;
                    btnOKUchastNew.IsEnabled = false;
                    btnCanselUchast.IsEnabled = false;
                }
                else MessageBox.Show("Заполните все поля");
            }
            catch { MessageBox.Show("Ошибка соединения"); }
        }

        private void BtnDelUchast_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (bdUchastiya.SelectedItem != null)
                {
                    UchastiyaDataContext db = new UchastiyaDataContext();
                    object item = bdUchastiya.SelectedItem;
                    long vb = Convert.ToInt64((bdUchastiya.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                    var uch = (from s in db.Uchastie where s.ID_участия == vb select s).Single<Uchastie>();
                    db.Uchastie.DeleteOnSubmit(uch);
                    db.SubmitChanges();
                    Update();

                    MessageBox.Show("Данные удалены");
                }
                else MessageBox.Show("Запись не выбрана");
            }
            catch { MessageBox.Show("Ошибка соединения"); }
        }



        //Вкладка Отчёты
        //Поиск участий по дате проведения соревнований
        private void BtnOk1Zapr_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DateZapr.Text.Length > 0)
                    using (DataContext db = new DataContext(Properties.Settings.Default.DbConnect))
                    {
                        Table<Uchastie> uchasties = db.GetTable<Uchastie>();
                        Table<Uchastniky> uchastnikies = db.GetTable<Uchastniky>();
                        Table<Olimpiadi> olimpiadis = db.GetTable<Olimpiadi>();
                        var query = from a in uchasties
                                    join b in uchastnikies on a.Участник equals b.ID_участника
                                    join c in olimpiadis on a.Олимпиада equals c.ID_олимпиады
                                    where c.Дата_проведения == Convert.ToDateTime(DateZapr.Text)
                                    select new { a.ID_участия, b.ФИО, c.Название, c.Дата_проведения, a.Баллы };
                        dbZapros.ItemsSource = query;
                    }
                else MessageBox.Show("Выберите дату");
            }
            catch { MessageBox.Show("Ошибка соединения"); }
        }

        //Поиск участий но названию соревнования
        private void BtnOk2Zapr_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cmbZapOlimp.Text.Length > 0)
                {
                    using (DataContext db = new DataContext(Properties.Settings.Default.DbConnect))
                    {
                        OlimpiadyDataContext dc = new OlimpiadyDataContext();
                        var ol = (from a in dc.Olimpiadi
                                   where a.Название == Convert.ToString(cmbZapOlimp.SelectedItem)
                                   select a).ToArray();
                        Table<Olimpiadi> olimpiadis = db.GetTable<Olimpiadi>();
                        Table<Uchastie> uchasties = db.GetTable<Uchastie>();
                        Table<Uchastniky> uchastnikies = db.GetTable<Uchastniky>();
                        var query = from a in uchasties
                                    join b in uchastnikies on a.Участник equals b.ID_участника
                                    join c in olimpiadis on a.Олимпиада equals c.ID_олимпиады
                                    where a.Олимпиада == ol[0].ID_олимпиады
                                    select new { a.ID_участия, b.ФИО, c.Название, c.Дата_проведения, a.Баллы };
                        dbZapros.ItemsSource = query;
                    }
                }
                else MessageBox.Show("Выберите соревнование");
            }
            catch { MessageBox.Show("Ошибка соединения"); }
        }

        //Поиск участий по ФИО участника
        private void BtnOk3Zapr_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cmbZapUch.Text.Length > 0)
                {
                    using (DataContext db = new DataContext(Properties.Settings.Default.DbConnect))
                    {
                        UchastnikiDataContext dc1 = new UchastnikiDataContext();
                        var uch = (from a in dc1.Uchastniky
                                   where a.ФИО == Convert.ToString(cmbZapUch.SelectedItem)
                                   select a).ToArray();
                        Table<Uchastniky> uchastnikies = db.GetTable<Uchastniky>();
                        Table<Olimpiadi> olimpiadis = db.GetTable<Olimpiadi>();
                        Table<Uchastie> uchasties = db.GetTable<Uchastie>();
                        var query = from a in uchasties
                                    join b in uchastnikies on a.Участник equals b.ID_участника
                                    join c in olimpiadis on a.Олимпиада equals c.ID_олимпиады
                                    where a.Участник == uch[0].ID_участника
                                    select new { a.ID_участия, b.ФИО, c.Название, c.Дата_проведения, a.Баллы };
                        dbZapros.ItemsSource = query;
                    }
                }
                else MessageBox.Show("Выберите участника");
            }
            catch { MessageBox.Show("Ошибка соединения"); }
        }
        
        //Код кнопки "Очистить"
        private void BtnClearZap_Click(object sender, RoutedEventArgs e)
        {
            DateZapr.Text = "";
            cmbZapUch.SelectedValue = "";
            cmbZapOlimp.SelectedValue = "";
            dbZapros.ItemsSource = null;
        }

        private void BtnOtchet_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            app.Visible = true;
            app.WindowState = XlWindowState.xlMaximized;
            Workbook wb = app.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Worksheet ws = wb.Worksheets[1];
            DateTime curretDate = DateTime.Now;
            ws.Columns.AutoFit();
            ws.Range["A1"].Value = "Отчёт Информация об участиях";
            for (int i = 0; i < dbZapros.Columns.Count; i++)
                for (int j = 0; j < dbZapros.Items.Count; j++)
                {
                    TextBlock text = dbZapros.Columns[i].GetCellContent(dbZapros.Items[j]) as TextBlock;
                    Range range = (Range)ws.Cells[j + 3, i + 1];
                    range.Value2 = text.Text;
                }

            for (int i = 0; i < dbZapros.Columns.Count; i++)
            {
                Range range = (Range)ws.Cells[2, i + 1];
                ws.Cells[2, i + 1].font.bold = true;
                ws.Cells[2, i + 1].columnwidth = 15;
                range.Value2 = dbZapros.Columns[i].Header;
            }
        }



        //Валидация
        private void ValNum_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
        }

        private void ValFIO_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if ((e.Text[0] < 'А' || e.Text[0] > 'Я' && e.Text[0] < 'а' || e.Text[0] > 'я') && e.Text[0] != '-' && e.Text[0] != '.')
                e.Handled = true;
        }

        private void Val3_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if ((e.Text[0] < 'А' || e.Text[0] > 'Я' && e.Text[0] < 'а' || e.Text[0] > 'я') && e.Text[0] != '-' && e.Text[0] != '.' && !Char.IsDigit(e.Text, 0))
                e.Handled = true;
        }
        
    }
}
