using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum BonusType { Addition, Difference, Product, Division }

public class Doors : MonoBehaviour
{
    [SerializeField] private SpriteRenderer rightDoorRenderer;
    [SerializeField] private SpriteRenderer leftDoorRenderer;
    [SerializeField] private TextMeshPro rightText;
    [SerializeField] private TextMeshPro leftText;

    [SerializeField] private BonusType rightBonusType;
    [SerializeField] private int rightBonusAmount;

    [SerializeField] private BonusType leftBonusType;
    [SerializeField] private int leftBonusAmount;

    [SerializeField] private Collider doorCollider;

    private void OnEnable()
    {
        this.rightBonusType = GetRandomEnumValue<BonusType>();
        this.rightBonusAmount = UnityEngine.Random.Range(0, 11);
        this.leftBonusType = GetRandomEnumValue<BonusType>();
        this.leftBonusAmount = UnityEngine.Random.Range(0, 11);
    }

    private void Update()
    {
        this.ConfigureDoors();
    }

    private void ConfigureDoors()
    {
        switch(this.rightBonusType)
        {
            case BonusType.Addition:
                rightText.text = "+ " + rightBonusAmount;
                break;
            case BonusType.Difference:
                rightText.text = "- " + rightBonusAmount;
                break;
            case BonusType.Division:
                rightText.text = "/ " + rightBonusAmount;
                break;
            case BonusType.Product:
                rightText.text = "* " + rightBonusAmount;
                break;

        }

        switch (this.leftBonusType)
        {
            case BonusType.Addition:
                leftText.text = "+ " + leftBonusAmount;
                break;
            case BonusType.Difference:
                leftText.text = "- " + leftBonusAmount;
                break;
            case BonusType.Division:
                leftText.text = "/ " + leftBonusAmount;
                break;
            case BonusType.Product:
                leftText.text = "* " + leftBonusAmount;
                break;
        }
    }

    public int GetBonusAmount(float xPosition)
    {
        if(xPosition > 0)
        {
            return this.rightBonusAmount;
        }
        else
        {
            return this.leftBonusAmount;
        }
    }    

    public BonusType GetBonusType(float xPosition)
    {
        if(xPosition > 0)
        {
            return this.rightBonusType;
        }
        else
        {
            return this.leftBonusType;
        }
    }

    public void DisableCollider()
    {
        this.doorCollider.enabled = false;
    }

    public static T GetRandomEnumValue<T>()
    {
        Array values = Enum.GetValues(typeof(T));
        System.Random random = new System.Random();
        return (T)values.GetValue(random.Next(values.Length));
    }
}
