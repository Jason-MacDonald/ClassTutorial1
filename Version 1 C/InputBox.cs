using System;
using System.Windows.Forms;

namespace Version_1_C
{
    public partial class InputBox : Form
    {
        private string _Answer;
        private int _SelectedIndex = 0;

        public InputBox(string prQuestion)
        {
            InitializeComponent();
            lblQuestion.Text = prQuestion;
            lblError.Text = "";
            cbType.Focus();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (cbType.Text.Length > 0)
            {
                _Answer = cbType.Text;
                DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                lblError.Text = "No type selected.";
            }
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        public string getAnswer()
        {
            return _Answer;
        }

        private void cbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            _SelectedIndex = cbType.SelectedIndex;
        }
    }
}