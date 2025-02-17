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
        static List<Quiz> quizzes = new();
        static List<int> UserAnswears = new();
        static int currentQuizId = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnLoad_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Szöveges állomány (*.txt)|*.txt|Táblázat (*.csv)|*.csv";
            openFileDialog.Title = "Fájl megnyitása";
            bool? gotValue = openFileDialog.ShowDialog();
            if (gotValue == true)
            {
                try
                {
                    using (StreamReader sr = new StreamReader(openFileDialog.FileName))
                    {
                        while (!sr.EndOfStream)
                        {
                            quizzes.Add(new Quiz(sr.ReadLine()));
                        }
                        MessageBox.Show($"Sikeresen beolvastad: {openFileDialog.SafeFileName}!");
                        NextQuestionLoader();
                    }
                }
                catch (Exception error)
                {
                    MessageBox.Show("Hiba a fájl beolvasásakor: " + error.Message);
                }
            }
        }

        private void NextQuestionLoader()
        {
            if (currentQuizId < quizzes.Count)
            {
                Quiz currentQuiz = quizzes[currentQuizId];
                tbkQuestion.Content = currentQuiz.Question;
                Ans1.Content = currentQuiz.Ans1;
                Ans2.Content = currentQuiz.Ans2;
                Ans3.Content = currentQuiz.Ans3;
            }
        }


        private void BtnGrade_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnQuit_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Biztosan ki akarsz lépni?", "Kilépés", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }


        private void BtnPrevious_Click(object sender, RoutedEventArgs e)
        {
            if (currentQuizId > 0)
            {
                currentQuizId--;
                NextQuestionLoader();
            }
        }


        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("");
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            currentQuizId++;
            NextQuestionLoader();
        }


    }
}