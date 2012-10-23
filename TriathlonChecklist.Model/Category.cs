// <copyright file="Category.cs" company="cematinla.com">
//     Ce matin là. All rights reserved.
// </copyright>
// <author>Léo Davesne</author>

namespace TriathlonChecklist.Model
{
    #region Usings

    using System.ComponentModel;
    using System.Data.Linq;
    using System.Data.Linq.Mapping;
    using Microsoft.Phone.Data.Linq.Mapping;

    #endregion Usings

    /// <summary>
    /// Category class.
    /// </summary>
    [Index(Columns = "Id", IsUnique = true, Name = "category_Id")]
    [Table]
    public class Category : INotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        /// The name.
        /// </summary>
        private string name;

        /// <summary>
        /// The items ref.
        /// </summary>
        private EntitySet<Item> itemsRef;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the Category class.
        /// </summary>
        public Category()
        {
            this.itemsRef = new EntitySet<Item>(this.OnItemAdded, this.OnItemRemoved);
        }

        /// <summary>
        /// Initializes a new instance of the Category class.
        /// </summary>
        /// <param name="name">The name.</param>
        public Category(string name)
        {
            this.itemsRef = new EntitySet<Item>(this.OnItemAdded, this.OnItemRemoved);

            this.Name = name;
        }

        /// <summary>
        /// Initializes a new instance of the Category class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name.</param>
        public Category(int id, string name)
        {
            this.itemsRef = new EntitySet<Item>(this.OnItemAdded, this.OnItemRemoved);

            this.Id = id;
            this.Name = name;
        }

        #endregion Constructors

        #region Events

        /// <summary>
        /// The property changed event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Enums

        /// <summary>
        /// Category enumeration.
        /// </summary>
        public enum CategoryEnum
        {
            /// <summary>
            /// Swim category.
            /// </summary>
            Swim = 1,

            /// <summary>
            /// Bike category.
            /// </summary>
            Bike = 2,

            /// <summary>
            /// Run category.
            /// </summary>
            Run = 3,

            /// <summary>
            /// Miscellaneous category.
            /// </summary>
            Misc = 4
        }

        #endregion Enums

        #region Properties

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
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
                }

                this.OnPropertyChanged("Name");
            }
        }

        /// <summary>
        /// Gets items.
        /// </summary>
        [Association(Name = "FK_Category_Items", Storage = "itemsRef", ThisKey = "Id", OtherKey = "CategoryId")]
        public EntitySet<Item> Items
        {
            get
            {
                return this.itemsRef;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// On property changed.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// On item added.
        /// </summary>
        /// <param name="item">The item.</param>
        private void OnItemAdded(Item item)
        {
            item.Category = this;
        }

        /// <summary>
        /// On item removed.
        /// </summary>
        /// <param name="item">The item.</param>
        private void OnItemRemoved(Item item)
        {
            item.Category = null;
        }

        #endregion Methods
    }
}
