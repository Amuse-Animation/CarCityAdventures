using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace CCH.VisualEffectsBehaviour.Terrain
{
    public class TerrainParticlesController : MonoBehaviour
    {
        [SerializeField] 
        private BoolVariable isDivingBoolVariable;
        
        [SerializeField]
        TrailRenderer[] tiresTrailRenderer;
        [SerializeField]
        ParticleSystem[] dustParticleSystem;

        private void OnEnable()
        {
            isDivingBoolVariable.Changed.Register(()=>
            {
                PlayVfx(true);
            });
        }

        private void OnDisable()
        {
            isDivingBoolVariable.Changed.Unregister(() =>
            {
                PlayVfx(true);
            });
        }

        public void PlayVfx(bool grounded)
        {
            foreach (TrailRenderer trail in tiresTrailRenderer)
            {
                if (isDivingBoolVariable.Value)
                {
                    trail.Clear();
                    return;
                }
                
                trail.emitting = grounded;
            }

            foreach (ParticleSystem dust in dustParticleSystem)
            {
                if (isDivingBoolVariable.Value)
                {
                    dust.Stop();
                    return;
                }
                
                if (grounded)
                {
                    dust.Play();
                }
                else
                {
                    dust.Stop();
                }
                
            }


        }
    }
}
