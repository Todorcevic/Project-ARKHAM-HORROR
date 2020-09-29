using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuffable
{
    bool ActiveBuffCondition(GameAction gameAction);
    void BuffEffect();
    void DeBuffEffect();
}
