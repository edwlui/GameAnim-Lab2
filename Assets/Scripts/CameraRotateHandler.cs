using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRotateHandler : MonoBehaviour
{
    private CameraController cameraController;
    public CameraRotateHandler(InputAction rotateCameraAction, CameraController cameraController)
    {
        rotateCameraAction.performed += RotateCameraAction_Performed;
        rotateCameraAction.Enable();
        this.cameraController = cameraController;
    }
    private void RotateCameraAction_Performed(InputAction.CallbackContext obj)
    {
        cameraController.RotateCamera();
    }
}
