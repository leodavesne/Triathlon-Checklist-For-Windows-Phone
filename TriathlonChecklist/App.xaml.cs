// <copyright file="App.xaml.cs" company="cematinlà.com">
//     Ce matin là. All rights reserved.
// </copyright>
// <author>Léo Davesne</author>

namespace TriathlonChecklist
{
    #region Usings

    using System.Windows;
    using System.Windows.Navigation;
    using Microsoft.Phone.Controls;
    using Microsoft.Phone.Shell;
    using TriathlonChecklist.Helper;
    using TriathlonChecklist.ViewModel;
    
    #endregion Usings

    /// <summary>
    /// App class.
    /// </summary>
    public partial class App : Application
    {
        #region Fields

        /// <summary>
        /// The main view model.
        /// </summary>
        private static MainViewModel mainViewModel = null;

        /// <summary>
        /// The add item view model.
        /// </summary>
        private static AddItemViewModel addItemViewModel = null;

        /// <summary>
        /// The about view model.
        /// </summary>
        private static AboutViewModel aboutViewModel = null;

        /// <summary>
        /// Avoid double-initialization.
        /// </summary>
        private bool phoneApplicationInitialized = false;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the App class.
        /// </summary>
        public App()
        {
            // Global handler for uncaught exceptions. 
            this.UnhandledException += this.Application_UnhandledException;

            DispatcherHelper.CurrentDispatcher = App.Current.RootVisual.Dispatcher;

            // Standard Silverlight initialization
            this.InitializeComponent();

            // Phone-specific initialization
            this.InitializePhoneApplication();

            // Show graphics profiling information while debugging.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // Display the current frame rate counters.
                Application.Current.Host.Settings.EnableFrameRateCounter = true;

                // Show the areas of the app that are being redrawn in each frame.
                ////Application.Current.Host.Settings.EnableRedrawRegions = true;

                // Enable non-production analysis visualization mode, 
                // which shows areas of a page that are handed off to GPU with a colored overlay.
                ////Application.Current.Host.Settings.EnableCacheVisualization = true;

                // Disable the application idle detection by setting the UserIdleDetectionMode property of the
                // application's PhoneApplicationService object to Disabled.
                // Caution:- Use this under debug mode only. Application that disables user idle detection will continue to run
                // and consume battery power when the user is not using the phone.
                PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
            }

            // Force the dark theme
            // TODO : manage the light theme
            ThemeManager.ToDarkTheme();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the MainViewModel
        /// </summary>
        public static MainViewModel MainViewModel
        {
            get
            {
                if (mainViewModel == null)
                {
                    mainViewModel = new MainViewModel();
                }

                return mainViewModel;
            }
        }

        /// <summary>
        /// Gets the AddItemViewModel
        /// </summary>
        public static AddItemViewModel AddItemViewModel
        {
            get
            {
                if (addItemViewModel == null)
                {
                    addItemViewModel = new AddItemViewModel();
                }

                return addItemViewModel;
            }
        }

        /// <summary>
        /// Gets the AboutViewModel
        /// </summary>
        public static AboutViewModel AboutViewModel
        {
            get
            {
                if (aboutViewModel == null)
                {
                    aboutViewModel = new AboutViewModel();
                }

                return aboutViewModel;
            }
        }

        /// <summary>
        /// Gets an easy access to the root frame of the Phone Application
        /// </summary>
        /// <returns>The root frame of the Phone Application</returns>
        public PhoneApplicationFrame RootFrame { get; private set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Get the data
        /// </summary>
        public static void GetData()
        {
            if (App.MainViewModel != null)
            {
                if (!App.MainViewModel.IsDataLoaded)
                {
                    App.MainViewModel.GetDataAndReinitializeUI(true);
                }
            }
        }

        /// <summary>
        /// Save the data
        /// </summary>
        public static void SaveData()
        {
            if (App.MainViewModel != null)
            {
                if (!App.MainViewModel.IsDataSaved)
                {
                    App.MainViewModel.SaveDataContext();
                }

                App.MainViewModel.ReinitializeContext();
            }
        }

        /// <summary>
        /// Get the categories
        /// </summary>
        public static void GetCategories()
        {
            if (App.AddItemViewModel != null)
            {
                if (!App.AddItemViewModel.IsDataLoaded)
                {
                    App.AddItemViewModel.GetCategories();
                }

                App.AddItemViewModel.InitializeValues();
            }
        }

        /// <summary>
        /// Code to execute when the application is launching (from Start)
        /// This code will not execute when the application is reactivated
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The e</param>
        private void Application_Launching(object sender, LaunchingEventArgs e)
        {
            // Tagging : launching
            this.Tag();
        }

        /// <summary>
        /// Code to execute when the application is activated (brought to foreground)
        /// This code will not execute when the application is first launched
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The e</param>
        private void Application_Activated(object sender, ActivatedEventArgs e)
        {
            // Tagging : restart session
            this.Tag();

            // Ensure that application state is restored appropriately
            GetData();
        }

        /// <summary>
        /// Code to execute when the application is deactivated (sent to background)
        /// This code will not execute when the application is closing
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The e</param>
        private void Application_Deactivated(object sender, DeactivatedEventArgs e)
        {
            SaveData();
        }

        /// <summary>
        /// Code to execute when the application is closing (user hit Back)
        /// This code will not execute when the application is deactivated
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The e</param>
        private void Application_Closing(object sender, ClosingEventArgs e)
        {
            SaveData();
        }

        /// <summary>
        /// Code to execute if a navigation fails
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The e</param>
        private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // A navigation has failed; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        /// <summary>
        /// Code to execute on Unhandled Exceptions
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The e</param>
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        /// <summary>
        /// Do not add any additional code to this method
        /// </summary>
        private void InitializePhoneApplication()
        {
            if (this.phoneApplicationInitialized)
            {
                return;
            }

            // Create the frame but don't set it as RootVisual yet; this allows the splash
            // screen to remain active until the application is ready to render.
            this.RootFrame = new PhoneApplicationFrame();
            this.RootFrame.Navigated += this.CompleteInitializePhoneApplication;

            // Handle navigation failures
            this.RootFrame.NavigationFailed += this.RootFrame_NavigationFailed;

            // Ensure we don't initialize again
            this.phoneApplicationInitialized = true;
        }

        /// <summary>
        /// Do not add any additional code to this method
        /// </summary>
        /// <param name="sender">The sender</param>
        /// <param name="e">The e</param>
        private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
        {
            // Set the root visual to allow the application to render
            if (this.RootVisual != this.RootFrame)
            {
                this.RootVisual = this.RootFrame;
            }

            // Remove this handler since it is no longer needed
            this.RootFrame.Navigated -= this.CompleteInitializePhoneApplication;
        }

        /// <summary>
        /// Tag with Flurry
        /// </summary>
        private void Tag()
        {
            // Configuration: Flurry API key
            FlurryWP7SDK.Api.StartSession("YOUR_OWN_FLURRY_API_KEY");
        }

        #endregion Methods
    }
}