using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
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

namespace HgSoftware.TestingPictureUi
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowView : Window
    {
        [DllImport("user32.dll", EntryPoint = "SystemParametersInfo")]
        public static extern int SystemParametersInfo(UAction uAction, int uParam, StringBuilder lpvParam, int fuWinIni);

        public enum UAction
        {
            /// <summary>
            /// set the desktop background image
            /// </summary>
            SPI_SETDESKWALLPAPER = 0x0014,

            /// <summary>
            /// set the desktop background image
            /// </summary>
            SPI_GETDESKWALLPAPER = 0x0073,
        }

        public MainWindowView()
        {
            InitializeComponent();
        }

        private string _currentBackground;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SetBackgroud(_currentBackground);
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _currentBackground = GetBackgroud();
            SetBackgroud($"{AppContext.BaseDirectory}Resources\\TestPattern.jpg");
        }

        public string GetBackgroud()
        {
            StringBuilder s = new StringBuilder(300);
            SystemParametersInfo(UAction.SPI_GETDESKWALLPAPER, 300, s, 0);
            return s.ToString();
        }

        /// <summary>
        /// set the desktop background image
        /// </summary>
        /// <param name="fileName">the path of image</param>
        /// <returns></returns>
        public int SetBackgroud(string fileName)
        {
            int result = 0;
            if (File.Exists(fileName))
            {
                StringBuilder s = new StringBuilder(fileName);
                result = SystemParametersInfo(UAction.SPI_SETDESKWALLPAPER, 0, s, 0x2);
            }
            return result;
        }

        /// <summary>
        /// set the option of registry
        /// </summary>
        /// <param name="optionsName">the name of registry</param>
        /// <param name="optionsData">set the data of registry</param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public bool SetOptions(string optionsName, string optionsData, ref string msg)
        {
            bool returnBool = true;
            RegistryKey classesRoot = Registry.CurrentUser;
            RegistryKey registryKey = classesRoot.OpenSubKey(@"Control Panel\Desktop", true);
            try
            {
                if (registryKey != null)
                {
                    registryKey.SetValue(optionsName.ToUpper(), optionsData);
                }
                else
                {
                    returnBool = false;
                }
            }
            catch
            {
                returnBool = false;
                msg = "Error when read the registry";
            }
            finally
            {
                classesRoot.Close();
                registryKey.Close();
            }
            return returnBool;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            dialog.IsOpen = true;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            dialog.IsOpen = false;
        }
    }
}