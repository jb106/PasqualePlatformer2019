using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro : MonoBehaviour
{
    public List<GameObject> animatedObjects = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(intro());
    }

    IEnumerator intro()
    {
        int objectIndex = 0;

        yield return new WaitForSeconds(2f);

        while(objectIndex<animatedObjects.Count)
        {
            animatedObjects[objectIndex].GetComponent<Animator>().SetTrigger("play");
            objectIndex++;

            yield return new WaitForSeconds(.2f);
        }
    }
}
