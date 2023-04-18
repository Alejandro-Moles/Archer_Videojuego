using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowCamera : MonoBehaviour
{
    #region Variables
    [Header("Jugador")]
    [SerializeField] private Transform playerTr;

    [Header("Camara")]
    [SerializeField] private Transform Cam;
    [SerializeField] private Transform cameraShoulder;
    [SerializeField] private Transform cameraHolder;

    [Header("Punto de Inicio")]
    [SerializeField] private Transform StartPoint;

    private float rotY = 0;

    private float rotationSpeed = 200f;
    #endregion


    #region Metodos Unity
    private void Start()
    {
        //gameObject.transform.LookAt(StartPoint.position);
    }

    private void Update()
    {
        CameraControll();
    }
    #endregion

    #region Metodos Propios
    private void CameraControll()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        float deltaT = Time.deltaTime;

        rotY += mouseY * rotationSpeed * deltaT;

        float rotX = mouseX * rotationSpeed * deltaT;

        playerTr.Rotate(0, rotX, 0);

    }
    #endregion
}
