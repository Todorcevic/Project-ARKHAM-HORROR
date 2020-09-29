using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class AnimationCardAction : GameAction
{
    readonly bool isBuffEvent;
    readonly bool withReturn;
    readonly bool? isBack;
    readonly CardComponent thisCard;
    readonly AudioClip audioClip;
    public override GameActionType GameActionType => GameActionType.Basic;

    /*****************************************************************************************/
    public AnimationCardAction(CardComponent card, bool withReturn = true, bool? isBack = null, AudioClip audioClip = null)
    {
        thisCard = card;
        this.withReturn = withReturn;
        this.isBack = isBack;
        this.audioClip = audioClip;
    }

    public AnimationCardAction(CardComponent card, bool isBuffEvent, AudioClip audioClip = null) : this(card)
    {
        this.audioClip = audioClip;
        this.isBuffEvent = isBuffEvent;
    }

    /*****************************************************************************************/
    protected override IEnumerator ActionLogic()
    {
        if (thisCard.IsEliminated) yield break;
        DOTween.Kill(thisCard.UniqueId + "ExitCard"); //Probar WaitWhile(Dotween.IsTweenen(thisCard.transform))
        yield return new SelectInvestigatorAction(thisCard.VisualOwner).RunNow();
        if (isBuffEvent) yield return BuffEventStartAnimation();
        yield return thisCard.TurnDown(isBack ?? thisCard.IsBack);
        if (thisCard.MyOwnZone?.ListCards.Count > 0) thisCard.CardTools.ShowCardsInMyOwnZone(false);
        yield return thisCard.Preview(timePause: GameData.ANIMATION_TIME_DEFAULT);
        if (audioClip != null)
        {
            yield return new WaitForSeconds(GameData.ANIMATION_TIME_DEFAULT * 2);
            thisCard.CardTools.PlayOneShotSound(audioClip);
        }
        yield return new WaitWhile(() => DOTween.IsTweening("Preview") || thisCard.CardTools.AudioSource.isPlaying);
        thisCard.CardTools.ShowCardsInMyOwnZone(true);
        if (withReturn) yield return thisCard.MoveFast(thisCard.CurrentZone, indexPosition: thisCard.Position);
        yield return new WaitWhile(() => DOTween.IsTweening("MoveFast"));
        if (isBuffEvent) yield return BuffEventEndAnimation();
    }

    IEnumerator BuffEventStartAnimation()
    {
        if (thisCard.Position == null) thisCard.Position = thisCard.CurrentZone.ListCards.IndexOf(thisCard);
        if (thisCard.CurrentZone.ListCards.Last() != thisCard)
            yield return thisCard.transform.DOLocalMoveX(1, GameData.ANIMATION_TIME_DEFAULT).WaitForCompletion();
    }

    IEnumerator BuffEventEndAnimation()
    {
        if (thisCard.CurrentZone.ListCards.Last() != thisCard)
            yield return thisCard.PutAtPosition((int)thisCard.Position).WaitForCompletion();
        thisCard.Position = null;
    }
}
