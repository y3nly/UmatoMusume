using UmatoMusume.Models;
using UmatoMusume.Utils;
using FuzzySharp;

namespace UmatoMusume.Data
{
    public static class UmaData
    {
        public static List<UmaObjective> GetUmaObjectives(this List<Umamusume> _umas, string _umaName)
        {
            return _umas.Where(x => x.UmaName.Contains(_umaName))
                .SelectMany(x => x.UmaObjectives)
                .DistinctBy(x => (x.ObjectiveName, x.Turn, x.ObjectiveCondition, x.Time))
                .ToList();
        }

        public static List<Dictionary<string, string>> GetUmaEventOptions(this List<Umamusume> _umas, string _umaName, string _eventName)
        {
            return _umas
                .Where(x => Fuzz.PartialRatio(x.UmaName, _umaName) >= 80)
                .SelectMany(x => x.UmaEvents)
                .Where(e => Fuzz.PartialRatio(e.EventName, _eventName) >= 80)
                .Select(e => new Dictionary<string, string>(e.EventOptions))
                .Distinct(new DictionaryComparer())
                .ToList();
        }

        public static bool FindUmaByName(this List<Umamusume> _umas, string _umaName, out Umamusume? _uma)
        {
            _uma = _umas.FirstOrDefault(x => x.UmaName.Contains(_umaName));
            return _uma != null;
        }
    }
}
