using EllieMae.Encompass.Forms;
using System.Collections.Generic;

namespace ControlsExtended.TableObjects
{
    public class Table
    {
        Panel _parent;
        public Table(Panel parent)
        {
            _parent = parent;

            SetDefaultStyles();
        }
        public override string ToString()
        {
            string html = AddHeader("table", TableStyleTypes.Table);

            if (!string.IsNullOrEmpty(_caption))
                html += AddHeader("caption", TableStyleTypes.Caption) + _caption + AddFooter("caption");

            html += AddHeader("tr", TableStyleTypes.TR);

            foreach (string header in _columnHeaders)
                html += AddHeader("th", TableStyleTypes.TH) + header + AddFooter("th");

            html += AddFooter("tr");

            foreach (string row in _rows)
                html += AddHeader("tr", TableStyleTypes.TR) + row + AddFooter("tr");

            html += AddFooter("table");

            return html;
        }

        string _caption = string.Empty;
        public void AddCaption(string caption)
        {
            _caption = caption;
        }

        List<string> _columnHeaders = new List<string>();
        public void AddColumnsHeaders(string[] columnHeaders)
        {
            //set default width to equal number of headers
            _columnHeaders.Clear();
            for (int loop = 0; loop < columnHeaders.Length; loop++)
                _columnHeaders.Add(columnHeaders[loop]);
        }
        List<string> _rows = new List<string>();
        public void AddRow(object[] row)
        {
            string html = string.Empty;
            for (int loop = 0; loop < row.Length; loop++)
                    html += AddHeader("td", TableStyleTypes.TD) + row[loop].ToString() + AddFooter("td");
            _rows.Add(html);
        }

        public void AddStyle(TableStyleTypes styleType, string styleName, string value)
        {
            if (_tableStyles[styleType].ContainsKey(styleName))
                _tableStyles[styleType][styleName] = value;
            else
                _tableStyles[styleType].Add(styleName, value);
        }

        Dictionary<TableStyleTypes, Dictionary<string, string>> _tableStyles = new Dictionary<TableStyleTypes, Dictionary<string, string>>();
        private void SetDefaultStyles()
        {
            _tableStyles.Add(TableStyleTypes.Table, new Dictionary<string, string>());
            _tableStyles.Add(TableStyleTypes.Caption, new Dictionary<string, string>());
            _tableStyles.Add(TableStyleTypes.TH, new Dictionary<string, string>());
            _tableStyles.Add(TableStyleTypes.TR, new Dictionary<string, string>());
            _tableStyles.Add(TableStyleTypes.TD, new Dictionary<string, string>());

            _tableStyles[TableStyleTypes.Table].Add("width", "100%");
            _tableStyles[TableStyleTypes.Table].Add("overflow-y", "scroll");
            _tableStyles[TableStyleTypes.Caption].Add("background-color", "DarkGray");
            _tableStyles[TableStyleTypes.TH].Add("background-color", "DarkGray");
            _tableStyles[TableStyleTypes.TH].Add("margin-top", "2px");
            _tableStyles[TableStyleTypes.TH].Add("margin-bottom", "2px");
            _tableStyles[TableStyleTypes.TD].Add("padding-left", "5px");
        }

        // create table html
        private string AddHeader(string headerName, TableStyleTypes styleType)
        {
            string html = "<" + headerName;

            if (_tableStyles[styleType].Count > 0)
            {
                html += " style='";
                foreach (string key in _tableStyles[styleType].Keys)
                    html += key + ": " + _tableStyles[styleType][key] + ";";
                html += "'";
            }

            return html + ">";
        }
        private string AddFooter(string headername)
            => "</" + headername + ">";
    }
}
