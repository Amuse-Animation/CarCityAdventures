using UnityAtoms.BaseAtoms;
using UnityEngine;

public class RoadPileHorizontalVfxController : MonoBehaviour
{
    const float SPEED_MULTIPLIER = 25f;

    ParticleSystem _ParticleSystem;

    [SerializeField] FloatVariable _CarSpeedNormalized;

    void Start()
    {
        _ParticleSystem = GetComponent<ParticleSystem>();
    }


    void OnTriggerEnter2D(Collider2D col)
    {
        //col.enabled = false; // avoid more triggers

        var main = _ParticleSystem.main;

        if (_CarSpeedNormalized)
        {
            var shape = _ParticleSystem.shape;
            shape.rotation = _CarSpeedNormalized.Value < 0 ? new Vector3(shape.rotation.x, shape.rotation.y, 90) : new Vector3(shape.rotation.x, shape.rotation.y, 0);

            //print("SpeedNormalized: " + _CarSpeedNormalized.Value);
            //main.startSpeed = new ParticleSystem.MinMaxCurve(Mathf.Abs(_CarSpeedNormalized.Value) * SPEED_MULTIPLIER, Mathf.Abs(_CarSpeedNormalized.Value) * SPEED_MULTIPLIER + 5f);
            main.startSpeedMultiplier = Mathf.Abs(_CarSpeedNormalized.Value) * SPEED_MULTIPLIER;
        }

        // Reset the particles and the simulation time with the same random seed
        var seed = _ParticleSystem.randomSeed;
        _ParticleSystem.Stop();
        _ParticleSystem.Clear();
        _ParticleSystem.randomSeed = seed;
        _ParticleSystem.Play();
    }
}
