using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card01062 : BasicTalent
{
    protected override Skill SkillToTest => (Skill.Intellect | Skill.Willpower);
}
