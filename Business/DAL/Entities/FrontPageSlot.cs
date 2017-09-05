using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Trainee.Business.DAL.Entities
{
    public class FrontPageSlot
    {
        public int SlotId { get; set; }
        public int ItemId { get; set; }

        [NotMapped]
        public FrontPageItem Item { get; set; }
    }
}
