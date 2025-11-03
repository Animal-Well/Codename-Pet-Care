using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public float laneOffset = 5f;
    public float speed = 5f;
    private int _currentLane = 1;

    public UnityEvent FailRun;
    public UnityEvent BeatRun;

    private Vector2 startPos;
    private Vector2 endPos;
    private bool isSwiping = false;

    [SerializeField] private float minSwipeDistance = 50f; // in pixels

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            isSwiping = true;
            startPos = Input.mousePosition;
        }

        if (Input.GetButtonUp("Fire1"))
        {
            if (!isSwiping) return;

            endPos = Input.mousePosition;
            Vector2 swipeDelta = endPos - startPos;

            if (swipeDelta.magnitude > minSwipeDistance)
            {
                DetectSwipeDirection(swipeDelta);
            }

            isSwiping = false;
        }

        // === MOVIMENTO SUAVE ENTRE LANES ===
        Vector3 targetPos = GetLanePosition(_currentLane);
        transform.position = Vector2.Lerp(transform.position, targetPos, Time.deltaTime * speed);
    }

    // Retorna a posição fixa de cada lane (0 = esquerda, 1 = centro, 2 = direita)
    private Vector3 GetLanePosition(int laneIndex)
    {
        float x = 0f;
        switch (laneIndex)
        {
            case 0: x = -laneOffset; break;
            case 1: x = 0f; break;
            case 2: x = laneOffset; break;
        }

        return new Vector3(x, transform.position.y, transform.position.z);
    }
    private void DetectSwipeDirection(Vector2 swipeDelta)
    {
        swipeDelta.Normalize();
        if (Vector2.Dot(swipeDelta, Vector2.left) > 0.7f)
        {
            _currentLane++;
        }
        else if (Vector2.Dot(swipeDelta, Vector2.right) > 0.7f)
        {
            _currentLane--;
        }
        StabalizeLanes();
    }
    private void StabalizeLanes()
    {
        _currentLane = Mathf.Clamp(_currentLane, 0, 2);
    }
    private IEnumerator WaitToChangeScene(string newScene)
    {
        yield return new WaitForSeconds(1.5f);
        CallChangeScene(newScene);
    }
    public void CallWaitChangeScene(string newScene)
    {
        StartCoroutine(WaitToChangeScene(newScene));
    }
    public void CallChangeScene(string newScene)
    {
        GameManager.Instance.ChangeScene(newScene);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            FailRun.Invoke();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WalkObjective"))
        {
            BeatRun.Invoke();
        }
    }
}

