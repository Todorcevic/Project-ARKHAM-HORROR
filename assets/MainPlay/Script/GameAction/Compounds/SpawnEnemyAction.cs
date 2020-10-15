using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArkhamGamePlay
{
    public class SpawnEnemyAction : GameAction
    {
        public CardComponent Enemy { get; set; }
        readonly CardComponent spawnSite;

        /*****************************************************************************************/
        public SpawnEnemyAction(CardComponent enemy, InvestigatorComponent investigator)
        {
            Enemy = enemy;
            spawnSite = ((CardEnemy)enemy.CardLogic).SpawnLocation ?? investigator.InvestigatorCardComponent;
        }

        /*****************************************************************************************/
        protected override IEnumerator ActionLogic()
        {
            if (spawnSite.CardType == CardType.Investigator)
                yield return new MoveCardAction(Enemy, spawnSite.Owner.Threat, isBack: false, withPreview: false).RunNow();
            else if (spawnSite.CardType == CardType.Location && spawnSite.IsInPlay)
                yield return new MoveCardAction(Enemy, spawnSite.MyOwnZone, isBack: false, withPreview: false).RunNow();
            else yield return new DiscardAction(Enemy, withTopUp: false).RunNow();
        }
    }
}