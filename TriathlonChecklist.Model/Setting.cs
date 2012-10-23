// <copyright file="Setting.cs" company="cematinla.com">
//     Ce matin là. All rights reserved.
// </copyright>
// <author>Léo Davesne</author>

namespace TriathlonChecklist.Model
{
    #region Usings

    using System.ComponentModel;
    using System.Data.Linq.Mapping;

    #endregion Usings

    /// <summary>
    /// Setting class.
    /// </summary>
    [Table]
    public class Setting : INotifyPropertyChanged
    {
        #region Fields

        /// <summary>
        /// The key.
        /// </summary>
        private string key;

        /// <summary>
        /// The value.
        /// </summary>
        private string value;

        #endregion Fields

        #region Events

        /// <summary>
        /// The property changed event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events

        #region Properties

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        [Column(IsPrimaryKey = true, IsDbGenerated = true, CanBeNull = false)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        [Column(CanBeNull = false)]
        public string Key
        {
            get
            {
                return this.key;
            }

            set
            {
                if (this.key != value)
                {
                    this.key = value;

                    ////this.OnPropertyChanged("Key");
                }
            }
        }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [Column(CanBeNull = false)]
        public string Value
        {
            get
            {
                if (this.value == null)
                {
                    return string.Empty;
                }

                return this.value;
            }

            set
            {
                if (this.value != value)
                {
                    this.value = value;

                    ////this.OnPropertyChanged("Value");
                }
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// On property changed method.
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

        #endregion Properties
    }
}
