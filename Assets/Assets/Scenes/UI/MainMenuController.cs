using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuController : MonoBehaviour
{

    public VisualElement ui;
    public Button playBtn;
    public Button soundBtn;
    public Button quitBtn;
    
    void Awake()
    {
        ui = GetComponent<UIDocument>().rootVisualElement;
    }

    private void OnEnable()
    {
        playBtn = ui.Q<Button>("IniciarBtn");
        playBtn.clicked += OnPlayBtnClicked;

        soundBtn = ui.Q<Button>("SonidoBtn");
        soundBtn.clicked += OnSoundBtnClicked;

        quitBtn = ui.Q<Button>("SalirBtn");
        quitBtn.clicked += OnQuitBtnClicked;
    }

    private void OnQuitBtnClicked(){
        Application.Quit();
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #endif
    }

    private void OnSoundBtnClicked(){
        Debug.Log("Sound Options");
    }

    private void OnPlayBtnClicked(){
        gameObject.SetActive(false);
    }

}
