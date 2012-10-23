// <copyright file="AddItemPage.xaml.cs" company="cematinla.com">
//     Ce matin là. All rights reserved.
// </copyright>
// <author>Léo Davesne</author>

namespace TriathlonChecklist
{
    #region Usings

    using System;

    #endregion Usings

    /// <summary>
    /// AddItemPage class.
    /// </summary>
    public partial class AddItemPage : PageBase
    {
        /// <summary>
        /// Initializes a new instance of the AddItemPage class.
        /// </summary>
        public AddItemPage()
        {
            this.InitializeComponent();
            this.DataContext = App.AddItemViewModel;
            this.Loaded += new System.Windows.RoutedEventHandler(this.AddItemPage_Loaded);
        }

        /// <summary>
        /// The page is loaded: attach the categories.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        protected void AddItemPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (App.AddItemViewModel != null)
            {
                if (!App.AddItemViewModel.IsDataLoaded)
                {
                    App.AddItemViewModel.GetCategories();
                }

                App.AddItemViewModel.InitializeValues();
            }

            // Focus on the text box.
            this.NameTextBox.Focus();
        }

        /// <summary>
        /// Save click linked to the AddItemCommand command.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void SaveBarIconButton_Click(object sender, EventArgs e)
        {
            // Force the binding
            this.NameTextBox.UpdateBinding();

            if (App.AddItemViewModel != null)
            {
                App.AddItemViewModel.AddItemCommand.Execute(null);
            }
        }

        /// <summary>
        /// See the about page.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void AboutAndHelpBarMenuItem_Click(object sender, System.EventArgs e)
        {
            if (App.AddItemViewModel != null)
            {
                App.AddItemViewModel.NavigateTo(new Uri("/Pages/AboutPage.xaml", UriKind.Relative));
            }
        }
    }
}