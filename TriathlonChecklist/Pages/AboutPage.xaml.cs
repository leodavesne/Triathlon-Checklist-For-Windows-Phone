// <copyright file="AboutPage.xaml.cs" company="cematinla.com">
//     Ce matin là. All rights reserved.
// </copyright>
// <author>Léo Davesne</author>

namespace TriathlonChecklist
{
    #region Usings

    using Microsoft.Phone.Tasks;

    #endregion Usings

    /// <summary>
    /// AboutPage class: tips, about, history and featured.
    /// </summary>
    public partial class AboutPage : PageBase
    {
        /// <summary>
        /// Initializes a new instance of the AboutPage class.
        /// </summary>
        public AboutPage()
        {
            this.InitializeComponent();
            this.DataContext = App.AboutViewModel;
        }

        /// <summary>
        /// Contact by email.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void ContactUs_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            EmailComposeTask emailComposeTask = new EmailComposeTask();

            // TODO: feedback
            ////emailComposeTask.Subject = "WP Triathlon Checklist feedback";

            // TODO: add phone model
            ////emailComposeTask.Subject += ;

            // TODO: add Windows Phone version
            ////emailComposeTask.Subject += ;

            // TODO: add Triathlon Checklist version
            ////emailComposeTask.Subject += ;

            // TODO: add Carrier
            ////emailComposeTask.Subject += ;

            emailComposeTask.To = "contact@cematinla.com";
            emailComposeTask.Show();
        }

        /// <summary>
        /// Rate and review the application.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void RateOurWork_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MarketplaceReviewTask marketplaceReviewTask = new MarketplaceReviewTask();
            marketplaceReviewTask.Show();
        }
    }
}