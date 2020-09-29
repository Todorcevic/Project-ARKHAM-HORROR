using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace CardManager
{
    public class Card
    {
        public string pack_code { get; set; }
        public string pack_name { get; set; }
        public string type_code { get; set; }
        public string type_name { get; set; }
        public string subtype_code { get; set; }
        public string subtype_name { get; set; }
        public string faction_code { get; set; }
        public string faction_name { get; set; }
        public int position { get; set; }
        public bool exceptional { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string real_name { get; set; }
        public string text { get; set; }
        public string real_text { get; set; }
        public int quantity { get; set; }
        public bool clues_fixed { get; set; }
        public bool health_per_investigator { get; set; }
        public int deck_limit { get; set; }
        public string traits { get; set; }
        public string real_traits { get; set; }
        public bool is_unique { get; set; }
        public bool exile { get; set; }
        public bool hidden { get; set; }
        public bool permanent { get; set; }
        public bool double_sided { get; set; }
        public string url { get; set; }
        public string subname { get; set; }
        public int? skill_willpower { get; set; }
        public int? skill_intellect { get; set; }
        public int? skill_combat { get; set; }
        public int? skill_agility { get; set; }
        public int? health { get; set; }
        public int? sanity { get; set; }
        // public DeckRequirements deck_requirements { get; set; }
        // public List<DeckOption> deck_options { get; set; }
        public string flavor { get; set; }
        public string illustrator { get; set; }
        public string back_text { get; set; }
        public string back_flavor { get; set; }
        public string octgn_id { get; set; }
        public string imagesrc { get; set; }
        public string backimagesrc { get; set; }
        public int? cost { get; set; }
        public int? skill_wild { get; set; }
        public string slot { get; set; }
        // public Restrictions restrictions { get; set; }
        public int? xp { get; set; }
        public int? enemy_damage { get; set; }
        public int? enemy_horror { get; set; }
        public int? enemy_fight { get; set; }
        public int? enemy_evade { get; set; }
        public int? victory { get; set; }
        public string linked_to_code { get; set; }
        public string linked_to_name { get; set; }
        // public LinkedCard linked_card { get; set; }
        public string faction2_code { get; set; }
        public string faction2_name { get; set; }
        public int? shroud { get; set; }
        public int? clues { get; set; }
    }

    public static class AllCards
    {
        public static Card[] DataCard { get; set; }

        public static Dictionary<string, Card> DataCardDictionary { get; set; } = new Dictionary<string, Card>();

        public static void MultiplyX2CoreSetQuantity()
        {
            var allData = DataCard.Where(c=> c.pack_code=="core" 
            && (c.type_code == "asset"
            || c.type_code == "event"
            || c.type_code == "skill"));

            foreach (Card card in allData)
                card.quantity *= 2;
        }
    }
}

