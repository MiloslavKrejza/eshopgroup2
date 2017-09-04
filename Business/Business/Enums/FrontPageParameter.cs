using System;
using System.Collections.Generic;
using System.Text;

namespace Trainee.Business.Business.Enums
{
    /// <summary>
    /// This enum is used as the filtering parameter for front page items.
    /// </summary>
    public enum FrontPageParameter
    {
        Date,
        Price,
        Rating,
        Name,
        /// <summary>
        /// Most sold items 
        /// </summary>
        Count
    }
}
