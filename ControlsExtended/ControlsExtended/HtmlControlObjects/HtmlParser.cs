using EllieMae.Encompass.Automation;
using System;
using System.Collections.Generic;

namespace ControlsExtended.HtmlControlObjects
{
    public static class HtmlParser
    {
        public static List<HtmlControl> GetControlList(ref string html)
        {
            List<HtmlControl> controls = new List<HtmlControl>();

            controls.AddRange(GetControlString(ref html, "TEXTBOX"));
            controls.AddRange(GetControlString(ref html, "CHECKBOX"));
            controls.AddRange(GetControlString(ref html, "LABEL"));
            controls.AddRange(GetControlString(ref html, "DROPDOWNBOX"));

            return controls;
        }

        public static string ReplaceTagWithControl(string html, string tag, string controlString)
        {
            return html.Replace("{{" + tag + "}}", controlString);
        }

        private static List<HtmlControl> GetControlString(ref string html, string controlType)
        {
            List<HtmlControl> controls = new List<HtmlControl>();
            int startPos = 0;

            while((startPos = html.ToUpper().IndexOf("<" + controlType)) > 0)
            {
                string tempHtml = html.Substring(startPos);

                int endTagLength = 2;
                int endPos = tempHtml.IndexOf("/>");
                if (endPos <= 0)
                {
                    endTagLength = ((string)("</" + controlType + ">")).Length;
                    endPos = tempHtml.ToUpper().IndexOf("</" + controlType + ">");
                }

                string controlString = tempHtml.Substring(0, endPos + endTagLength);
                var control = ParseControl(controlString, controlType);

                html = html.Replace(controlString, "{{" + control.Name + "}}");
                controls.Add(control);
            }

            return controls;
        }
        private static HtmlControl ParseControl(string controlString, string controlType)
        {
            HtmlControl control = new HtmlControl();

            // these properties must have a value. 
            // if no value is provided, default them.
            control.Name = GetControlProperty(controlString, "NAME");
            if (string.IsNullOrEmpty(control.Name))
                control.Name = Guid.NewGuid().ToString().Replace("-", string.Empty);

            control.Style = GetControlProperty(controlString, "STYLE");
            if (string.IsNullOrEmpty(control.Style))
                control.Style = "HEIGHT: 20px; VISIBILITY: inherit";

            control.Enabled = GetControlProperty(controlString, "ENABLED").ToUpper() == "FALSE" ? false : true;
            control.FieldSource = GetControlProperty(controlString, "FIELD-SOURCE").ToUpper() == "LINKEDLOAN" ? FieldSources.LinkedLoan : FieldSources.CurrentLoan;
            control.ControlType = GetControlType(controlType);

            // these properties are not required to have a value
            control.Field = GetControlProperty(controlString, "FIELD-ID");
            control.Text = GetControlProperty(controlString, "FIELD-TEXT");
            control.Options = GetOptions(controlString);

            return control;
        }
        private static string ReplaceControlWithTag(string html, string controlString, string tag)
        {
            return html.Replace(controlString, "{{" + tag + "}}");
        }

        private static string GetControlProperty(string controlString, string propertyName)
        {
            string propertyValue = string.Empty;

            int startPos = controlString.ToUpper().IndexOf(propertyName);
            if (startPos > 0)
            {
                string tempHtml = controlString.Substring(startPos);
                int valueStartPos = tempHtml.IndexOf("\"") == 0 ? tempHtml.IndexOf("'") : tempHtml.IndexOf("\"");
                if (valueStartPos > 0)
                {
                    string value = tempHtml.Substring(valueStartPos + 1);
                    int valueEndPos = value.IndexOf("\"") == 0 ? value.IndexOf("'") : value.IndexOf("\"");
                    propertyValue = value.Substring(0, valueEndPos);
                }
            }

            return propertyValue;
        }

        private static List<string> GetOptions(string controlString)
        {
            List<string> options = new List<string>();

            string tempHtml = controlString.ToUpper();
            int startPos = 0;

            while((startPos = tempHtml.IndexOf("<OPTION")) > 0)
            {
                int endPos = tempHtml.IndexOf("</OPTION>");
                string option = tempHtml.Substring(startPos, endPos - startPos) + "</OPTION>";

                options.Add(option);
                
                tempHtml = tempHtml.Replace(option, string.Empty);
            }

            return options;
        }

        private static ControlTypes GetControlType(string controlType)
        {
            switch(controlType)
            {
                case "DROPDOWNBOX":
                    return ControlTypes.DropDownBox;
                case "CHECKBOX":
                    return ControlTypes.CheckBox;
                case "LABEL":
                    return ControlTypes.Label;
                default:
                    return ControlTypes.TextBox;
            }
        }
    }
}
