using System.Collections;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Bathing Minigame")]
    [SerializeField] private HeldObject heldObject;
    private Ray ray;
    private RaycastHit hit;

    private void Start()
    {
        heldObject = FindFirstObjectByType<HeldObject>();
    }

    void Update()
    {
        DoBathing();
    }
    private void DoBathing()
    {
        if(Input.GetButton("Fire1"))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (heldObject != null)
            {
                heldObject.MoveHeldObject(ray);
            }
        }
    }
}
