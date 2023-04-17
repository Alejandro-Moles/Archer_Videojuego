using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    #region Variables
    [Header("Daño")]
    private int Damnage;

    [Header("Lanzamiento")]
    [SerializeField] private GameObject sumArrow;
    public bool flyArrow;
    Quaternion rotar;
    public float finalPower;

    [Header("Animaciones Enemigo")]
    [SerializeField] private Animator EnemyAnimator;
    #endregion

    #region Metodos Unity
    private void Start()
    {
        this.transform.localScale = new Vector3(1.5f,1.5f,1.5f);
        sumArrow = GameObject.FindGameObjectWithTag("arco");
        flyArrow = false;
        rotar = Quaternion.Euler(90,0,0);
    }

    private void Update()
    {
        if (flyArrow && finalPower <= 25)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, rotar, 0.5f * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<CapsuleCollider>().isTrigger = true;
            flyArrow = false;
            StartCoroutine(DestroyTime());
        }

        if(!collision.gameObject.CompareTag("arco"))
        {
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<CapsuleCollider>().isTrigger = true;
            flyArrow = false;
        }

    }
    #endregion

    #region Metodos Propios
    private void TakeArrow()
    {
        sumArrow.GetComponent<BowController>().numArow += 1;
        Destroy(gameObject);
    }
    #endregion

    #region Corrutinas
    private IEnumerator DestroyTime()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
    }
    #endregion
}
