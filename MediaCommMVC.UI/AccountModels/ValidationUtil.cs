using System;
using System.Diagnostics.CodeAnalysis;

namespace MediaCommMVC.UI.AccountModels
{
    /// <summary>The validation util.</summary>
    internal static class ValidationUtil
    {
        #region Constants and Fields

        /// <summary>The _string required error message.</summary>
        [SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "SA1303:ConstFieldNamesMustBeginWithUpperCaseLetter",
            Justification = "Reviewed. Suppression is OK here.")]
        private const string stringRequiredErrorMessage = "Value cannot be null or empty.";

        #endregion

        #region Public Methods

        /// <summary>The validate required string value.</summary>
        /// <param name="value">The value.</param>
        /// <param name="parameterName">The parameter name.</param>
        /// <exception cref="ArgumentException"></exception>
        public static void ValidateRequiredStringValue(string value, string parameterName)
        {
            if (String.IsNullOrEmpty(value))
            {
                throw new ArgumentException(stringRequiredErrorMessage, parameterName);
            }
        }

        #endregion
    }
}