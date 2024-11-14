using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour
{
    private bool isTarget;

    public void SetTarget()
    {
        this.isTarget = true;
    }

    public bool IsTarget()
    {
        return this.isTarget;
    }
}
