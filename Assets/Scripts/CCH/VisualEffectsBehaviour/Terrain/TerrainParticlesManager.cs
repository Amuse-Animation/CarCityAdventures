using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace CCH.VisualEffectsBehaviour.Terrain
{

    public class TerrainParticlesManager : MonoBehaviour
    {
        [SerializeField] 
        private BoolVariable isDivingBoolVariable;
        
        [SerializeField] 
        TrailRenderer[] tiresTrailRenderer;
        [SerializeField] 
        ParticleSystem[] dustParticleSystem;

        ParticleSystem.MainModule dustMainModule;
        ParticleSystemRenderer dustRenderer;

        TerrainParticlePropertiesAssigner terrainProperties;

        public void TriggerTerrainParticle(Collider2D collider2d)
        {        
            if(isDivingBoolVariable.Value) return;

            collider2d.TryGetComponent(out TerrainParticlePropertiesAssigner gettedTerrainProperties);
            if(gettedTerrainProperties == null) return;
            
            for (int i = 0; i < dustParticleSystem.Length; i++)
            {
                dustParticleSystem[i].TryGetComponent(out ParticleSystemRenderer particleSystemRenderer);
                if(!particleSystemRenderer) return;
                
                dustMainModule = dustParticleSystem[i].main;
                dustMainModule.startColor = terrainProperties.DustColor;
                dustRenderer = particleSystemRenderer;
                dustRenderer.material = terrainProperties.DustMaterial;
            }

            for (int i = 0; i < tiresTrailRenderer.Length; i++)
            {
                tiresTrailRenderer[i].colorGradient = terrainProperties.TrailGradient;
                tiresTrailRenderer[i].material = terrainProperties.TrailMaterial;
            }
        }
    }
}
