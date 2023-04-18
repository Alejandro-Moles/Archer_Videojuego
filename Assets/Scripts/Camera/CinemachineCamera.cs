using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemachineCamera : MonoBehaviour
{
    #region Variables
    [SerializeField] private Vector3 Offset;
    [SerializeField] private Transform target;
    [Range(0,1)][SerializeField] private float lerpValue;
    [SerializeField] private float sensibilidad;
    #endregion

    #region Metodos Unity
    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, target.position + Offset, lerpValue);
        Offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * sensibilidad, Vector3.up) * Offset;

        transform.LookAt(target);
    }
    #endregion
}
