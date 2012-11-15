// <copyright file="ItemDataContext.cs" company="cematinlà.com">
//     Ce matin là. All rights reserved.
// </copyright>
// <author>Léo Davesne</author>

namespace TriathlonChecklist.Model
{
    #region Usings

    using System.Data.Linq;

    #endregion Usings

    /// <summary>
    /// ItemDataContext class
    /// </summary>
    public class ItemDataContext : DataContext
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ItemDataContext class
        /// </summary>
        /// <param name="connectionString">The connection string</param>
        public ItemDataContext(string connectionString)
            : base(connectionString)
        {
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets categories
        /// </summary>
        public Table<Category> Categories
        {
            get
            {
                return this.GetTable<Category>();
            }
        }

        /// <summary>
        /// Gets items
        /// </summary>
        public Table<Item> Items
        {
            get
            {
                return this.GetTable<Item>();
            }
        }

        #endregion Properties
    }
}
