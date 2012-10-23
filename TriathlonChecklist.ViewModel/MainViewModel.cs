// <copyright file="MainViewModel.cs" company="cematinla.com">
//     Ce matin là. All rights reserved.
// </copyright>
// <author>Léo Davesne</author>

namespace TriathlonChecklist.ViewModel
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Linq;
    using System.Windows;
    using System.Xml.Linq;
    using Microsoft.Phone.Reactive;
    using TriathlonChecklist.Model;

    #endregion Usings

    /// <summary>
    /// MainViewModel class.
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        #region Fields

        /// <summary>
        /// The connection string.
        /// </summary>
        private const string ConnectionString = @"isostore:/ItemDB.sdf";

        /// <summary>
        /// The swim checklist.
        /// </summary>
        private ObservableCollection<Item> swimChecklist;

        /// <summary>
        /// The bike checklist.
        /// </summary>
        private ObservableCollection<Item> bikeChecklist;

        /// <summary>
        /// The run checklist.
        /// </summary>
        private ObservableCollection<Item> runChecklist;

        /// <summary>
        /// The miscellaneous checklist.
        /// </summary>
        private ObservableCollection<Item> miscChecklist;

        /// <summary>
        /// The splash screen visibility.
        /// </summary>
        private Visibility loadingScreenVisibility = Visibility.Visible;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            this.SwimChecklist = new ObservableCollection<Item>();
            this.BikeChecklist = new ObservableCollection<Item>();
            this.RunChecklist = new ObservableCollection<Item>();
            this.MiscChecklist = new ObservableCollection<Item>();
        }
        
        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets a value indicating whether the data is loaded or not.
        /// </summary>
        public bool IsDataLoaded { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the data is saved or not.
        /// </summary>
        public bool IsDataSaved { get; private set; }

        /// <summary>
        /// Gets or sets the swim checklist.
        /// </summary>
        public ObservableCollection<Item> SwimChecklist
        {
            get
            {
                ////return this._swimChecklist = new ObservableCollection<Item>(this._generalChecklist
                ////    .Where(i => i.CategoryId == (int)Category.CategoryEnum.Swim && i.Visibility == Visibility.Visible)
                ////    .OrderBy(i => i.Id)
                ////    .Select(i => new Item(i.Id, i.Name, i.CategoryId, i.IsSelected, i.Visibility)));
                return this.swimChecklist;
            }

            set
            {
                if (this.swimChecklist != value)
                {
                    this.swimChecklist = value;
                }

                this.OnPropertyChanged("SwimChecklist");
            }
        }

        /// <summary>
        /// Gets or sets the bike checklist.
        /// </summary>
        public ObservableCollection<Item> BikeChecklist
        {
            get
            {
                return this.bikeChecklist;
            }

            set
            {
                if (this.bikeChecklist != value)
                {
                    this.bikeChecklist = value;
                }

                this.OnPropertyChanged("BikeChecklist");
            }
        }

        /// <summary>
        /// Gets or sets the run checklist.
        /// </summary>
        public ObservableCollection<Item> RunChecklist
        {
            get
            {
                return this.runChecklist;
            }

            set
            {
                if (this.runChecklist != value)
                {
                    this.runChecklist = value;
                }

                this.OnPropertyChanged("RunChecklist");
            }
        }

        /// <summary>
        /// Gets or sets the miscellaneous checklist.
        /// </summary>
        public ObservableCollection<Item> MiscChecklist
        {
            get
            {
                return this.miscChecklist;
            }

            set
            {
                if (this.miscChecklist != value)
                {
                    this.miscChecklist = value;
                }

                this.OnPropertyChanged("MiscChecklist");
            }
        }

        /// <summary>
        /// Gets or sets the splash screen visibility.
        /// </summary>
        public Visibility LoadingScreenVisibility
        {
            get
            {
                return this.loadingScreenVisibility;
            }

            set
            {
                if (this.loadingScreenVisibility != value)
                {
                    this.loadingScreenVisibility = value;
                }

                this.OnPropertyChanged("LoadingScreenVisibility");
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Initialize method.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        public override void Initialize(IDictionary<string, string> parameters)
        {
            base.Initialize(parameters);

            if (parameters == null)
            {
                return;
            }

            string toReload = null;
            if (!parameters.TryGetValue("Reload", out toReload))
            {
                return;
            }

            bool reload = false;
            bool.TryParse(toReload, out reload);

            if (reload)
            {
                this.ReinitializeContext();
            }
        }

        /// <summary>
        /// Reinitialize database method.
        /// </summary>
        public void ReinitializeDatabase()
        {
            using (ItemDataContext context = new ItemDataContext(ConnectionString))
            {
                context.DeleteDatabase();
            }
        }

        /// <summary>
        /// Reinitialize context method.
        /// </summary>
        public void ReinitializeContext()
        {
            this.LoadingScreenVisibility = Visibility.Visible;

            this.ReinitializeItems();

            this.IsDataLoaded = false;
        }

        /// <summary>
        /// Reinitialize items method.
        /// </summary>
        public void ReinitializeItems()
        {
            this.SwimChecklist = new ObservableCollection<Item>();
            this.BikeChecklist = new ObservableCollection<Item>();
            this.RunChecklist = new ObservableCollection<Item>();
            this.MiscChecklist = new ObservableCollection<Item>();
        }

        /// <summary>
        /// Navigate method.
        /// </summary>
        /// <param name="uri">The uri.</param>
        public override void NavigateTo(Uri uri)
        {
            this.SaveDataContext();

            INavigationService navigationService = this.GetService<INavigationService>();
            if (navigationService == null)
            {
                return;
            }

            navigationService.Navigate(uri.ToString());
        }

        /// <summary>
        /// Save data method.
        /// </summary>
        public void SaveDataContext()
        {
            using (ItemDataContext context = new ItemDataContext(ConnectionString))
            {
                ////foreach (Item it in this.SwimChecklist.Where(i => i.ToRemove == true))
                ////{
                ////    context.Items.DeleteOnSubmit(context.Items.Single(i => i.Id == it.Id));
                ////    context.SubmitChanges();
                ////}

                ////this.SwimChecklist = new ObservableCollection<Item>(this.swimChecklist
                ////    .Where(i => i.ToRemove == false)
                ////    .OrderBy(i => i.Id)
                ////    .Select(i => new Item(i.Id, i.Name, i.CategoryId, i.IsSelected, i.Visibility)));

                ////foreach (Item it in this.SwimChecklist.Where(i => i.HasChanged == true && i.ToRemove == false))
                ////{
                ////    Item itemToModify = context.Items.Single(i => i.Id == it.Id);
                ////    itemToModify.IsSelected = it.IsSelected;
                ////    context.SubmitChanges();

                ////    this.SwimChecklist.Single(s => s.Id == it.Id).HasChanged = false;
                ////}

                foreach (Item it in this.SwimChecklist.Where(i => i.HasChanged == true))
                {
                    if (it.ToRemove == true)
                    {
                        context.Items.DeleteOnSubmit(context.Items.Single(i => i.Id == it.Id));
                    }
                    else
                    {
                        Item itemToModify = context.Items.Single(i => i.Id == it.Id);
                        itemToModify.IsSelected = it.IsSelected;
                    }

                    context.SubmitChanges();
                }

                foreach (Item it in this.BikeChecklist.Where(i => i.HasChanged == true))
                {
                    if (it.ToRemove == true)
                    {
                        context.Items.DeleteOnSubmit(context.Items.Single(i => i.Id == it.Id));
                    }
                    else
                    {
                        Item itemToModify = context.Items.Single(i => i.Id == it.Id);
                        itemToModify.IsSelected = it.IsSelected;
                    }

                    context.SubmitChanges();
                }

                foreach (Item it in this.RunChecklist.Where(i => i.HasChanged == true))
                {
                    if (it.ToRemove == true)
                    {
                        context.Items.DeleteOnSubmit(context.Items.Single(i => i.Id == it.Id));
                    }
                    else
                    {
                        Item itemToModify = context.Items.Single(i => i.Id == it.Id);
                        itemToModify.IsSelected = it.IsSelected;
                    }

                    context.SubmitChanges();
                }

                foreach (Item it in this.MiscChecklist.Where(i => i.HasChanged == true))
                {
                    if (it.ToRemove == true)
                    {
                        context.Items.DeleteOnSubmit(context.Items.Single(i => i.Id == it.Id));
                    }
                    else
                    {
                        Item itemToModify = context.Items.Single(i => i.Id == it.Id);
                        itemToModify.IsSelected = it.IsSelected;
                    }

                    context.SubmitChanges();
                }
            }

            this.IsDataSaved = true;
        }

        /// <summary>
        /// Get data method.
        /// </summary>
        public void GetData()
        {
            Observable.Start(() => this.LoadData())
            .ObserveOnDispatcher()
            .Subscribe(list =>
            {
                ////List<Item> swimItems = list.Where(s => s.CategoryId == (int)Category.CategoryEnum.Swim)
                ////    .Select(item => new Item
                ////    {
                ////        Name = item.Name,
                ////        CategoryId = item.CategoryId,
                ////        IsSelected = item.IsSelected,
                ////        Visibility = item.Visibility
                ////    })
                ////    .ToList();

                ////this.SwimChecklist = (ObservableCollection<Item>)swimItems.ToObservable();

                foreach (Item it in list.Where(s => s.CategoryId == (int)Category.CategoryEnum.Swim))
                {
                    this.SwimChecklist.Add(it);
                }

                foreach (Item it in list.Where(s => s.CategoryId == (int)Category.CategoryEnum.Bike))
                {
                    this.BikeChecklist.Add(it);
                }

                foreach (Item it in list.Where(s => s.CategoryId == (int)Category.CategoryEnum.Run))
                {
                    this.RunChecklist.Add(it);
                }

                foreach (Item it in list.Where(s => s.CategoryId == (int)Category.CategoryEnum.Misc))
                {
                    this.MiscChecklist.Add(it);
                }

                this.IsDataLoaded = true;
                this.LoadingScreenVisibility = Visibility.Collapsed;
            });
        }

        /// <summary>
        /// Load data method.
        /// </summary>
        /// <returns>The items.</returns>
        public ObservableCollection<Item> LoadData()
        {
            var results = new ObservableCollection<Item>();

            using (ItemDataContext context = new ItemDataContext(ConnectionString))
            {
                // For development use only
                ////context.DeleteDatabase();

                // Create database if it doesn't exist
                if (!context.DatabaseExists())
                {
                    context.CreateDatabase();
                }

                // Initialization
                if (context.Items.Count() == 0 || context.Categories.Count() == 0)
                {
                    XDocument data = new XDocument();

                    // For the different languages
                    CultureInfo currentCulture = CultureInfo.CurrentCulture;

                    // XML uri
                    string dataUri = string.Empty;

                    // Language selection
                    switch (currentCulture.TwoLetterISOLanguageName)
                    {
                        // Disable French for the first release
                        ////case "fr":
                        ////    dataUri = "Data/triathlonChecklist.fr-FR.xml";
                        ////    break;
                        case "en":
                            dataUri = "Data/triathlonChecklist.xml";
                            break;
                    }

                    // By default it is in English
                    if (string.IsNullOrEmpty(dataUri))
                    {
                        dataUri = "Data/triathlonChecklist.xml";
                    }

                    data = XDocument.Load(dataUri);

                    var dataCategories = from query in data.Descendants("Category")
                                         select new Category((string)query.Element("Name"));

                    var dataItems = from query in data.Descendants("Item")
                                    select new Item((string)query.Element("Name"), (int?)query.Element("CategoryId"), false, Visibility.Visible);

                    foreach (var item in dataCategories)
                    {
                        // Add the new category to the context
                        Category cat = new Category();
                        cat.Name = item.Name;
                        context.Categories.InsertOnSubmit(cat);
                    }

                    foreach (var item in dataItems)
                    {
                        // Add the new item to the context
                        Item it = new Item();
                        it.Id = item.Id;
                        it.Name = item.Name;
                        it.CategoryId = item.CategoryId;
                        it.IsSelected = item.IsSelected;
                        context.Items.InsertOnSubmit(it);
                    }

                    // Save changes to the database
                    context.SubmitChanges();
                }

                var contextItems = from i in context.Items
                                   select i;

                foreach (Item it in contextItems)
                {
                    results.Add(it);
                }
            }

            return results;
        }

        #endregion Methods
    }
}
