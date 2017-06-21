using MahApps.Metro.Controls.Dialogs;
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
    /// UC_Login.xaml 的互動邏輯
    /// </summary>
    public partial class UC_Login : UserControl
    {
        public UC_Login()
        {
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            switch (AdminUtilities.ValidateUser(tbUsername.Text, tbPassword.Password))
            {
                case AdminUtilities.LogonErrorTypes.ValidUser:
                    Window mainWindow = Application.Current.MainWindow;
            MainWindow.CurrentUser = tbUsername.Text;
            await ((MainWindow)mainWindow).ShowMessageAsync("毛孩隊長寵物網後臺管理介面", $"網站管理員【{MainWindow.CurrentUser}】您好!", MessageDialogStyle.Affirmative, new MetroDialogSettings { ColorScheme = MetroDialogColorScheme.Accented, AffirmativeButtonText = "開始", DialogTitleFontSize = 48 });

            ((MainWindow)mainWindow).showGrid.Children.Clear();
                    MainWindow.mainUC = new UC_Index();
                    ((MainWindow)mainWindow).showGrid.Children.Add(MainWindow.mainUC);

                    break;
                case AdminUtilities.LogonErrorTypes.LackOfUsername:
                    MessageBox.Show("請輸入使用者名稱。");
                    break;
                case AdminUtilities.LogonErrorTypes.LackOfPassword:
                    MessageBox.Show("請輸入密碼。");
                    break;
                case AdminUtilities.LogonErrorTypes.InvalidUser:
                    MessageBox.Show("密碼錯誤", "警告訊息", MessageBoxButton.OK, MessageBoxImage.Stop);
                    break;
                case AdminUtilities.LogonErrorTypes.UserNotExist:
                    MessageBox.Show("使用者不存在。");
                    break;
                case AdminUtilities.LogonErrorTypes.Unauthorized:
                    MessageBox.Show("您沒有管理者權限。");
                    break;
                default:
                    break;
            }
        }

        private void btnDemo_Click(object sender, RoutedEventArgs e)
        {
            tbUsername.Text = "admin@mao.com";
            tbPassword.Password = "qqq123";
        }
    }
}
