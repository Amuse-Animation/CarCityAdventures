using UnityAtoms.BaseAtoms;
using UnityEngine;

/*
    Suggested initial ParticleSystem values:
        - Start Lifetime    ->  Infinity
        - Start Speed       ->  0
        - PlayOnAwake       ->  True
        - Gravity Modifier  ->  0
 */

[RequireComponent(typeof(CircleCollider2D), typeof(ParticleSystem))]
public class LeavesProceduralVfxController : MonoBehaviour
{

    const float SPEED_MULTIPLIER = 25f;

    ParticleSystem _ParticleSystem;

    CircleCollider2D _CircleCollider2D;

    ParticleSystem.MainModule _MainModule;

    [SerializeField] FloatVariable _CarSpeedNormalized;

    void Start()
    {
        _CircleCollider2D = GetComponent<CircleCollider2D>();
        _ParticleSystem = GetComponent<ParticleSystem>();

        _MainModule = _ParticleSystem.main;
        _CircleCollider2D.radius = _ParticleSystem.shape.radius;
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        _CircleCollider2D.enabled = false; // avoid more triggers

        _MainModule.startLifetime = 3f;
        _MainModule.gravityModifier = 0.1f;
        _MainModule.gravityModifier = 0.1f;
        _MainModule.startSpeed = new ParticleSystem.MinMaxCurve(1, 15);//(5, 15);
        _MainModule.startSpeedMultiplier = Mathf.Max(0.5f, Mathf.Abs(_CarSpeedNormalized.Value)) * SPEED_MULTIPLIER;

        var limit = _ParticleSystem.limitVelocityOverLifetime;
        limit.enabled = true;

        var color = _ParticleSystem.colorOverLifetime;
        color.enabled = true;

        var rot = _ParticleSystem.rotationOverLifetime;
        rot.enabled = true;

        var noise = _ParticleSystem.noise;
        noise.enabled = true;

        /*
        var vel = _ParticleSystem.velocityOverLifetime;
        vel.enabled = true;

        var force = _ParticleSystem.forceOverLifetime;
        force.enabled = true;
        */

        // Reset the particles and the simulation time with the same random seed
        var seed = _ParticleSystem.randomSeed;
        _ParticleSystem.Stop();
        _ParticleSystem.Clear();
        _ParticleSystem.randomSeed = seed;
        _ParticleSystem.Play();



    }
}
