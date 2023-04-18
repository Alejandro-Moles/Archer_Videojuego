using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy_Controller : MonoBehaviour
{
    #region Variables
    [Header("Animaciones")]
    private Animator EnemyAnimator;

    [Header("AI Enemigo")]
    [SerializeField] private Transform Player;
    [SerializeField] private float Velocidad;
    [SerializeField] private NavMeshAgent AI;
    //Distancias
    [SerializeField] private float DistanciaWalk;
    private float DistaciaAttack = 1.7f;
    private float Distancia;


    [Header("Ataque del Enemigo")]
    [SerializeField] private GameObject AttackPoint;
    #endregion

    #region Metodos Unity
    private void Start()
    {
        EnemyAnimator = GetComponent<Animator>();   
    }

    private void Update()
    {
        Distancia = Vector3.Distance(gameObject.transform.position, Player.position);
        ComprobarDistancias();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("arco"))
        {
            EnemyAnimator.SetTrigger("Damage");
        }
    }
    #endregion

    #region Metodos Propios
    private void ComprobarDistancias()
    {
        if(Distancia <= DistanciaWalk && Distancia > DistaciaAttack)
        {
            gameObject.transform.LookAt(Player.position);
            EnemyAnimator.SetBool("Walk", true);
            AI.speed = Velocidad;
            AI.SetDestination(Player.position);

            AttackPoint.SetActive(false);
        }
        else if (Distancia <= DistaciaAttack)
        {
            EnemyAnimator.SetBool("Walk", false);
            AI.speed = 0;
            AI.acceleration = 0;
            DoAttack();

            AttackPoint.SetActive(true);
        }
    }


    private void DoAttack()
    {
        int numAttack = Random.Range(0, 4);

        switch (numAttack) 
        {
            case 0:
                EnemyAnimator.SetTrigger("Attack");
                break;
            case 1:
                EnemyAnimator.SetTrigger("Attack");
                break;
            case 2:
                EnemyAnimator.SetTrigger("Attack");
                break;
            case 3:
                EnemyAnimator.SetTrigger("Attack_02");        
                break;
        }

    }

    #endregion
}
