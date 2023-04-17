using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowAttack : MonoBehaviour
{
    #region Variables
    [Header("Animaciones")]
    private Animator animator;
    private bool isAiming;

    [Header("Camaras")]
    [SerializeField] private GameObject MainCam;
    [SerializeField] private GameObject BowCam;

    [Header("Mirilla")]
    [SerializeField] private GameObject Mirilla;

    [Header("Disparo")]
    [SerializeField] private BowController Bow;
    

    #endregion

    #region Metodos Unity
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Aim();
        AimAnimation();
        ShootArrow();
    }
    #endregion

    #region Metodos Propios
    private void Aim()
    {
        if (Input.GetKey(KeyCode.Mouse1))
        {
            Bow.DrawSight(BowCam.transform);
            isAiming = true;
            MainCam.SetActive(false);
            BowCam.SetActive(true);
            Mirilla.SetActive(true);
        }
        else
        {
            isAiming = false;
            MainCam.SetActive(true);
            BowCam.SetActive(false);
            Mirilla.SetActive(false);
        }
    }

    private void AimAnimation()
    {
        if (isAiming)
        {
            animator.SetBool("Aim", true);
        }
        else
        {
            animator.SetBool("Aim", false);
        }
    }

    private void ShootArrow()
    {
        if(isAiming) 
        {
            Bow.ShootArrow();
        }
    }
    #endregion

    #region Metodos Get/Set
    public bool GetSetIsAiming { get => isAiming; set => isAiming = value; }
    #endregion
}
