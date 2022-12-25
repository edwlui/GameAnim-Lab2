using System;
using System.Collections;
using System.Collections.Generic;
using ShareefSoftware;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private LevelGeneration levelGeneration;
    [SerializeField] private Transform reference;
    [SerializeField] private float speed = 5f;
    private Boolean rotate = false;
    private GameObject center;

    private void Start()
    {
        center = new GameObject("Center");
        center.transform.position = new Vector3(levelGeneration.numRows * 4, 0, levelGeneration.numColumns * 4);
        transform.position = new Vector3(levelGeneration.numRows * 4, 16, -levelGeneration.numRows * 4);
    }

    public void RotateCamera()
    {
        rotate = !rotate;
    }

    private void Update()
    {
        if(rotate)
        {
            reference.transform.RotateAround(center.transform.position, Vector3.up, speed * Time.deltaTime);
        }
    }
}
