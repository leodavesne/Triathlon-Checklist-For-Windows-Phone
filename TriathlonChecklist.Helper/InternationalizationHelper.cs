// <copyright file="InternationalizationHelper.cs" company="cematinlà.com">
//     Ce matin là. All rights reserved.
// </copyright>
// <author>Léo Davesne</author>

namespace TriathlonChecklist.Helper
{
    /// <summary>
    /// InternationalizationHelper class.
    /// </summary>
    public class InternationalizationHelper
    {
        #region Fields

        /// <summary>
        /// Localized resources.
        /// </summary>
        private static AppResources localizedResources = new AppResources();

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the InternationalizationHelper class.
        /// </summary>
        public InternationalizationHelper()
        {
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the app resources.
        /// </summary>
        public AppResources AppResources
        {
            get { return localizedResources; }
        }

        #endregion Properties
    }
}
