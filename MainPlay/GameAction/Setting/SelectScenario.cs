using System.Collections;
using System;
using System.Runtime.Remoting;

public class SelectScenario : GameAction
{
    public override GameActionType GameActionType => GameActionType.Setting;

    /*****************************************************************************************/
    protected override IEnumerator ActionLogic()
    {
        ObjectHandle handle = Activator.CreateInstance(null, GameData.Chapter + GameData.Scenario);
        GameAction currentScenario = (GameAction)handle.Unwrap();
        yield return new PanelHistoryAction(GameData.Chapter + GameData.Scenario).RunNow();
        yield return currentScenario.RunNow();
    }
}
