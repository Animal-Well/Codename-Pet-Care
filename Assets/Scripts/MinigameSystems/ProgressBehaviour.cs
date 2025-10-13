using UnityEngine;
using UnityEngine.UI;

public class ProgressBehaviour : MonoBehaviour
{
    [SerializeField] private int _maxProgress = 3;
    [SerializeField] private int _progress = 0;
    [SerializeField] private Slider _progressSlider;
    public bool IsProgressComplete()
    {
        return _progressSlider.normalizedValue == 1f;
    }
    private void Start()
    {
        _progressSlider = GetComponent<Slider>();
        ResetProgress();
    }
    public void ResetProgress()
    {
        _progress = 0;
        SetMaxProgress();
        UpdateSlider();
    }
    public void SetMaxProgress()
    {
        _maxProgress = StageManager.Instance.GetMinigameObjectives().Length;
        UpdateSlider();
    }
    public void AdvanceProgress()
    {
        int amount = _progress + 1;
        if (!(amount > _maxProgress))
        {
            _progress = amount;
        }
        else
            _progress = _maxProgress;
        UpdateSlider();
    }
    private void UpdateSlider()
    {
        _progressSlider.maxValue = _maxProgress;
        _progressSlider.value = _progress;
    }
}
