using System;
namespace OR.Data.Constants
{
    public class Data
    {
        public enum TypeOfCustomer
        {
            Resident = 1,
            Diplomat = 2,
            Tourist = 3
        }

        public enum Gender
        {
            Male = 1,
            Female = 2
        }

        public enum RequestCategory
        {
            New = 1,
            Renewal = 2,
            Replacement = 3
        }

        public enum Status
        {
            New = 1,
            Pending = 2,
            Rejected = 3,
            Approved = 4
        }
    }
}
