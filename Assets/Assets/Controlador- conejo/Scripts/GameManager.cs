using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int vidas = 5;

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
    }

    public void PerderVida()
    {
        vidas--;
        Debug.Log("Vidas: " + vidas);
        if(vidas <= 0){
            GameOver();
        }
    }

    public void GameOver(){
        SceneManager.LoadScene("GameOver");
    }
}
