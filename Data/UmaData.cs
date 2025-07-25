using Newtonsoft.Json;
using UmatoMusume.Models;

namespace UmatoMusume.Data
{
    public class UmaData
    {
        private const string FOLDER_NAME = "Assets";
        private const string FILE_NAME = "uma_data.json";

        public static List<Umamusume> LoadList()
        {
            using (StreamReader r = new StreamReader(Path.Combine(FOLDER_NAME, FILE_NAME)))
            {
                string umaDataJson = r.ReadToEnd();
                List<Umamusume> umaList = JsonConvert.DeserializeObject<List<Umamusume>>(umaDataJson) ?? new List<Umamusume>();
                return umaList;
            }
        }
    }
}
