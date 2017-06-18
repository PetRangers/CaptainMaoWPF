using CaptainMaoWPF.Model;
using CaptainMaoWPF.ViewModel;
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

namespace CaptainMaoWPF
{
    /// <summary>
    /// UC_SetAuth.xaml 的互動邏輯
    /// </summary>
    public partial class UC_SetAuth : UserControl
    {
        public UC_SetAuth()
        {
            InitializeComponent();
        }

        MaoEntities dataentity;
        System.Windows.Data.CollectionViewSource setAuthViewSource;

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            setAuthViewSource = (System.Windows.Data.CollectionViewSource)this.FindResource("setAuthViewSource");

            dataentity = new MaoEntities();
            var allUsers = dataentity.AspNetUsers.ToList();

            List<SetAuthViewModel> setAuthList = new List<SetAuthViewModel>();

            foreach (var u in allUsers)
            {
                SetAuthViewModel setAuth = new SetAuthViewModel
                {
                    Id=u.Id,
                    Experience=u.Experience,
                    UserName=u.UserName
                };
                if (u.LastName != null)
                {
                    setAuth.Name = u.LastName + " " + u.FirstName;
                }
                else
                {
                    setAuth.Name = dataentity.StoreInfoes.Where(s => s.StoreId == u.Id).First().StoreName;
                }

                var uRoles = u.AspNetRoles;
                List<string> roleList = new List<string>();
                foreach (var r in uRoles)
                {
                    roleList.Add(r.Name);
                }
                setAuth.IsAdmin = roleList.Contains("Admin");
                setAuth.IsMaster = roleList.Contains("Master");
                setAuth.IsStore = roleList.Contains("Store");
                setAuth.IsNormal = roleList.Contains("Normal");
                setAuth.IsInactivated = roleList.Contains("Inactivated");

                setAuth.IsBoardDogMaster = (dataentity.Boards.Where(b => b.BoardName == "狗").First().MasterUserID == u.Id) ? true : false;
                setAuth.IsBoardCatMaster = (dataentity.Boards.Where(b => b.BoardName == "貓").First().MasterUserID == u.Id) ? true : false;

                setAuthList.Add(setAuth);
            }


            setAuthViewSource.Source = setAuthList;
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            //this.normalUserViewSource.Source = dataentity.NormalUsers.Local.Where(u=> u.Username.Contains(tbSearchCondition.Text));

            var q = dataentity.AspNetUsers.Local.Where(u => u.UserName.Contains(tbSearchCondition.Text));
            //q.Load();
            this.setAuthViewSource.Source = q.ToList();
            //q.ToList();
            //this.normalUserViewSource.View.Filter(q);
        }

        private void btnPrevious_Click(object sender, RoutedEventArgs e)
        {
            this.setAuthViewSource.View.MoveCurrentToPrevious();
            if (this.setAuthViewSource.View.IsCurrentBeforeFirst)
            {
                this.setAuthViewSource.View.MoveCurrentToLast();
            }
            this.setAuthDataGrid.ScrollIntoView(this.setAuthViewSource.View.CurrentItem);
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            this.setAuthViewSource.View.MoveCurrentToNext();
            if (this.setAuthViewSource.View.IsCurrentAfterLast)
            {
                this.setAuthViewSource.View.MoveCurrentToFirst();
            }
            this.setAuthDataGrid.ScrollIntoView(this.setAuthViewSource.View.CurrentItem);
        }

        private void btnLast_Click(object sender, RoutedEventArgs e)
        {
            this.setAuthViewSource.View.MoveCurrentToLast();
            this.setAuthDataGrid.ScrollIntoView(this.setAuthViewSource.View.CurrentItem);
        }

        private void btnFirst_Click(object sender, RoutedEventArgs e)
        {
            this.setAuthViewSource.View.MoveCurrentToFirst();
            this.setAuthDataGrid.ScrollIntoView(this.setAuthViewSource.View.CurrentItem);
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
        
    }
}
