using UnityEngine;

public class LeafsVfxController : MonoBehaviour
{
    ParticleSystem _ParticleSystem;

    Collider2D _Collider2D;

    void Start()
    {
        _Collider2D = GetComponent<Collider2D>();
        _ParticleSystem = GetComponent<ParticleSystem>();
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        _Collider2D.enabled = false; // avoid more triggers

        var main = _ParticleSystem.main;
        main.startLifetime = 3f;
        

        var vel = _ParticleSystem.velocityOverLifetime;
        vel.enabled = true;

        var limit = _ParticleSystem.limitVelocityOverLifetime;
        limit.enabled = true;

        var force = _ParticleSystem.forceOverLifetime;
        force.enabled = true;

        var color = _ParticleSystem.colorOverLifetime;
        color.enabled = true;

        var noise = _ParticleSystem.noise;
        noise.enabled = true;

        // Reset the particles and the simulation time with the same random seed
        var seed = _ParticleSystem.randomSeed;
        _ParticleSystem.Stop();
        _ParticleSystem.Clear();
        _ParticleSystem.randomSeed = seed;
        _ParticleSystem.Play();



    }
}
