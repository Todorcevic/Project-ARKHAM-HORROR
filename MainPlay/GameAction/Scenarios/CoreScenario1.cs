using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreScenario1 : PhaseAction
{
    CardComponent studio = GameControl.GetCard("01111");
    CardComponent lita = GameControl.GetCard("01117");
    CardComponent sacerdoteGul = GameControl.GetCard("01116");
    public override string GameActionInfo => "Preparación del Escenario: El Encuentro";

    /*****************************************************************************************/
    protected override IEnumerator PhaseLogic()
    {
        yield return SetEncounterDeck();
        yield return SetScenario();
        yield return SetAgenda();
        yield return SetAct();
        yield return SetLocation();
        yield return SetPlayCards();
        GameControl.NoResolution = NoResolution;
    }

    IEnumerator SetEncounterDeck()
    {
        yield return new MoveDeck(GameControl.Deck[DeckType.Encounter], AllComponents.Table.EncounterDeck, isBack: true).RunNow();
        yield return new ShuffleAction(AllComponents.Table.EncounterDeck).RunNow();
    }

    IEnumerator SetScenario()
    {
        bool isBack = (Difficulty.Expert | Difficulty.Hard).HasFlag(GameData.Difficulty);
        yield return new ShowCardAction(GameControl.Deck[DeckType.Scenario][0], isBack: isBack).RunNow();
        yield return new MoveCardAction(GameControl.Deck[DeckType.Scenario][0], AllComponents.Table.Scenario, withPreview: false).RunNow();
    }

    IEnumerator SetAgenda()
    {
        yield return new MoveDeck(GameControl.Deck[DeckType.Agenda], AllComponents.Table.Agenda).RunNow();
        yield return new ShowCardAction(GameControl.CurrentAgenda.ThisCard).RunNow();
        yield return new MoveCardAction(GameControl.CurrentAgenda.ThisCard, AllComponents.Table.Agenda, withPreview: false).RunNow();
    }

    IEnumerator SetAct()
    {
        yield return new MoveDeck(GameControl.Deck[DeckType.Act], AllComponents.Table.Act).RunNow();
        yield return new ShowCardAction(GameControl.CurrentAct.ThisCard).RunNow();
        yield return new MoveCardAction(GameControl.CurrentAct.ThisCard, AllComponents.Table.Act, withPreview: false).RunNow();
    }

    IEnumerator SetLocation()
    {
        yield return new ShowCardAction(studio).RunNow();
        yield return new MoveCardAction(studio, AllComponents.Table.LocationZones[1], withPreview: false).RunNow();
    }

    IEnumerator SetPlayCards()
    {
        foreach (InvestigatorComponent investigator in GameControl.AllInvestigatorsInGame)
            yield return new MoveCardAction(investigator.PlayCard, studio.MyOwnZone, isFast: true).RunNow();
        yield return new WaitWhile(() => DOTween.IsTweening("MoveCard"));
    }

    public IEnumerator NoResolution()
    {
        if (GameControl.CurrentAct is Card01110)
        {
            yield return new PanelHistoryAction("no_resolution").RunNow();
            AllComponents.PanelCampaign.RegisterText("Tu casa sigue en pie.");
            AllComponents.PanelCampaign.RegisterText(sacerdoteGul.Info.Name + " sigue vivo.");
            yield return TakeLitaCard(GameControl.LeadInvestigator);
            GainXp();
        }
        else yield return R3();
    }

    public IEnumerator R1()
    {
        yield return new PanelHistoryAction("resolution1").RunNow();
        yield return new InvestigatorTraumaAction(GameControl.LeadInvestigator, "Tu casa ha ardido hasta los cimientos. (" + GameControl.LeadInvestigator.Name + " sufre un trauma Mental).", isPhysical: false).RunNow();
        yield return TakeLitaCard(GameControl.LeadInvestigator);
        GainXp();
    }

    public IEnumerator R2()
    {
        yield return new PanelHistoryAction("resolution2").RunNow();
        AllComponents.PanelCampaign.RegisterText("Tu casa sigue en pie. (" + GameControl.LeadInvestigator.Name + " gana 1 de experiencia).");
        GameControl.LeadInvestigator.Xp++;
        GainXp();
    }

    public IEnumerator R3()
    {
        yield return new PanelHistoryAction("resolution3").RunNow();
        AllComponents.PanelCampaign.RegisterText(lita.Info.Name + " se vio obligada a encontrara otros que la ayudaran en su causa.");
        AllComponents.PanelCampaign.RegisterText("Tu casa sigue en pie.");
        AllComponents.PanelCampaign.RegisterText(sacerdoteGul.Info.Name + " sigue vivo.");
        foreach (InvestigatorComponent investigator in GameControl.AllInvestigators.FindAll(i => !i.IsResign))
        {
            yield return new InvestigatorTraumaAction(investigator, investigator.Name + " ha muerto.", isPhysical: true).RunNow();
            investigator.PhysicalTraumas = (int)investigator.InvestigatorCardComponent.Info.Health;
        }
        if (GameControl.AllInvestigators.FindAll(i => !i.IsDie).Count < 1) Debug.Log("GameOver");
        else if (GameControl.LeadInvestigator.IsDie) yield return TakeLitaCard(GameControl.AllInvestigators.Find(i => i.IsResign));
    }

    void GainXp()
    {
        int victoryPoints = 0;
        foreach (CardComponent victoryCard in AllComponents.Table.Victory.ListCards.FindAll(c => c.Info.Victory != null))
        {
            victoryPoints += (int)victoryCard.Info.Victory;
            AllComponents.PanelCampaign.RegisterText(victoryCard.Info.Name + " otorga " + victoryCard.Info.Victory + " puntos de experiencia a cada investigador.");
            AllComponents.PanelCampaign.AddVictoryCard(victoryCard.ID);
        }
        foreach (InvestigatorComponent investigator in GameControl.AllInvestigators)
            investigator.Xp += victoryPoints + 2;
        AllComponents.PanelCampaign.RegisterText("Cada investigador obtiene 2 puntos de experiencia por haber adquirido conocimientos sobre el mundo oculto de los Mitos.");
    }

    IEnumerator TakeLitaCard(InvestigatorComponent investigator)
    {
        yield return new ChooseCardAction(new CardEffect(
            card: lita,
            effect: TakeLita,
            animationEffect: TakeLitaAnimation,
            type: EffectType.Choose,
            name: "Agregar " + lita.Info.Name + " al mazo de " + investigator.Name,
            investigatorImageCardInfoOwner: investigator), isOptionalChoice: true).RunNow();

        IEnumerator TakeLitaAnimation() => new AnimationCardAction(lita, withReturn: false, audioClip: lita.Effect2).RunNow();

        IEnumerator TakeLita()
        {
            yield return new MoveCardAction(lita, investigator.InvestigatorDeck, withPreview: false, returnIsBack: false).RunNow();
            AllComponents.PanelCampaign.RegisterText(investigator.Name + " obtiene la carta " + lita.Info.Name);
            AllComponents.PanelCampaign.AddVictoryCard(lita.ID);
            investigator.ListCardComponent.Add(lita);
            yield return null;
        }
    }
}
