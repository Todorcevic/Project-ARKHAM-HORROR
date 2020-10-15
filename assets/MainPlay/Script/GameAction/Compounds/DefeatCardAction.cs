using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArkhamGamePlay
{
    public class DefeatCardAction : GameAction
    {
        public CardComponent CardToDefeat { get; set; }

        /*****************************************************************************************/
        public DefeatCardAction(CardComponent cardToDie) => CardToDefeat = cardToDie;

        /*****************************************************************************************/
        protected override IEnumerator ActionLogic()
        {
            if (CardToDefeat.Info.Victory != null)
            {
                yield return new DiscardAction(CardToDefeat, outGame: true).RunNow();
                AllComponents.Table.Victory.AddCard(CardToDefeat);
            }
            else yield return new DiscardAction(CardToDefeat).RunNow();
        }

        protected override IEnumerator Animation()
        {
            CardToDefeat.CardTools.PlayOneShotSound(CardToDefeat.Effect6);
            yield return new WaitWhile(() => CardToDefeat.CardTools.AudioSource.isPlaying);
        }
    }
}