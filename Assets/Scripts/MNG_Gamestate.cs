using UnityEngine;

public class MNG_Gamestate : MonoBehaviour
{
    [SerializeField] private GameObject _PauseMenu;

    private bool isPaused = false;
    private bool isPressed = false;
    private float input;

    [HideInInspector] public static MNG_Gamestate Instance;

    void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Update()
    {
        input = Input.GetAxisRaw("Cancel");
        if (input > 0 && !isPressed)
        {
            isPressed = true;
            if (!isPaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
        else if (input == 0)
        {
            isPressed = false;
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
        _PauseMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        _PauseMenu.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}