using UnityEditor.SceneManagement;
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
    public void AdvanceProgress(int amount)
    {
        if (!(_progress > _maxProgress || _progress + amount > _maxProgress))
        {
            _progress += amount;
        }
        else
            _progress = _maxProgress;
        UpdateSlider();
    }
    private void UpdateSlider()
    {
        _progressSlider.value = GetPercentProgress();
    }
}
