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
                vida -= 1;
                EnemyAnimator.SetTrigger("Damage");
                GetDamage();
        }
    }

    private void Update()
    {
        if (vida <= 0)
        {
            EnemyAnimator.SetTrigger("Dead");
            this.GetComponent<BoxCollider>().enabled = false;
            this.GetComponent<EnemyTutorial>().enabled = false;
            ui.restEnemies();
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
