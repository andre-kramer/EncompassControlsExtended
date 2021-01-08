using ControlsExtended.TableObjects;
using EllieMae.Encompass.Automation;
using EllieMae.Encompass.Forms;
using System.Collections.Generic;

namespace ControlsExtended
{
    public class TableFactory
    {
        private Dictionary<string, Table> _tables = new Dictionary<string, Table>();

        public TableStyleTypes TableStyleTypes;

        public void Create(Panel parent)
        {
            if (!_tables.ContainsKey(parent.ControlID))
                _tables.Add(parent.ControlID, new Table(parent));
            else
                Macro.Alert("A table with the Id of '" + parent.ControlID + "' already exists");
        }
        public void AddCaption(Panel parent, string caption)
        {
            {
                if (_tables.ContainsKey(parent.ControlID))
                    _tables[parent.ControlID].AddCaption(caption);
                else
                    Macro.Alert("A table with the Id of '" + parent.ControlID + "' does not exist.");
            }
        }
        public void AddColumns(Panel parent, string[] columnHeaders)
        {
            if (_tables.ContainsKey(parent.ControlID))
                _tables[parent.ControlID].AddColumnsHeaders(columnHeaders);
            else
                Macro.Alert("A table with the Id of '" + parent.ControlID + "' does not exist.");
        }
        public void AddRow(Panel parent, object[] row)
        {
            if (_tables.ContainsKey(parent.ControlID))
                _tables[parent.ControlID].AddRow(row);
            else
                Macro.Alert("A table with the Id of '" + parent.ControlID + "' does not exist.");
        }
        public void AddStyle(Panel parent, TableStyleTypes styleType, string styleName, string value)
        {
            if (_tables.ContainsKey(parent.ControlID))
                _tables[parent.ControlID].AddStyle(styleType, styleName, value);
            else
                Macro.Alert("A table with the Id of '" + parent.ControlID + "' does not exist.");
        }
        public string GetHTML(Panel parent)
            => parent.HTMLElement.outerHTML;
        public void Show(Panel parent)
        {
            if (_tables.ContainsKey(parent.ControlID))
                parent.HTMLElement.innerHTML = _tables[parent.ControlID].ToString();
            else
                Macro.Alert("A table with the Id of '" + parent.ControlID + "' does not exist.");
        }
    }
}
