using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ArkhamGamePlay
{
    public class Card01011 : InvestigatorTreachery
    {
        int amountUsed;
        public override bool IsDiscarted => false;

        /*****************************************************************************************/
        protected override void BeginGameAction(GameAction gameAction)
        {
            if (gameAction is InvestigatorEliminatedAction investigatorEliminated && CountResources(investigatorEliminated))
                NoPayDebts();
            if (gameAction is FinishGameAction && CountResources()) NoPayDebts();
            if (gameAction is InteractableAction interactable && CanPlayFastEffect(interactable))
                PayDebts(interactable);
        }

        protected override void EndGameAction(GameAction gameAction)
        {
            if (gameAction is UpkeepPhase) amountUsed = 0;
        }

        bool CountResources(InvestigatorEliminatedAction finishGameAction)
        {
            if (!ThisCard.IsInPlay) return false;
            if (ThisCard.ResourcesToken.Amount >= 6) return false;
            if (finishGameAction.Investigator != Investigator) return false;
            return true;
        }

        bool CountResources()
        {
            if (!ThisCard.IsInPlay) return false;
            if (ThisCard.ResourcesToken.Amount >= 6) return false;
            return true;
        }

        bool CanPlayFastEffect(InteractableAction interactableAction)
        {
            if (!interactableAction.CanPlayFastAction) return false;
            if (!ThisCard.IsInPlay) return false;
            if (!GameControl.AllInvestigatorsInGame.Exists(i => i.Resources > 0 && i.CurrentLocation == Investigator.CurrentLocation)) return false;
            if (amountUsed >= 2) return false;
            return true;
        }

        void NoPayDebts()
        {
            AllComponents.PanelCampaign.AddVictoryCard(ThisCard.ID);
            AllComponents.PanelCampaign.RegisterText(Investigator.Name + " pierde 2 de experiencia por no pagar " + ThisCard.Info.Name);
            Investigator.Xp -= 2;
        }

        void PayDebts(InteractableAction interactableAction)
        {
            interactableAction.CardEffects.Add(new CardEffect(
                card: ThisCard,
                effect: ChooseInvestigatorToPay,
                type: EffectType.Instead,
                name: "Pagar 1 recurso a " + ThisCard.Info.Name));
            IEnumerator ChooseInvestigatorToPay()
            {
                List<CardEffect> cardEffects = new List<CardEffect>();
                foreach (CardComponent investigator in GameControl.AllInvestigatorsInGame.Where(i => i.Resources > 0 && i.CurrentLocation == Investigator.CurrentLocation).Select(i => i.InvestigatorCardComponent))
                    cardEffects.Add(new CardEffect(
                        card: investigator,
                        effect: () => PayResource(investigator),
                        type: EffectType.Choose,
                        name: "Pagar " + ThisCard.Info.Name + " con " + investigator.Info.Name,
                        investigatorImageCardInfoOwner: investigator.Owner,
                        investigatorRealOwner: ThisCard.VisualOwner));
                yield return new ChooseCardAction(cardEffects, isOptionalChoice: true).RunNow();
            }

            IEnumerator PayResource(CardComponent investigator)
            {
                amountUsed++;
                yield return new AddTokenAction(ThisCard.ResourcesToken, 1, investigator.ResourcesToken).RunNow();
            }
        }

        public override IEnumerator Revelation() =>
             new MoveCardAction(ThisCard, Investigator.Threat, withPreview: false).RunNow();
    }
}