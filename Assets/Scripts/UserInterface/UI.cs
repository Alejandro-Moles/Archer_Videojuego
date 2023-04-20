using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UI : MonoBehaviour
{
    #region Variables
    [Header("Jugador")]
    [SerializeField] private Transform Player;

    [Header("TextMesh Pro")]
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private string[] Lines;
    [SerializeField] private float TextSpeed = 0.1f;

    [Header("Pruebas del Movimiento")]
    [SerializeField] private GameObject Prueba_1;
    [SerializeField] private GameObject ControlMoverse;
    private int index;

    [Header("Pruebas del Salto")]
    [SerializeField] private GameObject Prueba_2;
    [SerializeField] private Transform SpawnPoint_2;
    [SerializeField] private GameObject ControlSalto;

    [Header("Pruebas del Disparo")]
    [SerializeField] private GameObject Prueba_3;
    [SerializeField] private GameObject ControlDisparo;
    [SerializeField] private Transform SpawnPoint_3;
    [SerializeField] private GameObject Enemigos;
    #endregion

    #region Metodos Unity
    private void Start()
    {
        Enemigos.SetActive(false);

        Prueba_1.SetActive(true);
        Prueba_2.SetActive(false);
        Prueba_3.SetActive(false);

        ControlMoverse.SetActive(false);
        ControlSalto.SetActive(false);
        ControlDisparo.SetActive(false);

        StartDialog();
    }

    private void Update()
    {
        activarPuntosControl();

        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(index == 3 || index == 7 || index == 10 || index == 12) 
            {
                StopAllCoroutines();
                dialogueText.text = Lines[index];
            }
            else
            {
                if (dialogueText.text == Lines[index])
                {
                    NextLine();
                }
                else
                {
                    StopAllCoroutines();
                    dialogueText.text = Lines[index];
                }
            } 
        }
    }
    #endregion

    #region Metodos Propios
    public void StartDialog()
    {
        index = 0;
        StartCoroutine(WriteLine());
    }

    private IEnumerator WriteLine()
    {
        dialogueText.text = "";
        foreach (char letter in Lines[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(TextSpeed); 
        }
        
    }

    private void NextLine()
    {
        if(index < Lines.Length - 1)
        {
            index++;
            StartCoroutine(WriteLine());
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public void MovimientoDone()
    {
        ControlMoverse.SetActive(false);
        Prueba_1.SetActive(false);
        Prueba_2.SetActive(true);
        NextLine();

        Player.position = SpawnPoint_2.position;
    }

    public void SaltoDone()
    {
        ControlSalto.SetActive(false);
        Prueba_2.SetActive(false);
        Prueba_3.SetActive(true);
        NextLine();

        Player.position = SpawnPoint_3.position;
    }

    private void activarPuntosControl()
    {
        if (index == 3 && dialogueText.text == Lines[index])
        {
            ControlMoverse.SetActive(true);
        }

        if (index == 7 && dialogueText.text == Lines[index])
        {
            ControlSalto.SetActive(true);
        }

        if (index == 10 && dialogueText.text == Lines[index])
        {
            Enemigos.SetActive(true);
        }
    }

    public void DoFirstHit()
    {
        NextLine();
    }
    #endregion
}
