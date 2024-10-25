using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSSILLATOR : MonoBehaviour
{
    Vector3 startingposition;
    [SerializeField] Vector3 movementvector;
    float movementFactor;
    [SerializeField] float periods;
    void Start()
    {
        startingposition = transform.position;
    }

    void Update()
    {
        if (periods <=Mathf.Epsilon) { return; }

        float cycles = Time.time / periods;//getting a increasing value
        const float tau=Mathf.PI*2;//a constant value of 6.28
        float rawsinwave=Mathf.Sin(cycles*tau);//getting a value from -1 to 1
        movementFactor = (rawsinwave + 1) / 2;

        Vector3 offset=movementvector*movementFactor;
        transform.position=offset+startingposition;
    }
}
