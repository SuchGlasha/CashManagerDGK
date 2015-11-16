using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace CashMana.Converters
{
    public class IncomeBackground : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();

            switch ((bool)value)
            {
                case true:
                    mySolidColorBrush.Color = Color.FromArgb(255, 215, 255, 145);

                    break;
                case false:
                    mySolidColorBrush.Color = Color.FromArgb(255, 255, 145, 145);

                    break;
          
            }
            return mySolidColorBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
