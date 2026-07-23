using UnityEngine;
using UnityEngine.SceneManagement;
public class Door : MonoBehaviour
{
    public enum DoorType
    {
        StartGame,
        QuitGame 
    }
    public DoorType doorType;

    public string nextSceneName = "Tutorial";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (doorType == DoorType.StartGame)
            {
                SceneManager.LoadScene(nextSceneName);
            }
            else if (doorType == DoorType.QuitGame)
            {
                Application.Quit();
            }
        }
    }
}
