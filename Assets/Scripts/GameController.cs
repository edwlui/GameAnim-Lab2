using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private Lab2Controls lab2Controls;
    [SerializeField] private CameraController cameraController;

    public void Awake()
    {
        lab2Controls = new Lab2Controls();
    }
    private void OnEnable()
    {
        var _ = new QuitHandler(lab2Controls.Controls.Quit);
        var rotateCameraHandler = new CameraRotateHandler(lab2Controls.Controls.Cameras, this.cameraController);
    }
}
