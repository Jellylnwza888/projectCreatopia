using UnityEngine;
using Cinemachine;

public class ZoomController : MonoBehaviour
{
    public CinemachineFreeLook freeLookCamera;
    public float zoomSpeed = 2f;
    public float minZoom = 2f;
    public float maxZoom = 10f;

    void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0f && freeLookCamera != null)
        {
            // ปรับทั้ง 3 rigs
            freeLookCamera.m_Orbits[0].m_Height -= scroll * zoomSpeed;
            freeLookCamera.m_Orbits[1].m_Height -= scroll * zoomSpeed;
            freeLookCamera.m_Orbits[2].m_Height -= scroll * zoomSpeed;

            freeLookCamera.m_Orbits[0].m_Radius -= scroll * zoomSpeed;
            freeLookCamera.m_Orbits[1].m_Radius -= scroll * zoomSpeed;
            freeLookCamera.m_Orbits[2].m_Radius -= scroll * zoomSpeed;

            // จำกัดระยะ
            for (int i = 0; i < 3; i++)
            {
                freeLookCamera.m_Orbits[i].m_Radius = Mathf.Clamp(freeLookCamera.m_Orbits[i].m_Radius, minZoom, maxZoom);
            }
        }
    }
}
