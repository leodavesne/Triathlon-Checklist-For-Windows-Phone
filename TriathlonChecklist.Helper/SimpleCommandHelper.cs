// <copyright file="SimpleCommandHelper.cs" company="cematinlà.com">
//     Ce matin là. All rights reserved.
// </copyright>
// <author>Léo Davesne</author>

namespace TriathlonChecklist.Helper
{
    #region Usings

    using System;
    using System.Windows.Input;
    
    #endregion Usings

    /// <summary>
    /// SimpleCommandHelper class
    /// </summary>
    public class SimpleCommandHelper : ICommand
    {
        #region Fields

        /// <summary>
        /// The action
        /// </summary>
        private Action action;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the SimpleCommandHelper class
        /// </summary>
        /// <param name="action">The parameter</param>
        public SimpleCommandHelper(Action action)
        {
            this.action = action;
        }

        #endregion Constructors

        #region Events

        /// <summary>
        /// Gets or sets the can execute changed event
        /// </summary>
        public event EventHandler CanExecuteChanged;

        #endregion Events

        #region Methods

        /// <summary>
        /// Can execute
        /// </summary>
        /// <param name="parameter">The parameter</param>
        /// <returns>True value</returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Execute method
        /// </summary>
        /// <param name="parameter">The parameter</param>
        public void Execute(object parameter)
        {
            this.action();
        }

        #endregion Methods
    }
}
