using System;
using System.Windows;

namespace Inventory_Management_System
{
    /// <summary>
    /// Interaction logic for ModifyPart.xaml
    /// </summary>
    public partial class ModifyPart : Window
    {
        //Modify Part form constructor.  We require the part ID to modify a part.
        public ModifyPart(int id)
        {
            InitializeComponent();
            Part p = Globals.inv.lookupPart(id);
            if(p != null)
            {
                txtbxID.Text = p.getPartID().ToString();
                txtbxPartName.Text = p.getName();
                txtbxInventory.Text = p.getInStock().ToString();
                txtbxMin.Text = p.getMin().ToString();
                txtbxMax.Text = p.getMax().ToString();
                txtbxPriceCost.Text = p.getPrice().ToString();
                //Determine what type of part we're dealing with then update accordingly.
                if(p.GetType() == typeof(Outsourced)) 
                {
                    radOutsourced.IsChecked = true;
                    radInHouse.IsChecked = false;
                    Outsourced o = (Outsourced) p;
                    lblCompanyMachineID.Content = "Company Name";
                    txtbxCompanyorMachineID.Text = o.getCompanyName().ToString();
                }
                if (p.GetType() == typeof(Inhouse))
                {
                    radOutsourced.IsChecked = false;
                    radInHouse.IsChecked = true;
                    Inhouse o = (Inhouse)p;
                    lblCompanyMachineID.Content = "Machine ID";
                    txtbxCompanyorMachineID.Text = o.getMachineID().ToString();
                }
            }
            
        }

        //Cancel button was clicked, just close the form.
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //Method for handling the save button being clicked.
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            int partID;
            int.TryParse(txtbxID.Text, out partID);
            //Determine if the part is inHouse or Outsourced so that we can create the correct replacement object.
            if (radInHouse.IsChecked == true)
            {
                try
                {
                    Inhouse p = new Inhouse();
                    p.setName(txtbxPartName.Text);
                    p.setPartID(int.Parse(txtbxID.Text));
                    p.setInStock(int.Parse(txtbxInventory.Text));
                    p.setPrice(double.Parse(txtbxPriceCost.Text));
                    p.setMin(int.Parse(txtbxMin.Text));
                    p.setMax(int.Parse(txtbxMax.Text));
                    p.setMachineID(int.Parse(txtbxCompanyorMachineID.Text));
                    Globals.inv.updatePart(partID, p);
                    this.Close();
                }
                catch(FormatException)
                {
                    MessageBox.Show("Please ensure that Min, Max, Machine ID, and Price are valid numbers with no special characters (e.g. $ or ,)", "Invalid Number", MessageBoxButton.OK, MessageBoxImage.Stop);
                }
            }
            else
            {
                try
                {
                    Outsourced p = new Outsourced();
                    p.setName(txtbxPartName.Text);
                    p.setName(txtbxPartName.Text);
                    p.setPartID(int.Parse(txtbxID.Text));
                    p.setInStock(int.Parse(txtbxInventory.Text));
                    p.setPrice(double.Parse(txtbxPriceCost.Text));
                    p.setMin(int.Parse(txtbxMin.Text));
                    p.setMax(int.Parse(txtbxMax.Text));
                    p.setCompanyName(txtbxCompanyorMachineID.Text);
                    Globals.inv.updatePart(partID, p);
                    this.Close();
                }
                catch (FormatException)
                {
                    MessageBox.Show("Please ensure that Min, Max, Machine ID, and Price are valid numbers with no special characters (e.g. $ or ,)", "Invalid Number", MessageBoxButton.OK, MessageBoxImage.Stop);
                }
            }
        }

        private void RadInHouse_Checked(object sender, RoutedEventArgs e)
        {
            if (txtbxCompanyorMachineID != null) //txtbxCompanyName may not be initialized when this triggers.  Should only happen during from load.
            {
                lblCompanyMachineID.Content = "Machine ID";
                if (txtbxCompanyorMachineID.Text == "Company Name" || txtbxCompanyorMachineID.Text == "Machine ID")
                {
                    txtbxCompanyorMachineID.Text = "Machine ID";
                }
            }
        }

        private void RadOutsourced_Checked(object sender, RoutedEventArgs e)
        {
            lblCompanyMachineID.Content = "Company Name";
            if (txtbxCompanyorMachineID.Text == "Company Name" || txtbxCompanyorMachineID.Text == "Machine ID")
            {
                txtbxCompanyorMachineID.Text = "Company Name";
            }
        }
    }
}
