using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvestigatorAttackAction : GameAction
{
    SkillTest combatTest;

    /*****************************************************************************************/
    public InvestigatorAttackAction(SkillTest combatTest) => this.combatTest = combatTest;

    /*****************************************************************************************/
    protected override IEnumerator ActionLogic() => new SkillTestAction(combatTest).RunNow();
}
