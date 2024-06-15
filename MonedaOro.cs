using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonedaOro : MonoBehaviour
{
    public int valor = 3;
    public AudioClip SonidoMoneda;

    void Start()
    {
        // Opcional: Puedes inicializar cosas aquí si es necesario
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("MonedaOro: OnTriggerEnter2D");

        if (collision.CompareTag("Player"))
        {
            Debug.Log("MonedaOro: Colisión con el jugador detectada");

            GameManager.Instance.SumarPuntos(valor);
            Destroy(gameObject);
            AudioManager.Instance.ReproducirSonido(SonidoMoneda);
        }
    }
}
