using UnityEngine;
using UnityEngine.UIElements;

public class PersistentCanvas : MonoBehaviour
{
    UIDocument ui;
    public static PersistentCanvas instance;
    public int vidasActuales;
    public Label textoVida;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        ui = GetComponent<UIDocument>();
        textoVida = ui.rootVisualElement.Q<Label>("DisplayVidas");
    }

    void Update()
    {
        vidasActuales = GameManager.instance.vidas;
        textoVida.text = $"Vidas: {vidasActuales}";
    }
}
