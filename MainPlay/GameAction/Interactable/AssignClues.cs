using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

public class AssignClues : InteractableCenterShow
{
    List<InvestigatorComponent> investigators;
    public int CluesAmount => ChosenCardEffects.Select(c => c.Card.CluesToken.AssignValue).Sum();

    /*****************************************************************************************/
    public AssignClues(InvestigatorComponent investigator) =>
        investigators = new List<InvestigatorComponent>() { investigator };

    public AssignClues(List<InvestigatorComponent> investigators) =>
        this.investigators = investigators;

    /*****************************************************************************************/
    protected override IEnumerator ActionLogic()
    {
        if (ActiveInvestigator.IsDefeat) yield break;
        PlayableCards();
        SetButton();
        ActiveTokens(true);
        yield return ShowPreviewCards();
        AllComponents.ShowHideChooseCard.ICenterShowableAction = this;
        GameControl.FlowIsRunning = false;
        yield return new WaitUntil(() => AnyIsClicked);
        GameControl.FlowIsRunning = true;
        AllComponents.ShowHideChooseCard.ICenterShowableAction = null;
        AllComponents.ReadyButton.State = ButtonState.Off;
        yield return ShowTable();
        ActiveTokens(false);
        AnyIsClicked = false;
        yield return AddingAllClues();
    }

    IEnumerator AddingAllClues()
    {
        foreach (CardComponent card in ChosenCardEffects.FindAll(c => c.Card.CluesToken.AssignValue > 0).Select(c => c.Card))
        {
            yield return new AddTokenAction(GameControl.CurrentAct.ThisCard.CluesToken, card.CluesToken.AssignValue, card.CluesToken).RunNow();
            card.CluesToken.AssignValue = 0;
        }
    }

    protected override void PlayableCards()
    {
        foreach (CardComponent investigatorCard in investigators.FindAll(i => i.Clues > 0).Select(i => i.InvestigatorCardComponent))
            ChosenCardEffects.Add(new CardEffect(
                card: investigatorCard,
                effect: () => null,
                type: EffectType.Choose,
                name: "Asignar pistas",
                investigatorImageCardInfoOwner: investigatorCard.Owner));
    }

    public override void SetButton()
    {
        if (CluesAmount < 1)
        {
            AllComponents.ReadyButton.SetReadyButton(this, state: ButtonState.StandBy);
            AllComponents.ReadyButton.ChangeButtonText("Cancelar");
        }
        else
        {
            AllComponents.ReadyButton.SetReadyButton(this, state: ButtonState.Ready);
            AllComponents.ReadyButton.ChangeButtonText("Entregar " + CluesAmount + " pistas.");
        }

        foreach (CardComponent card in ChosenCardEffects.Select(c => c.Card))
        {
            card.CluesToken.TokenText((card.CluesToken.Amount - card.CluesToken.AssignValue).ToString());
            card.CluesToken.ButtonUpActive(card.CluesToken.Amount - card.CluesToken.AssignValue > 0);
            card.CluesToken.ButtonDownActive(card.CluesToken.AssignValue > 0);
        }
    }

    public override void ReadyClicked(ReadyButton button) => AnyIsClicked = true;

    void ActiveTokens(bool isActive)
    {
        foreach (CardComponent card in ChosenCardEffects.Select(c => c.Card))
        {
            if (isActive)
            {
                card.ResourcesToken.ShowToken(false);
                card.DoomToken.ShowToken(false);
                card.CluesToken.ShowToken(true);
                card.HealthToken.ShowToken(false);
                card.SanityToken.ShowToken(false);
                SetButton();
            }
            else
            {
                card.HealthToken.ShowAmount();
                card.SanityToken.ShowAmount();
                card.ResourcesToken.ShowAmount();
                card.DoomToken.ShowAmount();
                card.CluesToken.ShowAmount();
                card.CluesToken.HideButtons();
            }
        }
    }

    public override void CardSelected(CardComponent card) { }
    public override void CardPlay(CardComponent card) { }
}
