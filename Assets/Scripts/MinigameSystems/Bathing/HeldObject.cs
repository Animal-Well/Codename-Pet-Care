using System.Collections;
using UnityEngine;

public class HeldObject : MonoBehaviour
{
    public GameObject[] holdables;
    public GameObject CurrentHeldObject { get; private set; }
    private ProgressBehaviour _progress;

    private void OnEnable()
    {
        StartCoroutine(CheckHeldObject());
    }

    private void ResetHeldObject()
    {
        Destroy(CurrentHeldObject);
    }
    private void NextHeldObject()
    {
        for (int i = 0; i < holdables.Length; i++)
        {
            if (i == _progress.GetRawProgress())
            {
                CurrentHeldObject = Instantiate(holdables[i]);
                break;
            }
        }
    }
    public void MoveHeldObject(Vector3 toPos)
    {
        toPos.z = StageManager.Instance.currentMinigame == StageManager.MinigameType.Bathing ? 13 : toPos.z;
        if (CurrentHeldObject != null)
        {
            CurrentHeldObject.transform.position = Vector3.Lerp(CurrentHeldObject.transform.position, toPos, Time.deltaTime);
        }
    }
    public IEnumerator CheckHeldObject()
    {
        _progress = StageManager.Instance.ProgressBarBehaviour;
        int nextExpectedProgress = _progress.GetRawProgress() + 1;
        yield return new WaitForEndOfFrame();


        yield return new WaitUntil(() => _progress.GetRawProgress() == nextExpectedProgress);
        ResetHeldObject();

        yield break;
    }
}
