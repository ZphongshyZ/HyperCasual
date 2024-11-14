using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDetection : MonoBehaviour
{
    [SerializeField] private CrowdSystem crowdSystem;
    [SerializeField] private bool isFinish = false;


    private void Update()
    {
        this.DetectDoors();
    }

    private void DetectDoors()
    {
        if (isFinish) return;
        Collider[] detectedColliders = Physics.OverlapSphere(transform.position, 1);
        for(int i=0; i < detectedColliders.Length; i ++)
        {
            if (detectedColliders[i].TryGetComponent(out Doors doors))
            {
                int bonusAmount = doors.GetBonusAmount(transform.position.x);
                BonusType bonusType = doors.GetBonusType(transform.position.x);

                crowdSystem.ApplyBonus(bonusAmount, bonusType);
                doors.DisableCollider();
            }
            else if (detectedColliders[i].CompareTag("Finish"))
            {
                isFinish = true;
                PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level") + 1);
                GameManager.instance.SetGameState(GameManager.GameState.LevelComplete);
            }
        }
    }
}
