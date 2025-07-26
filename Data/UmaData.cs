using Newtonsoft.Json;
using UmatoMusume.Models;
using UmatoMusume.Utils;

namespace UmatoMusume.Data
{
    public static class UmaData
    {
        private const string FOLDER_NAME = "Assets";
        private const string FILE_NAME = "uma_data.json";

        public static List<Umamusume> LoadUmaList()
        {
            using (StreamReader r = new StreamReader(Path.Combine(FOLDER_NAME, FILE_NAME)))
            {
                string umaDataJson = r.ReadToEnd();
                List<Umamusume> umaList = JsonConvert.DeserializeObject<List<Umamusume>>(umaDataJson) ?? new List<Umamusume>();
                return umaList;
            }
        }

        public static List<UmaObjective> GetUmaObjectives(this List<Umamusume> _umas, string _umaName)
        {
            return _umas.Where(x => x.UmaName.Contains(_umaName))
                .SelectMany(x => x.UmaObjectives)
                .DistinctBy(x => (x.ObjectiveName, x.Turn, x.ObjectiveCondition, x.Time))
                .ToList();
        }

        public static List<Dictionary<string, string>> GetUmaEventOptions(this List<Umamusume> _umas, string _umaName, string _eventName)
        {
            return _umas.Where(x => x.UmaName.Contains(_umaName))
                .SelectMany(x => x.UmaEvents)
                .Where(e => e.EventName.Contains(_eventName))
                .Select(e => new Dictionary<string, string>(e.EventOptions))
                .Distinct(new DictionaryComparer())
                .ToList();
        }
    }
}
