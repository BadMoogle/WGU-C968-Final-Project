using System;
using System.Collections.Generic;

namespace Inventory_Management_System
{
    class Product
    {
        private LinkedList<Part> associatedParts;
        private int productID;
        private String name;
        private double price;
        private int inStock;
        private int min;
        private int max;

        //Default constructor.
        public Product()
        {
            associatedParts = new LinkedList<Part>();
        }

        //Sets the name of the product
        public void setName(String n)
        {
            name = n;
        }

        //Gets the name of the product
        public String getName()
        {
            return name;
        }

        //Sets the price of the product
        public void setPrice(double p)
        {
            price = p;
        }

        //Gets the price of the product
        public double getPrice()
        {
            return price;
        }

        //Gets the product ID for the product
        public int getProductID()
        {
            return productID;
        }

        //Sets the Product ID for the product
        public void setProductID(int i)
        {
            productID = i;
        }

        //Sets the In Stock for the product
        public void setInStock(int s)
        {
            inStock = s;
        }

        //Gets the In Stock for the product
        public int getInStock()
        {
            return inStock;
        }

        //Sets the Min for the product
        public void setMin(int m)
        {
            min = m;
        }

        //Gets the Min for the product
        public int getMin()
        {
            return min;
        }

        //Sets the Max for the product
        public void setMax(int m)
        {
            max = m;
        }

        //Returns the max for the product
        public int getMax()
        {
            return max;
        }

        public int getNumberofAssociatedParts()
        {
            return associatedParts.Count;
        }

        //Adds an associated part to the product
        public void addAssociatedPart(Part p)
        {
            associatedParts.AddLast(p);
        }

        //Remove an associated part from the product
        public Boolean removeAssociatedPart(int i)
        {
            //Iterate the linked list and remove the first product that matches the ID.
            foreach (Part p in associatedParts)
            {
                if (p.getPartID() == i)
                {
                    associatedParts.Remove(p);
                    return true;
                }
            }
            return false;
        }

        //Lookup an associated part with the product.  Returns null if not found.
        public Part lookupAssociatedPart(int i)
        {
            //find the part in the linked list
            foreach(Part p in associatedParts)
            {
                if(p.getPartID() == i)
                {
                    return p;
                }
            }
            return null; //return null if we find nothing
        }

    }
}
