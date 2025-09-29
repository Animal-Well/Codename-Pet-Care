using UnityEngine;
using UnityEngine.UI;

public class ProgressBehaviour : MonoBehaviour
{
    [SerializeField] private int _maxProgress = 3;
    [SerializeField] private float _progress = 0f;
    [SerializeField] private Slider _progressSlider;
    public float GetProgress()
    {
        return _progress / _maxProgress;
    }
    private StageManager Stage;
    private void Start()
    {
        Stage = StageManager.Instance;
        _progressSlider = GetComponent<Slider>();
    }
    private void LateUpdate()
    {
        UpdateSlider();
    }
    public void ResetProgress()
    {
        _progress = 0f;
    }
    public void SetMaxProgress(int max)
    {
        _maxProgress = max;
    }
    public void AdvanceProgress(float amount)
    {
        if (!(_progress > _maxProgress || _progress + amount > _maxProgress))
        {
            _progress += amount;
        }
        else
            _progress = _maxProgress;
    }
    private void UpdateSlider()
    {
        _progressSlider.value = GetProgress();
    }
}
