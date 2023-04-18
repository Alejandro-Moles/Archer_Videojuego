using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Life : MonoBehaviour
{
    #region Variables
    [Header("Puntos vida")]
    [SerializeField] private int Vida = 100;

    [Header("Animaciones")]
    [SerializeField] private Animator anim;
    #endregion

    #region Metodos Unity
    
    #endregion

    #region Metodos Propios
    public void GetDamage(int Dmg)
    {
        anim.SetTrigger("Damage");
        Vida -= Dmg;
    }
    #endregion
}
