using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

namespace ArkhamGamePlay
{
    public class MultiCastAction : GameAction
    {
        readonly bool isOptionalChoice;
        ChooseMultiCast chooseMultiCast;
        public override GameActionType GameActionType => GameActionType.Compound;
        public List<CardEffect> ListCardsEffect { get; set; } = new List<CardEffect>();
        public bool IsCancel { get; set; }

        /*****************************************************************************************/
        public MultiCastAction(List<CardEffect> cardEffects, bool isOptionalChoice = true)
        {
            this.isOptionalChoice = isOptionalChoice;
            foreach (CardEffect effectStruct in cardEffects)
            {
                CardComponent cloneCard = effectStruct.Card.CardTools.Clone();
                cloneCard.ID = effectStruct.Card.ID;
                cloneCard.CardLogic = new CardLogic().WithThisCard(cloneCard);
                cloneCard.CurrentZone = effectStruct.Card.MyOwnZone;
                cloneCard.transform.position = effectStruct.Card.transform.position;
                cloneCard.transform.rotation = effectStruct.Card.transform.rotation;
                cloneCard.CardSensor.StackerZone.transform.DOScale(0, 0);
                effectStruct.Card = cloneCard;
                effectStruct.Effect = new ActionsTools().JoinEffects(Destroying, effectStruct.Effect);
                ListCardsEffect.Add(effectStruct);
                GameControl.AllCardComponents.Add(cloneCard);
            }
        }

        /*****************************************************************************************/
        protected override IEnumerator ActionLogic()
        {
            chooseMultiCast = new ChooseMultiCast(ListCardsEffect, isOptionalChoice: isOptionalChoice);
            yield return chooseMultiCast.RunNow();
            yield return new WaitWhile(() => DOTween.IsTweening("MoveFast"));
            if (chooseMultiCast.IsCancel) IsCancel = true;
            yield return Destroying();
        }

        IEnumerator Destroying()
        {
            if (chooseMultiCast.CardPlayed != null)
                yield return chooseMultiCast.CardPlayed.MoveTo(AllComponents.CardBuilder.Zone, timeAnimation: 0f).WaitForCompletion();
            yield return new WaitWhile(() => DOTween.IsTweening("HorizontalOrder"));
            foreach (CardComponent card in ListCardsEffect.Select(c => c.Card))
            {
                card.CurrentZone.ListCards.Remove(card);
                card.CardTools.Destroy();
            }
            ListCardsEffect.Clear();
        }
    }
}