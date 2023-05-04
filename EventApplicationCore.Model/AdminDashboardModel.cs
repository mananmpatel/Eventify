using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventApplicationCore.Model
{
    [NotMapped]
    public class AdminDashboardModel
    {
        //public List<BookingDetails> BookingDetails { get; set; }
        public int TotalBooking { get; set; }
        public int TotalApprovedBooking { get; set; }
        public int TotalCancelledBooking { get; set; }
        public int TotalPendingBooking { get; set; }
        public int TotalRejectedBooking { get; set; }

    }
}
