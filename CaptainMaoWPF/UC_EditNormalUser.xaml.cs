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
using CaptainMaoWPF.Model;
using System.Data.Entity;
using System.IO;
using CaptainMaoWPF.ViewModel;

namespace CaptainMaoWPF
{
    /// <summary>
    /// UC_EditNormalUser.xaml 的互動邏輯
    /// </summary>
    public partial class UC_EditNormalUser : UserControl
    {
        public UC_EditNormalUser()
        {
            InitializeComponent();
        }

        MaoEntities dataentity;
        System.Windows.Data.CollectionViewSource normalUserViewSource;

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            //註解掉的是用ViewModel來做繫結的方法。雖然可以成功呈現，但儲存機制需要另外寫，所以暫時用原來的。
            //normalUserViewSource = (System.Windows.Data.CollectionViewSource)this.FindResource("normalUserViewSource");

            //dataentity = new MaoEntities();
            //var aspUser = dataentity.AspNetRoles.Where(r => r.Name == "Normal").First().AspNetUsers.ToList();

            //List<NormalUserDetailViewModel> normalUsers = new List<NormalUserDetailViewModel>();
            //foreach (var u in aspUser)
            //{
            //    NormalUserDetailViewModel user = new NormalUserDetailViewModel
            //    {
            //        AccessFailedCount=u.AccessFailedCount,
            //        DateRegistered =u.DateRegistered,
            //        Id =u.Id,
            //        EmailConfirmed =u.EmailConfirmed,
            //        Experience =u.Experience,
            //        FirstName =u.FirstName,
            //        LastName =u.LastName,
            //        LockoutEnabled =u.LockoutEnabled,
            //        LockoutEndDateUtc =u.LockoutEndDateUtc,
            //        NickName =u.NickName,
            //        PhoneNumber =u.PhoneNumber,
            //        PhoneNumberConfirmed =u.PhoneNumberConfirmed,
            //        TwoFactorEnabled =u.TwoFactorEnabled,
            //        UserName =u.UserName,
            //        Photo = u.Photo
            //    };
            //    normalUsers.Add(user);
            //}
            //normalUserViewSource.Source = normalUsers;

            normalUserViewSource = (System.Windows.Data.CollectionViewSource)this.FindResource("normalUserViewSource");

            dataentity = new MaoEntities();
            dataentity.AspNetRoles.Where(r => r.Name == "Normal").First().AspNetUsers.ToList();
            normalUserViewSource.Source = dataentity.AspNetUsers.Local;


        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            //this.normalUserViewSource.Source = dataentity.NormalUsers.Local.Where(u=> u.Username.Contains(tbSearchCondition.Text));

            var q = dataentity.AspNetUsers.Local.Where(u => u.UserName.Contains(tbSearchCondition.Text));
            //q.Load();
            this.normalUserViewSource.Source = q.ToList();
            //q.ToList();
            //this.normalUserViewSource.View.Filter(q);
        }

        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            this.normalUserViewSource.View.MoveCurrentToPrevious();
            if (this.normalUserViewSource.View.IsCurrentBeforeFirst)
            {
                this.normalUserViewSource.View.MoveCurrentToLast();
            }
            this.normalUserDataGrid.ScrollIntoView(this.normalUserViewSource.View.CurrentItem);
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            this.normalUserViewSource.View.MoveCurrentToNext();
            if (this.normalUserViewSource.View.IsCurrentAfterLast)
            {
                this.normalUserViewSource.View.MoveCurrentToFirst();
            }
            this.normalUserDataGrid.ScrollIntoView(this.normalUserViewSource.View.CurrentItem);
        }

        private void btnLast_Click(object sender, RoutedEventArgs e)
        {
            this.normalUserViewSource.View.MoveCurrentToLast();
            this.normalUserDataGrid.ScrollIntoView(this.normalUserViewSource.View.CurrentItem);
        }

        private void btnFirst_Click(object sender, RoutedEventArgs e)
        {
            this.normalUserViewSource.View.MoveCurrentToFirst();
            this.normalUserDataGrid.ScrollIntoView(this.normalUserViewSource.View.CurrentItem);
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            //W_AdministratorAddNormalUser w = new W_AdministratorAddNormalUser();
            //view.UC_registration uc = new view.UC_registration();
            //w.grid1.Children.Add(uc);
            //w.Show();
            ////dataentity.NormalUsers.Local.Add(new Model.NormalUser {  });
            ////btnLast_Click(sender, e);
        }
        
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.dataentity.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBrowsePic_Click(object sender, RoutedEventArgs e)
        {
            //將對話方塊選取的圖片送到Image
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = "Image files|*.jpg;*.JPG;*.JPEG;*.png;*.PNG;*.bmp;*.BMP";
            BitmapImage bi = new BitmapImage();
            Nullable<bool> result = dlg.ShowDialog();
            if (result == true)
            {
                bi = new BitmapImage(new Uri(dlg.FileName, UriKind.Absolute));

                //將Image轉為byte[]
                MemoryStream ms = new MemoryStream();
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bi));
                encoder.Save(ms);
                byte[] imgData = ms.ToArray();
                ms.Close();
                ((Model.AspNetUser)this.normalUserViewSource.View.CurrentItem).Photo = imgData;
            }

            this.normalUserViewSource.View.Refresh();
            this.normalUserDataGrid.ScrollIntoView(this.normalUserViewSource.View.CurrentItem);
        }
    }
}
