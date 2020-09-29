using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card01007 : InvestigatorTreachery
{
    public override bool IsDiscarted => false;

    /*****************************************************************************************/
    protected override void BeginGameAction(GameAction gameAction)
    {
        if (gameAction is DiscoverCluesAction discoverClues && FindClue(discoverClues))
            discoverClues.ChooseCardOptionalAction.ChosenCardEffects.Add(new CardEffect(
                card: ThisCard,
                effect: () => PayClue(discoverClues),
                animationEffect: PayClueAnimation,
                type: EffectType.Reaction,
                name: "Pagar pista"));
        if (gameAction is InvestigatorEliminatedAction investigatorEliminated && CountClues(investigatorEliminated))
            TraumaEffect();
        if (gameAction is FinishGameAction && CountClues()) TraumaEffect();
    }

    bool FindClue(DiscoverCluesAction discoverCluesAction)
    {
        if (!ThisCard.IsInPlay) return false;
        if (discoverCluesAction.Location != Investigator.CurrentLocation) return false;
        return true;
    }

    bool CountClues(InvestigatorEliminatedAction finishGameAction)
    {
        if (!ThisCard.IsInPlay) return false;
        if (ThisCard.CluesToken.Amount < 1) return false;
        if (finishGameAction.Investigator != Investigator) return false;
        return true;
    }

    bool CountClues()
    {
        if (!ThisCard.IsInPlay) return false;
        if (ThisCard.CluesToken.Amount < 1) return false;
        return true;
    }

    void TraumaEffect() =>
        new InvestigatorTraumaAction(Investigator, Investigator.Name + " sufre 1 trauma mental por " + ThisCard.Info.Name, isPhysical: false, ThisCard).AddActionTo();

    IEnumerator PayClueAnimation() => new AnimationCardAction(ThisCard, audioClip: ThisCard.Effect1).RunNow();

    IEnumerator PayClue(DiscoverCluesAction discoverClues)
    {
        discoverClues.IsActionCanceled = true;
        yield return new AddTokenAction(ThisCard.CluesToken, -discoverClues.Amount).RunNow();
    }

    public override IEnumerator Revelation()
    {
        yield return new MoveCardAction(ThisCard, Investigator.Threat, withPreview: false).RunNow();
        yield return new AddTokenAction(ThisCard.CluesToken, 3).RunNow();
    }
}
