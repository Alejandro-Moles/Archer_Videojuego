using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowController : MonoBehaviour
{
    #region Variables
    [Header("Arco")]
    [SerializeField] private Transform ShootPoint;

    [Header("Flechas")]
    //GameObjects
    [SerializeField] private GameObject Arrow;
    [SerializeField] private GameObject ArrowPreFab;

    //numero de flechas
    public int numArow;

    //Transform
    [SerializeField] private Transform respawnArrow;
    [SerializeField] private Transform flechaCargada;

    //Fuerza al lanzar la flecha
    private float power;
    [SerializeField] private float fuerza;
    float movimiento;

    [Header("Animator")]
    [SerializeField] private Animator PlayerAnimator;
    #endregion

    #region Metodos Unity
    private void Update()
    {
        movimiento = 0.5f + fuerza;

        RecoilArrow();
    }
    #endregion

    #region Metodos Propios
    public void ShootArrow()
    {
       if(Input.GetMouseButton(0) && Arrow != null)
       {
            Debug.Log("Cargo");
            power += 0.2f + fuerza;
            power = Mathf.Clamp(power, 0, 30);

            Arrow.GetComponent<ArrowController>().finalPower= power;
            Arrow.transform.position = Vector3.Lerp(Arrow.transform.position, flechaCargada.position, movimiento * Time.deltaTime);
       }

       if(Input.GetMouseButtonUp(0) && Arrow != null)
        {
            Debug.Log("Disparo");
            PlayerAnimator.SetTrigger("Disparo");
            Arrow.GetComponent<Rigidbody>().velocity = transform.forward * power;
            Arrow.GetComponent<Rigidbody>().isKinematic= false;
            Arrow.GetComponent<ArrowController>().flyArrow= true;
            Arrow.transform.SetParent(null);
            Arrow = null;
            power = 0;
            numArow -= 1;
        }
    }

    public void RecoilArrow()
    {
        if (numArow > 0 && Arrow == null)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                PlayerAnimator.SetTrigger("Recoil");
                Arrow = Instantiate(ArrowPreFab, respawnArrow.position, respawnArrow.rotation);
                Arrow.transform.SetParent(respawnArrow);
            }
        }
    }
    #endregion
}
