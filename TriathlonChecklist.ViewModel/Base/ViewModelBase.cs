// <copyright file="ViewModelBase.cs" company="cematinlà.com">
//     Ce matin là. All rights reserved.
// </copyright>
// <author>Léo Davesne</author>

namespace TriathlonChecklist.ViewModel
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    #endregion Usings

    /// <summary>
    /// ViewModelBase class
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged
    {
        #region Events

        /// <summary>
        /// The property changed event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Constructors

        /// <summary>
        /// Initialize method.
        /// </summary>
        /// <param name="parameters">The parameters</param>
        public virtual void Initialize(IDictionary<string, string> parameters)
        {
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Navigate method
        /// </summary>
        /// <param name="uri">The uri</param>
        public virtual void NavigateTo(Uri uri)
        {
            INavigationService navigationService = this.GetService<INavigationService>();
            if (navigationService == null)
            {
                return;
            }

            navigationService.Navigate(uri.ToString());
        }

        /// <summary>
        /// On property changed method
        /// </summary>
        /// <param name="propertyName">The property name</param>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Gets a service
        /// </summary>
        /// <typeparam name="T">The T</typeparam>
        /// <returns>A T</returns>
        protected T GetService<T>() where T : class
        {
            if (typeof(T) == typeof(INavigationService))
            {
                return new SimpleNavigationService() as T;
            }

            return null;
        }

        #endregion Methods
    }
}
