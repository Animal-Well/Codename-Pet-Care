using UnityEngine;

public class ObjectiveCheck : MonoBehaviour
{
    void OnDestroy()
    {
        StageManager.Instance.GrowMinigameProgress();
    }
}
