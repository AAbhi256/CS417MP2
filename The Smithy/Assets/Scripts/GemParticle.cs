using UnityEngine;

public class GemParticle : MonoBehaviour
{

    ParticleSystem.EmissionModule emiss;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        emiss = GetComponent<ParticleSystem>().emission;
    }

    // Update is called once per frame
    void Update()
    {
        emiss.rateOverTime = PlayerBehavior.gemGenerationRate * 0.1F;
    }
}
