using UnityEngine;
using System.Linq;
using Zenject;

namespace CardManager
{
    public class ViewCardManager : MonoBehaviour
    {
        InvestigatorSelectorComponent investigatorSelector;

        [Inject]
        public void Constructor(InvestigatorSelectorComponent investigatorSelector)
        {
            this.investigatorSelector = investigatorSelector;
        }

        bool isShowAll;
        public bool IsShowAll
        {
            get => isShowAll;
            set
            {
                isShowAll = value;
                if (PanelManager.IsInInvestigatorPanel)
                    ShowInvestigatorCards();
                else
                    ShowDeckCards(investigatorSelector.CurrentInvestigator);
            }
        }

        string textSearch;
        public string TextSearch
        {
            get => textSearch;
            set
            {
                textSearch = value;
                if (PanelManager.IsInInvestigatorPanel)
                    ShowInvestigatorCards();
                else
                    ShowDeckCards(investigatorSelector.CurrentInvestigator);
            }
        }

        bool CheckToDisableCard(CardBaseComponent card)
        {
            return card.Quantity == 0
                || card.Info.pack_code != "core"
                || investigatorSelector.CurrentInvestigator.IsFull
                || investigatorSelector.CurrentInvestigator.XP < card.Info.xp
                || Zones.zone["CardsSelected"].zoneCards.Find(c => c.ID == card.ID).Quantity >= StaticsComponents.MaxUniqueCardsSelected
                || (investigatorSelector.CurrentInvestigator != null
                    && !AllInvestigator.InvestigatorDictionary[investigatorSelector.CurrentInvestigator.ID].deckBuilding.Contains(card.ID));
        }

        bool CheckToVisibleCard(CardBaseComponent card)
        {
            if (card.imageComponent.sprite == null) return false;
            if (!card.NameCard.Contains(textSearch ?? "")) return false;
            if (!IsShowAll && card.Info.pack_code != "core") return false;
            return (isShowAll || AllInvestigator.InvestigatorDictionary[investigatorSelector.CurrentInvestigator.ID].deckBuilding.Contains(card.ID));



            // return (card.Info.pack_code != "core" && !IsShowAll)
            ////&& card.imageComponent.sprite != null
            ////&& card.NameCard.Contains(textSearch ?? "")
            //&& (isShowAll
            //|| AllInvestigator.InvestigatorDictionary[investigatorSelector.CurrentInvestigator.ID].deckBuilding.Contains(card.ID));

        }

        public void ShowDeckCards(InvNCardComponent invCard)
        {
            ShowListSelectedCards(invCard);
            foreach (CardBaseComponent card in Zones.zone["DeckCards"].zoneCards)
            {
                card.IsDisable = CheckToDisableCard(card);
                card.IsVisible = CheckToVisibleCard(card);
            }
        }

        void ShowListSelectedCards(InvNCardComponent invCard)
        {
            CleanSelectedCards();
            foreach (string cardID in invCard.listCardsID)
            {
                CardBaseComponent card = Zones.zone["CardsSelected"].zoneCards.Find(c => c.ID == cardID);
                card.Quantity++;
                card.IsVisible = true;
                card.IsDisable = AllInvestigator.InvestigatorDictionary[invCard.ID].cardRequeriments.Contains(card.ID);
                SortingList(card);
            }
        }

        public void CleanSelectedCards()
        {
            foreach (CardBaseComponent card in Zones.zone["CardsSelected"].zoneCards)
            {
                card.Quantity = 0;
                card.IsVisible = false;
            }
        }

        void SortingList(CardBaseComponent card)
        {
            card.transform.SetParent(Zones.zone["OverlayerZone"].zoneTransform);
            card.transform.SetParent(Zones.zone["CardsSelected"].zoneTransform);
            StartCoroutine(StaticUtility.MoveScrollBar("ScrollbarCardsSelected", 0f));
        }

        public void ShowInvestigatorCards()
        {
            foreach (CardBaseComponent card in Zones.zone["InvestigatorCards"].zoneCards)
            {
                card.IsVisible = ChecKInvestigatorCardVisivility(card);
                card.IsDisable = card.Quantity < 1 || card.Info.pack_code != "core";
            }
        }

        bool ChecKInvestigatorCardVisivility(CardBaseComponent card)
        {
            if (card.imageComponent.sprite == null) return false;
            if (!card.NameCard.Contains(textSearch ?? "")) return false;
            if ((card.Info.pack_code != "core" && !IsShowAll)) return false;
            return true;
        }
    }
}
