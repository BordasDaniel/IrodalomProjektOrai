using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IrodalomProjektOrai
{
    public class Quiz
    {

        public string Question { get; set; }
        public string Ans1 { get; set; }
        public string Ans2 { get; set; }
        public string Ans3 { get; set; }
        public bool Ans1IsCorrect { get; set; }
        public bool Ans2IsCorrect { get; set; }
        public bool Ans3IsCorrect { get; set; }

        public Quiz(string question, string ans1, string ans2, string ans3, bool ans1IsCorrect, bool ans2IsCorrect, bool ans3IsCorrect)
        {
            Question = question;
            Ans1 = ans1;
            Ans2 = ans2;
            Ans3 = ans3;
            Ans1IsCorrect = ans1IsCorrect;
            Ans2IsCorrect = ans2IsCorrect;
            Ans3IsCorrect = ans3IsCorrect;
        }

       public Quiz(string line)
        {
            string[] parts = line.Split(';');
            Question = parts[0];
            Ans1 = parts[1];
            Ans2 = parts[2];
            Ans3 = parts[3];
            Ans1IsCorrect = bool.Parse(parts[4]);
            Ans2IsCorrect = bool.Parse(parts[5]);
            Ans3IsCorrect = bool.Parse(parts[6]);
        }


    }
}
