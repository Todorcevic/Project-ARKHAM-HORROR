using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card01049 : BasicTalent
{
    protected override Skill SkillToTest => (Skill.Combat | Skill.Agility);
}
