using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // поле для таргета
    [SerializeField] Transform m_target;
    // свойство с сетером для таргета
    public Transform target { set { m_target = value; } }
    
    // добавление поля
    Transform m_transform;
    
    // отступ камеры от игрока
    [SerializeField] Vector3 offset;
    
    // высота точки слежения (чтобы камера смотрела персонажу выше пояса)
    [SerializeField] float pitch = 2f;

    // регулировка чувствительности колесика мыши
    [SerializeField] float zoomSpeed = 4f;
    // ограничение приближения камеры
    [SerializeField] float minZoom = 5f;
    // ограничение удаления камеры
    [SerializeField] float maxZoom = 15f;
    // текущее значение приближения
    float currentZoom = 10f;

    // текущий угол вращения камеры
    float currentRot = 90f;
    // предыдущее положение мыши для отслеживания ее перемещения за кадр
    float prevMouseX;


    // Start is called before the first frame update
    void Start()
    {
        m_transform = transform;
    }

    void LateUpdate()
    {
        if (m_target != null)
        { // проверка наличия таргета
          // перемещение камеры к игроку с отступом
            m_transform.position = m_target.position - offset * currentZoom;
            // поворот камеры на игрока
            m_transform.LookAt(m_target.position + Vector3.up * pitch);

            // применение вращения
            m_transform.RotateAround(m_target.position, Vector3.up, currentRot);
        }
        // обновляем предыдущее положение мыши
        prevMouseX = Input.mousePosition.x;
    }

    void Update()
    {
        if (m_target != null)
        {
            // изменение зума
            currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
            // ограничение зума
            currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);
            
            // нажатие на среднюю кнопку мыши
            if (Input.GetMouseButton(2))
            {
                // изменение угла поворота камеры
                currentRot += Input.mousePosition.x - prevMouseX;
            }

        }
    }
}
