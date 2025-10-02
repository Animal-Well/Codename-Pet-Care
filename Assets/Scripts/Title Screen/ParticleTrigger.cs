using UnityEngine;

public class ParticleTrigger : MonoBehaviour
{
    public ParticleSystem ParticleSystem;
    void Update()
    {
        if (ParticleSystem != null)
            if(Input.GetButtonDown("Fire1"))
            {
                ParticleSystem.TriggerSubEmitter(0);
            }
    }
}
