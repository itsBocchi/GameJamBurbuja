using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject _VictoryScreen;
    [SerializeField] private bool _Win = false;

    public void ChangeLevel()
    {
        SceneManager.LoadScene("Test2");
    }

    public void Win()
    {
        _VictoryScreen.SetActive(true);
        Destroy(MNG_Gamestate.Instance.gameObject);
        Time.timeScale = 0f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (_Win)
            {
                Win();
            }
            else
            {
                ChangeLevel();
            }
        }
    }
}
