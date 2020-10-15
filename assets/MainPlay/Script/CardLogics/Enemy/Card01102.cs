﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArkhamGamePlay
{
    public class Card01102 : CardEnemy, ISpecialAttack
    {
        public override List<InvestigatorComponent> Prey(List<InvestigatorComponent> investigatorList) =>
            investigatorList.Contains(ThisCard.Owner) ? new List<InvestigatorComponent> { ThisCard.Owner } : investigatorList;

        public override bool IsHunter => true;

        /*****************************************************************************************/

        IEnumerator ISpecialAttack.Attack()
        {
            Effect animation = new AnimationCardAction(ThisCard, audioClip: ThisCard.Effect1).RunNow;
            Effect SpecialAttack = new AddTokenAction(GameControl.CurrentAgenda.ThisCard.DoomToken, 1).RunNow;
            new EffectAction(SpecialAttack, animation).AddActionTo();
            yield return null;
        }
    }
}