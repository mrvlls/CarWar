using UnityEngine;

public class EnemyManager : MonoBehaviour
{       
    public float spawnTime;       
    public Transform[] spawnPoints;       
    private GameObject enemyPrefab;



    void Start()
    {
        
        InvokeRepeating("Spawn", spawnTime, spawnTime); //Вызываем повторяющуюся функцию спавна танков.

        InvokeRepeating("SpawnIncrease", 30, 30);  //Увеличиваем частоту спавнов каждые 30 секунд.

        enemyPrefab = Resources.Load<GameObject>("Prefabs/Enemy"); //Загружаем преваб танка врага из папки Resources.

    }


    void Spawn()
    {
        // Если танк игрока мертв...
        if (TankHealth.m_Dead)
        {
            // ... выходим из функции.
            return;
        }


        //Выбираем случайную точку спавна.
        int spawnPointIndex = Random.Range(0, spawnPoints.Length); 
        Instantiate(enemyPrefab, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
       

        while (true)
        {

            int targetPointIndex = Random.Range(0, spawnPoints.Length); //Выбираем случайную точку цели.

            if (Mathf.Abs(spawnPointIndex - targetPointIndex) >= 2) //Уточняем чтобы точка не была на стороне спавна.
            {
                EnemyMovement.GoToTarget(spawnPoints[targetPointIndex].transform);
                break;
            }
            continue;
        }


    }


    public void SpawnIncrease() //Уменьшаем время между спавнами.
    {
        if (spawnTime >= 1)
         spawnTime -= 0.5f;
    }
}
