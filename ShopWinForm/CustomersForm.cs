using System.Data;
using System.Diagnostics.Contracts;

namespace ShopWinForm
{
    public partial class CustomersForm : Form
    {

        #region Enumerado
        public enum DisplayMode
        {
          Reading,
          Adding,
          Editing
        }

        public DisplayMode displayMode;

        #endregion


        public CustomersForm()
        {
            InitializeComponent();
            reloadGrid();
        }

        #region Events
        #region btnAddNew
        /// <summary>
        /// Add New button clicked
        /// </summary>
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            displayMode = DisplayMode.Adding;
            display();

        }
        #endregion

        #region btnEdit
        private void btnEdit_Click(object sender, EventArgs e)
        {
            displayMode = DisplayMode.Editing;
            display();
        }
        #endregion

        #region btnDelete
        private void btnDelete_Click(object sender, EventArgs e)
        {
            bool result = false;
            DialogResult response = MessageBox.Show("Do you really wanna delete this item?", "acknowledgemet", MessageBoxButtons.YesNo);

            if (response == DialogResult.Yes)
            {
                Customer c = getCustomerInfo();
                result = Repository.deleteCustomer(c);

                // Si todo ha ido bien, mostramos un mensaje
                if (result)
                {
                    MessageBox.Show("Your item was deleted successfully.");
                    displayMode = DisplayMode.Reading;
                    display();

                    //Recargamos el grid
                    reloadGrid();
                }

            }
        }
        #endregion

        #region btnSave
        private void btnSave_Click(object sender, EventArgs e)
        {
            bool result = false;

            if (!String.IsNullOrEmpty(txtName.Text))
            {
                Customer c = getCustomerInfo();
                switch (displayMode)
                {
                    case DisplayMode.Adding:
                        result = Repository.setCustomer(c);
                        break;
                    case DisplayMode.Editing:
                        result = Repository.editCustomer(c);
                        break;
                }

                if (result)
                {
                    MessageBox.Show("The database was update successfully.");
                    displayMode = DisplayMode.Reading;
                    display();
                    reloadGrid();
                }
                else
                {
                    MessageBox.Show("There was an error updating the databases. Please try again later.");
                }
            }
        }
        #endregion

        #region btnCancel
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult response = MessageBox.Show("Do you really wanna exit from editing?", "Confirmation", MessageBoxButtons.YesNo);

            if (response == DialogResult.Yes)
            {
                displayMode = DisplayMode.Reading;
                display();

                if (dgvList.SelectedRows.Count == 1)
                {
                    setSelectedRow(dgvList.SelectedRows[0]);
                }
            }
        }
        #endregion

        #region helpers
        private Customer getCustomerInfo()
        {
            Customer customer = new Customer();

            customer.name = txtName.Text;
            customer.registeredAt = dtRegisteredAt.Value;

            if (!String.IsNullOrEmpty(txtId.Text))
            {
                customer.id = Convert.ToInt32(txtId.Text);
            }

            return customer;
        }

        public void reloadGrid()
        {
            DataSet ds = Repository.getCustomers();
            dgvList.DataSource = ds.Tables[0];

            dgvList.Columns["id"].Width = 100;
            dgvList.Columns["name"].Width = 300;
            dgvList.Columns["registeredAt"].Width = 352;

            dgvList.Columns["registeredAt"].HeaderText = "Registered At";

            //Focus on first rows
            if (dgvList.Rows.Count > 0)
            {
                dgvList.Rows[0].Selected = true;
            }

        }

        /// <summary>
        /// Display screen according to displayMode
        /// </summary>
        private void display()
        {
            switch (displayMode)
            {
                case DisplayMode.Reading:

                    activeControlsForReading();
                    break;

                case DisplayMode.Editing:

                    

                    activeControlsForEditing();



                    break;

                case DisplayMode.Adding:
                    clearAll();
                    activeControlsForEditing();

                    break;
            }
        }

        /// <summary>
        /// Clear all data from TextBoxes
        /// </summary>
        private void clearAll()
        {
            txtId.Text = "";
            txtName.Text = "";
            dtRegisteredAt.Value = DateTime.Now;
        }

        /// <summary>
        /// Enable or Disable controls for editing mode
        /// </summary>
        private void activeControlsForEditing()
        {
            txtId.Enabled = false;
            txtName.Enabled = true;
            dtRegisteredAt.Enabled = true;


            btnAddNew.Enabled = true;
            btnCancel.Enabled = true;

            btnAddNew.Enabled = false;
            btnDelete.Enabled = false;
            btnEdit.Enabled = false;

            dgvList.Enabled = false;
        }
        private void activeControlsForReading()
        {

            txtId.Enabled = false;
            txtName.Enabled = false;
            dtRegisteredAt.Enabled = false;

            btnAddNew.Enabled = true;
            btnDelete.Enabled = true;
            btnEdit.Enabled = true;
            btnSave.Enabled = true;
            btnCancel.Enabled = true;

            dgvList.Enabled = true;


        }

        public void setSelectedRow(DataGridViewRow selectedRow)
        {
            txtId.Text = selectedRow.Cells["Id"].Value.ToString();
            txtName.Text = selectedRow.Cells["name"].Value.ToString();
            dtRegisteredAt.Value = (DateTime)selectedRow.Cells["registeredAt"].Value;

        }
        private void dgvList_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            if (e.StateChanged != DataGridViewElementStates.Selected) return;

            DataGridViewRow selectedRow = e.Row;
            setSelectedRow(selectedRow);

        }
        private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dgvList.Rows[e.RowIndex].Selected = true;
        }






        #endregion

        #endregion


    }
}