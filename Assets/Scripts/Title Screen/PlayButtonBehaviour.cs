using UnityEngine;
using UnityEngine.UI;

public class PlayButtonBehaviour : MonoBehaviour
{
    private GameManager Manager;
    private Button _button;
    void Start()
    {
        Manager = GameManager.Instance;
        _button = GetComponent<Button>();
    }
    public void StartGame()
    {
        Manager.ChangeScene("Minigame Banho");
    }
}
