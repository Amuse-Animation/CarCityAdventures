using UnityEngine;

namespace CCH.VisualEffectsBehaviour
{
    public class TruckTracksChangerColor : MonoBehaviour
    {
        [Space(10)]
        [Header("Particle systems assignation ------------------ ")]
        [Space(5)]
        [SerializeField] private GameObject terrainTrigger;
        [SerializeField] private ParticleSystem[] m_tiresTrail;
        [SerializeField] private TrailRenderer[] m_tiresTrailRenderer;
        [SerializeField] private ParticleSystem[] m_tiresSmoke;
        [SerializeField] private ParticleSystem m_tiresSandSmoke;
        [SerializeField] private TrailRenderer[] m_tiresTrailExtraRenderer;
        [SerializeField] private ParticleSystem tireDustSmoke;

        [Space(10)]
        [Header("Wheels particles colors  ------------------ ")]
        [SerializeField] private Color colorToChangeMug;
        [SerializeField] private Color colorToChangeWater;
        [SerializeField] private Color colorToChangeSandSmoke;
        [SerializeField] private Color colorToChangeSand;
        [SerializeField] private Color colorToChangeGrass;
        [SerializeField] private Color colorToChangeSnow;
        [SerializeField] private Color colorToChangeIce;


        [Space(10)]
        [Header("Colors for tire dusk smoke for particles systems ")]
        [SerializeField] private Color tireDuskSmokeColorToChangeMugTrail;
        [SerializeField] private Color tireDuskSmokeColorToChangeWaterTrail;
        [SerializeField] private Color tireDuskSmokeColorToChangeSandTrail;
        [SerializeField] private Color tireDuskSmokeColorToChangeGrassTrail;
        [SerializeField] private Color tireDuskSmokeColorToChangeSnowTrail;
        [SerializeField] private Color tireDuskSmokeColorToChangeIceTrail;

        [Space(10)]
        [Header("Trail Wheels colors  ------------------ ")]
        [SerializeField] private Gradient colorToChangeAsphaltTrailGradient_01;
        [SerializeField] private Gradient colorToChangeAsphaltTrailGradient_02;
        [SerializeField] private Gradient colorToChangeGrassTrailGradient;
        [SerializeField] private Gradient colorToChangeMugTrailGradient;
        [SerializeField] private Gradient colorToChangeWaterTrailGradient;
        [SerializeField] private Gradient colorToChangeSandTrailGradient;
        [SerializeField] private Gradient colorToChangeSnowTrailGradient;
        [SerializeField] private Gradient colorToChangeIceTrailGradient;

        
        ParticleSystem.MainModule MainModuleTires;
        ParticleSystem.MainModule MainModuleSmoke;
        ParticleSystem.MainModule MainModuleSandSmoke;

        [Space(10)]
        [Header("Material reassignation instances ------------------ ")]
        [SerializeField] Material[] matTiresGeneric;
        [SerializeField] Material[] matSmokeGeneric;
        [SerializeField] Material[] matTrailTiresGeneric;
        [SerializeField] Material matSmokeSandDust;
        [SerializeField] Material matTiresGrass;
        [SerializeField] Material matSmokeGrass;
        [SerializeField] Material matTiresSnow;
        [SerializeField] Material matSmokeSnow;
        [SerializeField] Material matTiresIce;
        [SerializeField] Material matSmokeIce;
        [SerializeField] Material matSmokeWater;
        [SerializeField] Material matSmokeSand;
        [SerializeField] Material matTiresAsphalt;

        [SerializeField] private Color currentColorBeingUsed;
        [SerializeField] private Color currentTireColorBeingUsed;
        [SerializeField] private Gradient currentColorBeingUsedGradient;
        private void Start()
        {
            for(int i = 0; i < m_tiresTrail.Length; i++) 
            {
                matTiresGeneric[i] = m_tiresTrail[i].GetComponent<ParticleSystemRenderer>().trailMaterial;
            }
            for (int i = 0; i < m_tiresSmoke.Length; i++)
            {
                matSmokeGeneric[i] = m_tiresSmoke[i].GetComponent<ParticleSystemRenderer>().material;
            }
            for (int i = 0; i < m_tiresTrailRenderer.Length; i++)
            {
                matTrailTiresGeneric[i] = m_tiresTrailRenderer[i].GetComponent<TrailRenderer>().material;
            }

            matSmokeSandDust = m_tiresSandSmoke.GetComponent<ParticleSystemRenderer>().material;

        }

