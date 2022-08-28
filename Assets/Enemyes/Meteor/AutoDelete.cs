using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDelete : MonoBehaviour
{
    public int time;
    void Start()
    {
        Destroy(gameObject, time);
    }

}
