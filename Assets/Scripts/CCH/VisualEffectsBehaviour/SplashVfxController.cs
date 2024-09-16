using UnityAtoms.BaseAtoms;
using UnityEngine;

public class SplashVfxController : MonoBehaviour
{
    const float SPEED_MULTIPLIER = 25f;

    ParticleSystem _ParticleSystem;

    [SerializeField] FloatVariable _CarSpeedNormalized;

    void Start()
    {
        _ParticleSystem = GetComponent<ParticleSystem>();
    }

    public void TriggerSplash() 
    {
        var main = _ParticleSystem.main;

        if (_CarSpeedNormalized)
        {
            var shape = _ParticleSystem.shape;
            shape.rotation = _CarSpeedNormalized.Value < 0 ? new Vector3(shape.rotation.x, 180f, shape.rotation.z) : new Vector3(shape.rotation.x, 0f, shape.rotation.z);

            print("SpeedNormalized: " + _CarSpeedNormalized.Value);
            //main.startSpeed = new ParticleSystem.MinMaxCurve(Mathf.Abs(_CarSpeedNormalized.Value) * SPEED_MULTIPLIER, Mathf.Abs(_CarSpeedNormalized.Value) * SPEED_MULTIPLIER + 5f);
            main.startSpeedMultiplier = Mathf.Abs(_CarSpeedNormalized.Value) * SPEED_MULTIPLIER;
        }

        _ParticleSystem.Play();
    }
}
