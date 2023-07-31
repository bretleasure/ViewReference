using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ViewReference.UI
{
    public partial class ConfigUI : Form
    {
        public ConfigUI()
        {
            InitializeComponent();
        }

        TextBox ActiveTextbox;

        private void ConfigUI_Load(object sender, EventArgs e)
        {
            ImportParameters();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ckb_Detail_CheckedChanged(object sender, EventArgs e)
        {
            pnl_DetailStyle.Enabled = ckb_Detail.Checked;
        }

        private void ckb_Section_CheckedChanged(object sender, EventArgs e)
        {
            pnl_SectionStyle.Enabled = ckb_Section.Checked;
        }

        private void ckb_Aux_CheckedChanged(object sender, EventArgs e)
        {
            pnl_AuxStyle.Enabled = ckb_Aux.Checked;
        }
                
        private void ckb_Projected_CheckedChanged(object sender, EventArgs e)
        {
            pnl_ProjectedStyle.Enabled = ckb_Projected.Checked;
        }

        private void btn_Callout1_Click(object sender, EventArgs e)
        {
            txb_CalloutStyle.Text = "<VIEW> (Sh. <VIEW SHEET #>)";
        }

        private void btn_Callout2_Click(object sender, EventArgs e)
        {
            txb_CalloutStyle.Text = "<VIEW> (<VIEW SHEET #>)";
        }

        private void btn_Callout3_Click(object sender, EventArgs e)
        {
            txb_CalloutStyle.Text = "<VIEW> (SH <VIEW SHEET #>)";
        }

        private void btn_Callout4_Click(object sender, EventArgs e)
        {
            txb_CalloutStyle.Text = "<VIEW><PARENT SHEET #> SH<VIEW SHEET #>";
        }

        private void btn_Label1_Click(object sender, EventArgs e)
        {
            //DETAIL A
            //SHEET 1

            txb_DetailStyle.Text = "<VIEW><DELIM>SHEET <PARENT SHEET #>";            
            txb_SectionStyle.Text = "<VIEW>-<VIEW><DELIM>SHEET <PARENT SHEET #>";
            txb_AuxStyle.Text = "<VIEW>-<VIEW><DELIM>SHEET <PARENT SHEET #>";
            txb_ProjectedStyle.Text = "<VIEW>-<VIEW><DELIM>SHEET <PARENT SHEET #>";
        }

        private void btn_Label2_Click(object sender, EventArgs e)
        {
            //DETAIL A (1)

            txb_DetailStyle.Text = "<VIEW> (<PARENT SHEET #>)";
            txb_SectionStyle.Text = "<VIEW>-<VIEW> (<PARENT SHEET #>)";
            txb_AuxStyle.Text = "<VIEW>-<VIEW> (<PARENT SHEET #>)";
            txb_ProjectedStyle.Text = "<VIEW>-<VIEW> (<PARENT SHEET #>)";
        }

        private void btn_Label3_Click(object sender, EventArgs e)
        {
            //DETAIL A1

            txb_DetailStyle.Text = "<VIEW><PARENT SHEET #>";
            txb_SectionStyle.Text = "<VIEW><PARENT SHEET #>-<VIEW><PARENT SHEET #>";
            txb_AuxStyle.Text = "<VIEW><PARENT SHEET #>-<VIEW><PARENT SHEET #>";
            txb_ProjectedStyle.Text = "<VIEW><PARENT SHEET #>-<VIEW><PARENT SHEET #>";
        }

        private void btn_SaveSettings_Click(object sender, EventArgs e)
        {
            if (!ValidateParameters())
                return;

            AddinGlobal.Settings = new ViewReferenceSettings
            {
                CalloutStyle = txb_CalloutStyle.Text,
                DetailLabelStyle = txb_DetailStyle.Text,
                SectionLabelStyle = txb_SectionStyle.Text,
                AuxLabelStyle = txb_AuxStyle.Text,
                ProjectedLabelStyle = txb_ProjectedStyle.Text,

                DetailView = ckb_Detail.Checked,
                SectionView = ckb_Section.Checked,
                AuxView = ckb_Aux.Checked,
                ProjectedView = ckb_Projected.Checked,
                UpdateBeforeSave = ckb_UpdateBeforeSave.Checked
        };

            //Save object to XML
            ViewReferenceTools.SaveSettings();

			//Create/Update Event Listener
			ViewReferenceTools.CreateUpdateEventListener();

            this.Close();
        }

        private bool ValidateParameters()
        {
            if (!ckb_Detail.Checked && !ckb_Section.Checked && !ckb_Aux.Checked && !ckb_Projected.Checked)
            {
                MessageBox.Show("You haven't selected any view types to have references added to.", "No View Types Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            else
                return true;
        }

        private void ImportParameters()
        {
            if (AddinGlobal.Settings != null)
            {
                txb_CalloutStyle.Text = AddinGlobal.Settings.CalloutStyle;
                txb_DetailStyle.Text = AddinGlobal.Settings.DetailLabelStyle;
                txb_SectionStyle.Text = AddinGlobal.Settings.SectionLabelStyle;
                txb_AuxStyle.Text = AddinGlobal.Settings.AuxLabelStyle;
                txb_ProjectedStyle.Text = AddinGlobal.Settings.ProjectedLabelStyle;

                ckb_Detail.Checked = AddinGlobal.Settings.DetailView;
                ckb_Section.Checked = AddinGlobal.Settings.SectionView;
                ckb_Aux.Checked = AddinGlobal.Settings.AuxView;
                ckb_Projected.Checked = AddinGlobal.Settings.ProjectedView;

				ckb_UpdateBeforeSave.Checked = AddinGlobal.Settings.UpdateBeforeSave;
            }            
        }

        private void Set_ActiveTextbox(object sender, EventArgs e)
        { 
            ActiveTextbox = (TextBox)sender;
        }

        private void Add_AttributeToTextBox(object sender, EventArgs e)
        {
            string s = (sender as Button).Text;

            if (ActiveTextbox != null)
            {
                if (ActiveTextbox.Name == "txb_CalloutStyle")
                {
                    //Only certain attributes are allowed for Callouts
                    switch (s)
                    {
                        case "<VIEW>":
                        case "<VIEW SHEET #>":
                        case "<VIEW SHEET NAME>":
                        case "<PARENT SHEET #>":
                        case "<PARENT SHEET NAME>":
                            ActiveTextbox.Text = ActiveTextbox.Text.Insert(ActiveTextbox.SelectionStart, s);
                            break;
                        default:
                            MessageBox.Show("This attribute can not be used in View Callouts.", "Invalid Attribute", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            break;
                    }
                }
                else
                {
                    ActiveTextbox.Text = ActiveTextbox.Text.Insert(ActiveTextbox.SelectionStart, s);
                }                

                ActiveTextbox = null;
            }
            
        }

    }
}
