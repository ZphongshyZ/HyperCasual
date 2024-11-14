using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CrowdCount : MonoBehaviour
{
    [SerializeField] private TextMeshPro countText;
    [SerializeField] private Transform runnerParent;

    private void Update()
    {
        this.countText.text = this.runnerParent.childCount.ToString();
    }
}
