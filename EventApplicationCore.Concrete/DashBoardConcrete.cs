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
            DashboardModel adminDashboardModel = new DashboardModel();

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
            adminDashboardModel.AdminDashboard = admindashboardmodel;
            return adminDashboardModel;
        }
        public DashboardModel CustomerDashboard(int userId)
        {
            DashboardModel customerDashboardModel = new DashboardModel();

            var CustomerDashboard = (from BD in _context.BookingDetails
                                     where BD.Createdby == userId
                                     select BD);

            int TotalCustomerBooking = CustomerDashboard.Where(x => x.BookingCompletedFlag.Equals("C")).Count();

            int TotalCustomerApprovedBooking = CustomerDashboard.Where(x => x.BookingCompletedFlag.Equals("C") && x.BookingApproval.Equals("A")).Count();

            int TotalCustomerPendingBooking = CustomerDashboard.Where(x => x.BookingCompletedFlag.Equals("C") && x.BookingApproval.Equals("P")).Count();
            
            int TotalCustomerRejectedBooking = CustomerDashboard.Where(x => x.BookingCompletedFlag.Equals("C") && x.BookingApproval.Equals("R")).Count();

            int TotalCustomerCancelledBooking = CustomerDashboard.Where(x => x.BookingCompletedFlag.Equals("C") && x.BookingApproval.Equals("C")).Count();

            CustomerDashboardModel customerdashboardmodel = new CustomerDashboardModel()
            {
                TotalCustomerBooking = TotalCustomerBooking,
                TotalCustomerApprovedBooking = TotalCustomerApprovedBooking,
                TotalCustomerPendingBooking = TotalCustomerPendingBooking,
                TotalCustomerRejectedBooking = TotalCustomerRejectedBooking,
                TotalCustomerCancelledBooking = TotalCustomerCancelledBooking
            };
            customerDashboardModel.CustomerDashBoard = customerdashboardmodel;
            return customerDashboardModel;
        }
    }
}
