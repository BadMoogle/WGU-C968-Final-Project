using System;
using System.Windows;


namespace Inventory_Management_System
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Default Constructor
        public MainWindow()
        {
            InitializeComponent();
        }

        //Handler for the Exit button being clicked.
        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        //Handler for the Add Part button being clicked.
        private void BtnAddPart_Click(object sender, RoutedEventArgs e)
        {
            AddPart addPart = new AddPart()
            {
                Owner = this
            };
            addPart.ShowDialog();
            updateDataGrids();
        }


        //Handler for the Add Product button being clicked.
        private void BtnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            AddProduct p = new AddProduct
            {
                Owner = this
            };
            p.ShowDialog();
            updateDataGrids();
        }

        //Update the two datagrids on the form
        private void updateDataGrids()
        {
            dgrdParts.Items.Clear();   
            for (int i = 1; i <= Globals.inv.getNumberOfParts(); i++)
            {
                Part p = Globals.inv.lookupPart(i);
                dgrdParts.Items.Add(new PartsDataItem() { PartID = p.getPartID(), PartName = p.getName(), InventoryLevel = p.getInStock(), price = p.getPrice() });
            }

            dgrdProducts.Items.Clear();
            for (int i = 1; i <= Globals.inv.getNumberofProducts(); i++)
            {
                Product p = Globals.inv.lookupProduct(i);
                dgrdProducts.Items.Add(new ProductsDataItem() { ProductID = p.getProductID(), PartName = p.getName(), InventoryLevel = p.getInStock(), price = p.getPrice() });
            }
        }

        private void BtnModifyPart_Click(object sender, RoutedEventArgs e)
        {
            //Check if something is selected and that whatever is selected is a partsdata item.
            if (dgrdParts.SelectedItem != null && dgrdParts.SelectedItem.GetType() == typeof(PartsDataItem))
            {
                PartsDataItem pdi = (PartsDataItem)dgrdParts.SelectedItem;
                ModifyPart p = new ModifyPart(pdi.PartID)
                {
                    Owner = this
                };
                p.ShowDialog();
                updateDataGrids();
            }
        }

        private void BtnModifyProduct_Click(object sender, RoutedEventArgs e)
        {
            //Check if something is selected and that whatever is selected is a partsdata item.
            if (dgrdProducts.SelectedItem != null && dgrdProducts.SelectedItem.GetType() == typeof(ProductsDataItem))
            {
                ProductsDataItem pdi = (ProductsDataItem)dgrdProducts.SelectedItem;
                ModifyProduct p = new ModifyProduct(pdi.ProductID)
                {
                    Owner = this
                };
                p.ShowDialog();
                updateDataGrids();
            }
        }

        private void BtnDeletePart_Click(object sender, RoutedEventArgs e)
        {
            //Check if something is selected and that whatever is selected is a partsdata item.
            if (dgrdParts.SelectedItem != null && dgrdParts.SelectedItem.GetType() == typeof(PartsDataItem))
            {
                PartsDataItem pdi = (PartsDataItem)dgrdParts.SelectedItem;
                Globals.inv.deletePart(Globals.inv.lookupPart(pdi.PartID));
                updateDataGrids();
            }
        }

        private void BtnDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            //Check if something is selected and that whatever is selected is a partsdata item.
            if (dgrdProducts.SelectedItem != null && dgrdProducts.SelectedItem.GetType() == typeof(ProductsDataItem))
            {
                ProductsDataItem pdi = (ProductsDataItem)dgrdProducts.SelectedItem;
                Product p = Globals.inv.lookupProduct(pdi.ProductID);
                if(p.getNumberofAssociatedParts() != 0)
                {
                    MessageBox.Show("Unable to remove product.  Selected product still has associated parts.", "Unable to Remove", MessageBoxButton.OK, MessageBoxImage.Stop);
                }
                else
                {
                    Globals.inv.removeProduct(pdi.ProductID);
                    updateDataGrids();
                }
            }
        }

        //Method for the event of the part search button being clicked.
        private void BtnPartSearch_Click(object sender, RoutedEventArgs e)
        {
            dgrdParts.Items.Clear();
            //If we get an int out of it, check part IDs for it.
            if (int.TryParse(txtbxPartSearch.Text, out int iSearch))
            {
                for (int i = 1; i <= Globals.inv.getNumberOfParts(); i++)
                {
                    Part p = Globals.inv.lookupPart(i);
                    if (iSearch == p.getPartID())
                    {
                        dgrdParts.Items.Add(new PartsDataItem() { PartID = p.getPartID(), PartName = p.getName(), InventoryLevel = p.getInStock(), price = p.getPrice() });
                    }
                }
                //In the event the part name begins with a number, search here as well.
                for (int i = 1; i <= Globals.inv.getNumberOfParts(); i++)
                {
                    Part p = Globals.inv.lookupPart(i);
                    if (p.getName().ToUpper().Contains(txtbxPartSearch.Text.ToUpper()))
                    {
                        dgrdParts.Items.Add(new PartsDataItem() { PartID = p.getPartID(), PartName = p.getName(), InventoryLevel = p.getInStock(), price = p.getPrice() });
                    }
                }
            }
            //If nothing is entered, reset the grid
            else if (txtbxPartSearch.Text == "")
            {
                updateDataGrids();
            }
            //Search the names for anything containing the specified text.  Converting to upper case to avoid case sensitivity.
            else
            {
                for (int i = 1; i <= Globals.inv.getNumberOfParts(); i++)
                {
                    Part p = Globals.inv.lookupPart(i);
                    if (p.getName().ToUpper().Contains(txtbxPartSearch.Text.ToUpper()))
                    {
                        dgrdParts.Items.Add(new PartsDataItem() { PartID = p.getPartID(), PartName = p.getName(), InventoryLevel = p.getInStock(), price = p.getPrice() });
                    }
                }
            }
        }

        //Method for the event that the product search button was clicked.
        private void BtnProductSearch_Click(object sender, RoutedEventArgs e)
        {
            dgrdProducts.Items.Clear();
            //If we get an int out of it, check product IDs for it.
            if (int.TryParse(txtbxProductSearch.Text, out int iSearch))
            {
                for (int i = 1; i <= Globals.inv.getNumberofProducts(); i++)
                {
                    Product p = Globals.inv.lookupProduct(i);
                    if (iSearch == p.getProductID())
                    {
                        dgrdProducts.Items.Add(new ProductsDataItem() { ProductID = p.getProductID(), PartName = p.getName(), InventoryLevel = p.getInStock(), price = p.getPrice() });
                    }
                }
                //In the event the product name begins with a number, search here as well.
                for (int i = 1; i <= Globals.inv.getNumberofProducts(); i++)
                {
                    Product p = Globals.inv.lookupProduct(i);
                    if (p.getName().ToUpper().Contains(txtbxProductSearch.Text.ToUpper()))
                    {
                        dgrdProducts.Items.Add(new ProductsDataItem() { ProductID = p.getProductID(), PartName = p.getName(), InventoryLevel = p.getInStock(), price = p.getPrice() });
                    }
                }
            }
            //If nothing is entered, reset the grid
            else if (txtbxProductSearch.Text == "")
            {
                updateDataGrids();
            }
            //Search the names for anything containing the specified text.  Converting to upper case to avoid case sensitivity.
            else
            {
                for (int i = 1; i <= Globals.inv.getNumberofProducts(); i++)
                {
                    Product p = Globals.inv.lookupProduct(i);
                    if (p.getName().ToUpper().Contains(txtbxProductSearch.Text.ToUpper()))
                    {
                        dgrdProducts.Items.Add(new ProductsDataItem() { ProductID = p.getProductID(), PartName = p.getName(), InventoryLevel = p.getInStock(), price = p.getPrice() });
                    }
                }
            }
        }
    }
}
