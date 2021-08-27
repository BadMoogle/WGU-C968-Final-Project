using System;
using System.Windows;

namespace Inventory_Management_System
{
    /// <summary>
    /// Interaction logic for AddPart.xaml
    /// </summary>
    public partial class AddPart : Window
    {
        public AddPart()
        {
            InitializeComponent();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //Replaces the default text on the text box when it gains or loses focus
        private void TxtbxPartName_GotFocus(object sender, RoutedEventArgs e)
        {
            if(txtbxPartName.Text == "Part Name")
            {
                txtbxPartName.Text = "";
            }
        }

        //Replaces the default text on the text box when it gains or loses focus
        private void TxtbxPartName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtbxPartName.Text == "")
            {
                txtbxPartName.Text = "Part Name";
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
        private void TxtbxMax_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtbxMax.Text == "")
            {
                txtbxMax.Text = "Max";
            }
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
        private void TxtbxMin_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtbxMin.Text == "Min")
            {
                txtbxMin.Text = "";
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
        private void TxtbxCompanyName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtbxCompanyName.Text == "")
            {
                txtbxCompanyName.Text = "Company Name";
            }
        }

        //Replaces the default text on the text box when it gains or loses focus
        private void TxtbxCompanyName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtbxCompanyName.Text == "Company Name" || txtbxCompanyName.Text == "Machine ID")
            {
                txtbxCompanyName.Text = "";
            }
        }

        //Switches the content of the label from Machine ID to Company name depending on if in house or outsourced is clicked.
        private void RadbtnOutsourced_Checked(object sender, RoutedEventArgs e)
        {
            lblCompanyMachineID.Content = "Company Name";
            if (txtbxCompanyName.Text == "Company Name" || txtbxCompanyName.Text == "Machine ID")
            {
                txtbxCompanyName.Text = "Company Name";
            }
        }

        //Switches the content of the label from Machine ID to Company name depending on if in house or outsourced is clicked.
        private void RadbtnInHouse_Checked(object sender, RoutedEventArgs e)
        {
            if (txtbxCompanyName != null) //txtbxCompanyName may not be initialized when this triggers.  Should only happen during from load.
            {
                lblCompanyMachineID.Content = "Machine ID";
                if (txtbxCompanyName.Text == "Company Name" || txtbxCompanyName.Text == "Machine ID")
                {
                    txtbxCompanyName.Text = "Machine ID";
                }
            }
        }

        //Method to handle if the save button is clicked.
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            //If this is an In House part, then add that to the part list.
            if (radbtnInHouse.IsChecked == true)
            {
                try
                { 
                    Inhouse inPart = new Inhouse();
                    int itemp; //temp variable for parsing strings
                    double dtemp; //temp variable for parsing strings
                    //Add the text the user inputted into our new part.
                    inPart.setName(txtbxPartName.Text);
                    inPart.setInStock(int.Parse(txtbxInventory.Text));
                    inPart.setPrice(double.Parse(txtbxPriceCost.Text));
                    inPart.setMax(int.Parse(txtbxMax.Text));
                    inPart.setMin(int.Parse(txtbxMin.Text));
                    inPart.setMachineID(int.Parse(txtbxCompanyName.Text));
                    inPart.setPartID(Globals.inv.getNextPartID()); //Assigns the next part ID to this part.
                    Globals.inv.addPart(inPart); //Add the new part to the inventory
                    this.Close(); //Close the current AddPart Window
                }
                catch(FormatException)
                {
                    MessageBox.Show("Please ensure that Min, Max, Machine ID, and Price are valid numbers with no special characters (e.g. $ or ,)", "Invalid Number", MessageBoxButton.OK,MessageBoxImage.Stop);
                }
            }
            else //If it isn't inhouse, then it's outsourced
            {
                try
                {
                    Outsourced inPart = new Outsourced();
                    inPart.setName(txtbxPartName.Text);
                    inPart.setInStock(int.Parse(txtbxInventory.Text));
                    inPart.setPrice(double.Parse(txtbxPriceCost.Text));
                    inPart.setMax(int.Parse(txtbxMax.Text));
                    inPart.setMin(int.Parse(txtbxMin.Text));
                    inPart.setCompanyName(txtbxCompanyName.Text);
                    inPart.setPartID(Globals.inv.getNextPartID()); //Assigns the next part ID to this part.
                    Globals.inv.addPart(inPart); //Add the new part to the inventory
                    this.Close(); //Close the current AddPart Window
                }
                catch(FormatException)
                {
                    MessageBox.Show("Please ensure that Min, Max, Machine ID, and Price are valid numbers with no special characters (e.g. $ or ,)", "Invalid Number", MessageBoxButton.OK, MessageBoxImage.Stop);
                }
            }
        }
    }
}
