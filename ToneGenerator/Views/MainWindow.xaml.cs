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
using ToneGenerator.ViewModels;
using ToneGenerator.Views;

namespace ToneGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ConnectionViewModel cvm;
        public MainWindow()
        {
            InitializeComponent();
            cvm = new ConnectionViewModel("Comm1", 100, 1, "None", 0, "None");
            DataContext = cvm;
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            ConnectionView ConnectionView = new ConnectionView();
            ConnectionView.DataContext = cvm;
            ConnectionView.Show();

        }
    }
}
