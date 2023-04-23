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
    private float numEnemies = 3;
    private bool firstHit;
    private bool EnemigosMuertos;
    [SerializeField] private GameObject Enemigo_2;
    [SerializeField] private GameObject Enemigo_3;

    [Header("Final")]
    [SerializeField] private GameObject Final;
    [SerializeField] private GameObject RepetirTuto;
    [SerializeField] private GameObject TerminarTuto;
    [SerializeField] private Transform SpawnPoint_4;
    [SerializeField] private BoxCollider RepetirTutoColl;
    [SerializeField] private BoxCollider TerminarTutoColl;
    #endregion

    #region Metodos Unity
    private void Start()
    {
        Enemigo_2.SetActive(false);
        Enemigo_3.SetActive(false);

        RepetirTutoColl.enabled = false;
        TerminarTutoColl.enabled = false;
        RepetirTuto.SetActive(false);
        TerminarTuto.SetActive(false);

        Final.SetActive(false);
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
        if(!Player.GetComponent<Movimiento>().GetSetIsPaused) 
        {
            activarPuntosControl();
            EnemigosDone();
            AvanzarTexto();
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

    public void DisparoDone()
    {
        NextLine();
    }

    private void EnemigosDone()
    {
        if(numEnemies<= 0 && !EnemigosMuertos)
        {
            EnemigosMuertos = true;
            NextLine();
        }   
    }

    public void ActivateFinal()
    {
        ControlDisparo.SetActive(false);
        Prueba_3.SetActive(false);
        Final.SetActive(true);
        NextLine();

        Player.position = SpawnPoint_4.position;

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

        if (index == 12 && dialogueText.text == Lines[index])
        {
            Enemigo_2.SetActive(true);
            Enemigo_3.SetActive(true);
        }

        if (index == 13 && dialogueText.text == Lines[index])
        {
            ControlDisparo.SetActive(true);
        }

        if(index == 15 && dialogueText.text == Lines[index])
        {
            RepetirTuto.SetActive(true);
        }

        if (index == 16 && dialogueText.text == Lines[index])
        {
            TerminarTuto.SetActive(true);
            RepetirTutoColl.enabled= true;
            TerminarTutoColl.enabled= true;
        }
    }

    public void DoFirstHit()
    {
        if (!firstHit)
        {
            firstHit = true;
            NextLine();
        }
    }

    public void restEnemies()
    {
        numEnemies--;
    }

    private void AvanzarTexto()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (index == 3 || index == 7 || index == 10 || index == 12 || index == 13)
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
}
