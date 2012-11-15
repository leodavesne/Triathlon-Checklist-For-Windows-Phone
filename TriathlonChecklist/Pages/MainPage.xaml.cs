// <copyright file="MainPage.xaml.cs" company="cematinlà.com">
//     Ce matin là. All rights reserved.
// </copyright>
// <author>Léo Davesne</author>

namespace TriathlonChecklist
{
    #region Usings

    using System;

    #endregion Usings

    /// <summary>
    /// MainPage class
    /// </summary>
    public partial class MainPage : PageBase
    {
        /// <summary>
        /// Initializes a new instance of the MainPage class
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();
            this.DataContext = App.MainViewModel;
            this.Loaded += new System.Windows.RoutedEventHandler(this.MainPage_Loaded);
        }

        /// <summary>
        /// The page is loaded: attach the data
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The e</param>
        protected void MainPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            App.GetData();
        }

        /// <summary>
        /// Add an item click
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The e</param>
        private void AddItemBarMenuItem_Click(object sender, System.EventArgs e)
        {
            if (App.MainViewModel != null)
            {
                App.MainViewModel.NavigateTo(new Uri("/Pages/AddItemPage.xaml", UriKind.Relative));
            }
        }

        /// <summary>
        /// Reinitialize all lists
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The e</param>
        private void ReinitializeBarMenuItem_Click(object sender, System.EventArgs e)
        {
            if (App.MainViewModel != null)
            {
                App.MainViewModel.ReinitializeDatabase();
            }
        }

        /// <summary>
        /// See the about page.
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The e</param>
        private void AboutAndHelpBarMenuItem_Click(object sender, System.EventArgs e)
        {
            if (App.MainViewModel != null)
            {
                App.MainViewModel.NavigateTo(new Uri("/Pages/AboutPage.xaml", UriKind.Relative));
            }
        }
    }
}