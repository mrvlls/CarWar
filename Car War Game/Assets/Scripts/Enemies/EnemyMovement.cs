using UnityEngine;
using System.Collections;
using UnityEngine.AI;



public class EnemyMovement : MonoBehaviour
{      
    public static NavMeshAgent nav;              
    private Transform player;
    private EnemyHealth enemyHealth;
    private NavMeshAgent navFollowing;

    void Awake()
    {
        nav = GetComponent<NavMeshAgent>();  // Получаем компонент NavMeshAgent танка (статичная переменная).
        player = GameObject.FindWithTag("Player").transform;  //Получаем Transform игрока.
        enemyHealth = gameObject.GetComponent<EnemyHealth>();  //Получаем компонент EnemyHealth танка.
        navFollowing = GetComponent<NavMeshAgent>(); // Получаем компонент NavMeshAgent танка(нестатичная переменная).
    }

    private void Update()
    {
        if (enemyHealth.scoreValue == 30 && navFollowing != null)  //Проверяем танк на принадлежность к типу Hunter (по очкам) и его активность.
        {
            if (player != null) //Проверяем Transform игрока на активность.
            {
                navFollowing.SetDestination(player.position);  //Устанавливаем слежение танка з атанком игрока.
                navFollowing.speed = 5;  //Устанавливаем скорость движения танка.
            }
        }
    }

    public static void GoToTarget(Transform target)
    {

        nav.SetDestination(target.position);  // Устанавливаем путь танка.
    }

    public static void Stop()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy"); //Находим все танки врагов по тегу.

        foreach (GameObject nav in enemies)
        {
            nav.GetComponent<NavMeshAgent>().isStopped = true; // Отключаем движение через NavMeshAgent.
        }
    }
}



