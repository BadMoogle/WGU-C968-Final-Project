using System;


namespace Inventory_Management_System
{
    class Outsourced : Part
    {
        private String companyName;

        //Gets the Company Name
        public String getCompanyName()
        {
            return companyName;
        }

        //Sets the Company Name.
        public void setCompanyName(String name)
        {
            companyName = name;
        }

    }
}
