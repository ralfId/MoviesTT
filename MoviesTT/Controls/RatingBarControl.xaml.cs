using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Shapes;
using Xamarin.Forms.Xaml;

namespace MoviesTT.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RatingBarControl
    {

        public RatingBarControl()
        {
            InitializeComponent();

            star1.Fill = new SolidColorBrush(Color.FromHex("#805b10"));
            star2.Fill = new SolidColorBrush(Color.FromHex("#805b10"));
            star3.Fill = new SolidColorBrush(Color.FromHex("#805b10"));
            star4.Fill = new SolidColorBrush(Color.FromHex("#805b10")); 
            star5.Fill = new SolidColorBrush(Color.FromHex("#805b10"));

        }

        public static readonly BindableProperty MovieScoreProperty =
            BindableProperty.Create(nameof(MovieScore), typeof(double), typeof(RatingBarControl),null, propertyChanged: FillScore);

       
        public double MovieScore
        {
            get { return (double)GetValue(MovieScoreProperty); }
            set { SetValue(MovieScoreProperty, value); }
        }

        private static void FillScore(BindableObject bindable, object oldValue, object newValue)
        {
            var controls = bindable as RatingBarControl;
            if ((double)newValue > 0)
            {
                var score = (double)newValue / 2;
                var numberStar = Convert.ToInt32(score);
                switch (numberStar)
                {
                    case 1:
                        controls.star1.Fill = new SolidColorBrush(Color.FromHex("#ffd60a"));
                        break;
                    case 2:
                        controls.star1.Fill = new SolidColorBrush(Color.FromHex("#ffd60a"));
                        controls.star2.Fill = new SolidColorBrush(Color.FromHex("#ffd60a"));
                        break;
                    case 3:
                        controls.star1.Fill = new SolidColorBrush(Color.FromHex("#ffd60a"));
                        controls.star2.Fill = new SolidColorBrush(Color.FromHex("#ffd60a"));
                        controls.star3.Fill = new SolidColorBrush(Color.FromHex("#ffd60a"));
                        break;
                    case 4:
                        controls.star1.Fill = new SolidColorBrush(Color.FromHex("#ffd60a"));
                        controls.star2.Fill = new SolidColorBrush(Color.FromHex("#ffd60a"));
                        controls.star3.Fill = new SolidColorBrush(Color.FromHex("#ffd60a"));
                        controls.star4.Fill = new SolidColorBrush(Color.FromHex("#ffd60a"));
                        break;
                    case 5:
                        controls.star1.Fill = new SolidColorBrush(Color.FromHex("#ffd60a"));
                        controls.star2.Fill = new SolidColorBrush(Color.FromHex("#ffd60a"));
                        controls.star3.Fill = new SolidColorBrush(Color.FromHex("#ffd60a"));
                        controls.star4.Fill = new SolidColorBrush(Color.FromHex("#ffd60a"));
                        controls.star5.Fill = new SolidColorBrush(Color.FromHex("#ffd60a"));
                        break;
                }
            }
        }

     

    }
}