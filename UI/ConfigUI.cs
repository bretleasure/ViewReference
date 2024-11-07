using System;
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
            txb_CalloutStyle.Text = $"{AttributeTags.ViewName} (Sh. {AttributeTags.ViewSheetNumber})";
        }

        private void btn_Callout2_Click(object sender, EventArgs e)
        {
            txb_CalloutStyle.Text = $"{AttributeTags.ViewName} ({AttributeTags.ViewSheetNumber})";
        }

        private void btn_Callout3_Click(object sender, EventArgs e)
        {
            txb_CalloutStyle.Text = $"{AttributeTags.ViewName} (SH {AttributeTags.ViewSheetNumber})";
        }

        private void btn_Callout4_Click(object sender, EventArgs e)
        {
            txb_CalloutStyle.Text = $"{AttributeTags.ViewName}{AttributeTags.ParentSheetNumber} SH{AttributeTags.ViewSheetNumber}";
        }

        private void btn_Label1_Click(object sender, EventArgs e)
        {
            //DETAIL A
            //SHEET 1
            txb_DetailStyle.Text = $"{AttributeTags.ViewName}{AttributeTags.Delim}SHEET {AttributeTags.ParentSheetNumber}";
            txb_SectionStyle.Text = $"{AttributeTags.ViewName}-{AttributeTags.ViewName}{AttributeTags.Delim}SHEET {AttributeTags.ParentSheetNumber}";
            txb_AuxStyle.Text = $"{AttributeTags.ViewName}-{AttributeTags.ViewName}{AttributeTags.Delim}SHEET {AttributeTags.ParentSheetNumber}";
            txb_ProjectedStyle.Text = $"{AttributeTags.ViewName}-{AttributeTags.ViewName}{AttributeTags.Delim}SHEET {AttributeTags.ParentSheetNumber}";
        }

        private void btn_Label2_Click(object sender, EventArgs e)
        {
            //DETAIL A (1)

            txb_DetailStyle.Text = $"{AttributeTags.ViewName} ({AttributeTags.ParentSheetNumber})";
            txb_SectionStyle.Text = $"{AttributeTags.ViewName}-{AttributeTags.ViewName} ({AttributeTags.ParentSheetNumber})";
            txb_AuxStyle.Text = $"{AttributeTags.ViewName}-{AttributeTags.ViewName} ({AttributeTags.ParentSheetNumber})";
            txb_ProjectedStyle.Text = $"{AttributeTags.ViewName}-{AttributeTags.ViewName} ({AttributeTags.ParentSheetNumber})";
        }

        private void btn_Label3_Click(object sender, EventArgs e)
        {
            //DETAIL A1

            txb_DetailStyle.Text = $"{AttributeTags.ViewName}{AttributeTags.ParentSheetNumber}";
            txb_SectionStyle.Text = $"{AttributeTags.ViewName}{AttributeTags.ParentSheetNumber}-{AttributeTags.ViewName}{AttributeTags.ParentSheetNumber}";
            txb_AuxStyle.Text = $"{AttributeTags.ViewName}{AttributeTags.ParentSheetNumber}-{AttributeTags.ViewName}{AttributeTags.ParentSheetNumber}";
            txb_ProjectedStyle.Text = $"{AttributeTags.ViewName}{AttributeTags.ParentSheetNumber}-{AttributeTags.ViewName}{AttributeTags.ParentSheetNumber}";
        }

        private void btn_SaveSettings_Click(object sender, EventArgs e)
        {
            if (!ValidateParameters())
                return;

            var viewReferenceSettings = new ViewReferenceSettings
            {
                CalloutStyle = txb_CalloutStyle.Text,
                DetailViewLabelStyle = txb_DetailStyle.Text,
                SectionViewLabelStyle = txb_SectionStyle.Text,
                AuxiliaryViewLabelStyle = txb_AuxStyle.Text,
                ProjectedViewLabelStyle = txb_ProjectedStyle.Text,
                AddReferencesToDetailViews = ckb_Detail.Checked,
                AddReferencesToSectionViews = ckb_Section.Checked,
                AddReferencesToAuxiliaryViews = ckb_Aux.Checked,
                AddReferencesToProjectedViews = ckb_Projected.Checked,
            };

            AddinServer.Settings = new ViewReferenceAddinSettings
            {
                UpdateBeforeSave = ckb_UpdateBeforeSave.Checked,
                ViewReferenceSettings = viewReferenceSettings
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
            if (AddinServer.Settings != null)
            {
                var viewReferenceSettings = AddinServer.Settings.ViewReferenceSettings;
                txb_CalloutStyle.Text = viewReferenceSettings.CalloutStyle;
                txb_DetailStyle.Text = viewReferenceSettings.DetailViewLabelStyle;
                txb_SectionStyle.Text = viewReferenceSettings.SectionViewLabelStyle;
                txb_AuxStyle.Text = viewReferenceSettings.AuxiliaryViewLabelStyle;
                txb_ProjectedStyle.Text = viewReferenceSettings.ProjectedViewLabelStyle;

                ckb_Detail.Checked = viewReferenceSettings.AddReferencesToDetailViews;
                ckb_Section.Checked = viewReferenceSettings.AddReferencesToSectionViews;
                ckb_Aux.Checked = viewReferenceSettings.AddReferencesToAuxiliaryViews;
                ckb_Projected.Checked = viewReferenceSettings.AddReferencesToProjectedViews;

				ckb_UpdateBeforeSave.Checked = AddinServer.Settings.UpdateBeforeSave;
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
                        case AttributeTags.ViewName:
                        case AttributeTags.ViewSheetNumber:
                        case AttributeTags.ViewSheetName:
                        case AttributeTags.ParentSheetNumber:
                        case AttributeTags.ParentSheetName:
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
