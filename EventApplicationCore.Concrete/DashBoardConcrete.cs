using EventApplicationCore.Interface;
using EventApplicationCore.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EventApplicationCore.Concrete
{
    public class DashBoardConcrete : IDashboard
    {
        private DatabaseContext _context;

        public DashBoardConcrete(DatabaseContext context)
        {
            _context = context;
        }
        public DashboardModel AdminDashboard()
        {
            //where BD.BookingCompletedFlag.Equals('C')
            DashboardModel dashboardModel = new DashboardModel();
            var AdminDashboard = (from BD in _context.BookingDetails
                                  select BD);
            int TotalBooking = AdminDashboard.Where(x => x.BookingCompletedFlag.Equals("C")).Count();

            int TotalApprovedBooking = AdminDashboard.Where(x => x.BookingApproval.Equals("A")).Count();

            int TotalCancelledBooking = AdminDashboard.Where(x => x.BookingApproval.Equals("C")).Count();

            int TotalPendingBooking = AdminDashboard.Where(x => x.BookingCompletedFlag.Equals("C") && x.BookingApproval.Equals("P")).Count();

            int TotalRejectedBooking = AdminDashboard.Where(x => x.BookingApproval.Equals("R")).Count();
            AdminDashboardModel admindashboardmodel = new AdminDashboardModel()
            {
                TotalBooking = TotalBooking,
                TotalApprovedBooking = TotalApprovedBooking,
                TotalCancelledBooking = TotalCancelledBooking,
                TotalPendingBooking = TotalPendingBooking,
                TotalRejectedBooking = TotalRejectedBooking
            };
            dashboardModel.AdminDashboard = admindashboardmodel;
            return dashboardModel;
        }
    }
}
