using EllieMae.Encompass.Automation;
using EllieMae.Encompass.Forms;
using Encompass.Utilities.CustomDataObjects;
using System;
using System.Collections.Generic;

namespace ControlsExtended.HtmlControlObjects
{
    public static class HtmlControlBuilder
    {
        public static List<HtmlControl> Build(Form form)
        {
            List<HtmlControl> controls = new List<HtmlControl>();

            var configFiles = GetFormControls(form.Name);
            if (configFiles != null)
            {
                foreach (ConfigFile configFile in configFiles)
                {
                    var html = GetControlHtml(configFile.ControlFile);
                    if (!string.IsNullOrEmpty(html))
                    {
                        var htmlControls = HtmlParser.GetControlList(ref html);
                        foreach (HtmlControl control in htmlControls)
                        {
                            BuildControlHtml(control);
                            html = HtmlParser.ReplaceTagWithControl(html, control.Name, control.Html);
                        }

                        if (AddHtmlToParent(form, configFile.Parent, html))
                            controls.AddRange(htmlControls);
                        else
                            return null;
                    }
                    else
                        Macro.Alert("Error: Unable to load " + configFile.ControlFile + " from the Global CDO.");
                }
            }

            return controls;
        }

        private static bool AddHtmlToParent(Form form, string parent, string html)
        {
            Panel panel = null;
            try
            {
                panel = (Panel)form.FindControl(parent);
                if (panel == null)
                {
                    Macro.Alert("Error: parent control '" + parent + "' was not found on the form.");
                    return false;
                }
            }
            catch(Exception ex)
            {
                Macro.Alert("Error: The parent control is not a Panel.");
                return false;
            }

            panel.HTMLElement.innerHTML = html;
            return true;
        }
        private static List<ConfigFile> GetFormControls(string formName)
        {
            JsonCDO jsonCdo = new JsonCDO(formName + ".json");
            return jsonCdo.DeserializeData<List<ConfigFile>>();
        }
        private static string GetControlHtml(string fileName)
        {
            StringCDO stringCdo = new StringCDO(fileName);
            string data = stringCdo.Data;

            int startPos = data.IndexOf("<");
            return data.Substring(startPos);
        }

        private static void BuildControlHtml(HtmlControl control)
        {
            switch(control.ControlType)
            {
                case ControlTypes.CheckBox:
                    BuildCheckBox(control);
                    break;
                case ControlTypes.DropDownBox:
                    BuildDropdownBox(control);
                    return;
                case ControlTypes.Label:
                    BuildLabel(control);
                    return;
                default:
                    BuildTextBox(control);
                    break;
            }
        }
        private static void BuildTextBox(HtmlControl control)
        {
            string html = "<INPUT id='{{Name}}' class='{{Enabled}}' style='{{Style}}' controlType='TextBox' fieldId='{{Name}}'/>";
            html = html.Replace("{{Name}}", control.Name);
            html = html.Replace("{{Enabled}}", control.Enabled ? "inputTextAlpha" : "inputTextAlphaDisabled");
            html = html.Replace("{{Style}}", control.Style);

            control.Html = html;
        }
        private static void BuildCheckBox(HtmlControl control)
        {
            Guid uid = new Guid();
            string html = "<SPAN id='{{Name}}' style='{{Style}}' controlType='CheckBox'>" +
                            "<INPUT id='" + uid.ToString() + "' class='checkboxField' type='checkbox' value='Y' fieldId='{{Name}}' {{Enabled}}/>" +
                            "<LABEL for='" + uid.ToString() + "' labelId='{{Name}}'>{{Text}}</LABEL></SPAN>";

            html = html.Replace("{{Name}}", control.Name);
            html = html.Replace("{{Enabled}}", control.Enabled ? string.Empty : "emdisabled='1'");
            html = html.Replace("{{Style}}", control.Style);
            html = html.Replace("{{Text}}", control.Text);

            control.Html = html;
        }
        private static void BuildDropdownBox(HtmlControl control)
        {
            string html = "<SELECT id='{{Name}}' class='{{Enabled}}' style='{{Style}}' size='1' controlType='DropdownBox' fieldId='{{Name}}' {{Enabled-Tag}}>";
            html = html.Replace("{{Name}}", control.Name);
            html = html.Replace("{{Enabled}}", control.Enabled ? "inputSelect" : "inputSelectDisabled");
            html = html.Replace("{{Enabled-Tag}}", control.Enabled ? string.Empty : "emdisabled='1'");
            html = html.Replace("{{Style}}", control.Style);

            foreach (string option in control.Options)
                html += option;

            html += "</SELECT>";
            
            control.Html = html;
        }
        private static void BuildLabel(HtmlControl control)
        {
            string html = "<SPAN id='{{Name}}' style='{{Style}}' controlType='Label'>{{Text}}</SPAN>";

            html = html.Replace("{{Name}}", control.Name);
            html = html.Replace("{{Style}}", control.Style);
            html = html.Replace("{{Text}}", control.Text);

            control.Html = html;
        }
    }
}
