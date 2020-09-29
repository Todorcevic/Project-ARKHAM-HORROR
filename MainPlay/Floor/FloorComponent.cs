using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorComponent : MonoBehaviour
{
    private void OnTriggerEnter(Collider item)
    {
        if (item.gameObject.CompareTag("ChaosToken"))
        {
            item.transform.localPosition = Vector3.zero;
            item.transform.GetComponent<Rigidbody>().Sleep();
        }
    }
}