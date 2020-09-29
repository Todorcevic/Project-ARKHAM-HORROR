using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IButtonClickable
{
    void SetButton();
    void ReadyClicked(ReadyButton button);
}
