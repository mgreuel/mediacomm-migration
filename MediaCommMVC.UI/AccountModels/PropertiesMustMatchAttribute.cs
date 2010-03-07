using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace MediaCommMVC.UI.AccountModels
{
    /// <summary>The properties must match attribute.</summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public sealed class PropertiesMustMatchAttribute : ValidationAttribute
    {
        #region Constants and Fields

        /// <summary>The _default error message.</summary>
        [SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1303:ConstFieldNamesMustBeginWithUpperCaseLetter",
            Justification = "Reviewed. Suppression is OK here.")]
        private const string defaultErrorMessage = "'{0}' and '{1}' do not match.";

        /// <summary>The _type id.</summary>
        private readonly object typeId = new object();

        #endregion

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="PropertiesMustMatchAttribute"/> class.</summary>
        /// <param name="originalProperty">The original property.</param>
        /// <param name="confirmProperty">The confirm property.</param>
        public PropertiesMustMatchAttribute(string originalProperty, string confirmProperty)
            : base(defaultErrorMessage)
        {
            this.OriginalProperty = originalProperty;
            this.ConfirmProperty = confirmProperty;
        }

        #endregion

        #region Properties

        /// <summary>Gets ConfirmProperty.</summary>
        /// <value>The confirm property.</value>
        public string ConfirmProperty { get; private set; }

        /// <summary>Gets OriginalProperty.</summary>
        /// <value>The original property.</value>
        public string OriginalProperty { get; private set; }

        /// <summary>Gets TypeId.</summary>
        /// <value>The type id.</value>
        public override object TypeId
        {
            get
            {
                return this.typeId;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>The format error message.</summary>
        /// <param name="name">The user name.</param>
        /// <returns>The format error  message.</returns>
        public override string FormatErrorMessage(string name)
        {
            return String.Format(
                CultureInfo.CurrentUICulture, this.ErrorMessageString, this.OriginalProperty, this.ConfirmProperty);
        }

        /// <summary>The is valid.</summary>
        /// <param name="value">The value.</param>
        /// <returns>The is  valid.</returns>
        public override bool IsValid(object value)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(value);
            object originalValue = properties.Find(this.OriginalProperty, true /* ignoreCase */).GetValue(value);
            object confirmValue = properties.Find(this.ConfirmProperty, true /* ignoreCase */).GetValue(value);
            return Object.Equals(originalValue, confirmValue);
        }

        #endregion
    }
}