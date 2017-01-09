using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Num2Korean_Duke
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void TranslateButton_Click(object sender, RoutedEventArgs e)
        {
            Functions F = new Functions();
            string Checking = F.InsertCheck(InsertBox.Text);
            int[] NumArr = new int[4];

            if (Checking == InsertBox.Text)
            {
                NumArr = F.NumCheck(int.Parse(Checking));
                ResultBox.Text = F.Translate(NumArr);
            }
            else
                ResultBox.Text = Checking;      
        }

        private void InsertBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (InsertBox.Text == "Insert The Num") InsertBox.Text = "";
        }

        private void InsertBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (InsertBox.Text == "") InsertBox.Text = "Insert The Num";
        }

        private void InsertBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
                TranslateButton_Click(sender, e);
        }
    }

    public class Functions
    {
        public string InsertCheck(string i)
        {
            int Num;
            if (int.TryParse(i, out Num))
            {
                if (Num >= 1000)
                {
                    return "Too Big";
                }
                else if (Num <= -1000)
                {
                    return "Too Small";
                }
                else
                    return i;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(i) || i == "Insert The Num")
                    return "No Insert";
                else
                    return "Wrong Insert";
            }
        }

        public int[] NumCheck(int Num)
        {
            int[] NumArr = new int[4];

            if (Num > 0)
                NumArr[0] = 1;
            else if (Num == 0)
                NumArr[0] = 0;
            else
                NumArr[0] = -1;

            Num = Math.Abs(Num);

            NumArr[1] = Num / 100;
            NumArr[2] = (Num % 100) / 10;
            NumArr[3] = (Num % 10) / 1;

            return NumArr;
        }

        public string Translate(int[] arr)
        {
            string Korean = "";
            string[] KoreanNum = { "", "일", "이", "삼", "사", "오", "육", "칠", "팔", "구" };
            switch (arr[0])
            {
                case -1:
                    Korean = "마이너스 ";break;
                case 0:
                    Korean = "영";return Korean;
                case 1:
                    break;
            }
            for(int i =1; i < 4; i++)
            {
                if ((arr[1] == 1 && i == 1) || (arr[2] == 1 && i == 2));
                else
                    Korean = Korean + KoreanNum[arr[i]];
                if (arr[1] != 0 && i == 1) Korean = Korean + "백";
                else if (arr[2] != 0 && i == 2) Korean = Korean + "십";                                  
            }
            return Korean;
        }
    }
}
