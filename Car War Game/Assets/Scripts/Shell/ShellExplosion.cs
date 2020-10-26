using UnityEngine;

public class ShellExplosion : MonoBehaviour
{
    public LayerMask m_TankMask;
    public ParticleSystem m_ExplosionParticles;       
    public AudioSource m_ExplosionAudio;              
    public float m_MaxDamage = 100f;                  
    public float m_ExplosionForce = 1000f;            
    public float m_MaxLifeTime = 2f;                  
    public float m_ExplosionRadius = 5f;              


    private void Start()
    {
        Destroy(gameObject, m_MaxLifeTime); //Уничтожаем объект.
    }

    [System.Obsolete]
    private void OnTriggerEnter(Collider other)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, m_ExplosionRadius, m_TankMask);

        for (int i = 0; i < colliders.Length; i++)
        {
            Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>();

            if (!targetRigidbody)
                continue;

            targetRigidbody.AddExplosionForce(m_ExplosionForce, transform.position, m_ExplosionRadius); //Создаем врзрывную силу от взрыва.
        }

        m_ExplosionParticles.transform.parent = null; //Убираем родителя экземпляра взрыва.
        m_ExplosionParticles.Play(); //Проигрываем взрыв.
        m_ExplosionAudio.Play();  //Проигрываем аудио взрыва.
        Destroy(m_ExplosionParticles.gameObject, m_ExplosionParticles.duration); //Удаляем экземпяр взрыва после времени его проигрывания.
        Destroy(gameObject); //Удаляем объект.
    }
}