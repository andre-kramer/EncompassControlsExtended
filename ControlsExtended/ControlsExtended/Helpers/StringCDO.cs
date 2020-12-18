using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encompass.Utilities.CustomDataObjects
{
    public interface IStringCDO
    {
        string Data { get; set; }
    }
    public class StringCDO : BaseCDO, IStringCDO
    {
        string _stringData;
        public StringCDO(string name)
            : base(name)
        {
            _stringData = Encoding.ASCII.GetString(base.RawData);
        }

        public string Data
        {
            get
            {
                return _stringData;
            }
            set
            {
                _stringData = value;
                base.RawData = Encoding.ASCII.GetBytes(_stringData);
            }
        }
    }
}
