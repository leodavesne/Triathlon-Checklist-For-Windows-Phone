// <copyright file="DispatcherHelper.cs" company="cematinlà.com">
//     Ce matin là. All rights reserved.
// </copyright>
// <author>Léo Davesne</author>

namespace TriathlonChecklist.Helper
{
    #region Usings

    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Threading;

    #endregion Usings

    /// <summary>
    /// DispatcherHelper class
    /// </summary>
    public static class DispatcherHelper
    {
        #region Fields

        /// <summary>
        /// A single Dispatcher instance to marshall actions to the user
        /// interface thread.
        /// </summary>
        private static Dispatcher instance;

        /// <summary>
        /// Backing field for a value indicating whether this is a design-time
        /// environment.
        /// </summary>
        private static bool? designer;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets the current dispatcher.
        /// </summary>
        public static Dispatcher CurrentDispatcher { get; set; }

        #endregion Properties

        #region Methods
 
        /// <summary>
        /// Initializes the SmartDispatcher system, attempting to use the
        /// RootVisual of the plugin to retrieve a Dispatcher instance.
        /// </summary>
        public static void Initialize()
        {
            if (instance == null)
            {
                RequireInstance();
            }
        }
 
        /// <summary>
        /// Initializes the SmartDispatcher system with the dispatcher
        /// instance.
        /// </summary>
        /// <param name="dispatcher">The dispatcher instance</param>
        public static void Initialize(Dispatcher dispatcher)
        {
            if (dispatcher == null)
            {
                throw new ArgumentNullException("dispatcher");
            }
 
            instance = dispatcher;
 
            if (designer == null)
            {
                designer = DesignerProperties.IsInDesignTool;
            }
        }
 
        /// <summary>
        /// Check access method.
        /// </summary>
        /// <returns>A boolean</returns>
        public static bool CheckAccess()
        {
            if (instance == null)
            {
                RequireInstance();
            }
 
            return instance.CheckAccess();
        }
 
        /// <summary>
        /// Executes the specified delegate asynchronously on the user interface
        /// thread. If the current thread is the user interface thread, the
        /// dispatcher if not used and the operation happens immediately.
        /// </summary>
        /// <param name="a">A delegate to a method that takes no arguments and
        /// does not return a value, which is either pushed onto the Dispatcher
        /// event queue or immediately run, depending on the current thread</param>
        public static void BeginInvoke(Action a)
        {
            if (instance == null)
            {
                RequireInstance();
            }
 
            // If the current thread is the user interface thread, skip the
            // dispatcher and directly invoke the Action.
            if (instance.CheckAccess() || designer == true)
            {
                a();
            }
            else
            {
                instance.BeginInvoke(a);
            }
        }

        /// <summary>
        /// Requires an instance and attempts to find a Dispatcher if one has
        /// not yet been set.
        /// </summary>
        private static void RequireInstance()
        {
            if (designer == null)
            {
                designer = DesignerProperties.IsInDesignTool;
            }

            // Design-time is more of a no-op, won't be able to resolve the
            // dispatcher if it isn't already set in these situations.
            if (designer == true)
            {
                return;
            }

            // Attempt to use the RootVisual of the plugin to retrieve a
            // dispatcher instance. This call will only succeed if the current
            // thread is the UI thread.
            try
            {
                instance = Application.Current.RootVisual.Dispatcher;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("The first time SmartDispatcher is used must be from a user interface thread. Consider having the application call Initialize, with or without an instance.", e);
            }

            if (instance == null)
            {
                throw new InvalidOperationException("Unable to find a suitable Dispatcher instance.");
            }
        }

        #endregion Methods
    }
}
