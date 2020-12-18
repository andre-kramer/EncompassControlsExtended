using System.Collections.Generic;

namespace ControlsExtended.HtmlControlObjects
{
    public enum ControlTypes
    {
        TextBox,
        DropDownBox,
        CheckBox,
        Label
    }
    public enum FieldSources
    {
        CurrentLoan,
        LinkedLoan
    }

    public class HtmlControl
    {
        public string Name { get; set; }
        public ControlTypes ControlType { get; set; }
        public FieldSources FieldSource { get; set; }
        public string Field { get; set; }
        public string Style { get; set; }
        public string Html { get; set; }
        public bool Enabled { get; set; }
        public string Text { get; set; }
        public List<string> Options { get; set; }
    }
}
