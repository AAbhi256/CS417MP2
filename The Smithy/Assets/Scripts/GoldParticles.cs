using UnityEngine;

public class GoldParticles : MonoBehaviour
{
    ParticleSystem.EmissionModule emission;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        emission = GetComponent<ParticleSystem>().emission;
    }

    // Update is called once per frame
    void Update()
    {
        emission.rateOverTime = PlayerBehavior.goldGenerationRate * 0.1F;
    }
}
