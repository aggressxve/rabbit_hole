using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
public class MenuPausa : MonoBehaviour
{
    public GameObject menuPausa;
    public bool juegoPausado = false;
    UIDocument ui;
    Button reanudarBtn;
    Button menuPrincipalBtn;

    void OnEnable()
    {
        ui = GetComponent<UIDocument>();
        reanudarBtn = ui.rootVisualElement.Q<Button>("Reanudar");
        menuPrincipalBtn = ui.rootVisualElement.Q<Button>("MenuPrincipal");

        reanudarBtn.clicked += OnReanudar;
        menuPrincipalBtn.clicked += OnMenuPrincipal;
        menuPausa = gameObject;
        Pausar();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            OnReanudar();
        }
    }



    void OnDisable()
    {
        reanudarBtn.clicked -= OnReanudar;
        menuPrincipalBtn.clicked -= OnMenuPrincipal;
    }

    void OnReanudar()
    {
        menuPausa.SetActive(false);
        Time.timeScale = 1;
        juegoPausado = false;
    }

    void OnMenuPrincipal()
    {
        menuPausa.SetActive(false);
        SceneManager.LoadScene(0);
    }

    public void Pausar()
    {
        menuPausa.SetActive(true);
        Time.timeScale = 0;
        juegoPausado = true;
    }

}
