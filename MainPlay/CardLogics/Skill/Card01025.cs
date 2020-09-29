using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Card01025 : CardSkill
{
    bool isActive;

    /*****************************************************************************************/
    protected override void BeginGameAction(GameAction gameAction)
    {
        if (gameAction is SkillTestActionComplete skillTestComplete && CheckSkillTestWin(skillTestComplete))
            isActive = true;
        if (gameAction is DamageEnemyAction damageEnemyAction && isActive)
            new EffectAction(() => DamageEnemy(damageEnemyAction), DamageenemyAnimation).AddActionTo();
    }

    protected override void EndGameAction(GameAction gameAction)
    {
        if (gameAction is SkillTestActionComplete && isActive) isActive = false;
    }

    protected override bool CheckSkillTestWin(SkillTestActionComplete skillTestComplete)
    {
        if (ThisCard.CurrentZone != AllComponents.Table.SkillTest) return false;
        if (skillTestComplete.SkillTest.SkillTestType != SkillTestType.Attack) return false;
        if (!skillTestComplete.SkillTest.IsWin) return false;
        return true;
    }

    IEnumerator DamageenemyAnimation() =>
        new AnimationCardAction(ThisCard, audioClip: ThisCard.Effect1).RunNow();

    IEnumerator DamageEnemy(DamageEnemyAction damageEnemyAction)
    {
        damageEnemyAction.Amount++;
        yield return null;
    }
}
