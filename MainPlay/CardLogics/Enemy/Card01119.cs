using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card01119 : CardEnemy
{
    CardComponent Cellar => GameControl.GetCard("01114");
    public override CardComponent SpawnLocation => Cellar;
}
