using UnityEngine;

public class PlayButtonBehaviour : MonoBehaviour
{
    public void StartBathGame()
    {
        GameManager.Instance.ChangeScene("Minigame Banho");
    }
    public void StartWalkGame()
    {
        GameManager.Instance.ChangeScene("RunGameplay");
    }
}
