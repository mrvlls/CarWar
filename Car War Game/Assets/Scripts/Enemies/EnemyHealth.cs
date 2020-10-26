using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{              
    private GameObject m_EnemyExplosionPrefab;                
    private AudioSource m_EnemyExplosionAudio;              
    private ParticleSystem m_EnemyExplosionParticles;       
    private float m_EnemyCurrentHealth;                                          
    private EnemyData[] enemies;

    public int scoreValue;
    public float m_EnemyStartingHealth;

    private void Awake()
    {
        //Загружаем нужные префабы из папки Resources.

        m_EnemyExplosionPrefab = Resources.Load<GameObject>("Prefabs/TankExplosion");

        enemies = Resources.LoadAll<EnemyData>("Enemies");

        //Создаем экземпляр префаба взрыва и получаем его компонент Particle System.

        m_EnemyExplosionParticles = Instantiate(m_EnemyExplosionPrefab).GetComponent<ParticleSystem>();

        //Получаем компонент AudioSource у созданного экхземпляра.

        m_EnemyExplosionAudio = m_EnemyExplosionParticles.GetComponent<AudioSource>();

        //Деактивируем экземпляр взрыва.

        m_EnemyExplosionParticles.gameObject.SetActive(false);
    }

    private void Start()
    {
        SetProperties();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Enemy")) //При столкновении с другим танком вражеский танк умирает.
        {
            OnDeathEnemy();
        }
        else if (other.CompareTag("Shell")) //При столкновении с зарядом уменьшаем здоровье.
        {
            m_EnemyCurrentHealth--;

            if (m_EnemyCurrentHealth <= 0)  //Проверяем количества здоровья, если меньше или равно нулю, танк умирает.
            {
                OnDeathEnemy();
                ScoreManager.score += scoreValue; // Прибавляем очки игроку за убийство танка.
            }
        }
    }

    public void OnDeathEnemy()
    {
        m_EnemyExplosionParticles.transform.position = transform.position;  // Перемещаем экземпляр взрыва на позицию умирающего танка.
        m_EnemyExplosionParticles.gameObject.SetActive(true);  // Активируем экземпляр взрыва.

        m_EnemyExplosionParticles.Play();  // Запускаем проигрывание экземпляра взрыва.

        m_EnemyExplosionAudio.Play();  // Запускаем проигрывание звука взрыва.

        m_EnemyCurrentHealth = m_EnemyStartingHealth;  //Обновляем здоровье танка, чтобы данные не перезаписались в Scriptable Object.

        gameObject.SetActive(false);  // Деактивируем танк.

        Destroy(m_EnemyExplosionParticles.gameObject, m_EnemyExplosionParticles.duration); //Уничтожаем экземпляр взрыва после того как проиграет система частиц.
        Destroy(gameObject);  // Уничтожаем экземпляр танка.
    }

    public void ChangeColor(Color enemycolor)
    {
        MeshRenderer[] renderers = gameObject.GetComponentsInChildren<MeshRenderer>(); // Собираем Mesh Renderer всех дочерних объектов экземпляра танка.

        for (int i = 0; i < renderers.Length; i++)  //Итерирцемся по Mesh Renderer.
        {
            renderers[i].material.color = enemycolor;  //Присваиваем нужный цвет материалу.
        }
    }

    public void SetProperties()
    {
        int enemyType = Random.Range(0, enemies.Length);   // Генерируем рандомное число в пределах количества экземпляров врагов.

        EnemyData enemy = enemies[enemyType];  // Сохраняем случайный экземпляр врага.
        m_EnemyStartingHealth = enemy.m_EnemyStartingHealth; // Устанавливаем танку начальное здоровье сохраненного экземпляра врага.
        m_EnemyCurrentHealth = m_EnemyStartingHealth;  
        scoreValue = enemy.scoreValue;  // Устанавливаем танку количество очков за его убийство из параметров экземпляра врага.


        ChangeColor(enemy.color);  // Меняем цвет танка на соответсвующий цвет экземпляра врага.
    }
}
