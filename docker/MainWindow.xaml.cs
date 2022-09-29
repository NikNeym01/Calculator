using DynamicExpresso;
using System.Windows;
using System.Windows.Controls;

namespace docker
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

        private bool defaultMainTbUi = false;
        private bool repeatedSign = false;
        private string memmory = "0"; 

        private void NumberBtn_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            if (button != null)
            {
                if (Main2TbUI.Text.Contains("="))
                {
                    Main2TbUI.Text = "";
                    MainTbUI.Text = "0";
                }

                if (defaultMainTbUi)
                {
                    MainTbUI.Text = "0";
                    defaultMainTbUi = false;
                }

                if (Main2TbUI.Text != "")
                {
                    repeatedSign = true;
                }


                if (MainTbUI.Text.Length == 1 && MainTbUI.Text.Equals("0")) 
                    MainTbUI.Text = "";
                
                
                if (button.Content.Equals("."))
                {
                    if (MainTbUI.Text.Contains('.'))
                        return;

                    if (MainTbUI.Text.Equals(""))
                        MainTbUI.Text += '0';
                }
                
                MainTbUI.Text += button.Content;

            }
        }

        private void LogikBtn_Click(object sender, RoutedEventArgs e)
        {
            var interpreter = new Interpreter();

            Button button = (Button)sender;

            if (button != null)
            {
                switch (button.Content)
                {
                    case "CE":
                        MainTbUI.Text = "0";
                        break;
                    case "C":
                        MainTbUI.Text = "0";
                        Main2TbUI.Text = "";
                        break;
                    case "<--":
                        string num = MainTbUI.Text;
                        MainTbUI.Text = num.Substring(0, num.Length - 1);
                        num = MainTbUI.Text;
                        if (num == "")
                        {
                            MainTbUI.Text = "0";
                        }
                        break;
                    case "MC":
                        MemmoryTbUI.Text = "0";
                        break;
                    case "M+":
                        var result = interpreter.Eval(MemmoryTbUI.Text + "+" + MainTbUI.Text);
                        MemmoryTbUI.Text = $"{result}";
                        break;
                    case "M-":
                        var result2 = interpreter.Eval(MemmoryTbUI.Text + "+" + $"{int.Parse(MainTbUI.Text) * -1}");
                        MemmoryTbUI.Text = $"{result2}";
                        break;
                    case "MR":
                        MainTbUI.Text = MemmoryTbUI.Text;
                        break;
                }
            }
        }

        private void NegativeNumberBtn_Click(object sender, RoutedEventArgs e)
        {
            int num = int.Parse(MainTbUI.Text);
            MainTbUI.Text = $"{num * (-1)}";
        }
        private void PercentBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Main2TbUI.Text != "" && MainTbUI.Text != "0")
            {
                MainTbUI.Text = $"{float.Parse(MainTbUI.Text) / 100}";
            }
        }

        private void OneXBtn_Click(object sender, RoutedEventArgs e)
        {
            Main2TbUI.Text = "1/"+ MainTbUI.Text;
            MainTbUI.Text = $"{1 / float.Parse(MainTbUI.Text)}";
        }

        private void SquareBtn_Click(object sender, RoutedEventArgs e)
        {
            int num = int.Parse(MainTbUI.Text);
            Main2TbUI.Text = $"{num * num}";
            MainTbUI.Text = Main2TbUI.Text;
        }

        private void RootBtn_Click (object sender, RoutedEventArgs e)
        {
            int num = int.Parse(MainTbUI.Text);
            Main2TbUI.Text = $"{num / 2}";
            MainTbUI.Text = Main2TbUI.Text;
        }

        private void PlusBtn_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string num = MainTbUI.Text;
            string num2 = Main2TbUI.Text;
            if (button != null)
            {
                if (num[num.Length - 1] == '.')
                {
                    num = num[..^1];
                    MainTbUI.Text = num;
                }

                if (repeatedSign)
                {
                    var interpreter = new Interpreter();
                    var result = interpreter.Eval(num2 + num);
                    MainTbUI.Text = $"{result}";
                    repeatedSign = false;
                }

                if (num[^1] == '-' || num[num.Length - 1] == '+' || num[num.Length - 1] == '/' || num[num.Length - 1] == '*')
                {
                    MainTbUI.Text = num[..^1];
                    
                }
                Main2TbUI.Text = MainTbUI.Text + button.Content;
                defaultMainTbUi = true;
            }
        }

        private void EquallyBtn_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;

            try
            {
                string num;
                string signs = "";
                string b = "";
                string n = MainTbUI.Text;

                if (n[n.Length - 1] == '.')
                {
                    MainTbUI.Text = n[..^1];
                }
                
                if (Main2TbUI.Text.Contains('='))
                {
                    signs = Main2TbUI.Text;
                    if (signs.Contains('+'))
                    {
                        signs = signs.Split("+")[1];
                        b = "+";
                    }
                    else if (signs.Contains('-'))
                    {
                        signs = signs.Split("-")[1];
                        b = "-";
                    }
                    else if (signs.Contains('*'))
                    {
                        signs = signs.Split("*")[1];
                        b = "*";
                    }
                    else
                    {
                        signs = signs.Split("/")[1];
                        b = "/";
                    }
                    num = MainTbUI.Text + b + signs.Substring(0, signs.Length - 1);
                }
                else
                {
                    num = Main2TbUI.Text + MainTbUI.Text;
                }

                string replacement = "";
                if (button != null)
                {
                    if (num.Contains('/')) replacement = $"{float.Parse(Main2TbUI.Text.Split('/')[0]) / float.Parse(MainTbUI.Text)}";
                    else
                    {
                        var interpreter = new Interpreter();
                        var result = interpreter.Eval(num);
                        replacement = $"{result}";
                    }

                    if (replacement.Contains(','))
                    {
                        replacement = replacement.Split(',')[0] + '.' + replacement.Split(',')[1];
                    }

                    MainTbUI.Text = replacement;
                    Main2TbUI.Text = num + "=";
                }
                repeatedSign = false;
                HistoryTbUI.Text = Main2TbUI.Text + "\n" + MainTbUI.Text + "\n" + HistoryTbUI.Text;
            }
            catch
            {
                Main2TbUI.Text = "";
                MainTbUI.Text = "0";
            }
        }

        private void HistoryButton_Click(object sender, RoutedEventArgs e)
        {
            if (HistoryBarBorderUI.Visibility == Visibility.Visible)
            {
                HistoryBarBorderUI.Visibility = Visibility.Collapsed;
            }
            else
            {
                HistoryBarBorderUI.Visibility = Visibility.Visible;
            }
        }

        private void HistoryMemmory_Click(object sender, RoutedEventArgs e)
        {
            HistoryRb.Visibility = Visibility.Visible;
            MemmoryRb.Visibility = Visibility.Collapsed;
        }

        private void HistoryMemmory2_Click(object sender, RoutedEventArgs e)
        {
            MemmoryRb.Visibility = Visibility.Visible;
            HistoryRb.Visibility = Visibility.Collapsed;
        }
    }
}
