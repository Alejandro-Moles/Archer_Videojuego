using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTutorial : MonoBehaviour
{
    #region Variables
    [Header("Enemigo de Prueba")]
    private Animator EnemyAnimator;
    private int vida = 3;
    
    [Header("UI")]
    [SerializeField] private UI ui;

    #endregion

    #region Metodos Unity
    private void Start()
    {
        EnemyAnimator = GetComponent<Animator>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("arco"))
        {
            if(vida == 0) 
            {

            }
            else
            {
                vida -= 1;
                EnemyAnimator.SetTrigger("Damage");
                GetDamage();
            }
            
        }
    }

    #endregion

    #region Metodos Propios
    private void GetDamage()
    {
       ui.DoFirstHit();
    }

    #endregion
}
