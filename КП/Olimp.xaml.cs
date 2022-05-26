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
                    Table<Sorevnovaniya> sorevnovaniyas = db.GetTable<Sorevnovaniya>();
                    bdSorevnovanya.ItemsSource = sorevnovaniyas;
                    Table<Uchastie> uchasties = db.GetTable<Uchastie>();

                    var query = from a in uchasties
                                join b in uchastnikies on a.Участник equals b.ID_участника
                                join c in sorevnovaniyas on a.Соревнование equals c.ID_соревнования
                                select new { a.ID_участия, b.ФИО, c.Название, a.Баллы, a.Место };
                    bdUchastiya.ItemsSource = query;

                    SorevnovaniyaDataContext dc = new SorevnovaniyaDataContext();
                    var sor = (from a in dc.Sorevnovaniya
                               select a.Название);
                    cmbSorev.ItemsSource = sor;
                    cmbZapSor.ItemsSource = sor;

                    UchastnikiDataContext dc1 = new UchastnikiDataContext();
                    var uch = (from a in dc1.Uchastniky
                               select a.ФИО);
                    cmbFIO.ItemsSource = uch;
                    cmbZapUch.ItemsSource = uch;
                }
            }
            catch { MessageBox.Show("Ошибка соединения"); }
        }


        //Вкладка Участники
        private void BtnNewUchastnik_Click(object sender, RoutedEventArgs e)
        {
            txtFIO.IsEnabled = true;
            txtAdress.IsEnabled = true;
            DateBith.IsEnabled = true;
            txtPhone.IsEnabled = true;
            txtRukvod.IsEnabled = true;
            btnCanselUch.IsEnabled = true;
            btnOKUchNew.Visibility = Visibility.Visible;
            btnOKUchRed.Visibility = Visibility.Hidden;
            btnOKUchNew.IsEnabled = true;
        }

        private void BtnRedUch_Click(object sender, RoutedEventArgs e)
        {
            txtFIO.IsEnabled = true;
            txtAdress.IsEnabled = true;
            txtPhone.IsEnabled = true;
            txtRukvod.IsEnabled = true;
            btnCanselUch.IsEnabled = true;
            btnOKUchNew.Visibility = Visibility.Hidden;
            btnOKUchRed.Visibility = Visibility.Visible;
            btnOKUchRed.IsEnabled = true;

            try
            {
                UchastnikiDataContext db = new UchastnikiDataContext();
                object item = bdUchastnik.SelectedItem;
                long vb = Convert.ToInt64((bdUchastnik.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                txtFIO.Text = (from u in db.Uchastniky
                               where u.ID_участника == vb
                               select u.ФИО).FirstOrDefault();
                txtAdress.Text = (from u in db.Uchastniky
                                  where u.ID_участника == vb
                                  select u.Адрес).FirstOrDefault();
                txtPhone.Text = (from u in db.Uchastniky
                                 where u.ID_участника == vb
                                 select u.Телефон).FirstOrDefault();
                txtRukvod.Text = (from u in db.Uchastniky
                                  where u.ID_участника == vb
                                  select u.Руководитель).FirstOrDefault();
                DateBith.Text = Convert.ToString((from u in db.Uchastniky
                                                  where u.ID_участника == vb
                                                  select u.Дата_рождения).FirstOrDefault());
            }
            catch { MessageBox.Show("Ошибка соединения"); }
        }

        private void BtnCanselUch_Click(object sender, RoutedEventArgs e)
        {
            txtFIO.IsEnabled = false;
            txtAdress.IsEnabled = false;
            txtPhone.IsEnabled = false;
            txtRukvod.IsEnabled = false;
            txtFIO.Clear();
            txtAdress.Clear();
            DateBith.Text = "";
            txtPhone.Clear();
            txtRukvod.Clear();
            btnCanselUch.IsEnabled = false;
            btnOKUchNew.IsEnabled = false;
            btnOKUchRed.IsEnabled = false;
        }

        private void BtnOKUchNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UchastnikiDataContext db = new UchastnikiDataContext();
                Uchastniky uchastnik = new Uchastniky();
                uchastnik.ФИО = txtFIO.Text;
                uchastnik.Телефон = txtPhone.Text;
                uchastnik.Дата_рождения = Convert.ToDateTime(DateBith.Text);
                uchastnik.Адрес = txtAdress.Text;
                uchastnik.Руководитель = txtRukvod.Text;
                db.GetTable<Uchastniky>().InsertOnSubmit(uchastnik);
                db.SubmitChanges();
                Update();
            }
            catch { MessageBox.Show("Ошибка соединения"); }
            txtFIO.IsEnabled = false;
            txtAdress.IsEnabled = false;
            txtPhone.IsEnabled = false;
            txtRukvod.IsEnabled = false;
            txtFIO.Clear();
            txtAdress.Clear();
            DateBith.Text = "";
            txtPhone.Clear();
            txtRukvod.Clear();
            btnCanselUch.IsEnabled = false;
            btnOKUchNew.IsEnabled = false;
            btnOKUchRed.IsEnabled = false;
        }

        private void BtnOKUchRed_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object item = bdUchastnik.SelectedItem;
                long vb = Convert.ToInt64((bdUchastnik.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                UchastnikiDataContext db = new UchastnikiDataContext();
                Uchastniky uch = db.Uchastniky.FirstOrDefault(uchid => uchid.ID_участника.Equals(vb));
                uch.ФИО = txtFIO.Text;
                uch.Адрес = txtAdress.Text;
                uch.Телефон = txtPhone.Text;
                uch.Руководитель = txtRukvod.Text;
                uch.Дата_рождения = Convert.ToDateTime(DateBith.Text);
                var SelectQuery =
                    from a in db.GetTable<Uchastniky>()
                    select a;
                db.SubmitChanges();
                bdUchastnik.ItemsSource = SelectQuery;
                MessageBox.Show("Данные изменены");
            }
            catch { MessageBox.Show("Ошибка соединения"); }

            txtFIO.IsEnabled = false;
            txtAdress.IsEnabled = false;
            txtPhone.IsEnabled = false;
            txtRukvod.IsEnabled = false;
            txtFIO.Clear();
            txtAdress.Clear();
            txtPhone.Clear();
            DateBith.Text = "";
            txtRukvod.Clear();
            btnCanselUch.IsEnabled = false;
            btnOKUchNew.IsEnabled = false;
            btnOKUchRed.IsEnabled = false;
        }

        private void BtnDelUch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UchastnikiDataContext db = new UchastnikiDataContext();
                object item = bdUchastnik.SelectedItem;
                long vb = Convert.ToInt64((bdUchastnik.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                var uch = (from s in db.Uchastniky where s.ID_участника == vb select s).Single<Uchastniky>();
                db.Uchastniky.DeleteOnSubmit(uch);
                db.SubmitChanges();
                Update();
            }
            catch { MessageBox.Show("Ошибка соединения"); }
        }


        //Вкладка Соревнования
        private void BtnNewSorev_Click(object sender, RoutedEventArgs e)
        {
            txtNazv.IsEnabled = true;
            txtNum.IsEnabled = true;
            dateProv.IsEnabled = true;
            btnCanselSor.IsEnabled = true;
            btnOKSorRed.Visibility = Visibility.Hidden;
            btnOKSorNew.Visibility = Visibility.Visible;
            btnOKSorNew.IsEnabled = true;
        }

        private void BtnRedSor_Click(object sender, RoutedEventArgs e)
        {
            txtNazv.IsEnabled = true;
            txtNum.IsEnabled = true;
            dateProv.IsEnabled = true;
            btnCanselSor.IsEnabled = true;
            btnOKSorNew.Visibility = Visibility.Hidden;
            btnOKSorRed.Visibility = Visibility.Visible;
            btnOKSorRed.IsEnabled = true;
            try
            {
                SorevnovaniyaDataContext db = new SorevnovaniyaDataContext();
                object item = bdSorevnovanya.SelectedItem;
                long vb = Convert.ToInt64((bdSorevnovanya.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                txtNazv.Text = (from u in db.Sorevnovaniya
                                where u.ID_соревнования == vb
                                select u.Название).FirstOrDefault();
                txtNum.Text = Convert.ToString((from u in db.Sorevnovaniya
                                                where u.ID_соревнования == vb
                                                select u.Максимальное_количество_участников).FirstOrDefault());
                dateProv.Text = Convert.ToString((from u in db.Sorevnovaniya
                                                  where u.ID_соревнования == vb
                                                  select u.Дата_проведения).FirstOrDefault());
            }
            catch { MessageBox.Show("Ошибка соединения"); }
        }

        private void BtnCanselSor_Click(object sender, RoutedEventArgs e)
        {
            txtNazv.IsEnabled = false;
            txtNum.IsEnabled = false;
            dateProv.IsEnabled = false;
            txtNazv.Clear();
            dateProv.Text = "";
            txtNum.Clear();
            btnCanselSor.IsEnabled = false;
            btnOKSorNew.IsEnabled = false;
            btnOKSorRed.IsEnabled = false;
        }

        private void BtnOKSorNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SorevnovaniyaDataContext db = new SorevnovaniyaDataContext();
                Sorevnovaniya sorev = new Sorevnovaniya();
                sorev.Название = txtNazv.Text;
                sorev.Максимальное_количество_участников = Convert.ToInt32(txtNum.Text);
                sorev.Дата_проведения = Convert.ToDateTime(dateProv.Text);
                db.GetTable<Sorevnovaniya>().InsertOnSubmit(sorev);
                db.SubmitChanges();
                Update();
            }
            catch { MessageBox.Show("Ошибка соединения"); }

            txtNazv.IsEnabled = false;
            txtNum.IsEnabled = false;
            dateProv.IsEnabled = false;
            txtNazv.Clear();
            txtNum.Clear();
            dateProv.Text = "";
            btnCanselSor.IsEnabled = false;
            btnOKSorNew.IsEnabled = false;
            btnOKSorRed.IsEnabled = false;
        }

        private void BtnOKSorRed_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                object item = bdSorevnovanya.SelectedItem;
                long vb = Convert.ToInt64((bdSorevnovanya.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                SorevnovaniyaDataContext db = new SorevnovaniyaDataContext();
                Sorevnovaniya sor = db.Sorevnovaniya.FirstOrDefault(uchid => uchid.ID_соревнования.Equals(vb));
                sor.Название = txtNazv.Text;
                sor.Максимальное_количество_участников = Convert.ToInt32(txtNum.Text);
                sor.Дата_проведения = Convert.ToDateTime(dateProv.Text);
                var SelectQuery =
                    from a in db.GetTable<Sorevnovaniya>()
                    select a;
                db.SubmitChanges();
                bdSorevnovanya.ItemsSource = SelectQuery;
                MessageBox.Show("Данные изменены");
            }
            catch { MessageBox.Show("Ошибка соединения"); }

            txtNazv.IsEnabled = false;
            txtNum.IsEnabled = false;
            dateProv.IsEnabled = false;
            txtNazv.Clear();
            txtNum.Clear();
            dateProv.Text = "";
            btnCanselSor.IsEnabled = false;
            btnOKSorNew.IsEnabled = false;
            btnOKSorRed.IsEnabled = false;
        }

        private void BtnDelSor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SorevnovaniyaDataContext db = new SorevnovaniyaDataContext();
                object item = bdSorevnovanya.SelectedItem;
                long vb = Convert.ToInt64((bdSorevnovanya.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                var uch = (from s in db.Sorevnovaniya where s.ID_соревнования == vb select s).Single<Sorevnovaniya>();
                db.Sorevnovaniya.DeleteOnSubmit(uch);
                db.SubmitChanges();
                Update();
            }
            catch { MessageBox.Show("Ошибка соединения"); }
        }

        private void TxtNum_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0)) e.Handled = true;
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
                                      select new { a.ID_участника, a.ФИО, a.Дата_рождения, a.Телефон };
                        bdInf.ItemsSource = infoUch;
                    }
                }
                Update();
            }
            catch { MessageBox.Show("Ошибка соединения"); }
        }

        private void BtnDelUchast_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UchastiyaDataContext db = new UchastiyaDataContext();
                object item = bdUchastiya.SelectedItem;
                long vb = Convert.ToInt64((bdUchastiya.SelectedCells[0].Column.GetCellContent(item) as TextBlock).Text);
                var uch = (from s in db.Uchastie where s.ID_участия == vb select s).Single<Uchastie>();
                db.Uchastie.DeleteOnSubmit(uch);
                db.SubmitChanges();
                Update();
            }
            catch { MessageBox.Show("Ошибка соединения"); }
        }

        private void BtnNewUchastie_Click(object sender, RoutedEventArgs e)
        {
            cmbFIO.IsEnabled = true;
            cmbSorev.IsEnabled = true;
            txtBall.IsEnabled = true;
            btnOKUchastNew.IsEnabled = true;
        }

        private void BtnCanselUchast_Click(object sender, RoutedEventArgs e)
        {
            cmbSorev.IsEnabled = false;
            cmbFIO.IsEnabled = false;
            txtBall.Clear();
            txtBall.IsEnabled = false;
        }

        private void BtnOKUchastNew_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string newUch = Convert.ToString(cmbFIO.SelectedItem);
                UchastnikiDataContext dc = new UchastnikiDataContext();
                var uch = (from a in dc.Uchastniky
                           where a.ФИО == newUch
                           select a).ToArray();
                string newSor = Convert.ToString(cmbSorev.SelectedItem);
                SorevnovaniyaDataContext dc1 = new SorevnovaniyaDataContext();
                var sor = (from a in dc1.Sorevnovaniya
                           where a.Название == newSor
                           select a).ToArray();
                UchastiyaDataContext dc2 = new UchastiyaDataContext();
                Uchastie uchastie = new Uchastie();
                uchastie.Участник = uch[0].ID_участника;
                uchastie.Соревнование = sor[0].ID_соревнования;
                dc2.GetTable<Uchastie>().InsertOnSubmit(uchastie);
                dc2.SubmitChanges();
                Update();
            }
            catch { MessageBox.Show("Ошибка соединения"); }

            txtNazv.IsEnabled = false;
            txtNum.IsEnabled = false;
            dateProv.IsEnabled = false;
            txtNazv.Clear();
            txtNum.Clear();
            dateProv.Text = "";
            btnCanselSor.IsEnabled = false;
            btnOKSorNew.IsEnabled = false;
            btnOKSorRed.IsEnabled = false;
        }


        private void TxtFIO_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //string Symbol = e.KeyChar.ToString();
            //if (!Regex.Match(Symbol, @"[а-яА-Я]|[a-zA-Z]").Success)
            //{
            //    e.Handled = true;
            //}
        }


        //Вкладка Запросы
        private void BtnOk1Zapr_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (DataContext db = new DataContext(Properties.Settings.Default.DbConnect))
                {
                    string date = DateZapr.Text;
                    Table<Uchastie> uchasties = db.GetTable<Uchastie>();
                    Table<Uchastniky> uchastnikies = db.GetTable<Uchastniky>();
                    Table<Sorevnovaniya> sorevnovaniyas = db.GetTable<Sorevnovaniya>();
                    var query = from a in uchasties
                                join b in uchastnikies on a.Участник equals b.ID_участника
                                join c in sorevnovaniyas on a.Соревнование equals c.ID_соревнования
                                where c.Дата_проведения == Convert.ToDateTime(date)
                                select new { a.ID_участия, b.ФИО, c.Название, c.Дата_проведения };
                    dbZapros.ItemsSource = query;
                }
            }
            catch { MessageBox.Show("Ошибка соединения"); }
        }

        private void BtnOk2Zapr_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (DataContext db = new DataContext(Properties.Settings.Default.DbConnect))
                {
                    string sorSelected = Convert.ToString(cmbZapSor.SelectedItem);
                    SorevnovaniyaDataContext dc = new SorevnovaniyaDataContext();
                    var sor = (from a in dc.Sorevnovaniya
                               where a.Название == sorSelected
                               select a).ToArray();
                    Table<Sorevnovaniya> sorevnovaniyas = db.GetTable<Sorevnovaniya>();
                    Table<Uchastie> uchasties = db.GetTable<Uchastie>();
                    Table<Uchastniky> uchastnikies = db.GetTable<Uchastniky>();
                    var query = from a in uchasties
                                join b in uchastnikies on a.Участник equals b.ID_участника
                                join c in sorevnovaniyas on a.Соревнование equals c.ID_соревнования
                                where a.Соревнование == sor[0].ID_соревнования
                                select new { a.ID_участия, b.ФИО, c.Название, c.Дата_проведения };
                    dbZapros.ItemsSource = query;
                }
            }
            catch { MessageBox.Show("Ошибка соединения"); }
        }

        private void BtnOk3Zapr_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (DataContext db = new DataContext(Properties.Settings.Default.DbConnect))
                {
                    string uchSel = Convert.ToString(cmbZapUch.SelectedItem);
                    UchastnikiDataContext dc1 = new UchastnikiDataContext();
                    var uch = (from a in dc1.Uchastniky
                               where a.ФИО == uchSel
                               select a).ToArray();
                    Table<Uchastniky> uchastnikies = db.GetTable<Uchastniky>();
                    Table<Sorevnovaniya> sorevnovaniyas = db.GetTable<Sorevnovaniya>();
                    Table<Uchastie> uchasties = db.GetTable<Uchastie>();
                    var query = from a in uchasties
                                join b in uchastnikies on a.Участник equals b.ID_участника
                                join c in sorevnovaniyas on a.Соревнование equals c.ID_соревнования
                                where a.Участник == uch[0].ID_участника
                                select new { a.ID_участия, b.ФИО, c.Название, c.Дата_проведения };
                    dbZapros.ItemsSource = query;
                }
            }
            catch { MessageBox.Show("Ошибка соединения"); }
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
            ws.Range["A1"].Value = "Отчёт об информации участий";
            for (int i = 0; i < dbZapros.Columns.Count; i++)
            {
                for (int j = 0; j < dbZapros.Items.Count; j++)
                {
                    TextBlock text = dbZapros.Columns[i].GetCellContent(dbZapros.Items[j]) as TextBlock;
                    Range range = (Range)ws.Cells[j + 3, i + 1];
                    range.Value2 = text.Text;
                }
            }

            for (int i = 0; i < dbZapros.Columns.Count; i++)
            {
                Range range = (Range)ws.Cells[2, i + 1];
                ws.Cells[2, i + 1].font.bold = true;
                ws.Cells[2, i + 1].columnwidth = 15;
                range.Value2 = dbZapros.Columns[i].Header;
                
            }
        }
    }
}
