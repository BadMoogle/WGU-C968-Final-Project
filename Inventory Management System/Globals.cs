using System;

namespace Inventory_Management_System
{
    //Used to keep a global inventory and index of parts that is available to all forms within the application
    static class Globals
    {
        public static Inventory inv = new Inventory();  //The complete inventory
    }

    //Class datastructure used to populate the datagrids with items
    public class PartsDataItem
    {
        public int PartID { get; set; }
        public String PartName { get; set; }
        public int InventoryLevel { get; set; }
        public double price { get; set; }
    }

    //Class datastructure used to populate the datagrids with items
    public class ProductsDataItem
    {
        public int ProductID { get; set; }
        public String PartName { get; set; }
        public int InventoryLevel { get; set; }
        public double price { get; set; }
    }
}
