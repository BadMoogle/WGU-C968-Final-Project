using System;
using System.Windows;


namespace Inventory_Management_System
{
    /// <summary>
    /// Interaction logic for AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        public AddProduct()
        {
            InitializeComponent();
            updateDataGrids();
        }

        //Update the two datagrids on the form
        private void updateDataGrids()
        {
            dgrdPartsGlobal.Items.Clear();
            for (int i = 1; i <= Globals.inv.getNumberOfParts(); i++)
            {
                Part p = Globals.inv.lookupPart(i);
                dgrdPartsGlobal.Items.Add(new PartsDataItem() { PartID = p.getPartID(), PartName = p.getName(), InventoryLevel = p.getInStock(), price = p.getPrice() });
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //Replaces the default text on the text box when it gains or loses focus
        private void TxtbxMax_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtbxMax.Text == "Max")
            {
                txtbxMax.Text = "";
            }
        }

        //Replaces the default text on the text box when it gains or loses focus
        private void TxtbxMax_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtbxMax.Text == "")
            {
                txtbxMax.Text = "Max";
            }
        }

        //Replaces the default text on the text box when it gains or loses focus
        private void TxtbxMin_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtbxMin.Text == "")
            {
                txtbxMin.Text = "Min";
            }
        }

        //Replaces the default text on the text box when it gains or loses focus
        private void TxtbxMin_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtbxMin.Text == "Min")
            {
                txtbxMin.Text = "";
            }
        }

        //Replaces the default text on the text box when it gains or loses focus
        private void TxtbxPriceCost_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtbxPriceCost.Text == "Price/Cost")
            {
                txtbxPriceCost.Text = "";
            }
        }

        //Replaces the default text on the text box when it gains or loses focus
        private void TxtbxPriceCost_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtbxPriceCost.Text == "")
            {
                txtbxPriceCost.Text = "Price/Cost";
            }
        }

        //Replaces the default text on the text box when it gains or loses focus
        private void TxtbxInventory_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtbxInventory.Text == "")
            {
                txtbxInventory.Text = "Inv";
            }
        }

        //Replaces the default text on the text box when it gains or loses focus
        private void TxtbxInventory_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtbxInventory.Text == "Inv")
            {
                txtbxInventory.Text = "";
            }
        }

        //Replaces the default text on the text box when it gains or loses focus
        private void TxtbxProductName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtbxProductName.Text == "Product Name")
            {
                txtbxProductName.Text = "";
            }
        }

        //Replaces the default text on the text box when it gains or loses focus
        private void TxtbxProductName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtbxProductName.Text == "")
            {
                txtbxProductName.Text = "Product Name";
            }
        }

        //Adds a part to the product part list (doesn't add it to a product object yet since we only create one when the user hits save)
        private void BtnAddPart_Click(object sender, RoutedEventArgs e)
        {
            //Check if something is selected and that whatever is selected is a partsdata item.
            if (dgrdPartsGlobal.SelectedItem != null && dgrdPartsGlobal.SelectedItem.GetType() == typeof(PartsDataItem))
            {
                dgrdPartsProduct.Items.Add(dgrdPartsGlobal.SelectedItem);
            }
        }

        //Save button click method.  Creates a new product and adds to the inventory.
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Product pNew = new Product();
                pNew.setName(txtbxProductName.Text);
                pNew.setProductID(Globals.inv.getNextProductID());
                pNew.setMin(int.Parse(txtbxMin.Text));
                pNew.setMax(int.Parse(txtbxMax.Text));
                pNew.setInStock(int.Parse(txtbxInventory.Text));
                pNew.setPrice(double.Parse(txtbxPriceCost.Text));
                //Add the part to the new product by looking up each part that the user added to the list
                foreach (PartsDataItem pdi in dgrdPartsProduct.Items)
                {
                    pNew.addAssociatedPart(Globals.inv.lookupPart(pdi.PartID));
                }
                Globals.inv.addProduct(pNew);
                this.Close();
            }
            catch(FormatException)
            {
                MessageBox.Show("Please ensure that Min, Max, Machine ID, and Price are valid numbers with no special characters (e.g. $ or ,)", "Invalid Number", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }

        //Method to handle if the delete button is pressed.  Simply removes the entry from the datagrid.
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            //Check if something is selected and that whatever is selected is a partsdata item.
            if (dgrdPartsProduct.SelectedItem != null && dgrdPartsProduct.SelectedItem.GetType() == typeof(PartsDataItem))
            {
                dgrdPartsProduct.Items.Remove(dgrdPartsProduct.SelectedItem);
            }
        }

        //Method to handle the event that the search button was clicked.  Reset the datagrid if nothing is entered.
        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            dgrdPartsGlobal.Items.Clear();
            //If we get an int out of it, check part IDs for it.
            if (int.TryParse(txtbxSearch.Text, out int iSearch))
            {
                for (int i = 1; i <= Globals.inv.getNumberOfParts(); i++)
                {
                    Part p = Globals.inv.lookupPart(i);
                    if (iSearch == p.getPartID())
                    {
                        dgrdPartsGlobal.Items.Add(new PartsDataItem() { PartID = p.getPartID(), PartName = p.getName(), InventoryLevel = p.getInStock(), price = p.getPrice() });
                    }
                }
                //In the event the part name begins with a number, search here as well.
                for (int i = 1; i <= Globals.inv.getNumberOfParts(); i++)
                {
                    Part p = Globals.inv.lookupPart(i);
                    if (p.getName().ToUpper().Contains(txtbxSearch.Text.ToUpper()))
                    {
                        dgrdPartsGlobal.Items.Add(new PartsDataItem() { PartID = p.getPartID(), PartName = p.getName(), InventoryLevel = p.getInStock(), price = p.getPrice() });
                    }
                }
            }
            //If nothing is entered, reset the grid
            else if (txtbxSearch.Text == "")
            {
                updateDataGrids();
            }
            //Search the names for anything containing the specified text.  Converting to upper case to avoid case sensitivity.
            else
            {
                for (int i = 1; i <= Globals.inv.getNumberOfParts(); i++)
                {
                    Part p = Globals.inv.lookupPart(i);
                    if (p.getName().ToUpper().Contains(txtbxSearch.Text.ToUpper()))
                    {
                        dgrdPartsGlobal.Items.Add(new PartsDataItem() { PartID = p.getPartID(), PartName = p.getName(), InventoryLevel = p.getInStock(), price = p.getPrice() });
                    }
                }
            }
        }
    }
}
