using System;
using System.Windows.Forms;
using System.Diagnostics;

namespace Version_1_C
{
    public partial class frmArtist : Form
    {
        public frmArtist()
        {
            InitializeComponent();
        }

        private clsWorksList _WorksList;
        private clsArtist _Artist;

        private void UpdateDisplay()
        {
            txtName.Enabled = txtName.Text == "";
            if (_WorksList.SortOrder == 0)
            {
                _WorksList.SortByName();
                rbByName.Checked = true;
            }
            else
            {
                _WorksList.SortByDate();
                rbByDate.Checked = true;
            }

            lstWorks.DataSource = null;
            lstWorks.DataSource = _WorksList;
            lblTotal.Text = Convert.ToString(_WorksList.GetTotalValue());
        }

        public void SetDetails(clsArtist prArtist)
        {
            _Artist = prArtist;
            updateForm();
            UpdateDisplay();
            ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteWork(lstWorks.SelectedIndex);
            UpdateDisplay();
        }

        private void DeleteWork(int prIndex)
        {
            if (prIndex >= 0 && prIndex < _WorksList.Count)
            {
                if (MessageBox.Show("Are you sure?", "Deleting work", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _WorksList.DeleteWork(prIndex);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            _WorksList.AddWork();
            UpdateDisplay();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (isValid())
            { // Best place to protect and mb's. Validation should be completed in setter. 
                try
                {
                    pushData();
                    MessageBox.Show("New Artist Added!");
                    DialogResult = DialogResult.OK;
                }
                catch (Exception ex)
                {
                    // Message box?
                    MessageBox.Show("Unable to add artist.");
                    Debug.WriteLine(ex.GetBaseException().Message);
                }
                
            }
        }

        public virtual bool isValid()
        {   // Logic isn't accurate here.
            if (txtName.Enabled && txtName.Text != "")
                if (_Artist.isDuplicate(txtName.Text))
                {
                    return false;
                }
                else
                {
                    return true;
                }                   
            else
                return true;
        }

        private void lstWorks_DoubleClick(object sender, EventArgs e)
        {
            int lcIndex = lstWorks.SelectedIndex;
            if (lcIndex >= 0)
            {
                EditWork(lcIndex);
                UpdateDisplay();
            }
        }

        private void EditWork(int prIndex)
        {
            if (prIndex >= 0 && prIndex < _WorksList.Count)
            {
                _WorksList.EditWork(prIndex);
            }
            else
            {
                MessageBox.Show("Sorry no work selected #" + Convert.ToString(prIndex));
            }
        }

        private void rbByDate_CheckedChanged(object sender, EventArgs e)
        {
            _WorksList.SortOrder = Convert.ToByte(rbByDate.Checked);
            UpdateDisplay();
        }

        private void updateForm()
        {
            txtName.Text = _Artist.Name;
            txtSpeciality.Text = _Artist.Speciality;
            txtPhone.Text = _Artist.Phone;
            _WorksList = _Artist.WorksList;
            UpdateDisplay();
        }

        private void pushData()
        {
            _Artist.Name = txtName.Text;
            _Artist.Speciality = txtSpeciality.Text;
            _Artist.Phone = txtPhone.Text;
            _WorksList.SortOrder = _WorksList.SortOrder;
        }
    }
}