using EllieMae.Encompass.Automation;
using EllieMae.Encompass.BusinessObjects;
using System;

namespace Encompass.Utilities.CustomDataObjects
{
    public interface IBaseCDO
    {
        string Name { get; }
    }
    public abstract class BaseCDO : IBaseCDO
    {
        byte[] rawData = null;
        internal byte[] RawData {
            get
            {
                if (rawData == null)
                    rawData = LoadData();
                return rawData;
            }
            set
            {
                rawData = value;
                SaveData(rawData);
            }
        }
        public string Name { get; private set; }

        public BaseCDO(string cdoName)
        {
            Name = cdoName;
        }

        private byte[] LoadData()
        {
            try
            {
                return EncompassApplication.Session.DataExchange.GetCustomDataObject(Name).Data;
            }
            catch (Exception ex)
            {
                //Macro.Alert("Function LoadData() encountered an error:\n\n" + ex.Message);
            }

            return null;
        }
        private void SaveData(byte[] data)
        {
            try
            {
                DataObject dataObject = new DataObject(data);
                EncompassApplication.Session.DataExchange.SaveCustomDataObject(Name, dataObject);
            }
            catch (Exception ex)
            {
                Macro.Alert("Function SaveData(byte[] data) encountered an error:\n\n" + ex.Message);
            }
        }
    }
}
