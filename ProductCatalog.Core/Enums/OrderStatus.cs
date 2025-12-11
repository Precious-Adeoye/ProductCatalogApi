using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.Core.Enums
{
    public enum OrderStatus
    {
        [Display(Name = "Pending")]
        Pending = 1,

        [Display(Name = "Processing")]
        Processing = 2,

        [Display(Name = "Completed")]
        Completed = 3,

        [Display(Name = "Cancelled")]
        Cancelled = 4,

        [Display(Name = "Failed")]
        Failed = 5
    }
}
