// <copyright file="EmailHelper.cs" company="cematinlà.com">
//     Ce matin là. All rights reserved.
// </copyright>
// <author>Léo Davesne</author>

namespace TriathlonChecklist.Helper
{
    #region Usings

    using Microsoft.Phone.Tasks;

    #endregion Usings

    /// <summary>
    /// EmailHelper class
    /// </summary>
    public static class EmailHelper
    {
        /// <summary>
        /// Send method
        /// </summary>
        /// <param name="subject">The subject</param>
        /// <param name="body">The body</param>
        public static void Send(string subject, string body)
        {
            EmailComposeTask emailcomposer = new EmailComposeTask();
            emailcomposer.Subject = subject;
            emailcomposer.Body = body;

            // Configuration: email to
            emailcomposer.To = "contact@example.com";

            emailcomposer.Show();
        }
    }
}
