using UnityEngine;
using UnityEngine.SceneManagement;
public class Door : MonoBehaviour
{
    public enum DoorType
    {
        StartGame, // 게임 시작 (씬 이동)
        QuitGame   // 게임 종료
    }

    [Header("문 종류 선택")]
    public DoorType doorType;

    [Header("이동할 씬 이름 (StartGame일 때만)")]
    public string nextSceneName = "Tutorial"; // 이동할 튜토리얼 씬 이름 작성

    // 플레이어가 문(Is Trigger가 체크된 Collider)에 들어왔을 때 실행
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 들어온 오브젝트의 태그가 "Player"인지 확인
        if (collision.CompareTag("Player"))
        {
            if (doorType == DoorType.StartGame)
            {
                // 지정한 씬으로 이동
                SceneManager.LoadScene(nextSceneName);
            }
            else if (doorType == DoorType.QuitGame)
            {
                // 실제 빌드된 게임 종료
                Application.Quit();
            }
        }
    }
}
