using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Zenject;

namespace CardManager
{
    public class CardFactory : MonoBehaviour
    {
        DeckBuildManager deckBuildManager;
        [SerializeField] GameObject cardPrefab;
        [SerializeField] GameObject rowCardPrefab;

        [Inject]
        public void Constructor(DeckBuildManager deckBuildManager)
        {
            this.deckBuildManager = deckBuildManager;
        }

        void Start()
        {
            CreatingInvestigatorCards();
            CreatingDeckCards();
            CreatingRowCards();
        }

        void CreatingInvestigatorCards()
        {
            List<string> InvestigatorsCards = AllCards.DataCard.Where(c => c.type_code == "investigator")
                .OrderBy(c=>c.faction_code)
                .Select(c => c.code).ToList();
            foreach (string card in InvestigatorsCards)
            {
                CardBaseComponent investigatorCard = InstantiateCard(card, Zones.zone["InvestigatorCards"], cardPrefab);
                investigatorCard.IsInvestigator = true;
            }
        }

        void CreatingDeckCards()
        {
            List<string> DeckCards = AllCards.DataCard.Where(c => (c.type_code == "asset"
            || c.type_code == "event"
            || c.type_code == "skill"))
                .OrderBy(c => c.faction_code)
                .Select(c => c.code).ToList();
            foreach (string card in DeckCards)
                InstantiateCard(card, Zones.zone["DeckCards"], cardPrefab);
        }

        void CreatingRowCards()
        {
            List<string> DeckCards = AllCards.DataCard.Where(c => (c.type_code == "asset"
            || c.type_code == "event"
            || c.type_code == "skill"
            || c.subtype_code == "weakness"
            || c.subtype_code == "basicweakness"))
                .Select(c => c.code).ToList();
            foreach (string card in DeckCards)
                InstantiateCard(card, Zones.zone["CardsSelected"], rowCardPrefab);
        }

        CardBaseComponent InstantiateCard(string idCard, Zones zone, GameObject prefab)
        {
            CardBaseComponent cardMain = Instantiate(prefab, zone.zoneTransform).GetComponent<CardBaseComponent>();
            cardMain.ID = idCard;
            cardMain.deckBuildManager = deckBuildManager;
            zone.zoneCards.Add(cardMain);
            return cardMain;
        }
    }
}
