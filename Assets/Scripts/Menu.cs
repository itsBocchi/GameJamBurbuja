using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void StartNewGame()
    {
        SceneManager.LoadScene("Test2");
    }
}
