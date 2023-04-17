using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowController : MonoBehaviour
{
    #region Variables
    [Header("Arco")]
    [SerializeField] private Transform ShootPoint;

    [Header("Flechas")]
    [SerializeField] private GameObject Arrow;
    [SerializeField] private GameObject ArrowPreFab;
    public int numArow;

    [SerializeField] private Transform respawnArrow;
    [SerializeField] private Transform flechaCargada;

    private float power;
    [SerializeField] private float fuerza;
    float movimiento;

    [Header("Animator")]
    [SerializeField] private Animator PlayerAnimator;

    [Header("Bow Attack")]
    [SerializeField] private BowAttack bowAttack;

    [Header("Mira")]
    [SerializeField] private Transform sight;
    #endregion

    #region Metodos Unity
    private void Update()
    {
        movimiento = 0.7f + fuerza;

        RecoilArrow();
    }
    #endregion

    #region Metodos Propios
    public void ShootArrow()
    {
       if(Input.GetMouseButton(0) && Arrow != null)
       {
            power += 0.2f + fuerza;
            power = Mathf.Clamp(power, 0, 30);

            Arrow.GetComponent<ArrowController>().finalPower= power;
            Arrow.transform.position = Vector3.Lerp(Arrow.transform.position, flechaCargada.position, movimiento * Time.deltaTime);
       }

       if(Input.GetMouseButtonUp(0) && Arrow != null)
        {
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
        if (numArow > 0 && Arrow == null && bowAttack.GetSetIsAiming)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                PlayerAnimator.SetTrigger("Recoil");
                Arrow = Instantiate(ArrowPreFab, respawnArrow.position, respawnArrow.rotation);
                Arrow.transform.SetParent(respawnArrow);
            }
        }
    }

    public void Recoil()
    {
        if (numArow > 0 && Arrow == null && bowAttack.GetSetIsAiming)
        {
            PlayerAnimator.SetTrigger("Recoil");
            Arrow = Instantiate(ArrowPreFab, respawnArrow.position, respawnArrow.rotation);
            Arrow.transform.SetParent(respawnArrow);
        }
    }

    public void DrawSight(Transform camera)
    {
        RaycastHit hit;

        if(Physics.Raycast(camera.position, camera.forward, out hit))
        {
            ShootPoint.LookAt(hit.point);
        }
        else
        {
            Vector3 end = camera.position+ camera.forward;
            ShootPoint.LookAt(end);
        }
    }
    #endregion
}
