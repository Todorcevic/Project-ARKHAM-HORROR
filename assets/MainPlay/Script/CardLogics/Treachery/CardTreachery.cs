using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArkhamGamePlay
{
    public abstract class CardTreachery : CardLogic, IRevelation
    {
        public virtual bool IsDiscarted => true;

        /*****************************************************************************************/
        public abstract IEnumerator Revelation();
    }
}