using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Newtonsoft.Json;

namespace ArkhamShared
{
    public class InvestigatorData
    {
        public string Id { get; set; }
        public bool IsRetired { get; set; }
        public bool IsKilled => PhysicalTrauma >= Card.DataCardDictionary[Id].Health;
        public bool IsInsane => MentalTrauma >= Card.DataCardDictionary[Id].Sanity;
        public bool CanPlay => !IsRetired && !IsKilled && !IsInsane;
        public bool IsPlaying { get; set; }
        public int DeckPosition { get; set; }
        public int PhysicalTrauma { get; set; }
        public int MentalTrauma { get; set; }
        public int Xp { get; set; }
        public int DeckSize { get; set; }
        public List<string> ListCardsID { get; set; } = new List<string>();
        public List<string> CardRequeriments { get; set; }
        [JsonIgnore] public List<string> FullListCards => CardRequeriments.Concat(ListCardsID).ToList();
        public List<string> DeckBuildingFactionConditions { get; set; }
        public List<int> DeckBuildingXpConditions { get; set; }

        [JsonIgnore]
        public List<string> DeckBuilding
        {
            get
            {
                List<string> deckBuildingResult = new List<string>();
                int i = 0;
                foreach (string faction in DeckBuildingFactionConditions)
                {
                    deckBuildingResult.AddRange(Card.DataCard
                        .Where(c => c.Faction_code == faction && c.Xp <= DeckBuildingXpConditions[i])
                        .Select(c => c.Code).ToList());
                    i++;
                }
                return deckBuildingResult;
            }
        }
        public static List<InvestigatorData> AllInvestigatorsData { get; set; } = new List<InvestigatorData>();
    }
}
