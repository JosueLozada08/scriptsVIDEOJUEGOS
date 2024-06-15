using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jugador : MonoBehaviour
{
    public float velocidad = 5f;
    public float fuerzaSalto = 2f;
    public LayerMask capaSuelo;
    public float MaxSaltos;
    private Rigidbody2D rb;
    private bool puedeSaltar = true;
    private bool derecha = true;
    private BoxCollider2D boxCollider;
    private Animator animator;
    private float RestSaltos;

    private Vector2 posicionInicio;

   // public AudioManager audioManager;
    public AudioClip sonidoSalto;


    //interaccion con enemigo 
    public float fuerzaGolpe;
    private bool puedeMoverse = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        RestSaltos = MaxSaltos;
        animator = GetComponent<Animator>();
        posicionInicio = new Vector2(-3.65f, -5.41f);
        transform.position = posicionInicio;
    }

    void Update()
    {
        // Movimiento lateral
        float movimiento = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(movimiento * velocidad, rb.velocity.y);

        // Animación de movimiento
        animator.SetBool("Running", movimiento != 0f);

        // Orientación del jugador
        Orientacion(movimiento);

        // Función de salto
        salto();

        // Verificar posición de transición
        VerificarPosicionTransicion();
    }

    void Orientacion(float movimiento)
    {
        Vector3 escalaActual = transform.localScale;
        if (movimiento < 0 && escalaActual.x > 0)
        {
            escalaActual.x = -Mathf.Abs(escalaActual.x);
        }
        else if (movimiento > 0 && escalaActual.x < 0)
        {
            escalaActual.x = Mathf.Abs(escalaActual.x);
        }
        transform.localScale = escalaActual;
    }

    bool TocaSuelo()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, new Vector2(boxCollider.bounds.size.x, boxCollider.bounds.size.y), 0f, Vector2.down, 0.2f, capaSuelo);
        return raycastHit.collider != null;
    }

    void salto()
    {
        if (TocaSuelo())
        {
            RestSaltos = MaxSaltos;
        }
        if (Input.GetKeyDown(KeyCode.Space) && RestSaltos > 0)
        {
            RestSaltos--;
            rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
            AudioManager.Instance.ReproducirSonido(sonidoSalto);
        }
    }

    void VerificarPosicionTransicion()
    {
        if (transform.position.y <= -7)
        {
            transform.position = posicionInicio;
        }
    }


    public void AplicarGolpe()
    {
        puedeMoverse = false;
        Vector2 direccionGolpe;

        if (rb.velocity.x > 0)
        {
            direccionGolpe = new Vector2(-1, 1);
        }
        else
        {
            direccionGolpe = new Vector2(1, 1);
        }

        rb.AddForce(direccionGolpe * fuerzaGolpe, ForceMode2D.Impulse);
        StartCoroutine(EsperarYActivarMovimiento());
    }
    IEnumerator EsperarYActivarMovimiento()
    {
        // Esperamos antes de comprobar si esta en el suelo.
        yield return new WaitForSeconds(0.1f);

        while (!TocaSuelo())
        {
            // Esperamos al siguiente frame.
            yield return null;
        }

        // Si ya está en suelo activamos el movimiento.
        puedeMoverse = true;
    }
}
