using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UmatoMusume.Models;
using UmatoMusume.Utils;
using FuzzySharp;

namespace UmatoMusume.Data
{
    public static class SupportCardData
    {
        public static List<Dictionary<string, string>> GetSupportCardEventOptions(this List<SupportCard> _cards, string _eventName)
        {
            const int threshold = 80; // Adjust based on tolerance (0-100)

            return _cards
                .Where(card => Fuzz.PartialRatio(card.EventName, _eventName) >= threshold)
                .Select(card => new Dictionary<string, string>(card.EventOptions))
                .Distinct(new DictionaryComparer())
                .ToList();
        }
    }
}
