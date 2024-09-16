using UnityEngine;

/*
public enum TerrainType 
{ 
    Mud,
    Grass,
    Water,
    Snow,
    Ice,
    AsphaltA,
    AsphaltB
}  
*/

namespace CCH.VisualEffectsBehaviour.Terrain
{
    public class TerrainParticlePropertiesAssigner : MonoBehaviour
    {
        //public TerrainType Terrain;
        //[Space]
        public Color DustColor = Color.magenta;
        public Gradient TrailGradient;
        [Space]
        public Material DustMaterial;
        [Space]
        public Material TrailMaterial;

    }
}
