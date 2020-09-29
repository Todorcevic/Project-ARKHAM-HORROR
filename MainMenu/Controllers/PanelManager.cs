using UnityEngine;
using Michsky.UI.Dark;
using UnityEngine.UI;
using Zenject;

namespace CardManager
{
    public class PanelManager : MonoBehaviour
    {
        ViewCardManager visivilityManager;
        InvestigatorSelectorComponent investigatorSelector;
        [SerializeField] Button investigatorButton;
        [SerializeField] Button backButton;
        [SerializeField] Button readyButton;

        [Inject]
        public void Constructor(ViewCardManager visivilityManager, InvestigatorSelectorComponent investigatorSelector)
        {
            this.visivilityManager = visivilityManager;
            this.investigatorSelector = investigatorSelector;
        }

        private static bool isInInvestigatorPanel;
        public static bool IsInInvestigatorPanel { get => isInInvestigatorPanel; set => isInInvestigatorPanel = value; }

        public void GoToInvestigatorPanel()
        {
            StaticsComponents.ButtonsOnOff(readyButton, false);
            SettingButtons(false);
            RestartingInvestigator();
            IsInInvestigatorPanel = true;
            GetComponent<PanelTabManager>().PanelAnim(0);           
            visivilityManager.ShowInvestigatorCards();
        }

        void RestartingInvestigator()
        {
            visivilityManager.CleanSelectedCards();
            foreach (CardBaseComponent card in Zones.zone["InvestigatorCards"].zoneCards)
                card.Quantity = AllCards.DataCardDictionary[card.ID].quantity;
            investigatorSelector.CleanAllInvCard();
        }

        public void GoToDeckPanel()
        {
            SettingButtons(true);
            RestartingDeckCards();
            AddingRequerimentsCards();
            IsInInvestigatorPanel = false;
            investigatorSelector.CurrentInvestigator = investigatorSelector.GetComponentInChildren<InvNCardComponent>(); //Select the first InvCard
            GetComponent<PanelTabManager>().PanelAnim(1);
            visivilityManager.ShowDeckCards(investigatorSelector.CurrentInvestigator);
        }

        void RestartingDeckCards()
        {
            foreach (CardBaseComponent card in Zones.zone["DeckCards"].zoneCards)
                card.Quantity = AllCards.DataCardDictionary[card.ID].quantity;
        }

        void AddingRequerimentsCards()
        {
            foreach (InvNCardComponent invCard in InvestigatorSelectorComponent.listActiveInvCard)
                foreach (string card in AllInvestigator.InvestigatorDictionary[invCard.ID].cardRequeriments)
                {
                    invCard.listCardsID.Add(card);
                    invCard.RequerimentCardsQuantity++;
                }
        }

        public void SettingButtons(bool state)
        {
            StaticsComponents.ButtonsOnOff(investigatorButton, state);
            StaticsComponents.ButtonsOnOff(backButton, state);
        }
    }
}
