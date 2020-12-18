using ControlsExtended.HtmlControlObjects;
using EllieMae.Encompass.Automation;
using EllieMae.Encompass.BusinessObjects.Loans;
using EllieMae.Encompass.Forms;
using System.Collections.Generic;

namespace ControlsExtended
{
    public class Controller : Form
    {
        List<HtmlControl> _labelControls = new List<HtmlControl>();

        public override void CreateControls()
        {
            var controls = HtmlControlBuilder.Build(this);

            if (controls != null && controls.Count > 0)
            {
                foreach (HtmlControl control in controls)
                {
                    switch (control.ControlType)
                    {
                        case ControlTypes.CheckBox:
                            DataBindCheckBox(control);
                            break;
                        case ControlTypes.DropDownBox:
                            DataBindDropdownBox(control);
                            break;
                        case ControlTypes.Label:
                            AddLabelToList(control);
                            break;
                        default:
                            DataBindTextBox(control);
                            break;
                    }
                }

                // create events
                this.Unload += Controller_Unload;
                EncompassApplication.CurrentLoan.FieldChange += CurrentLoan_FieldChange;
                if (EncompassApplication.CurrentLoan.LinkedLoan != null)
                    EncompassApplication.CurrentLoan.LinkedLoan.FieldChange += LinkedLoan_FieldChange;
            }
            
            //textBox.Refresh();
            base.CreateControls();
        }

        #region Events
        private void LinkedLoan_FieldChange(object source, FieldChangeEventArgs e)
        {

            var control = _labelControls.Find(f => f.Field == e.FieldID);
            if (control != null && control.FieldSource == FieldSources.LinkedLoan)
                DataBindLabel(control);
        }

        private void CurrentLoan_FieldChange(object source, FieldChangeEventArgs e)
        {
            var control = _labelControls.Find(f => f.Field == e.FieldID);
            if (control != null && control.FieldSource == FieldSources.CurrentLoan)
                DataBindLabel(control);
        }

        private void Controller_Unload(object sender, System.EventArgs e)
        {
            EncompassApplication.CurrentLoan.FieldChange -= CurrentLoan_FieldChange;
            if (EncompassApplication.CurrentLoan.LinkedLoan != null)
                EncompassApplication.CurrentLoan.LinkedLoan.FieldChange -= LinkedLoan_FieldChange;
        }
        #endregion
        #region Simple Table Functions
        public void SetControlStyle(Control control, string styleName, object value)
            => Style.Apply(control, styleName, value.ToString());

        public TableFactory TableFactory = new TableFactory();

        public void CreateControlFromHtml(Panel parent, string html)
            => parent.HTMLElement.innerHTML = html;
        
        public string GetFormHtml()
            => this.HTMLElement.outerHTML;

        public string GetParentHtml(Panel parent)
            => parent.HTMLElement.innerHTML;
        #endregion
        #region Data Bind functions
        private void DataBindTextBox(HtmlControl control)
        {
            if (!string.IsNullOrEmpty(control.Field))
            {
                TextBox textBox = (TextBox)FindControl(control.Name);
                if (textBox != null)
                {
                    textBox.FieldSource = control.FieldSource == FieldSources.CurrentLoan ? FieldSource.CurrentLoan : FieldSource.LinkedLoan;
                    textBox.Field = DataBindField(control);
                }
                else
                    Macro.Alert("Error: TextBox '" + control.Name + "' was not found.");
            }
        }
        private void DataBindCheckBox(HtmlControl control)
        {
            if (!string.IsNullOrEmpty(control.Field))
            {
                CheckBox checkBox = (CheckBox)FindControl(control.Name);
                if (checkBox != null)
                {
                    checkBox.FieldSource = control.FieldSource == FieldSources.CurrentLoan ? FieldSource.CurrentLoan : FieldSource.LinkedLoan;
                    checkBox.Field = DataBindField(control);
                }
                else
                    Macro.Alert("Error: CheckBox '" + control.Name + "' was not found.");
            }
        }
        private void DataBindDropdownBox(HtmlControl control)
        {
            if (!string.IsNullOrEmpty(control.Field))
            {
                DropdownBox dropdownBox = (DropdownBox)FindControl(control.Name);
                if (dropdownBox != null)
                {
                    dropdownBox.FieldSource = control.FieldSource == FieldSources.CurrentLoan ? FieldSource.CurrentLoan : FieldSource.LinkedLoan;
                    dropdownBox.Field = DataBindField(control);
                }
                else
                    Macro.Alert("Error: DropdownBox '" + control.Name + "' was not found.");
            }
        }

        private void AddLabelToList(HtmlControl control)
        {
            if (!string.IsNullOrEmpty(control.Field))
            {
                _labelControls.Add(control);
                DataBindLabel(control);
            }
        }
        private void DataBindLabel(HtmlControl control)
        {
            Label label = (Label)FindControl(control.Name);
            string fieldValue = GetField(control).FormattedValue;

            if (!string.IsNullOrEmpty(fieldValue))
                label.Text = fieldValue;
            else
                label.Text = control.Text;
        }

        private FieldDescriptor DataBindField(HtmlControl control)
        {
            if (control.FieldSource == FieldSources.CurrentLoan || EncompassApplication.CurrentLoan.LinkedLoan == null)
                return EncompassApplication.CurrentLoan.Fields[control.Field].Descriptor;
            else
                return EncompassApplication.CurrentLoan.LinkedLoan.Fields[control.Field].Descriptor;
        }
        private Field GetField(HtmlControl control)
        {
            if (control.FieldSource == FieldSources.CurrentLoan || EncompassApplication.CurrentLoan.LinkedLoan == null)
                return EncompassApplication.CurrentLoan.Fields[control.Field];
            else
                return EncompassApplication.CurrentLoan.LinkedLoan.Fields[control.Field];
        }
        #endregion
    }
}
