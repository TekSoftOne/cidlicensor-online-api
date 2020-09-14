using System;
namespace OR.Data.Constants
{
    public class Data
    {
        public enum CustomerType
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
    }
}
