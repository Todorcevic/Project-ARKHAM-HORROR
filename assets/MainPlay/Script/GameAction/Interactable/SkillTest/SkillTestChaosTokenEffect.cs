using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.Emit;
using UnityEngine;

namespace ArkhamGamePlay
{
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
                yield return new AnimationCardAction(GameControl.CurrentScenarioCard.ThisCard, audioClip: TakeAudio(chaosToken.Type)).RunNow();
            else if (ChaosTokenType.Win.HasFlag(chaosToken?.Type))
                yield return new AnimationCardAction(GameControl.ActiveInvestigator.InvestigatorCardComponent, audioClip: GameControl.ActiveInvestigator.InvestigatorCardComponent.Effect5).RunNow();
            yield return chaosToken.Effect?.Invoke();
        }

        AudioClip TakeAudio(ChaosTokenType chaosToken)
        {
            switch (chaosToken)
            {
                case ChaosTokenType.Cultist: return GameControl.CurrentScenarioCard.ThisCard.ClipType1;
                case ChaosTokenType.Tablet: return GameControl.CurrentScenarioCard.ThisCard.ClipType2;
                case ChaosTokenType.Skull: return GameControl.CurrentScenarioCard.ThisCard.ClipType3;
                case ChaosTokenType.Kthulu: return GameControl.CurrentScenarioCard.ThisCard.ClipType4;
            }
            return null;
        }
    }
}