
using System.Collections;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    #region Variables
    [SerializeField] private Animator animator;
    [SerializeField] private Transform CamaraTransform;

    private float referenciaSmooth;
    private float smoothAngulo = 20f;
    

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
    #endregion

    #region Metodos Unity
    private void Start()
    {
        Cursor.lockState = (Input.GetKey(KeyCode.Escape) ? CursorLockMode.None : CursorLockMode.Locked);
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
        Jump();
    }

    private void Update()
    {
        IsRunning();
        animator.SetBool("IsGrounded", isGrounded);
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
    #endregion

    #region Metodos Get/Set
    public bool GetSetIsGrounded { get => isGrounded; set => isGrounded = value; }
    #endregion
}
