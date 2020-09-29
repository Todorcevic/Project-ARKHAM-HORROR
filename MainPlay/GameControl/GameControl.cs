using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class GameControl
{
    static InvestigatorComponent currentInvestigator;
    static InvestigatorComponent activeInvestigator;
    static GameAction currentAction;

    /*****************************************************************************************/
    public static bool IsTestMode { get; set; }
    public static bool GameIsStarted { get; set; }
    public static bool FlowIsRunning { get; set; } = true;
    public static ControlPanelComponent CurrentPanelActive { get; set; }
    public static List<InvestigatorComponent> AllInvestigators => AllInvestigatorsInGame.Concat(InvestigatorsDefeat).ToList();
    public static List<InvestigatorComponent> AllInvestigatorsInGame { get; set; } = new List<InvestigatorComponent>();
    public static List<InvestigatorComponent> InvestigatorsDefeat { get; set; } = new List<InvestigatorComponent>();
    public static InvestigatorComponent CurrentInvestigator
    {
        get => currentInvestigator;
        set
        {
            if (currentInvestigator != null) currentInvestigator.SelectGlow.enabled = false;
            value.SelectGlow.enabled = true;
            currentInvestigator = value;
        }
    }
    public static InvestigatorComponent ActiveInvestigator
    {
        get => activeInvestigator ?? LeadInvestigator;
        set
        {
            activeInvestigator = value;
            if (value == null) CurrentInteractableAction = null;
        }
    }
    public static InvestigatorComponent TurnInvestigator { get; set; }
    public static InvestigatorComponent LeadInvestigator => AllInvestigators[0];
    public static List<CardComponent> AllCardComponents { get; set; } = new List<CardComponent>();
    public static CardComponent TopDeck => ActiveInvestigator.InvestigatorDeck.ListCards.LastOrDefault();
    public static CardAct CurrentAct => (CardAct)AllComponents.Table.Act.ListCards.LastOrDefault()?.CardLogic;
    public static CardAgenda CurrentAgenda => (CardAgenda)AllComponents.Table.Agenda.ListCards.LastOrDefault()?.CardLogic;
    public static CardScenario CurrentScenarioCard => (CardScenario)AllComponents.Table.Scenario.ListCards.LastOrDefault()?.CardLogic;
    public static CardComponent GetCard(string id) => AllCardComponents.Find(c => c.ID == id);
    public static Dictionary<DeckType, List<CardComponent>> Deck { get; set; } = new Dictionary<DeckType, List<CardComponent>>();
    public static PhaseAction CurrentPhase { get; set; }
    public static GameAction CurrentAction
    {
        get => currentAction;
        set
        {
            currentAction = value;
            if (currentAction is InteractableAction interactableAction) CurrentInteractableAction = interactableAction;
        }
    }
    public static InteractableAction CurrentInteractableAction { get; set; }
    public static SkillTestAction CurrentSkillTestAction { get; set; }
    public static Effect NoResolution { get; set; }
}
