using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Waypoints : MonoBehaviour
{
    Transform[] children;

    int index = 0;

    public Transform Current => children[index];

    private void Awake()
    {
        children = new Transform[children.Length];

        
    }
}
