using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

/*
 * Helper class for switching the camera onto TV and vice versa.
 * Guidance from: Dan Pos, YouTube
 * https://www.youtube.com/watch?v=ZVrfNivw7PY
 *
 * - NK
 */

// Added static since it will not be added to an object, hence removal of 'MonoBehaviour'
public static class CameraSwitchHelper
{
    private static List<CinemachineVirtualCamera> _cameras = new List<CinemachineVirtualCamera>();
    private static int highPriority = 10, lowPriority = 0;

    private static CinemachineVirtualCamera activeCamera = null;

    public static bool LiveCamera(CinemachineVirtualCamera camera)
    {
        return camera == activeCamera;
    }

    public static void SwitchCamera(CinemachineVirtualCamera camera)
    {
        camera.Priority = highPriority; // Should maybe be hardcoded.
        activeCamera = camera;

        foreach (CinemachineVirtualCamera virtualCamera in _cameras)
        {
            if (virtualCamera != camera && virtualCamera.Priority != lowPriority)
            {
                virtualCamera.Priority = lowPriority;
            }
        }
    }

    public static void Register(CinemachineVirtualCamera camera)
    {
        _cameras.Add(camera);
    }
    
    public static void Unregister(CinemachineVirtualCamera camera)
    {
        _cameras.Remove(camera);
    }
}
