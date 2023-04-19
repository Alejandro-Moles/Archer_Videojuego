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

    [Header("Puntos de control")]
    //Moverse
    [SerializeField] private GameObject Prueba_1;
    [SerializeField] private GameObject ControlMoverse;
    private int index;

    //Saltar
    [SerializeField] private GameObject Prueba_2;
    [SerializeField] private Transform SpawnPoint_2;
    #endregion

    #region Metodos Unity
    private void Start()
    {
        Prueba_1.SetActive(true);
        Prueba_2.SetActive(false);
        ControlMoverse.SetActive(false);
        StartDialog();
    }

    private void Update()
    {
        activarPuntosControl();

        if(Input.GetKeyDown(KeyCode.Q))
        {
            if(index == 3) 
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

    private void activarPuntosControl()
    {
       if(index == 3 && dialogueText.text == Lines[index])
       {
            ControlMoverse.SetActive(true);
       }
    }
    #endregion
}
