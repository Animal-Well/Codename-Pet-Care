using UnityEngine;

public class PlayButtonBehaviour : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.Instance.ChangeScene("Minigame Banho");
    }
}
