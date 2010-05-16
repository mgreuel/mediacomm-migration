#region Using Directives

using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Web.Security;

#endregion

namespace MediaCommMVC.UI.AccountModels
{
    /// <summary>The validate password length attribute.</summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class ValidatePasswordLengthAttribute : ValidationAttribute
    {
        #region Constants and Fields

        /// <summary>
        ///   The _default error message.
        /// </summary>
        [SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1303:ConstFieldNamesMustBeginWithUpperCaseLetter", 
            Justification = "Reviewed. Suppression is OK here.")]
        private const string defaultErrorMessage = "'{0}' must be at least {1} characters long.";

        /// <summary>
        ///   The _min characters.
        /// </summary>
        private readonly int minCharacters = Membership.Provider.MinRequiredPasswordLength;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///   Initializes a new instance of the <see cref = "ValidatePasswordLengthAttribute" /> class.
        /// </summary>
        public ValidatePasswordLengthAttribute()
            : base(defaultErrorMessage)
        {
        }

        #endregion

        #region Public Methods

        /// <summary>The format error message.</summary>
        /// <param name="name">The      name.</param>
        /// <returns>The format error  message.</returns>
        public override string FormatErrorMessage(string name)
        {
            return String.Format(CultureInfo.CurrentUICulture, this.ErrorMessageString, name, this.minCharacters);
        }

        /// <summary>The is valid.</summary>
        /// <param name="value">The    value.</param>
        /// <returns>The is    valid.</returns>
        public override bool IsValid(object value)
        {
            string valueAsString = value as string;
            return valueAsString != null && valueAsString.Length >= this.minCharacters;
        }

        #endregion
    }
}