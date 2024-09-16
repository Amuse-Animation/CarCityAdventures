using UnityAtoms.BaseAtoms;
using UnityEngine;

public class RoadPileVfxController : MonoBehaviour
{
    const float SPEED_MULTIPLIER = 25f;

    ParticleSystem _ParticleSystem;

    Collider2D _Collider2D;
    [SerializeField] FloatVariable _CarSpeedNormalized;

    void Start()
    {
        _Collider2D = GetComponent<Collider2D>();
        _ParticleSystem = GetComponent<ParticleSystem>();
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        //_Collider2D.enabled = false; // avoid more triggers

        var main = _ParticleSystem.main;

        if (_CarSpeedNormalized)
        {
            print("SpeedNormalized: " + _CarSpeedNormalized.Value);
            //main.startSpeed = new ParticleSystem.MinMaxCurve(Mathf.Abs(_CarSpeedNormalized.Value) * SPEED_MULTIPLIER, Mathf.Abs(_CarSpeedNormalized.Value) * SPEED_MULTIPLIER + 5f);
            main.startSpeedMultiplier = Mathf.Abs(_CarSpeedNormalized.Value) * SPEED_MULTIPLIER;
        }

        //Rigidbody2D rb = col.GetComponent<Rigidbody2D>();


        //if (rb)
        //{
        //    print("x velocity: " + rb.velocity.x);
        //    main.startSpeed = new ParticleSystem.MinMaxCurve(Mathf.Abs(rb.velocity.x) * 8f, Mathf.Abs(rb.velocity.x) * 8f + 5f);
        //}

        // Reset the particles and the simulation time with the same random seed
        var seed = _ParticleSystem.randomSeed;
        _ParticleSystem.Stop();
        _ParticleSystem.Clear();
        _ParticleSystem.randomSeed = seed;
        _ParticleSystem.Play();
    }
}
