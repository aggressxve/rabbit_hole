using UnityEngine;
using UnityEngine.UI;
public class ContadorVidas : MonoBehaviour
{
    public Text textoVida;
    public int vidasActuales;
    public static ContadorVidas Instance;
    
    void Update()
    {
        vidasActuales = GameManager.instance.vidas;
        textoVida.text = $"Vidas: {vidasActuales}";
    }

}
