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
        static List<(int, bool)> UserAnswears = new();
        static int currentQuizId = 0;
        static int GoodAns = 0;
        static int BadAns = 0;

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
                    quizzes.Clear();
                    UserAnswears.Clear(); // Fontos, hogy töröljük az előző válaszokat

                    using (StreamReader sr = new StreamReader(openFileDialog.FileName))
                    {
                        while (!sr.EndOfStream)
                        {
                            quizzes.Add(new Quiz(sr.ReadLine()));
                        }
                    }

                    for (int i = 0; i < quizzes.Count; i++)
                    {
                        UserAnswears.Add((i, false));
                    }

                    MessageBox.Show($"Sikeresen beolvastad: {openFileDialog.SafeFileName}!");
                    currentQuizId = 0; // Az első kérdésre kell ugrani
                    NextQuestionLoader();
                }
                catch (Exception error)
                {
                    MessageBox.Show("Hiba a fájl beolvasásakor: " + error.Message);
                }
            }
        }


        private void NextQuestionLoader()
        {
            if (quizzes.Count == 0)
            {
                MessageBox.Show("Nincsenek betöltött kérdések!", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (currentQuizId < quizzes.Count)
            {
                Quiz currentQuiz = quizzes[currentQuizId];
                tbkQuestion.Content = currentQuiz.Question;
                Ans1.Content = currentQuiz.Ans1;
                Ans2.Content = currentQuiz.Ans2;
                Ans3.Content = currentQuiz.Ans3;

                // Clean the checkboxes
                Ans1.IsChecked = false;
                Ans2.IsChecked = false;
                Ans3.IsChecked = false;
            }
            else
            {
                MessageBox.Show("Nincs több kérdés!");
            }
        }



        private void BtnGrade_Click(object sender, RoutedEventArgs e)
        {
            if (UserAnswears.Count == 0)
            {
                MessageBox.Show("Nem olvastál még be fájlt!"); return;
            }

            if (UserAnswears.Count < quizzes.Count)
            {
                MessageBoxResult result = MessageBox.Show("Még nem válaszoltál az összes kérdésre, biztos ki akarod értékelni?", "Figyelmeztetés", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.No) return;
            }

            GoodAns = 0;
            BadAns = 0;
            TbkResults.Text = "";
            for (int i = 0; i < quizzes.Count; i++)
            {
                TbkResults.Text += $"{i}. Kérdés: ";
                TbkResults.Text += UserAnswears[i].Item2 ? "Helyes válasz" : "Helytelen válasz";
                TbkResults.Text += "\n";
                if(UserAnswears[i].Item2)
                {
                    GoodAns++;
                } else
                {
                    BadAns++;
                }
            }
            TbkResults.Text += $"\n {GoodAns} / {BadAns + GoodAns}";
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
            else
            {
                MessageBox.Show("Nincs előző kérdés!");
            }
        }



        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (UserAnswears.Count == 0)
            {
                MessageBox.Show("Nem olvastál még be fájlt!");
            }
            else
            {
                Quiz currentQuiz = quizzes[currentQuizId];
                if (Ans1.IsChecked == true && currentQuiz.Ans1IsCorrect)
                {
                    UserAnswears[currentQuizId] = (currentQuizId, true);
                    MessageBox.Show("Sikeresen mentve lett a válaszod!");
                }
                else if (Ans2.IsChecked == true && currentQuiz.Ans2IsCorrect)
                {
                    UserAnswears[currentQuizId] = (currentQuizId, true);
                    MessageBox.Show("Sikeresen mentve lett a válaszod!");

                }
                else if (Ans3.IsChecked == true && currentQuiz.Ans3IsCorrect)
                {
                    UserAnswears[currentQuizId] = (currentQuizId, true);
                    MessageBox.Show("Sikeresen mentve lett a válaszod!");

                }
                else if (Ans1.IsChecked == false && Ans2.IsChecked == false && Ans3.IsChecked == false)
                {
                    MessageBox.Show("Nem válaszoltál egyik kérdésre sem!");
                }
                else
                {
                    UserAnswears.Add((currentQuizId, false));
                    MessageBox.Show("Sikeresen mentve lett a válaszod!");
                }
            }

        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            if (currentQuizId+1 < quizzes.Count)
            {
                currentQuizId++;
                NextQuestionLoader();
            }
            else
            {
                MessageBox.Show($"Nincs több kérdés!");
            }

        }
    }
}