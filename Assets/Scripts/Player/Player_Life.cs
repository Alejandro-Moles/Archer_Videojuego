using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_Life : MonoBehaviour
{
    #region Variables
    [Header("Puntos vida")]
    [SerializeField] private int Vida = 100;

    [Header("Animaciones")]
    [SerializeField] private Animator anim;

    public int GetSetVida { get => Vida; set => Vida = value; }
    #endregion

    #region Metodos Unity
    private void Update()
    {
        if(Vida <= 0)
        {
            SceneManager.LoadScene("Menu");
        }
    }
    #endregion

    #region Metodos Propios
    public void GetDamage(int Dmg)
    {
        anim.SetTrigger("Damage");
        Vida -= Dmg;
    }
    #endregion
}
