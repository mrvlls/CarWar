using UnityEngine;
using UnityEngine.UI;

public class TankShooting : MonoBehaviour
{      
    public Rigidbody m_Shell;            // Снаряд
    public Transform m_FireTransform;    // Трансформ (невидимый) из которого вылетает снаряд
    public Slider m_AimSlider;           // Слайдер прицела (желтая стрелка во время выстрела)
    public AudioSource m_ShootingAudio;  // Источник звука выстрела
    public AudioClip m_ChargingClip;     // Звук заряда выстрела
    public AudioClip m_FireClip;         // Звук выстрела
    public float m_MinLaunchForce = 15f; 
    public float m_MaxLaunchForce = 30f; 
    public float m_MaxChargeTime = 0.75f;

    private string m_FireButton;         
    private float m_CurrentLaunchForce;  
    private float m_ChargeSpeed;         
    private bool m_Fired;

    public float m_StartingShells = 15f;             
    public Slider m_Slider;                     
    public Image m_FillImage;                         
    public Color m_FullShellsColor = Color.green;     
    public Color m_ZeroShellsColor = Color.red;                   

    public float m_CurrentShells;


    private void OnEnable()
    {
        m_CurrentLaunchForce = m_MinLaunchForce; // Сила выстрела равна минимальной (т.к. в данный момент мы не стреляем)
        m_AimSlider.value = m_MinLaunchForce; // Слайдер прицела на минимальном значении (его не видно)

        m_CurrentShells = m_StartingShells; // Кол-во снарядов равно начальному
        SetHealthUI(); // Обновляем слайдер кол-ва снарядов
    }


    private void Start()
    {
        m_ChargingClip = Resources.Load<AudioClip>("Audio/ShotCharging");  // Загружаем звук заряда выстрела

        m_FireClip = Resources.Load<AudioClip>("Audio/ShotFiring");  // Загружаем звук выстрела

        m_FireButton = "Fire";  // Кнопка выстрела

        m_ChargeSpeed = (m_MaxLaunchForce - m_MinLaunchForce) / m_MaxChargeTime; // Скорость заряда снараяда

        InvokeRepeating("AddShell", 5f, 5f); // Добавляем снаряд танку каждые 5 секунд
    }


    private void Update()
    {
        Aim();  // Прицел

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bonus"))  // Если сталкиваемся с бонусом увеличиваем кол-во снарядов до максимума
        {
            m_CurrentShells = 15;
            SetHealthUI();
            Destroy(other.gameObject);  // Уничтожаем объект бонуса после столкновения
        }
    }


    private void Fire()  // Выстрел
    {
        if (m_CurrentShells > 0)  // Проверяем что снарядов в запасе больше нуля
        {
            m_Fired = true;

            Rigidbody shellInstance = Instantiate(m_Shell, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;

            shellInstance.velocity = m_CurrentLaunchForce * m_FireTransform.forward;

            m_ShootingAudio.clip = m_FireClip;
            m_ShootingAudio.Play();

            m_CurrentLaunchForce = m_MinLaunchForce;

            Shoot();
        }
    }

    private void Shoot()  // Уменьшаем кол-во снарядов и обновляем слайдер снарядов
    {

        m_CurrentShells--;  


        SetHealthUI();
    }


    private void SetHealthUI()  // Обновление слайдера снарядов
    {

        m_Slider.value = m_CurrentShells;


        m_FillImage.color = Color.Lerp(m_ZeroShellsColor, m_FullShellsColor, m_CurrentShells / m_StartingShells);
    }

    private void AddShell()  // Добавление снаряда танку, если их меньше максимума
    {
        if (m_CurrentShells < 15)
        {
            m_CurrentShells++;
            SetHealthUI();
        }
    }

    private void Aim()  // Момент прицела и выстрела
    {
        m_AimSlider.value = m_MinLaunchForce;

        if (m_CurrentLaunchForce >= m_MaxLaunchForce && !m_Fired)
        {
            m_CurrentLaunchForce = m_MaxLaunchForce;
            Fire();
        }
        else if (Input.GetButtonDown(m_FireButton))
        {
            m_Fired = false;
            m_CurrentLaunchForce = m_MinLaunchForce;

            m_ShootingAudio.clip = m_ChargingClip;
            m_ShootingAudio.Play();
        }
        else if (Input.GetButton(m_FireButton) && !m_Fired)
        {
            m_CurrentLaunchForce += m_ChargeSpeed * Time.deltaTime;

            m_AimSlider.value = m_CurrentLaunchForce;
        }
        else if (Input.GetButtonUp(m_FireButton) && !m_Fired)
        {
            Fire();
        }
    }
}