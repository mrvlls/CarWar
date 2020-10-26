using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TankHealth : MonoBehaviour
{
    public GameObject m_ExplosionPrefab;              
    private AudioSource m_ExplosionAudio;             
    private ParticleSystem m_ExplosionParticles;                    
    static public bool m_Dead;                      
    public Text message;

    private void Awake()
    {
        m_ExplosionPrefab = Resources.Load<GameObject>("Prefabs/TankExplosion"); //Загружаем префаб взрыва танка  

        message = GameObject.Find("GameOverText").GetComponent<Text>(); // Находим компонент текста окончания игры

        message.enabled = false; //Деактивируем объект текста окончания игры

        m_ExplosionParticles = Instantiate(m_ExplosionPrefab).GetComponent<ParticleSystem>(); //Инстанциируем экземпляр взрыва


        m_ExplosionAudio = m_ExplosionParticles.GetComponent<AudioSource>(); // Находим компонент Audio Source экземпляра взрыва

        m_ExplosionParticles.gameObject.SetActive(false); //Деактивируем экземпляр взрыва
    }


    private void OnEnable()
    {
        m_Dead = false;  
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Shell")) //Если танк сталкивается с врагом или снарядом, он погибает
        {
            OnDeath();
        }
    }

    public void OnDeath()
    {

        m_Dead = true; 


        m_ExplosionParticles.transform.position = transform.position; // Позицию экземпляра взрыва делаем равной позиции танка
        m_ExplosionParticles.gameObject.SetActive(true); //Активируем экземпляр взрыва


        m_ExplosionParticles.Play(); //Проигрываем взрыв


        m_ExplosionAudio.Play(); //Проигрываем звук взрыва


        gameObject.SetActive(false); //Деактивируем объект танка

        EnemyMovement.Stop(); //Останавливаем движение танков

        message.enabled = true; //Включаем текст Game Over

        if (PlayerPrefs.GetInt("maxScore") < ScoreManager.score)  //Меняем рекорд, если он побит
            PlayerPrefs.SetInt("maxScore", ScoreManager.score);

        Invoke("loadMenu", 3); //Загружаем меню через 3 секунды

        

    }

    private void loadMenu()  //Загрузка меню
    {
        SceneManager.LoadScene("Menu");

        Destroy(m_ExplosionParticles.gameObject, m_ExplosionParticles.duration);
        Destroy(gameObject);
    }
    
}