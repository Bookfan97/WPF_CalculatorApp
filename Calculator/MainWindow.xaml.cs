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

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double lastNumber, result;
        SelectedOperators selectedOperator;

        public MainWindow()
        {
            InitializeComponent();
            //resultLabel.Content = "8675309";
            
            //Function Buttons
            ACButton.Click += ACButton_Click;
            signChangeButton.Click += SignChangeButton_Click;
        }

        private void SignChangeButton_Click(object sender, RoutedEventArgs e)
        {
            if(double.TryParse(resultLabel.Content.ToString(), out lastNumber))
            {
                lastNumber = lastNumber * -1;
                resultLabel.Content = lastNumber.ToString();
            }
        }

        private void EqualButton_Click(object sender, RoutedEventArgs e)
        {
            double resultNum;
            if(double.TryParse(resultLabel.Content.ToString(), out resultNum))
            {
                switch(selectedOperator)
                { 
                    case SelectedOperators.Addition:
                        result = SimpleMath.Add(lastNumber, resultNum);
                    break;
                    case SelectedOperators.Subtraction:
                        result = SimpleMath.Subtract(lastNumber, resultNum);
                    break;
                    case SelectedOperators.Multiplication:
                        result = SimpleMath.Multiply(lastNumber, resultNum);
                    break;
                    case SelectedOperators.Division:
                        result = SimpleMath.Divide(lastNumber, resultNum);
                    break;
                }
                resultLabel.Content = result.ToString();
            }
        }

        private void PercentButton_Click(object sender, RoutedEventArgs e)
        {
            double tempNumber;
            if(double.TryParse(resultLabel.Content.ToString(), out tempNumber))
            {
                tempNumber /= 100;
                if(lastNumber!=0)
                {
                    tempNumber *= lastNumber;
                }
                resultLabel.Content = tempNumber.ToString();
            }
        }

        private void ACButton_Click(object sender, RoutedEventArgs e)
        {
            resultLabel.Content="0";
            result = 0;
            lastNumber=0;
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            //Previous value saved as out variable
            if(double.TryParse(resultLabel.Content.ToString(), out lastNumber))
            {
                resultLabel.Content = "0";
            }
            if(sender == multiplyButton)
            {
                selectedOperator = SelectedOperators.Multiplication;
            }
            if(sender == divideButton)
            {
                selectedOperator = SelectedOperators.Division;
            }
            if(sender == plusButton)
            {
                selectedOperator = SelectedOperators.Addition;
            }
            if(sender == minusButton)
            {
                selectedOperator = SelectedOperators.Subtraction;
            }
        }

        private void periodButton_Click(object sender, RoutedEventArgs e)
        {
            if(!resultLabel.Content.ToString().Contains("."))
            { 
                resultLabel.Content = $"{resultLabel.Content}.";
            }
        }

        private void numberButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedValue =  int.Parse((sender as Button).Content.ToString());
            
            if(resultLabel.Content.ToString() == "0")
            {
                resultLabel.Content = $"{selectedValue}";
            }
            else
            {
                resultLabel.Content = $"{resultLabel.Content}{selectedValue}";
            }
        }
    }

    public enum SelectedOperators
    {
        Addition,
        Subtraction,
        Multiplication,
        Division
    }

    public class SimpleMath
    {
        public static double Add( double n1, double n2)
        {
            return n1 + n2;
        }
        public static double Subtract( double n1, double n2)
        {
            return n1 - n2;
        }
        public static double Multiply( double n1, double n2)
        {
            return n1 * n2;
        }
        public static double Divide( double n1, double n2)
        {
            if(n2 == 0)
            {
                MessageBox.Show("DIVIDE BY ZERO ERROR", "WRONG OPERATION", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0;
            }
            return n1 / n2;
        }
    }
}
