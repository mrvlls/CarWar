using System;
using UnityEngine;

[Serializable]
public class TankManager : MonoBehaviour
{
    private GameObject player;            
    public Transform m_SpawnPoint;         


    public void Start()
    {
        player = Resources.Load<GameObject>("Prefabs/Tank"); //Загружаем префаб танка игрока из папки Resources.
        Instantiate(player, m_SpawnPoint); //Создаем экземпляр танка игрока в точке спавна.
    }
}
