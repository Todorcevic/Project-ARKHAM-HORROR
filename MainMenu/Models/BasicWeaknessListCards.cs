using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace CardManager
{
    public class basicWeaknessListCards
    {
        public List<string> basicWeakness = AllCards.DataCard.Where(c => c.subtype_code == "basicweakness").Select(c => c.code).ToList();
    }
}
