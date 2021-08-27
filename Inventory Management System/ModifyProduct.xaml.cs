using System;
using System.Windows;


namespace Inventory_Management_System
{

    /// <summary>
    /// Interaction logic for ModifyProduct.xaml
    /// </summary>
    public partial class ModifyProduct : Window
    {

        //The only constructor requires a product ID so that we can look it up and populate the fields
        public ModifyProduct(int product)
        {
            InitializeComponent();
            Product pTemp;
            pTemp = Globals.inv.lookupProduct(product);
            txtbxID.Text = pTemp.getProductID().ToString();
            txtbxProductName.Text = pTemp.getName();
            txtbxMin.Text = pTemp.getMin().ToString();
            txtbxMax.Text = pTemp.getMax().ToString();
            txtbxPriceCost.Text = pTemp.getPrice().ToString();
            txtbxInventory.Text = pTemp.getInStock().ToString();
            updateDataGrids();
            //Iterate through the entire parts list populating the datagrid with what matches the product.
            //I'm sure I could do this more efficiently by exposing the linked list, but that is beyond the scope of the assignment.
            for(int i = 1; i <= Globals.inv.getNumberOfParts(); i++)
            {
                Part p = pTemp.lookupAssociatedPart(i);
                if(p != null)
                {
                    dgrdPartsProduct.Items.Add(new PartsDataItem() { PartID = p.getPartID(), PartName = p.getName(), InventoryLevel = p.getInStock(), price = p.getPrice() });
                }
                
            }
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

        //Method to handle when the Cancel button is clicked.
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //Method to handle when the save button is clicked.
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Product pNew = new Product();
                pNew.setName(txtbxProductName.Text);
                pNew.setProductID(int.Parse(txtbxID.Text));
                pNew.setMin(int.Parse(txtbxMin.Text));
                pNew.setMax(int.Parse(txtbxMax.Text));
                pNew.setInStock(int.Parse(txtbxInventory.Text));
                pNew.setPrice(double.Parse(txtbxPriceCost.Text));
                //Add the part to the new product by looking up each part that the user added to the list
                foreach (PartsDataItem pdi in dgrdPartsProduct.Items)
                {
                    pNew.addAssociatedPart(Globals.inv.lookupPart(pdi.PartID));
                }
                Globals.inv.updateProduct(pNew.getProductID(), pNew);
                this.Close();
            }
            catch(FormatException)
            {
                MessageBox.Show("Please ensure that Min, Max, and Price are valid numbers with no special characters (e.g. $ or ,)", "Invalid Number", MessageBoxButton.OK, MessageBoxImage.Stop);
            }
        }

        //Method to handle when the delete button is clicked.
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            //Check if something is selected and that whatever is selected is a partsdata item.
            if (dgrdPartsProduct.SelectedItem != null && dgrdPartsProduct.SelectedItem.GetType() == typeof(PartsDataItem))
            {
                dgrdPartsProduct.Items.Remove(dgrdPartsProduct.SelectedItem);
            }
        }

        //Method to handle when the search button is clicked.
        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            dgrdPartsGlobal.Items.Clear();
            //If we get an int out of it, check part IDs for it.
            if (int.TryParse(txtbxSearch.Text, out int iSearch))
            {
                for (int i = 1; i <= Globals.inv.getNumberOfParts(); i++)
                {
                    Part p = Globals.inv.lookupPart(i);
                    if(iSearch == p.getPartID())
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

        //Method to handle the Add button being clicked.  Simply adds the selected item to the product datagrid.
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            //Check if something is selected and that whatever is selected is a partsdata item.
            if (dgrdPartsGlobal.SelectedItem != null && dgrdPartsGlobal.SelectedItem.GetType() == typeof(PartsDataItem))
            {
                dgrdPartsProduct.Items.Add(dgrdPartsGlobal.SelectedItem);
            }
        }
    }
}
