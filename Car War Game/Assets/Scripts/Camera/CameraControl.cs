using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public float m_DampTime = 0.2f;      // Приблизительное время передвижения камеры в нужную позицию.           
    public float m_ScreenEdgeBuffer = 4f;           // Граница экрана, за которую танки не выходят.
    public float m_MinSize = 6.5f;      // Миинимальный размер приближения.            


    private Transform target;
    private Camera m_Camera;                                              
    private Vector3 m_MoveVelocity;                              


    private void Awake()
    {
        m_Camera = GetComponentInChildren<Camera>();

    }

    private void Start()
    {
        Invoke("findPlayer", 0.5f);
    }


    private void FixedUpdate()
    {
        Move();
    }


    private void Move()
    {

        if (target != null)
        {
            transform.position = Vector3.SmoothDamp(transform.position, target.position, ref m_MoveVelocity, m_DampTime);
        }
    }


    private float FindRequiredSize()
    {
        Vector3 desiredLocalPos = transform.InverseTransformPoint(target.position);

        float size = 0f;

        if (target.gameObject.activeSelf)
        {
            Vector3 targetLocalPos = transform.InverseTransformPoint(target.position);

            Vector3 desiredPosToTarget = targetLocalPos - desiredLocalPos;

            size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.y));

            size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.x) / m_Camera.aspect);
        }

        size += m_ScreenEdgeBuffer;

        size = Mathf.Max(size, m_MinSize);

        return size;
    }


    public void SetStartPositionAndSize()
    {


        transform.position = target.position;

        m_Camera.orthographicSize = FindRequiredSize();
    }

    public void findPlayer()
    {
        target =  GameObject.FindWithTag("Player").transform;
    }
}