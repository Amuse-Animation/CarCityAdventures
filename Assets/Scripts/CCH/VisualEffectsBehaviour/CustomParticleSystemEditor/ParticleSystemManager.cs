using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particlesSystem;
    private ParticleSystem.MainModule _particlesMainSettings;
    private ParticleSystem.EmissionModule _particlesEmissionSettings;
    private ParticleSystem.ShapeModule _particlesShapeModuleSettings;
    private ParticleSystem.ColorOverLifetimeModule _particlesColorOverLifetimeSettings;

    public Vector3 _particleShapeAngleY;
    public Color _particleStartColor;
    public float _particleAngleArc;

    void Awake()
    {
        _particlesMainSettings = _particlesSystem.main;
        _particlesEmissionSettings = _particlesSystem.emission;
        _particlesShapeModuleSettings = _particlesSystem.shape;
        _particlesColorOverLifetimeSettings = _particlesSystem.colorOverLifetime;
    }

    private void Start()
    {
        _particlesShapeModuleSettings.rotation = _particleShapeAngleY;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
     

    }
}