        public void ChangeColorMug()
        {
            for (int i = 0;  i < m_tiresTrail.Length; i++) 
            {
                
                MainModuleTires = m_tiresTrail[i].main;
                MainModuleTires.startColor = colorToChangeMug;

            }
            for (int i = 0; i < m_tiresSmoke.Length; i++)
            {
                MainModuleSmoke = m_tiresSmoke[i].main;
                MainModuleSmoke.startColor = colorToChangeMug;
            }
            for (int i = 0; i < m_tiresTrailRenderer.Length; i++)
            {

                m_tiresTrailRenderer[i].colorGradient = colorToChangeMugTrailGradient;

            }

            for (int i = 0; i < m_tiresTrailExtraRenderer.Length; i++)
            {
                m_tiresTrailExtraRenderer[i].colorGradient = colorToChangeMugTrailGradient;
            }

            ParticleSystem.MainModule realTireDustMainModule = tireDustSmoke.main;
            realTireDustMainModule.startColor = tireDuskSmokeColorToChangeMugTrail;

            currentColorBeingUsed = colorToChangeMug;
            currentTireColorBeingUsed = tireDuskSmokeColorToChangeMugTrail;
            currentColorBeingUsedGradient = colorToChangeMugTrailGradient;
        }
        public void ChangeColorWater()
        {


            for (int i = 0; i < m_tiresTrail.Length; i++)
            {

                MainModuleTires = m_tiresTrail[i].main;
                MainModuleTires.startColor = colorToChangeWater;

            }
            for (int i = 0; i < m_tiresSmoke.Length; i++)
            {
                MainModuleSmoke = m_tiresSmoke[i].main;
                MainModuleSmoke.startColor = colorToChangeWater;
                m_tiresSmoke[i].GetComponent<ParticleSystemRenderer>().material = matSmokeWater;

            }
            for (int i = 0; i < m_tiresTrailRenderer.Length; i++)
            {

                m_tiresTrailRenderer[i].colorGradient = colorToChangeWaterTrailGradient;

            }

            for (int i = 0; i < m_tiresTrailExtraRenderer.Length; i++)
            {
                m_tiresTrailExtraRenderer[i].colorGradient = colorToChangeWaterTrailGradient;
            }

            ParticleSystem.MainModule realTireDustMainModule = tireDustSmoke.main;
            realTireDustMainModule.startColor = tireDuskSmokeColorToChangeWaterTrail;

            currentColorBeingUsed = colorToChangeWater;
            currentTireColorBeingUsed = tireDuskSmokeColorToChangeWaterTrail;
            currentColorBeingUsedGradient = colorToChangeWaterTrailGradient;
            
        }
        public void ChangeColorSand()
        {
            MainModuleSandSmoke = m_tiresSandSmoke.main;
            MainModuleSandSmoke.startColor = colorToChangeSandSmoke;
            

            for (int i = 0; i < m_tiresTrail.Length; i++)
            {

                MainModuleTires = m_tiresTrail[i].main;
                MainModuleTires.startColor = colorToChangeSand;

            }
            for (int i = 0; i < m_tiresSmoke.Length; i++)
            {
                MainModuleSmoke = m_tiresSmoke[i].main;
                MainModuleSmoke.startColor = colorToChangeSand;

            }
            for (int i = 0; i < m_tiresTrailRenderer.Length; i++)
            {

                m_tiresTrailRenderer[i].colorGradient = colorToChangeSandTrailGradient;
                
            }

            for (int i = 0; i < m_tiresTrailExtraRenderer.Length; i++)
            {
                m_tiresTrailExtraRenderer[i].colorGradient = colorToChangeSandTrailGradient;
            }

            ParticleSystem.MainModule realTireDustMainModule = tireDustSmoke.main;
            realTireDustMainModule.startColor = tireDuskSmokeColorToChangeSandTrail;
            
            currentColorBeingUsed = colorToChangeSand;
            currentTireColorBeingUsed = tireDuskSmokeColorToChangeSandTrail;
            currentColorBeingUsedGradient = colorToChangeSandTrailGradient;

        }
        public void ChangeColorGrass()
        {


            for (int i = 0; i < m_tiresTrail.Length; i++)
            {
                MainModuleTires = m_tiresTrail[i].main;
                MainModuleTires.startColor = colorToChangeGrass;
                m_tiresTrail[i].GetComponent<ParticleSystemRenderer>().trailMaterial = matTiresGrass;

            }

            for (int i = 0; i < m_tiresSmoke.Length; i++)
            {
                MainModuleSmoke = m_tiresSmoke[i].main;
                MainModuleSmoke.startColor = colorToChangeGrass;
                m_tiresSmoke[i].GetComponent<ParticleSystemRenderer>().material = matSmokeGrass;
            }

            for (int i = 0; i < m_tiresTrailRenderer.Length; i++)
            {

                m_tiresTrailRenderer[i].colorGradient = colorToChangeGrassTrailGradient;
                m_tiresTrailRenderer[i].GetComponent<TrailRenderer>().material = matTiresGrass;
            }


            for (int i = 0; i < m_tiresTrailExtraRenderer.Length; i++)
            {
                m_tiresTrailExtraRenderer[i].colorGradient = colorToChangeGrassTrailGradient;
            }

            ParticleSystem.MainModule realTireDustMainModule = tireDustSmoke.main;
            realTireDustMainModule.startColor = tireDuskSmokeColorToChangeGrassTrail;
            
            currentColorBeingUsed = colorToChangeGrass;
            currentTireColorBeingUsed = tireDuskSmokeColorToChangeGrassTrail;
            currentColorBeingUsedGradient = colorToChangeGrassTrailGradient;
        }

