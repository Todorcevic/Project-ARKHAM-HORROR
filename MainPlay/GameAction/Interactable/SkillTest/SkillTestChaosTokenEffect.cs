using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.Emit;
using UnityEngine;

public class SkillTestChaosTokenEffect : GameAction
{
    readonly ChaosTokenComponent chaosToken;

    /*****************************************************************************************/
    public SkillTestChaosTokenEffect(ChaosTokenComponent chaosToken) => this.chaosToken = chaosToken;

    /*****************************************************************************************/
    protected override IEnumerator ActionLogic()
    {
        if (chaosToken == null) yield break;
        if ((ChaosTokenType.Cultist | ChaosTokenType.Kthulu | ChaosTokenType.Skull | ChaosTokenType.Tablet).HasFlag(chaosToken?.Type))
            yield return new ShowCardAction(GameControl.CurrentScenarioCard.ThisCard, isBack: GameControl.CurrentScenarioCard.ThisCard.IsBack, withReturn: true).RunNow();
        else if (ChaosTokenType.Win.HasFlag(chaosToken?.Type))
            yield return new ShowCardAction(GameControl.ActiveInvestigator.InvestigatorCardComponent, withReturn: true, audioClip: GameControl.ActiveInvestigator.InvestigatorCardComponent.Effect5).RunNow();
        yield return chaosToken.Effect?.Invoke();
    }
}
