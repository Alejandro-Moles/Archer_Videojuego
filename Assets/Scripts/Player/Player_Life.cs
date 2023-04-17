using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Life : MonoBehaviour
{
    #region Variables
    [Header("Puntos vida")]
    [SerializeField] private int Vida;

    [Header("Animaciones")]
    [SerializeField] private Animator anim;
    #endregion

    #region Metodos Unity
    
    #endregion

    #region Metodos Propios
    public void GetDamage()
    {
        anim.SetTrigger("Damage");
    }


    #endregion
}