        public void ChangeColorSnow()
        {


            for (int i = 0; i < m_tiresTrail.Length; i++)
            {
                MainModuleTires = m_tiresTrail[i].main;
                MainModuleTires.startColor = colorToChangeSnow;
                m_tiresTrail[i].GetComponent<ParticleSystemRenderer>().trailMaterial = matTiresSnow;

            }

            for (int i = 0; i < m_tiresSmoke.Length; i++)
            {
                MainModuleSmoke = m_tiresSmoke[i].main;
                MainModuleSmoke.startColor = colorToChangeSnow;
                m_tiresSmoke[i].GetComponent<ParticleSystemRenderer>().material = matSmokeSnow;
            }

            for (int i = 0; i < m_tiresTrailRenderer.Length; i++)
            {

                m_tiresTrailRenderer[i].colorGradient = colorToChangeSnowTrailGradient;
                m_tiresTrailRenderer[i].GetComponent<TrailRenderer>().material = matTiresSnow;
            }

            for (int i = 0; i < m_tiresTrailExtraRenderer.Length; i++)
            {
                m_tiresTrailExtraRenderer[i].colorGradient = colorToChangeSnowTrailGradient;
            }

            ParticleSystem.MainModule realTireDustMainModule = tireDustSmoke.main;
            realTireDustMainModule.startColor = tireDuskSmokeColorToChangeSnowTrail;
            
            currentColorBeingUsed = colorToChangeSnow;
            currentTireColorBeingUsed = tireDuskSmokeColorToChangeSnowTrail;
            currentColorBeingUsedGradient = colorToChangeSnowTrailGradient;

        }

        public void ChangeColorIce()
        {


            for (int i = 0; i < m_tiresTrail.Length; i++)
            {
                MainModuleTires = m_tiresTrail[i].main;
                MainModuleTires.startColor = colorToChangeIce;
                m_tiresTrail[i].GetComponent<ParticleSystemRenderer>().trailMaterial = matTiresIce;

            }

            for (int i = 0; i < m_tiresSmoke.Length; i++)
            {
                MainModuleSmoke = m_tiresSmoke[i].main;
                MainModuleSmoke.startColor = colorToChangeIce;
                m_tiresSmoke[i].GetComponent<ParticleSystemRenderer>().material = matSmokeIce;
            }

            for (int i = 0; i < m_tiresTrailRenderer.Length; i++)
            {

                m_tiresTrailRenderer[i].colorGradient = colorToChangeIceTrailGradient;
                m_tiresTrailRenderer[i].GetComponent<TrailRenderer>().material = matTiresIce;
            }

            for (int i = 0; i < m_tiresTrailExtraRenderer.Length; i++)
            {
                m_tiresTrailExtraRenderer[i].colorGradient = colorToChangeIceTrailGradient;
            }

            ParticleSystem.MainModule realTireDustMainModule = tireDustSmoke.main;
            realTireDustMainModule.startColor = tireDuskSmokeColorToChangeIceTrail;
            
            currentColorBeingUsed = colorToChangeIce;
            currentTireColorBeingUsed = tireDuskSmokeColorToChangeIceTrail;
            currentColorBeingUsedGradient = colorToChangeIceTrailGradient;
        }

