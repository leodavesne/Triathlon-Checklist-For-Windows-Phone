// <copyright file="CheckBoxConverterHelper.cs" company="cematinlà.com">
//     Ce matin là. All rights reserved.
// </copyright>
// <author>Léo Davesne</author>

namespace TriathlonChecklist.Helper
{
    #region Usings

    using System;
    using System.Windows.Media;

    #endregion Usings

    /// <summary>
    /// Check box converter class
    /// </summary>
    public class CheckBoxConverterHelper : System.Windows.Data.IValueConverter
    {
        /// <summary>
        /// Convert a check box
        /// </summary>
        /// <param name="value">The value</param>
        /// <param name="targetType">The target type</param>
        /// <param name="parameter">The parameter</param>
        /// <param name="culture">The culture</param>
        /// <returns>An object</returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            bool isChecked = (bool)value;
            return isChecked ? new SolidColorBrush(Color.FromArgb(255, 117, 184, 236)) : new SolidColorBrush(Colors.White);
        }

        /// <summary>
        /// Convert back a check box
        /// </summary>
        /// <param name="value">The value</param>
        /// <param name="targetType">The target type</param>
        /// <param name="parameter">The parameter</param>
        /// <param name="culture">The culture</param>
        /// <returns>An object</returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
