// <copyright file="AddItemViewModel.cs" company="cematinlà.com">
//     Ce matin là. All rights reserved.
// </copyright>
// <author>Léo Davesne</author>

namespace TriathlonChecklist.ViewModel
{
    #region Usings

    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Input;
    using System.Xml.Linq;
    using Microsoft.Phone.Reactive;
    using TriathlonChecklist.Helper;
    using TriathlonChecklist.Model;

    #endregion Usings

    /// <summary>
    /// Add and item ViewModel
    /// </summary>
    public class AddItemViewModel : ViewModelBase
    {
        #region Fields

        /// <summary>
        /// Gets the XML source.
        /// Configuration: XML source.
        /// </summary>
        private const string XMLSource = "Data/dataExample.xml";

        /// <summary>
        /// Gets the database connection string.
        /// </summary>
        private const string ConnectionString = @"isostore:/ItemDB.sdf";

        /// <summary>
        /// Gets the item name to add.
        /// </summary>
        private string name = string.Empty;

        /// <summary>
        /// Gets the default category.
        /// </summary>
        private Category defaultCategory;

        /// <summary>
        /// Gets the selected category.
        /// </summary>
        private Category selectedCategory;

        /// <summary>
        /// Gets all the categories.
        /// </summary>
        private ObservableCollection<Category> categories;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the AddItemViewModel class
        /// </summary>
        public AddItemViewModel()
        {
            this.Categories = new ObservableCollection<Category>();
            this.AddItemCommand = new SimpleCommandHelper(this.AddItem);
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets a value indicating whether the item is loaded
        /// </summary>
        public bool IsDataLoaded { get; private set; }

        /// <summary>
        /// Gets or sets the item name to add
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                this.name = value;
                this.OnPropertyChanged("Name");
            }
        }

        /// <summary>
        /// Gets or sets the selected category
        /// </summary>
        public Category SelectedCategory
        {
            get
            {
                return this.selectedCategory;
            }

            set
            {
                if (this.selectedCategory == value)
                {
                    return;
                }

                this.selectedCategory = value;
                this.OnPropertyChanged("SelectedCategory");
            }
        }

        /// <summary>
        /// Gets or sets all the categories
        /// </summary>
        public ObservableCollection<Category> Categories
        {
            get
            {
                return this.categories;
            }

            set
            {
                if (this.categories != value)
                {
                    this.categories = value;
                }

                this.OnPropertyChanged("Categories");
            }
        }

        /// <summary>
        /// Gets or sets the AddItemCommand
        /// </summary>
        public ICommand AddItemCommand { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Add an item.
        /// </summary>
        public void AddItem()
        {
            string name = this.Name;
            Category category = this.SelectedCategory;

            if (string.IsNullOrWhiteSpace(name))
            {
                this.Name = string.Empty;
                return;
            }

            // Force the default category
            if (category == null)
            {
                category = this.defaultCategory;
            }

            // 30 characters maximum
            if (name.Length > 30 ||
                category == null)
            {
                return;
            }

            // We keep the selected category
            if (category != null)
            {
                this.defaultCategory = category;
            }

            using (ItemDataContext context = new ItemDataContext(ConnectionString))
            {
                Item itemToCreate = new Item(name, category.Id, false, Visibility.Visible);

                if (context.Items.Where(i => i.Name == itemToCreate.Name &&
                    i.CategoryId == itemToCreate.CategoryId)
                    .FirstOrDefault() == null)
                {
                    context.Items.InsertOnSubmit(itemToCreate);
                    context.SubmitChanges();
                }
            }

            INavigationService navigationService = this.GetService<INavigationService>();
            if (navigationService == null)
            {
                return;
            }

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("Reload", "true");

            navigationService.Navigate("/Pages/MainPage.xaml", parameters);
        }

        /// <summary>
        /// Initialize the ViewModel
        /// </summary>
        /// <param name="parameters">Initialization parameters</param>
        public override void Initialize(IDictionary<string, string> parameters)
        {
            base.Initialize(parameters);
        }

        /// <summary>
        /// Reinitialize categories method
        /// </summary>
        public void ReinitializeCategories()
        {
            this.Categories = new ObservableCollection<Category>();

            this.IsDataLoaded = false;
        }

        /// <summary>
        /// Get all the categories
        /// </summary>
        public void GetCategories()
        {
            Observable.Start(() => this.LoadCategories())
            .ObserveOnDispatcher()
            .Subscribe(list =>
            {
                foreach (Category cat in list)
                {
                    this.Categories.Add(cat);

                    // Configuration: default category
                    if (cat.Name == "Swim")
                    {
                        this.defaultCategory = cat;
                    }
                }

                this.IsDataLoaded = true;
            });
        }

        /// <summary>
        /// Load categories
        /// </summary>
        /// <returns>All categories</returns>
        public ObservableCollection<Category> LoadCategories()
        {
            var results = new ObservableCollection<Category>();

            using (ItemDataContext context = new ItemDataContext(ConnectionString))
            {
                // For development use only
                ////context.DeleteDatabase();

                if (!context.DatabaseExists())
                {
                    // Create database if it doesn't exist
                    context.CreateDatabase();
                }

                if (context.Categories.Count() == 0)
                {
                    // Initialization
                    XDocument data = XDocument.Load(XMLSource);

                    var dataCategories = from query in data.Descendants("Category")
                                         select new Category((string)query.Element("Name"));

                    foreach (var item in dataCategories)
                    {
                        // Add the new category to the context
                        Category cat = new Category();
                        cat.Name = item.Name;
                        context.Categories.InsertOnSubmit(cat);
                    }

                    // Save changes to the database
                    context.SubmitChanges();
                }

                var contextCategories = from i in context.Categories
                                        select i;

                foreach (Category cat in contextCategories)
                {
                    results.Add(cat);
                }
            }

            return results;
        }

        /// <summary>
        /// Initializes default values
        /// </summary>
        public void InitializeValues()
        {
            this.Name = string.Empty;

            if (this.defaultCategory != null)
            {
                this.SelectedCategory = this.defaultCategory;
            }
        }

        #endregion Methods
    }
}
