// <copyright file="PageBase.cs" company="cematinla.com">
//     Ce matin là. All rights reserved.
// </copyright>
// <author>Léo Davesne</author>

namespace TriathlonChecklist
{
    #region Usings

    using Microsoft.Phone.Controls;
    using TriathlonChecklist.ViewModel;
    
    #endregion Usings

    /// <summary>
    /// PageBase class.
    /// </summary>
    public abstract class PageBase : PhoneApplicationPage
    {
        /// <summary>
        /// On navigated to overridden.
        /// </summary>
        /// <param name="e">The e.</param>
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            ViewModelBase viewModel = this.DataContext as ViewModelBase;
            viewModel.Initialize(this.NavigationContext.QueryString);
        }
    }
}