        public void ChangeColorAsphalt_01()
        {


            // for (int i = 0; i < m_tiresTrailRenderer.Length; i++)
            // {
            //
            //     m_tiresTrailRenderer[i].colorGradient = colorToChangeAsphaltTrailGradient_01;
            //     m_tiresTrailRenderer[i].GetComponent<TrailRenderer>().material = matTiresAsphalt;
            // }
            
            
            for (int i = 0; i < m_tiresTrail.Length; i++)
            {
                MainModuleTires = m_tiresTrail[i].main;
                MainModuleTires.startColor = colorToChangeIce;
                m_tiresTrail[i].GetComponent<ParticleSystemRenderer>().trailMaterial = matTiresAsphalt;

            }

            for (int i = 0; i < m_tiresSmoke.Length; i++)
            {
                MainModuleSmoke = m_tiresSmoke[i].main;
                MainModuleSmoke.startColor = colorToChangeIce;
                m_tiresSmoke[i].GetComponent<ParticleSystemRenderer>().material = matTiresAsphalt;
            }

            for (int i = 0; i < m_tiresTrailRenderer.Length; i++)
            {

                m_tiresTrailRenderer[i].colorGradient = colorToChangeAsphaltTrailGradient_01;
                m_tiresTrailRenderer[i].GetComponent<TrailRenderer>().material = matTiresAsphalt;
            }

            for (int i = 0; i < m_tiresTrailExtraRenderer.Length; i++)
            {
                m_tiresTrailExtraRenderer[i].colorGradient = colorToChangeAsphaltTrailGradient_01;
            }

            ParticleSystem.MainModule realTireDustMainModule = tireDustSmoke.main;
            realTireDustMainModule.startColor = new Color(0,0,0,0);
            
            currentColorBeingUsed = new Color(0,0,0,0);;
            currentTireColorBeingUsed = new Color(0,0,0,0);;
            currentColorBeingUsedGradient = colorToChangeAsphaltTrailGradient_01;

        }
        public void ChangeColorAsphalt_02()
        {


            // for (int i = 0; i < m_tiresTrailRenderer.Length; i++)
            // {
            //
            //     m_tiresTrailRenderer[i].colorGradient = colorToChangeAsphaltTrailGradient_02;
            //     m_tiresTrailRenderer[i].GetComponent<TrailRenderer>().material = matTiresAsphalt;
            // }
            
            for (int i = 0; i < m_tiresTrail.Length; i++)
            {
                MainModuleTires = m_tiresTrail[i].main;
                MainModuleTires.startColor = colorToChangeIce;
                m_tiresTrail[i].GetComponent<ParticleSystemRenderer>().trailMaterial = matTiresAsphalt;

            }

            for (int i = 0; i < m_tiresSmoke.Length; i++)
            {
                MainModuleSmoke = m_tiresSmoke[i].main;
                MainModuleSmoke.startColor = colorToChangeIce;
                m_tiresSmoke[i].GetComponent<ParticleSystemRenderer>().material = matTiresAsphalt;
            }

            for (int i = 0; i < m_tiresTrailRenderer.Length; i++)
            {

                m_tiresTrailRenderer[i].colorGradient = colorToChangeAsphaltTrailGradient_02;
                m_tiresTrailRenderer[i].GetComponent<TrailRenderer>().material = matTiresAsphalt;
            }

            for (int i = 0; i < m_tiresTrailExtraRenderer.Length; i++)
            {
                m_tiresTrailExtraRenderer[i].colorGradient = colorToChangeAsphaltTrailGradient_02;
            }

            ParticleSystem.MainModule realTireDustMainModule = tireDustSmoke.main;
            realTireDustMainModule.startColor = new Color(0,0,0,0);
            
            currentColorBeingUsed = new Color(0,0,0,0);;
            currentTireColorBeingUsed = new Color(0,0,0,0);;
            currentColorBeingUsedGradient = colorToChangeAsphaltTrailGradient_02;

        }



    }
}
