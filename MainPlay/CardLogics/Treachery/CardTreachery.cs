using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CardTreachery : CardLogic, IRevelation
{
    public virtual bool IsDiscarted => true;

    /*****************************************************************************************/
    public abstract IEnumerator Revelation();
}
