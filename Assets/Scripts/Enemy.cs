using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    enum State { Idle, Running}

    [SerializeField] private float searchRadius;
    [SerializeField] private float moveSpeed;
    private State state;
    private Transform targetRunner;

    private void Update()
    {
        this.ManageState();
    }

    private void ManageState()
    {
        switch (state)
        {
            case State.Idle:
                this.SearchForTarget(); 
                break;
            case State.Running:
                this.RunnerToTarget(); 
                break;
        }
    }

    private void SearchForTarget()
    {
        Collider[] detectedColliders = Physics.OverlapSphere(this.transform.position, this.searchRadius);
        for(int i = 0; i < detectedColliders.Length; i++)
        {
            if (detectedColliders[i].TryGetComponent(out Runner runner))
            {
                if(runner.IsTarget())
                    continue;

                runner.SetTarget();
                targetRunner = runner.transform;

                this.StartRunningToTarget();
            }
        }
    }

    private void StartRunningToTarget()
    {
        state = State.Running;
        GetComponent<Animator>().Play("Running");
    }

    private void RunnerToTarget()
    {
        if(targetRunner == null)
            return;
        transform.position = Vector3.MoveTowards(transform.position, targetRunner.position, Time.deltaTime * moveSpeed);
        if(Vector3.Distance(transform.position, targetRunner.position) < 0.1f)
        {
            Destroy(targetRunner.gameObject);
            Destroy(gameObject);
        }
    }
}
