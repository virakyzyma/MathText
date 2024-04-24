using System;
using System.Drawing;
using System.Windows.Forms;


namespace NewHw
{
    public partial class Form1 : Form
    {
        private Label questionLabel;
        private TextBox answerTextBox;
        private Button checkAnswerButton;
        private ComboBox levelComboBox;

        private readonly Random random = new Random();
        private int currentQuestionIndex = -1;
        private int correctAnswersCount = 0;

        private string[] selectedQuestions;
        private readonly string[] easyLevel = { "2+2", "3*3", "5-1", "10/2", "7+2", "9-6", "5*2", "20/5", "8+3", "15-9" };
        private readonly string[] mediumLevel = { "8+6", "12-5", "4*7", "45/5", "18+9", "30-12", "6*8", "64/8", "15+20", "40-23" };
        private readonly string[] hardLevel = { "24 + (7*5) - (42/6)", "(36/9) + (4*3) - 5", "(25/5) + (6*3) - Sqrt(49)",
            "(8*4) - (64/8) + 11", "(64/8) + (9*2) - Sqrt(64)", "Sqrt(16) + (4*23) - (33/11)", "12 -(36/6) + (4*8)",
            "(28/7) * (5+1) - Sqrt(81)", "(45/9) * (6+1) - 9", "9 + (4*5) - (30/6)" };

        public Form1()
        {
            InitializeComponent();

            InitializeControls();
            ShowNextQuestion();
            (Width, Height) = (270, 250);
        }
        private void InitializeControls()
        {
            selectedQuestions = easyLevel;

            levelComboBox = new ComboBox()
            {
                Location = new Point(20, 30),
                Width = 200
            };
            levelComboBox.Items.AddRange(new string[]
            {
                "Easy", "Medium", "Hard"
            });
            levelComboBox.SelectedIndex = 0;
            levelComboBox.SelectedIndexChanged += LevelComboBox_SelectedIndexChanged;

            questionLabel = new Label()
            {
                Text = "Question",
                Location = new Point(20, 70),
                AutoSize = true
            };
            answerTextBox = new TextBox()
            {
                Location = new Point(20, 100),
                Width = 200
            };
            checkAnswerButton = new Button()
            {
                Text = "Check Answer",
                Location = new Point(20, 130),
                Width = 200
            };
            checkAnswerButton.Click += CheckAnswerButton_Click;

            Controls.Add(levelComboBox);
            Controls.Add(questionLabel);
            Controls.Add(answerTextBox);
            Controls.Add(checkAnswerButton);
        }

        private void LevelComboBox_SelectedIndexChanged(object? sender, EventArgs e)
        {
            switch (levelComboBox.SelectedIndex)
            {
                case 0:
                    selectedQuestions = easyLevel;
                    break;
                case 1:
                    selectedQuestions = mediumLevel;
                    break;
                case 2:
                    selectedQuestions = hardLevel;
                    break;
            }
            currentQuestionIndex = -1;
            correctAnswersCount = 0;
            ShowNextQuestion();
        }

        private void ShowNextQuestion()
        {
            currentQuestionIndex++;
            if (currentQuestionIndex >= selectedQuestions.Length)
            {
                MessageBox.Show($"Test completed. Correct answers: {correctAnswersCount}/{selectedQuestions.Length}");
                return;
            }
            questionLabel.Text = selectedQuestions[currentQuestionIndex];
            answerTextBox.Text = "";
        }

        private void CheckAnswerButton_Click(object? sender, EventArgs e)
        {
            int userAnswer;
            if (!int.TryParse(answerTextBox.Text, out userAnswer))
            {
                MessageBox.Show("Enter a valid number");
                return;
            }

            int expectedResult = EvaluateExpression(selectedQuestions[currentQuestionIndex]);
            if (userAnswer == expectedResult)
            {
                MessageBox.Show("That's correct");
                correctAnswersCount++;
            }
            else
            {
                MessageBox.Show($"Wrong answer. The correct answer is {expectedResult}.");
            }
            ShowNextQuestion();
        }
        private int EvaluateExpression(string expression)
        {
            return (int)new System.Data.DataTable().Compute(expression, null);
        }
        private void Form1_Load(object sender, EventArgs e) { }
    }
}