using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdSystem : MonoBehaviour
{
    [SerializeField] private Transform runnerParent;
    [SerializeField] private GameObject runnerPrefab;
    [SerializeField] private PlayerAnimator playerAnimator;

    [SerializeField] private float radius;
    [SerializeField] private float angel;

    private void Update()
    {
        if (!GameManager.instance.IsGameState()) return;

        this.PlaceRunner();

        if (this.runnerParent.childCount <= 0)
            GameManager.instance.SetGameState(GameManager.GameState.GameOver);
    }

    private void PlaceRunner()
    {
        for(int i = 0; i < this.runnerParent.childCount; i ++)
        {
            float x = i * 1;
            this.runnerParent.GetChild(i).localPosition = this.PlayerRunnerLocalPosition(i);
        }
    }

    private Vector3 PlayerRunnerLocalPosition(int index)
    {
        float x = radius * Mathf.Sqrt(index) * Mathf.Cos(Mathf.Deg2Rad * index * angel);
        float z = radius * Mathf.Sqrt(index) * Mathf.Sin(Mathf.Deg2Rad * index * angel);
        return new Vector3(x, 0, z);
    }

    public float GetCrowdRadius()
    {
        return radius * Mathf.Sqrt(runnerParent.childCount);
    }

    public void ApplyBonus(int bonusAmount, BonusType bonusType)
    {
        switch(bonusType)
        {
            case BonusType.Addition:
                this.AddRunner(bonusAmount);
                break;
            case BonusType.Product:
                int runnerToAdd = (this.runnerParent.childCount * bonusAmount) - this.runnerParent.childCount;
                this.AddRunner(runnerToAdd);
                break;
            case BonusType.Difference:
                if (this.runnerParent.childCount < bonusAmount) GameManager.instance.SetGameState(GameManager.GameState.GameOver);
                this.RemoveRunner(bonusAmount);
                break;
            case BonusType.Division:
                int runnerToRemove = this.runnerParent.childCount - (this.runnerParent.childCount / bonusAmount);
                this.RemoveRunner(runnerToRemove);
                break;

        }

    }

    private void AddRunner(int amount)
    {
        for(int i = 0; i < amount; i++)
        {
            Instantiate(this.runnerPrefab, runnerParent);
        }
        this.playerAnimator.Run();
    }

    private void RemoveRunner(int amount)
    {
        if(amount > this.runnerParent.childCount)
        {
            amount = this.runnerParent.childCount;

        }

        int runnerAmount = runnerParent.childCount;

        for(int i = runnerAmount - 1; i > runnerAmount - amount; i--)
        {
            Transform runnerToDestroy = this.runnerParent.GetChild(i);
            runnerToDestroy.SetParent(null);
            Destroy(runnerToDestroy.gameObject);
        }
    }
}
