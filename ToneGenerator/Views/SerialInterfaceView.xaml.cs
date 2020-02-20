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
using ToneGenerator.Models;
namespace ToneGenerator
{
    /// <summary>
    /// Interaction logic for SerialInterfaceView.xaml
    /// </summary>
    public partial class SerialInterfaceView : Window
    {   //Build connection with defaults.
        Connection connection = new Connection("COM3",115200,8,"None","One","None");
        public SerialInterfaceView()
        {            
            InitializeComponent();
            //assign data context to the connection view model
            DataContext = new ConnectionViewModel(connection);   
         }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {   //pass the same connection in to update the settings.
            ConnectionView ConnectionView = new ConnectionView();
            ConnectionView.DataContext = new ConnectionViewModel(connection);
            ConnectionView.Show();
        }
    }
}
