using System;


namespace Inventory_Management_System
{
    public abstract class Part
    {
        private int partID;
        private String name;
        private double price;
        private int inStock;
        private int min;
        private int max;

        //Sets the Name of the part
        public void setName(String n)
        {
            name = n;
        }

        //Gets the Name of the part
        public String getName()
        {
            return name;
        }

        //Sets the Price
        public void setPrice(double p)
        {
            price = p;
        }

        //Gets the Price
        public double getPrice()
        {
            return price;
        }

        //Sets the In Stock
        public void setInStock(int stock)
        {
            inStock = stock;
        }

        //Gets the In Stock
        public int getInStock()
        {
            return inStock;
        }

        //Sets the Min
        public void setMin(int m)
        {
            min = m;
        }

        //Gets the Min
        public int getMin()
        {
            return min;
        }

        //Sets the Max
        public void setMax(int m)
        {
            max = m;
        }

        //Gets the Max
        public int getMax()
        {
            return max;
        }

        //Sets the Part ID
        public void setPartID(int p)
        {
            partID = p;
        }

        //Gets the part ID
        public int getPartID()
        {
            return partID;
        }

    }
}
