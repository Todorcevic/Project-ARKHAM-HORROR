using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICenterShowable
{
    bool CardsInPreview { get; set; }
    IEnumerator ShowPreviewCards();
    IEnumerator ShowTable();
}
