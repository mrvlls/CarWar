using UnityEngine;

public class TankMovement : MonoBehaviour
{       
    public float m_Speed = 12f;            
    public float m_TurnSpeed = 180f;       
    public AudioSource m_MovementAudio;    
    private AudioClip m_EngineIdling;       
    private AudioClip m_EngineDriving;      
    public float m_PitchRange = 0.2f;
        
    private Rigidbody m_Rigidbody;         
    private float m_MovementInputValue;    
    private float m_TurnInputValue;        
    private float m_OriginalPitch;         


    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>(); //Получаем компонент Rigidbody
    }


    private void OnEnable ()
    {
        m_Rigidbody.isKinematic = false; // Делаем объект не кинематичным
        m_MovementInputValue = 0f; // Обнуляем значение движения
        m_TurnInputValue = 0f; //Обнуляем значение поворота
    }


    private void OnDisable ()
    {
        m_Rigidbody.isKinematic = true; //Делаем объект кинематичным
    }


    private void Start()
    {
        m_EngineIdling = Resources.Load<AudioClip>("Audio/EngineIdle"); // Загружаем звук мотора не в движении

        m_EngineDriving = Resources.Load<AudioClip>("Audio/EngineDriving");  // Загружаем звук мотора в движении

        m_OriginalPitch = m_MovementAudio.pitch; // Запоминаем начальную частоту звука (питч)
       
    }

    private void Update()
    {
        // Cохраняем ввод данных игрока и включаем звук.
        m_MovementInputValue = Input.GetAxis("Vertical");
        m_TurnInputValue = Input.GetAxis("Horizontal");

        EngineAudio();
    }


    private void EngineAudio()
    {
        // Включаем нужную звуковую дорожку в зависимости о того двигается ли танк и какая звуковая дорожка сейчас играет.
        if (Mathf.Abs(m_MovementInputValue) < 0.1f && Mathf.Abs(m_TurnInputValue) < 0.1f)
        {
            if (m_MovementAudio.clip == m_EngineDriving)
            {
                m_MovementAudio.clip = m_EngineIdling;
                m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
                m_MovementAudio.Play();
            }
        }
        else
        {
            if (m_MovementAudio.clip == m_EngineIdling)
            {
                m_MovementAudio.clip = m_EngineDriving;
                m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
                m_MovementAudio.Play();
            }
        }
    }


    private void FixedUpdate()
    {
        // Двигаем и поворачиваем танк.
        Move();
        Turn();
    }


    private void Move()
    {
        // Меняем позицию танка в зависимости от вводимых игроком данных.
        Vector3 movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime;
        m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
    }


    private void Turn()
    {
        // Меняем поворот танка в зависимости от вводимых игроком данных.
        float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;

        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
    }
}