using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchGround : MonoBehaviour
{
    [SerializeField] private Movimiento movimiento;

    private void OnTriggerStay(Collider other)
    {
        movimiento.GetSetIsGrounded = true;
    }

    private void OnTriggerExit(Collider other)
    {
        movimiento.GetSetIsGrounded = false;
    }
}
