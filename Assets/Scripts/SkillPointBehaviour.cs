using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class SkillPointBehaviour : MonoBehaviour
{
    [SerializeField] private int skillPointsInside = 1;
    private void OnTriggerEnter(Collider other)
    {
        GiveBonus(other);
    }

    private void GiveBonus(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
            GameManager.AddSp(skillPointsInside);
            Destroy(gameObject);
        }
    }
}
