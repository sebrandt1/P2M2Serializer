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
using System.Windows.Shapes;

namespace InputRecordingEditor.UI
{
    /// <summary>
    /// Interaction logic for QuestionAnswerPrompt.xaml
    /// </summary>
    public partial class QuestionAnswerPrompt : Window
    {
        public QuestionAnswerPrompt()
        {
            InitializeComponent();
        }

        public QuestionAnswerPrompt(string question, object answerPlaceHolder, string buttonText)
        {
            InitializeComponent();
            QuestionTextBlock.Text = question;
            AnswerTextBox.Text = answerPlaceHolder.ToString();
            AcceptButton.Content = buttonText;
        }

        private void AcceptButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
