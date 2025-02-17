using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;

namespace IrodalomProjektOrai
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private static void LoadFromFiles(string FileNameWPath)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Szöveges állomány (*.txt)|*.txt|Táblázat (*.csv)|*.csv";
            openFileDialog.Title = "Fájl megnyitása";
            bool? gotValue = openFileDialog.ShowDialog();
            if (gotValue == true)
            {
                
            }
        }

        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnGrade_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnQuit_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("");
        }
        private void BtnPrevious_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("");
        }


        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("");
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("");
        }


    }
}