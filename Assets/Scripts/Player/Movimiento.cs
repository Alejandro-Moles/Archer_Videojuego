
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movimiento : MonoBehaviour
{
    #region Variables
    [SerializeField] private Animator animator;
    [SerializeField] private Transform CamaraTransform;

    private float referenciaSmooth;
    private float smoothAngulo = 7f;
    private bool isPaused;

    [Header("Movimiento")]
    [SerializeField] private float speedMove = 5f;
    private Vector3 direccion, InputDireccion;
    [SerializeField] float x, y;

    [Header("Correr")]
    [SerializeField] private float velocidadCorrer = 8f;
    private bool isRunning;


    [Header("Salto")]
    private Rigidbody rb;
    [SerializeField] private float JumpForce = 6f;
    private bool isGrounded;


    [Header("Ataque de Arco")]
    private BowAttack bowAttack;
    private float speedAim = 3f;

    [Header("Tutorial")]
    [SerializeField] private UI userInterface;

    [Header("Menu")]
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject TextoArcher;
    [SerializeField] private GameObject Marco;
    [SerializeField] private GameObject Personaje_Panel;
    [SerializeField] private TextMeshProUGUI Flechas_text;

    [Header("Bow Controller")]
    [SerializeField] private BowController bowController;

    [Header("Vida")]
    [SerializeField] private Player_Life playerLife;
    [SerializeField] private TextMeshProUGUI Vida_text;
    #endregion

    #region Metodos Unity
    private void Start()
    {
        Cursor.lockState =  CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        StartCoroutine(doIdleAnimation());
        bowAttack = GetComponent<BowAttack>();
    }
    private void FixedUpdate()
    {
        if (!bowAttack.GetSetIsAiming)
        {
            MovimientoJugador();
        }
        else
        {
            MovimientoAiming();
        }
    }

    private void Update()
    {
        Menu();
        ArrowsNum();
        PlayerLife();

        if (!isPaused)
        {
            Jump();
            IsRunning();
            animator.SetBool("IsGrounded", isGrounded);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ControlMover"))
        {
            userInterface.MovimientoDone();
        }

        if (other.gameObject.CompareTag("ControlSaltar"))
        {
            userInterface.SaltoDone();
        }

        if (other.gameObject.CompareTag("Final"))
        {
            userInterface.ActivateFinal();
        }

        if (other.gameObject.CompareTag("FinTuto"))
        {
            SceneManager.LoadScene("Game");
        }

        if (other.gameObject.CompareTag("Reinicio"))
        {
            SceneManager.LoadScene("Tutorial");
        }

    }
    #endregion

    #region Metodos Propios
    private void MovimientoJugador()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        animator.SetFloat("velX", x);
        animator.SetFloat("velY", y);

        direccion = new Vector3(x, 0f, y).normalized;
        InputDireccion = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));


        if(direccion.magnitude > 0.1f ) 
        {
            animator.SetTrigger("StopIdle");

            if (isRunning)
            {
                animator.SetBool("IsRunning", true);
                transform.Translate(0, 0, 0.5f * Time.deltaTime * velocidadCorrer);
            }
            else
            {
                animator.SetBool("IsRunning", false);
                transform.Translate(0, 0, 0.5f * Time.deltaTime * speedMove);
            }

            
            float medirAngulo = Mathf.Atan2(direccion.x, direccion.z) * Mathf.Rad2Deg + CamaraTransform.eulerAngles.y;

            float angulo = Mathf.SmoothDampAngle(transform.eulerAngles.y, medirAngulo, ref referenciaSmooth, smoothAngulo * Time.deltaTime);

            transform.rotation = Quaternion.Euler(0f, angulo, 0f);


        }
    }

    private void IsRunning()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
    }

    private void Jump()
    {
        if (isGrounded)
        {
            if(Input.GetKeyUp(KeyCode.Space)) 
            {
                animator.SetTrigger("jump");
                rb.AddForce(new Vector3(0, JumpForce, 0), ForceMode.Impulse);
            }
        }
    }

    private void MovimientoAiming()
    {
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        direccion = new Vector3(x, 0f, y).normalized;
        InputDireccion = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        if (direccion.magnitude > 0.1f)
        {
            animator.SetBool("AimingWalk", true);
            animator.SetInteger("Horizontal", (int)x);
            animator.SetInteger("Vertical", (int)y);

            Vector3 Move = transform.right * x + transform.forward * y;

            rb.MovePosition(transform.position + Move * speedAim * Time.deltaTime);
        }
        else
        {
            animator.SetBool("AimingWalk", false);
        }
        
    }
    #endregion

    #region Corrutinas
    private IEnumerator doIdleAnimation()
    {
        yield return new WaitForSeconds(4);

        if (direccion.magnitude <= 0f)
        {
            int animacionIdle = Random.Range(0, 3);

            switch (animacionIdle)
            {
                case 0:
                    animator.SetTrigger("Idle");
                    break;
                case 1:
                    animator.SetTrigger("Idle_2");
                    break;
                case 2:
                    animator.SetTrigger("Idle_3");
                    break;
            }
        }

        StartCoroutine(doIdleAnimation());
    }


    private void Menu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
            {
                isPaused= false;
            }
            else
            {
                isPaused= true;
            }
        }

        if (isPaused)
        {
            menu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale= 0f;
        }
        else
        {
            Time.timeScale = 1f;
            menu.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Personaje()
    {
        TextoArcher.SetActive(false);
        Marco.SetActive(false);
        Personaje_Panel.SetActive(true);
    }

    public void Back()
    {
        TextoArcher.SetActive(true);
        Marco.SetActive(true);
        Personaje_Panel.SetActive(false);
    }


    private void ArrowsNum()
    {
        Flechas_text.text = bowController.numArow.ToString();
    }

    private void PlayerLife()
    {
        Vida_text.text = playerLife.GetSetVida + "/300";
    }
    #endregion

    #region Metodos Get/Set
    public bool GetSetIsGrounded { get => isGrounded; set => isGrounded = value; }
    public bool GetSetIsPaused { get => isPaused; set => isPaused = value; }
    #endregion
}
