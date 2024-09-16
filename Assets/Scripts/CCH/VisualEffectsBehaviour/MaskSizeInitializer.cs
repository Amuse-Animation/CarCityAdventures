using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskSizeInitializer : MonoBehaviour
{
    ParticleSystem _ParticleSystem;

    void Start()
    {
        _ParticleSystem = GetComponentInParent<ParticleSystem>();

        ParticleSystem.ShapeModule shape = _ParticleSystem.shape;

        transform.localScale = shape.scale;
    }


}
