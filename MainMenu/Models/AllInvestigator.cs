using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace CardManager
{
    public class AllInvestigator
    {
        public string investigatorID;
        public int deckSize;
        public List<string> cardRequeriments;
        public List<string> deckBuilding;

        AllInvestigator(string ID, int deckSize, List<string> cardRequeriments, List<string> deckBuilding)
        {
            this.investigatorID = ID;
            this.deckSize = deckSize;
            this.cardRequeriments = cardRequeriments;
            this.deckBuilding = deckBuilding;
        }

        public static Dictionary<string, AllInvestigator> InvestigatorDictionary { get; set; } = new Dictionary<string, AllInvestigator>();

        public static void CreateDictionaryInvestigator()
        {
            AllInvestigator investigator = new AllInvestigator(
            "01001",
            30,
            new List<string>
            {
                "01000",
                "01006",
                "01007"
            },
            AllCards.DataCard.Where(c => ((c.faction_code == "guardian" && c.xp <= 5)
                 || (c.faction_code == "seeker" && c.xp <= 2)
                 || (c.faction_code == "neutral" && c.xp <= 5)))
                .Select(c => c.code).ToList()
            );
            InvestigatorDictionary.Add(investigator.investigatorID, investigator);

            investigator = new AllInvestigator(
            "01002",
            30,
            new List<string>
            {
                "01000",
                "01008",
                "01009"
            },
            AllCards.DataCard.Where(c => ((c.faction_code == "seeker" && c.xp <= 5)
                 || (c.faction_code == "mystic" && c.xp <= 2)
                 || (c.faction_code == "neutral" && c.xp <= 5)))
                .Select(c => c.code).ToList()
            );
            InvestigatorDictionary.Add(investigator.investigatorID, investigator);

            investigator = new AllInvestigator(
            "01003",
            30,
            new List<string>
            {
                "01000",
                "01010",
                "01011"
            },
            AllCards.DataCard.Where(c => ((c.faction_code == "rogue" && c.xp <= 5)
                 || (c.faction_code == "guardian" && c.xp <= 2)
                 || (c.faction_code == "neutral" && c.xp <= 5)))
                .Select(c => c.code).ToList()
            );
            InvestigatorDictionary.Add(investigator.investigatorID, investigator);

            investigator = new AllInvestigator(
            "01004",
            30,
            new List<string>
            {
                "01000",
                "01012",
                "01013"
            },
            AllCards.DataCard.Where(c => ((c.faction_code == "mystic" && c.xp <= 5)
                 || (c.faction_code == "survivor" && c.xp <= 2)
                 || (c.faction_code == "neutral" && c.xp <= 5)))
                .Select(c => c.code).ToList()
            );
            InvestigatorDictionary.Add(investigator.investigatorID, investigator);

            investigator = new AllInvestigator(
            "01005",
            30,
            new List<string>
            {
                "01000",
                "01014",
                "01015"
            },
            AllCards.DataCard.Where(c => ((c.faction_code == "survivor" && c.xp <= 5)
                 || (c.faction_code == "rogue" && c.xp <= 2)
                 || (c.faction_code == "neutral" && c.xp <= 5)))
                .Select(c => c.code).ToList()
            );
            InvestigatorDictionary.Add(investigator.investigatorID, investigator);
        }
    }
}
