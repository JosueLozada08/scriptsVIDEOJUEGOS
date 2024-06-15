using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int PuntosTotales { get { return puntosTotales; } }
    private int puntosTotales;

    public Canvas_Puntos hud;

    private int vidas = 4; // Número actual de vidas
    private int maxVidas = 4; // Número máximo de vidas permitido

    private void Awake() // Cambié 'awake' a 'Awake'
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Cuidado: ya existe una instancia de GameManager.");
            Destroy(gameObject);
        }
    }

    public void SumarPuntos(int puntosASumar)
    {
        puntosTotales += puntosASumar;
        hud.ActualizarPuntos(puntosTotales);
    }

    public void PerderVida()
    {
        if (vidas > 0)
        {
            vidas -= 1;
            hud.DesactivarVida(vidas);
            if (vidas == 0)
            {
                SceneManager.LoadScene(0);
            }
        }
    }

    public bool RecuperarVida()
    {
        if (vidas < maxVidas)
        {
            vidas += 1;
            hud.ActivarVida(vidas - 1); // Activar la vida recuperada en el HUD
            return true;
        }
        return false;
    }
}
