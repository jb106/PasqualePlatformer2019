using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCollisionStatus : MonoBehaviour
{
    public string objectName = "";
    public bool isColliding = false;

    private void OnTriggerStay(Collider other)
    {
        if (objectName == "")
            return;

        if (other.name == objectName)
            isColliding = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (objectName == "")
            return;

        if (other.name == objectName)
            isColliding = false;
    }
}
