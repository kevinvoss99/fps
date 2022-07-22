using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Assets.Scripts.Util
{
    /**
     * Diese Klasse stellt einen rudimentären JsonParser zur Verfügung. Diesen zu implementieren war notwendig, da es Versionskonflikte mit Unity <-> C# 4.6 gab und es somit nicht möglich war, alle externen Bibliotheken zu verwenden.
     * Soll ein Objekt (de-) serialisiert werden, so muss die Klasse des Objekts den Default-Konstruktor implementieren (where T : new()), da die Variante mit dem MemoryStream sonst nicht funktioniert.
     */
    public static class JsonParser<T> where T : new()
    {
        /**
         * Diese Methode serialisiert ein gegebenenes Objekt und gibt dieses als JSON-formatierten String zurück.
         */
        public static string SerializeObject(T obj)
        {
            MemoryStream ms = new MemoryStream();
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            ser.WriteObject(ms, obj);

            byte[] json = ms.ToArray();
            ms.Close();

            return Encoding.UTF8.GetString(json, 0, json.Length);
        }

        /**
         * Diese Methode deserialisiert einen gegebenenen JSON-formatierten String und gibt diesen als Objekt der Klasse <T> zurück.
         */
        public static T DeserializeObject(string json)
        {
            T val = new T();

            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
            DataContractJsonSerializer ser = new DataContractJsonSerializer(val.GetType());

            val = (T)ser.ReadObject(ms);
            ms.Close();

            return val;
        }
    }

}
