using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusManager : MonoBehaviour
{
    private float spawnTime;            // Как часто запускаюся бонусы.
    private GameObject[] spawnPoints;
    private GameObject prefab;
    private TankShooting shells;


    void Start()
    {
        prefab = Resources.Load<GameObject>("Prefabs/ShellBonus"); //Загружаем префаб бонуса из папки Resources.
        spawnPoints = GameObject.FindGameObjectsWithTag("Bonus"); //Находим точки спавна бонусов. 
        InvokeRepeating("Bonus", 30, 30); // Запускаем бонусы каждые 30 секунд.
        Invoke("FindPlayer", 1); // Находим танк игрока. 
    }

    public void Bonus()
    {
        if (shells.m_CurrentShells < 15f) //Проверяем что снарядов меньше 15.
        {
            int spawnPointIndex = Random.Range(0, spawnPoints.Length); // Генерируем рандомное число в пределах количества точек спавна.
            Instantiate(prefab, spawnPoints[spawnPointIndex].transform.position, spawnPoints[spawnPointIndex].transform.rotation); //Создаем экземпляр бонуса.
        }
    }

    public void FindPlayer()
    {
        shells = GameObject.FindWithTag("Player").GetComponent<TankShooting>(); // Находим танк игрока по тегу и его компонент TankShooting.
    }
}
