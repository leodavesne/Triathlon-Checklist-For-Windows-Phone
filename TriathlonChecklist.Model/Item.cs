// <copyright file="Item.cs" company="cematinlà.com">
//     Ce matin là. All rights reserved.
// </copyright>
// <author>Léo Davesne</author>

namespace TriathlonChecklist.Model
{
    #region Usings

    using System.ComponentModel;
    using System.Data.Linq;
    using System.Data.Linq.Mapping;
    using System.Windows;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Microsoft.Phone.Data.Linq.Mapping;

    #endregion Usings

    /// <summary>
    /// Item class
    /// </summary>
    [Index(Columns = "Id", IsUnique = true, Name = "item_Id")]
    [Table]
    public class Item : INotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        /// The name.
        /// </summary>
        private string name;

        /// <summary>
        /// The category identifier.
        /// </summary>
        private int? categoryId;

        /// <summary>
        /// The category ref.
        /// </summary>
        private EntityRef<Category> categoryRef = new EntityRef<Category>();

        /// <summary>
        /// Is selected.
        /// </summary>
        private bool isSelected;

        /// <summary>
        /// The remove command.
        /// </summary>
        private ICommand removeCommand = null;

        /// <summary>
        /// The visibility.
        /// </summary>
        private Visibility visibility;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Item class.
        /// </summary>
        public Item()
        {
            this.removeCommand = new RelayCommand<Item>(this.RemoveAction);
        }

        /// <summary>
        /// Initializes a new instance of the Item class.
        /// </summary>
        /// <param name="name">The name</param>
        /// <param name="idCategory">The category identifier</param>
        /// <param name="isSelected">The is selected property</param>
        /// <param name="visibility">The visibility</param>
        public Item(string name, int? idCategory, bool isSelected, Visibility visibility)
        {
            this.Name = name;
            this.CategoryId = idCategory;
            this.IsSelected = isSelected;
            this.Visibility = visibility;

            this.removeCommand = new RelayCommand<Item>(this.RemoveAction);
        }

        /// <summary>
        /// Initializes a new instance of the Item class.
        /// </summary>
        /// <param name="id">The identifier</param>
        /// <param name="name">The name</param>
        /// <param name="idCategory">The category identifier</param>
        /// <param name="isSelected">The is selected property</param>
        /// <param name="visibility">The visibility</param>
        public Item(int id, string name, int? idCategory, bool isSelected, Visibility visibility)
        {
            this.Id = id;
            this.Name = name;
            this.CategoryId = idCategory;
            this.IsSelected = isSelected;
            this.Visibility = visibility;

            this.removeCommand = new RelayCommand<Item>(this.RemoveAction);
        }

        #endregion Constructors

        #region Events

        /// <summary>
        /// The property changed event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Properties

        /// <summary>
        /// Gets or sets the identifier
        /// </summary>
        [Column(IsPrimaryKey = true, IsDbGenerated = true, CanBeNull = false)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        [Column(CanBeNull = false)]
        public string Name
        {
            get
            {
                return this.name;
            }

            set
            {
                if (this.name != value)
                {
                    this.name = value;

                    ////this.OnPropertyChanged("Name");
                }
            }
        }
        
        /// <summary>
        /// Gets or sets the category identifier
        /// </summary>
        [Column(Storage = "categoryId", DbType = "Int")]
        public int? CategoryId
        {
            get
            {
                return this.categoryId;
            }

            set
            {
                if (this.categoryId != value)
                {
                    this.categoryId = value;

                    ////this.OnPropertyChanged("Category");
                }
            }
        }

        /// <summary>
        /// Gets or sets the category
        /// </summary>
        [Association(Name = "FK_Category_Items", Storage = "categoryRef", ThisKey = "CategoryId", OtherKey = "Id", IsForeignKey = true)]
        public Category Category
        {
            get
            {
                return this.categoryRef.Entity;
            }

            set
            {
                Category previousValue = this.categoryRef.Entity;
                if (previousValue != value || this.categoryRef.HasLoadedOrAssignedValue == false)
                {
                    if (previousValue != null)
                    {
                        this.categoryRef.Entity = null;
                        previousValue.Items.Remove(this);
                    }

                    this.categoryRef.Entity = value;

                    if (value != null)
                    {
                        value.Items.Add(this);
                        this.categoryId = value.Id;
                    }
                    else
                    {
                        this.categoryId = default(int?);
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the item is selected
        /// </summary>
        [Column(CanBeNull = false)]
        public bool IsSelected
        {
            get
            {
                return this.isSelected;
            }

            set
            {
                if (this.isSelected != value)
                {
                    this.isSelected = value;
                    this.HasChanged = true;
                    this.OnPropertyChanged("IsSelected");
                }
            }
        }

        /// <summary>
        /// Gets or sets the visibility
        /// </summary>
        [Column(CanBeNull = false)]
        public Visibility Visibility
        {
            get
            {
                return this.visibility;
            }

            set
            {
                if (this.visibility != value)
                {
                    this.visibility = value;
                    this.OnPropertyChanged("Visibility");
                    this.HasChanged = true;
                    this.OnPropertyChanged("HasChanged");
                    this.ToRemove = true;
                    this.OnPropertyChanged("ToRemove");
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the item has changed
        /// </summary>
        public bool HasChanged { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the item is to remove
        /// </summary>
        public bool ToRemove { get; set; }

        /// <summary>
        /// Gets the remove command
        /// </summary>
        public ICommand RemoveCommand
        {
            get
            {
                return this.removeCommand;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// On property changed method
        /// </summary>
        /// <param name="propertyName">The property name</param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Remove an item
        /// </summary>
        /// <param name="item">The item</param>
        private void RemoveAction(Item item)
        {
            if (item != null)
            {
                this.Visibility = Visibility.Collapsed;
            }
        }

        #endregion Methods
    }
}
