using UnityEngine;

public class GemParticles : MonoBehaviour
{
    public ParticleSystem ps;

    ParticleSystem.EmissionModule emission;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ParticleSystem.EmissionModule emission = ps.emission;
    }

    // Update is called once per frame
    void Update()
    {
        emission.rateOverTime = PlayerBehavior.gemGenerationRate * 0.1F;
    }
}
