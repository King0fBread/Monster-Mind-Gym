using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableSelfOrSpecified : MonoBehaviour
{
    public GameObject objectToDisable;
    public void DisableSelf()
    {
        gameObject.SetActive(false);
    }
    public void DisableSpecified()
    {
        objectToDisable.SetActive(false);
    }

}
