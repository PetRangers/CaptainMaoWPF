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
    /// UC_Index.xaml 的互動邏輯
    /// </summary>
    public partial class UC_Index : UserControl
    {
        public UC_Index()
        {
            InitializeComponent();
        }

        private void btnEditNormalUserProfile_Click(object sender, RoutedEventArgs e)
        {
            gridAdministrator.Children.Clear();
            UC_EditNormalUser uc = new UC_EditNormalUser();
            gridAdministrator.Children.Add(uc);
        }

        private void btnViewWebStat_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnEditUserAuth_Click(object sender, RoutedEventArgs e)
        {
gridAdministrator.Children.Clear();
            UC_SetAuth uc = new UC_SetAuth();
            gridAdministrator.Children.Add(uc);
        }
    }
}
