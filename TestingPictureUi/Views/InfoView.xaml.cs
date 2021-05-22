using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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

namespace HgSoftware.TestingPictureUi.Views
{
    /// <summary>
    /// Interaction logic for InfoView.xaml
    /// </summary>
    public partial class InfoView : UserControl
    {
        public InfoView()
        {
            InitializeComponent();
            Version.Content = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            PublishDate.Content = File.GetCreationTime(Assembly.GetExecutingAssembly().Location).ToString().Split(' ')[0];
            License.Text = File.ReadAllText(($"{AppContext.BaseDirectory}/License.txt"));
        }
    }
}