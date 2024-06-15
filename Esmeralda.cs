using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Esmeralda : MonoBehaviour
{
    public int valor = 8;
    public AudioClip SonidoMoneda;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.SumarPuntos(valor);
            Destroy(this.gameObject);
            AudioManager.Instance.ReproducirSonido(SonidoMoneda);
        }
    }
}
