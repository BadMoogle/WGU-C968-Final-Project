using System;
using System.Collections.Generic;

namespace Inventory_Management_System
{
    class Inventory
    {
        private LinkedList<Product> products;  //Linked list containing all the products and their associated parts
        private LinkedList<Part> allParts; //Linked list containing all the parts
        private int nextPartID;  //Used to keep track of the highest part ID so we don't duplicate IDs
        private int nextProductID; //used to keep track of the highest product ID so we don't duplicate IDs.

        //Default constructor
        public Inventory()
        {
            //initialize the linked lists
            products = new LinkedList<Product>();
            allParts = new LinkedList<Part>();
            nextPartID = 1;
            nextProductID = 1;
        }

        //Returns the total number of parts
        public int getNumberOfParts()
        {
            return allParts.Count;
        }

        //Returns the total number of products
        public int getNumberofProducts()
        {
            return products.Count;
        }

        //Gets the next available product ID
        public int getNextProductID()
        {
            return nextProductID;
        }

        //Gets the next available part ID
        public int getNextPartID()
        {
            return nextPartID;
        }
        
        //Adds a product to the products list
        public void addProduct(Product prod)
        {
            products.AddLast(prod);
            nextProductID++;
        }

        //Removes a product from the list
        public Boolean removeProduct(int prod)
        {
            //since we're removing by product ID, we need to parse the list for them.
            bool removedSomething = false;
            foreach(Product p in products)
            {
                if(p.getProductID() == prod)
                {
                    products.Remove(p);
                    removedSomething = true;
                    break; //Break out of the modified collection
                }
            }
            return removedSomething; //return true if we removed something or false if we didn't
        }

        //Looks up a product based on the product ID
        public Product lookupProduct(int prod)
        {
            foreach (Product p in products)
            {
                if(p.getProductID() == prod)
                {
                    return p;
                }
            }
            return null; //Didn't find a product on ID.  Returning null.
        }

        //Updates a product in the list.
        public void updateProduct(int i, Product prod)
        {
            foreach (Product index in products)
            {
                if (index.getProductID() == i)
                {
                    products.Remove(index); //Remove the current part in the list
                    prod.setProductID(i); //set the ID to the old part
                    products.AddLast(prod); //Add it to the list
                    break; //break out of the loop since we modified the collection.
                }
            }
        }

        //Adds a part to the list
        public void addPart(Part p)
        {
            allParts.AddLast(p);
            nextPartID++;
        }

        //Deletes a part from the list
        public Boolean deletePart(Part p)
        {
            return allParts.Remove(p);
        }

        //Looks up a part based on part ID
        public Part lookupPart(int i)
        {
            foreach(Part p in allParts)
            {
                if (p.getPartID() == i)
                {
                    return p;
                }
            }
            return null; //Didn't find a part.  Returning null.
        }

        //Updates a part based on part ID
        public void updatePart(int i, Part p)
        {
           foreach(Part index in allParts)
           {
                if (index.getPartID() == i)
                {
                    allParts.Remove(index); //Remove the current part in the list
                    p.setPartID(i); //set the ID to the old part
                    allParts.AddLast(p); //Add it to the list
                    break; //Break out of the loop since we've modified the collection.
                }
           }
        }        
    }
}
