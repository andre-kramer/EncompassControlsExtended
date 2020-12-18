using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Encompass.Utilities.CustomDataObjects
{
    public interface IJsonCDO : IBaseCDO
    {
        string JsonData { get; set; }
        T DeserializeData<T>() where T : class;
        void SerializeData<T>(T dataObject);
    }
    public class JsonCDO : BaseCDO, IJsonCDO
    {
        string _jsonData;
        public JsonCDO(string name)
            : base(name)
        {
            if (base.RawData != null)
                _jsonData = Encoding.ASCII.GetString(base.RawData);
            else
                _jsonData = string.Empty;
        }

        public string JsonData
        {
            get
            {
                return _jsonData;
            }
            set
            {
                _jsonData = value;
                base.RawData = Encoding.ASCII.GetBytes(_jsonData);
            }
        }

        public T DeserializeData<T>() where T : class
        {
            T result = default(T);

            try
            {
                MemoryStream stream = new MemoryStream(System.Text.ASCIIEncoding.ASCII.GetBytes(this.JsonData));
                stream.Position = 0;

                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
                result = (T)serializer.ReadObject(stream);
            }
            catch (Exception ex)
            { }

            return result;
        }
        public void SerializeData<T>(T dataObject)
        {
            string jsonString = string.Empty;

            try
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(dataObject.GetType());
                using (var stream = new MemoryStream())
                {
                    serializer.WriteObject(stream, dataObject);
                    byte[] jsonBytes = stream.ToArray();
                    this.JsonData = Encoding.UTF8.GetString(jsonBytes, 0, jsonBytes.Length);
                }
            }
            catch (Exception ex)
            { }
        }

    }
}
