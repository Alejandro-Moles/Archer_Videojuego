using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    #region Variables
    [Header("Player Life")]
    [SerializeField] private Player_Life playerLife;
    #endregion

    #region Metodos Unity
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerLife.GetDamage(20);
        }
    }
    #endregion
}
