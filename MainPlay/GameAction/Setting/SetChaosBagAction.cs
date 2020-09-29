using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SetChaosBagAction : GameAction
{
    readonly bool testMode;
    public override string GameActionInfo => "Creando la bolsa de Caos.";
    public override GameActionType GameActionType => GameActionType.Setting;
    string TokenListPath => GameFiles.ChaosBagPath + GameData.Difficulty + ".json";

    /*****************************************************************************************/
    public SetChaosBagAction(bool testMode = false) => this.testMode = testMode;

    /*****************************************************************************************/
    protected override IEnumerator ActionLogic()
    {
        List<string> tokenList = new JsonDataManager().CreateListFromJson<List<string>>(TokenListPath);
        yield return AllComponents.ChaosBag.SetChaosBag(tokenList, testMode);
    }
}
