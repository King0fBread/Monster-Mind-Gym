using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableSelfOrSpecified : MonoBehaviour
{
    public GameObject objectToToggle;
    public void DisableSelf()
    {
        gameObject.SetActive(false);
    }
    public void DisableSpecified()
    {
        objectToToggle.SetActive(false);
    }
    public void EnableSpecified()
    {
        objectToToggle.SetActive(true);
    }

}
