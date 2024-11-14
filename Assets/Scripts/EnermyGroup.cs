using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnermyGroup : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefabs;
    [SerializeField] private Transform enemyParent;

    [SerializeField] private int amount;
    [SerializeField] private float radius;
    [SerializeField] private float angel;

    private void OnEnable()
    {
        amount = UnityEngine.Random.Range(0, 11);
    }

    private void Start()
    {
        this.EnemyGenerate();
    }

    private void EnemyGenerate()
    {
        for(int i = 0; i < amount; i++)
        {
            Vector3 enemyLocalPosition = PlayerRunnerLocalPosition(i);
            Vector3 enemyWorldPosition = this.enemyParent.TransformPoint(enemyLocalPosition);
            Instantiate(enemyPrefabs, enemyWorldPosition,Quaternion.identity, this.enemyParent);
        }    
    }

    private Vector3 PlayerRunnerLocalPosition(int index)
    {
        float x = radius * Mathf.Sqrt(index) * Mathf.Cos(Mathf.Deg2Rad * index * angel);
        float z = radius * Mathf.Sqrt(index) * Mathf.Sin(Mathf.Deg2Rad * index * angel);
        return new Vector3(x, 0, z);
    }
}
