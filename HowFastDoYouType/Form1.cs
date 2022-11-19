using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HowFastDoYouType
{
    public partial class Form1 : Form
    {
        int whichWord = 0;
        double correctWords = 0;
        double wrongWords = 0;
        int seconds = 60;
        int secondsCalculate = 60;
        bool check = true;


        string filePath = @"C:\Users\90538\source\repos\HowFastDoYouType\200Words\200Words.txt";

        Random randomNumber = new Random();
        List<string> Words = new List<string>();
        List<System.Windows.Forms.Label> labelList = new List<System.Windows.Forms.Label>();
        

        //adding all the labels to labelList
        public void SetList()
        {
            labelList.Add(label1);
            labelList.Add(label2);
            labelList.Add(label3);
            labelList.Add(label4);
            labelList.Add(label5);
            labelList.Add(label6);
            labelList.Add(label7);
            labelList.Add(label8);
            labelList.Add(label9);
            labelList.Add(label10);
            labelList.Add(label11);
            labelList.Add(label12);
            labelList.Add(label13);
            labelList.Add(label14);
            labelList.Add(label15);
            labelList.Add(label16);
            labelList.Add(label17);
            labelList.Add(label18);
        }
        //changing all the label texts to a random word in the txt file
        public void GetRandomWords()
        {
            foreach (var item in labelList)
            {
                item.Text = Words[randomNumber.Next(0, 200)];
            }
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Words = File.ReadAllLines(filePath).ToList();
            SetList();
            GetRandomWords();

        }
        
        //the function that decides the color of the words and calculates the correct/wrong words
        public void changeColor(int i)
        {

            if (userTextBox.Text == labelList[i].Text || userTextBox.Text == " " + labelList[i].Text)
            {
                labelList[i].ForeColor = Color.Green;
                labelList[i].BackColor = Color.Transparent;
                if (i < 17)
                {
                    labelList[i + 1].BackColor = Color.Gray;
                }
                i++;
                whichWord = i;
                userTextBox.Text = string.Empty;
                correctWords++;
            }
            else
            {
                labelList[i].ForeColor = Color.Red;
                labelList[i].BackColor = Color.Transparent;
                if (i < 17)
                {
                    labelList[i + 1].BackColor = Color.Gray;
                }
                i++;
                whichWord = i;
                userTextBox.Text = string.Empty;
                wrongWords++;
            }

            if (whichWord%18 == 0 && whichWord != 0)
            {
                GetRandomWords();
                whichWord = 0;
                foreach (var item in labelList)
                {
                    item.ForeColor = Color.Black;
                }
            }

        }
        // the function that starts counting down
        public void StartCountDown()
        {
            
            
                countDownTimer.Start();
            
        }

        //the function to see whether a key is pressed or not in userTextBox
        private void textBoxKeyPress(object sender, KeyPressEventArgs e)
        {
            if (check)
            {
                StartCountDown();
            }
            if (e.KeyChar == Convert.ToChar(Keys.Space))
            {
                
                changeColor(whichWord);
            }

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        //countdown timer
        private void countDownTimer_Tick(object sender, EventArgs e)
        {
            seconds--;
            textTimer.Text = seconds.ToString();
            if (seconds == 0)
            {
                GameOver();
                countDownTimer.Stop();
            }
        }

        //controls what happens when the game is over
        public void GameOver()
        {
            check = false;
            countDownTimer.Stop();
            lblResult.Text = WPMCalculator().ToString() + " WPM";
            lblCorrectWordsCount.Text = correctWords.ToString();
            lblWrongWordsCount.Text = wrongWords.ToString();
            lblAccuracyAmount.Text = "%" + AccuracyCalculator().ToString();
            labelPanel.Hide();
        }

        //controls what happens when you want to restart the game
        public void Restart()
        {

            countDownTimer.Stop();
            labelPanel.Show();
            lblCorrectWordsCount.Text = "0";
            lblWrongWordsCount.Text = "0";
            lblAccuracyAmount.Text = "0";
            lblResult.Text = "0";
            GetRandomWords();
            textTimer.Text = "60";
            seconds = 60;
            foreach (var item in labelList)
            {
                item.ForeColor = Color.Black;
            }
            labelList[whichWord].BackColor = Color.Transparent;
            whichWord = 0;

            userTextBox.Text = String.Empty;
        }

        //word per minute calculator
        public int WPMCalculator()
        {
            int result;
            result = Convert.ToInt32((correctWords / (secondsCalculate / 60)));
            return result;
        }

        //accuracy calculator
        public double AccuracyCalculator()
        {
            double result;
            result = (correctWords / (correctWords + wrongWords)) * 100;
            return Math.Round(result, 2);
        }
       

        private void column_Click(object sender, EventArgs e)
        {

        }

        private void lblCorrectWords_Click(object sender, EventArgs e)
        {

        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            check = true;
            Restart();
        }

       
    }
}
