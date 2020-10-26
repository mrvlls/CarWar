using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(menuName = "Enemies/Standart Enemy", fileName = "New Enemy", order = 1)]

public class EnemyData : ScriptableObject
{
    

    [SerializeField]
    public float m_EnemyStartingHealth;
    [SerializeField]
    public int scoreValue = 10;

    [SerializeField]
    public GameObject[] targetPoints;

    [SerializeField]
    public Color color;
}
