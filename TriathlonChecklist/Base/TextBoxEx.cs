// <copyright file="TextBoxEx.cs" company="cematinlà.com">
//     Ce matin là. All rights reserved.
// </copyright>
// <author>Léo Davesne</author>

namespace TriathlonChecklist
{
    #region Usings

    using System.Windows.Controls;
    using System.Windows.Data;
    using Microsoft.Phone.Controls;
    
    #endregion Usings

    /// <summary>
    /// TextBoxEx class
    /// </summary>
    public static class TextBoxEx
    {
        /// <summary>
        /// Update the binding
        /// </summary>
        /// <param name="textBox">The TextBox</param>
        public static void UpdateBinding(this TextBox textBox)
        {
            BindingExpression bindingExpression = textBox.GetBindingExpression(TextBox.TextProperty);
            if (bindingExpression != null)
            {
                bindingExpression.UpdateSource();
            }
        }
    }
}
