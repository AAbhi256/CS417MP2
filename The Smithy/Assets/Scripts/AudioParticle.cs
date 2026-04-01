using System.Runtime.Serialization;
using UnityEngine;

public class AudioParticle : MonoBehaviour
{
    public AudioSource aud;
    public ParticleSystem particle;

    public int particle_count = 50;
    public void Proc()
    {
        aud.Play();
        particle.Emit(particle_count);
    }
}
