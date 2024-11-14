using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Transform runnerParent;
    public void Idle()
    {
        for (int i = 0; i < this.runnerParent.childCount; i++)
        {
            Transform runner = this.runnerParent.GetChild(i);
            Animator runnerAnimator = runner.GetComponent<Animator>();
            runnerAnimator.Play("Idle");
        }
    }

    public void Run()
    {
        for(int i = 0; i < this.runnerParent.childCount; i++)
        {
            Transform runner = this.runnerParent.GetChild(i);
            Animator runnerAnimator = runner.GetComponent<Animator>();
            runnerAnimator.Play("Running");
        }
    }
}
