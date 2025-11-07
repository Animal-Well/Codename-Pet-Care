using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ObjectiveCheck : MonoBehaviour
{
    public BathMinigame.ObjectiveTypes objectiveType = BathMinigame.ObjectiveTypes.Dirt;
    public UnityEvent OnDestroyEvent;

    [SerializeField] private bool unlocksNewObjective;
    [SerializeField] private bool _finalObjective;
    [SerializeField] private GameObject objectiveToUnlock;
    [SerializeField] private GameObject lastHoldable;
    [SerializeField] private Animator dogAnimator;

    public void CallCoroutine()
    {
        StartCoroutine(ClearObjective());
    }

    public IEnumerator ClearObjective()
    {
        yield return new WaitForSeconds(1.2f);

        if (objectiveToUnlock != null && unlocksNewObjective)
        {
            objectiveToUnlock.SetActive(unlocksNewObjective);
        }
        StageManager.Instance.ProgressBarBehaviour.AdvanceProgress();
        gameObject.SetActive(false);

        if (_finalObjective)
        {
            lastHoldable.SetActive(false);
            dogAnimator.SetTrigger("Finish");
            yield return new WaitForSeconds(0.5f);
        }
    }
}
