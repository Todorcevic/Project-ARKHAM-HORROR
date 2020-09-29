using System.Collections;
using System;
using UnityEngine;

public class EventAction<T> : GameAction
{
    readonly Action<T> OnAction;
    readonly T parameter;
    public override GameActionType GameActionType => GameActionType.Basic;

    /*****************************************************************************************/
    public EventAction(Action<T> OnAction, T parameter)
    {
        this.OnAction = OnAction;
        this.parameter = parameter;
    }

    /*****************************************************************************************/
    protected override IEnumerator ActionLogic()
    {
        OnAction?.Invoke(parameter);
        yield return null;
    }
}
