using UnityEngine;
using Unity.Cinemachine;

public class CameraManager : MonoBehaviour
{
    public CinemachineVirtualCameraBase[] cameras;

    public CinemachineVirtualCameraBase sala;
    public CinemachineVirtualCameraBase quarto;
    public CinemachineVirtualCameraBase cozinha;
    public CinemachineVirtualCameraBase banheiro;

    public CinemachineVirtualCameraBase startCamera;
    private CinemachineVirtualCameraBase currentCam;

    private void Start()
    {
        currentCam = startCamera;

        SwitchCamera(sala);

        for (int i = 0; i < cameras.Length; i++)
        {
            if (cameras[i] == currentCam)
            {
                cameras[i].Priority = 20;
            }
            else
            {
                cameras[i].Priority = 10;
            }
                    
        }
    }
    public void SwitchCamera(CinemachineVirtualCameraBase newCam)
    {
        currentCam = newCam;

        currentCam.Priority = 20;

        for (int i = 0; i < cameras.Length; i++)
        {
            if (cameras[i] != currentCam)
            {
                cameras[i].Priority = 10;
            }
        }
    }
}