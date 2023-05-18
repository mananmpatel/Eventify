using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventApplicationCore.Model
{
    [NotMapped]
    public class CustomerDashboardModel
    {
        public int TotalCustomerBooking { get; set; }
        public int TotalCustomerApprovedBooking { get; set; }
        public int TotalCustomerPendingBooking { get; set; }
        public int TotalCustomerRejectedBooking { get; set; }
        public int TotalCustomerCancelledBooking { get; set; }
    }
}
