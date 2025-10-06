using UnityEngine;
using UnityEngine.UI;

public class ProgressBehaviour : MonoBehaviour
{
    [SerializeField] private int _maxProgress = 3;
    [SerializeField] private float _progress = 0f;
    [SerializeField] private Slider _progressSlider;
    public float GetRawProgress()
    {
        return _progress;
    }
    public float GetPercentProgress()
    {
        return _progress / _maxProgress;
    }
    private void Start()
    {
        _progressSlider = GetComponent<Slider>();
        ResetProgress();
    }
    public void ResetProgress()
    {
        _progress = 0f;
        SetMaxProgress();
        UpdateSlider();
    }
    public void SetMaxProgress()
    {
        _maxProgress = StageManager.Instance.GetMinigameObjectives().Length;
        UpdateSlider();
    }
    public void AdvanceProgress(float amount)
    {
        if (!(_progress > _maxProgress || _progress + amount > _maxProgress))
        {
            float newProgress = _progress + amount;
            _progress = Mathf.Lerp(_progress, newProgress, Time.deltaTime);
        }
        else
            _progress = Mathf.Lerp(_progress, _maxProgress, Time.deltaTime);
        UpdateSlider();
    }
    private void UpdateSlider()
    {
        _progressSlider.value = GetPercentProgress();
    }
}
