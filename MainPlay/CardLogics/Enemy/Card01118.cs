using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card01118 : CardEnemy
{
    CardComponent Attic => GameControl.GetCard("01113");
    public override CardComponent SpawnLocation => Attic;
}
