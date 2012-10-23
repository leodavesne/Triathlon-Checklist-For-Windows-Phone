// <copyright file="AboutViewModel.cs" company="cematinla.com">
//     Ce matin là. All rights reserved.
// </copyright>
// <author>Léo Davesne</author>

namespace TriathlonChecklist.ViewModel
{
    #region Usings

    using System.Collections.Generic;

    #endregion Usings

    /// <summary>
    /// AboutViewModel class.
    /// </summary>
    public class AboutViewModel : ViewModelBase
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the AboutViewModel class.
        /// </summary>
        public AboutViewModel()
        {
        }
        
        #endregion Constructors

        #region Methods

        /// <summary>
        /// Initializes the view model.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        public override void Initialize(IDictionary<string, string> parameters)
        {
            base.Initialize(parameters);
        }

        #endregion Methods
    }
}
