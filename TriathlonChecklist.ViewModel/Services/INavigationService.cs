// <copyright file="INavigationService.cs" company="cematinlà.com">
//     Ce matin là. All rights reserved.
// </copyright>
// <author>Léo Davesne</author>

namespace TriathlonChecklist.ViewModel
{
    #region Usings

    using System.Collections.Generic;

    #endregion Usings

    /// <summary>
    /// INavigationService interface
    /// </summary>
    public interface INavigationService
    {
        /// <summary>
        /// Navigate method
        /// </summary>
        /// <param name="uri">The uri</param>
        void Navigate(string uri);

        /// <summary>
        /// Navigate method
        /// </summary>
        /// <param name="uri">The uri</param>
        /// <param name="parameters">The parameters</param>
        void Navigate(string uri, IDictionary<string, string> parameters);
    }
}
