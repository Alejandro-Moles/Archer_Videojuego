using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara_Controller : MonoBehaviour
{
    #region Variables

    #region Movimiento del raton
    //variable que controla la sensibilidad
    [SerializeField] private float sensibilidad = 300f;
    //variables que controlan la entrada del raton
    private Vector2 entradaAngulos, angulos;

    //variables que ponen un limite a la camara
    [SerializeField] private Vector2 limitesCamara;
    #endregion

    #region Seguimiento del jugador
    [SerializeField] private Transform jugador;
    [SerializeField] private float velocidadAcercamiento;
    #endregion

    #endregion

    #region Metodos Unity
    private void FixedUpdate()
    {
        MovimientoRaton();
        SeguirJugador();
    }
    #endregion

    #region Metodos Propios
    private void MovimientoRaton()
    {
        entradaAngulos = new Vector2(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X")) * Time.deltaTime;

        angulos.x -= entradaAngulos.x * sensibilidad;
        angulos.y += entradaAngulos.y * sensibilidad;

        angulos.x = Mathf.Clamp(angulos.x, limitesCamara.x, limitesCamara.y);

        transform.localRotation = Quaternion.Euler(angulos.x, angulos.y, 0);
    }

    private void SeguirJugador()
    {
        transform.position = Vector3.Lerp(transform.position, jugador.position, velocidadAcercamiento * Time.deltaTime);
    }
    #endregion
}
