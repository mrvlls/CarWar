using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretMoving : MonoBehaviour
{
    private GameObject cursor;
    public Transform turret;
    private Vector3 cur;

    private void Start()
    {
        cursor = new GameObject();
    }

    void Update()
    {
        TurretRotation();
    }

    void TurretRotation()
    {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (!hit.collider.gameObject.CompareTag("Player")) // Убеждаемся что башня не может смотреть внутрь танка
            {
                cur = new Vector3(hit.point.x, 1, hit.point.z);
                cursor.transform.position = cur;
                turret.LookAt(cursor.transform);

            }
        }
    }
}
