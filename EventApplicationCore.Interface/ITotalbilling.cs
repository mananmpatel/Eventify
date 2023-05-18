using EventApplicationCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventApplicationCore.Interface
{
    public interface ITotalbilling
    {
        int TotalAmount(int? BookingID);
        BillingModel GetBillingDetailsbyBookingNo(string BookingNo);
        CompleteBookingModel ShowCompleteBookingDetailsbyBookingNo(string BookingNo);
    }
}
