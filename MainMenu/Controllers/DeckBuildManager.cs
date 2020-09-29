using UnityEngine;
using DG.Tweening;
using Zenject;

namespace CardManager
{
    public class DeckBuildManager
    {
        CheckReady check;
        ViewCardManager viewCardManager;
        PanelManager panelManager;
        InvestigatorSelectorComponent investigatorSelector;
        PreviewComponent previewComponent;
        AudioComponent audioComponent;

        [Inject]
        public void Constructor(
            CheckReady check,
            ViewCardManager viewCardManager,
            PanelManager panelManager,
            InvestigatorSelectorComponent investigatorSelector,
            PreviewComponent previewComponent,
            AudioComponent audioComponent)
        {
            this.check = check;
            this.viewCardManager = viewCardManager;
            this.panelManager = panelManager;
            this.investigatorSelector = investigatorSelector;
            this.previewComponent = previewComponent;
            this.audioComponent = audioComponent;
        }

        public void ShowingPreviewCard(CardBaseComponent card)
        {
            audioComponent.PlayHover();
            previewComponent.ShowingPreviewCard(card);
        }

        public void HidePreviewCard(CardBaseComponent card)
        {
            previewComponent.HidePreviewCard(card);
        }

        public void ChooseInvestigatorCard(CardBaseComponent card)
        {
            previewComponent.ChooseInvestigator(card);
            audioComponent.PlayClick();
            investigatorSelector.AddInvestigator(card.ID);
            card.Quantity--;
            viewCardManager.ShowInvestigatorCards();
            if (InvestigatorSelectorComponent.listActiveInvCard.Count == investigatorSelector.InvestigatorQuantity)
                panelManager.GoToDeckPanel();
        }
        public void DeChooseInvestigatorCard(InvNCardComponent card)
        {
            if (PanelManager.IsInInvestigatorPanel)
            {
                audioComponent.PlayClick();
                Zones.zone["InvestigatorCards"].zoneCards.Find(c => c.ID == card.ID).Quantity++;
                investigatorSelector.RemoveInvestigator(card);
                viewCardManager.ShowInvestigatorCards();
            }
        }

        public void InvestigatorSelect(InvNCardComponent invCard)
        {
            audioComponent.PlayHover();
            investigatorSelector.CurrentInvestigator = invCard;
            if (!PanelManager.IsInInvestigatorPanel)
                viewCardManager.ShowDeckCards(invCard);
        }

        public void ChooseCard(CardBaseComponent card)
        {
            previewComponent.ChooseCard(card);
            audioComponent.PlayClick();
            investigatorSelector.CurrentInvestigator.UpdateListAdd_Remove(card.ID, true);
            card.Quantity--;
            viewCardManager.ShowDeckCards(investigatorSelector.CurrentInvestigator);
            check.CheckingAllReady();
        }

        public void DechooseCard(CardBaseComponent card)
        {
            CardBaseComponent cardInDeckCardsZone = Zones.zone["DeckCards"].zoneCards.Find(c => c.ID == card.ID);
            previewComponent.DeselectCard(cardInDeckCardsZone);
            audioComponent.PlayClick();
            investigatorSelector.CurrentInvestigator.UpdateListAdd_Remove(card.ID, false);
            cardInDeckCardsZone.Quantity++;
            viewCardManager.ShowDeckCards(investigatorSelector.CurrentInvestigator);
            check.CheckingAllReady();
        }
    }
}
