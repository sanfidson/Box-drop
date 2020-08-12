using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{
    public GameObject box_prefab;

    public void SpawnBox()
    {
        Vector3 temp = transform.position;
        temp.z = 0f;

        GameObject box_Obj = Instantiate(box_prefab);
        box_Obj.transform.position = temp;
    }
}
