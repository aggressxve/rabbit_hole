using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameOverScreen : MonoBehaviour
{
    public VisualElement ui;
    public Button menuPrincipalBtn;

    void Awake()
    {
        ui = GetComponent<UIDocument>().rootVisualElement;
    }

    void OnEnable()
    {
        menuPrincipalBtn = ui.Q<Button>("Inicio");
        menuPrincipalBtn.clicked += onMenuPrincipalBtnClicked;
    }

    private void onMenuPrincipalBtnClicked() {
        SceneManager.LoadScene(0);
    }
}