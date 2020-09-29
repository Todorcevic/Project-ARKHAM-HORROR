using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRevelation
{
    bool IsDiscarted { get; }
    IEnumerator Revelation();
}
