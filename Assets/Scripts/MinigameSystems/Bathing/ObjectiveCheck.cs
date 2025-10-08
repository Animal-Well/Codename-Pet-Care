using UnityEngine;

public class ObjectiveCheck : MonoBehaviour
{
    public int objectiveIndex;

    private void OnDisable()
    {
        Debug.Log("Disabled");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            if (hitInfo.collider == gameObject)
                StageManager.Instance.ProgressBarBehaviour.AdvanceProgress();
        }
    }
}
