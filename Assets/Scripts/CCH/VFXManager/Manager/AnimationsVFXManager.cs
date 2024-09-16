using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AmuseEngine.Assets.Scripts.ArgsStructObjects.PoolableObjectSpawnAsyncArgs;
using AmuseEngine.Assets.Scripts.HelplerStaticClasses.UnitsConverter;
using AmuseEngine.Assets.Scripts.PoolSystem.Manager;
using AmuseEngine.Assets.Scripts.UnityPoolableObjects.Interface.PoolableObject;
using CCH.CustomArgsStructsObjects.HealingPhaseAnimationArgs;
using UnityEngine;

namespace CCH.VFXManager.Manager
{
    public class AnimationsVFXManager : MonoBehaviour
    {
        [SerializeField]
        private List<VFXData> vFXDataList;

        [SerializeField]
        private GenericPoolSystemManager genericPoolSystem;

        private CancellationTokenSource cancellationTokenSource;

        #region UnityEvents

        private void OnEnable()
        {
            cancellationTokenSource = new CancellationTokenSource();
        }

        private void OnDisable()
        {
            if (cancellationTokenSource == null) return;
            CancelAwait();
        }

        #endregion

        public void OnHealingPhaseAnimationEvent(HealingPhaseAnimationArgs healingPhaseAnimationArgs)
        {
            if(cancellationTokenSource == null)
                cancellationTokenSource = new CancellationTokenSource();

            VFXData currentVfxDataToUse = vFXDataList.Find(x => !string.IsNullOrEmpty(x.GetAnimationNameId(healingPhaseAnimationArgs.CurrentPlayingAnimation)));//(x => x.AnimationNameId.Equals(healingPhaseAnimationArgs.CurrentPlayingAnimation));
            if (currentVfxDataToUse != null)
            {
                int currentVfxDataToUsePoolableObjectSize = currentVfxDataToUse.PoolableObjectSpawnAsyncArgsStructs.Count;
                for (int i = 0; i < currentVfxDataToUsePoolableObjectSize; i++)
                {
                    _ = WaitForDelay(currentVfxDataToUse.PoolableObjectSpawnAsyncArgsStructs[i].SpawningDelayInSeconds,
                                     cancellationTokenSource.Token,
                                     currentVfxDataToUse.PoolableObjectSpawnAsyncArgsStructs[i].PrefabToInstantiate,
                                     healingPhaseAnimationArgs.VFXPlacingPoint.position);
                }
            }
        }

        private async Task WaitForDelay(float delay, CancellationToken cancellationToken, IPoolableObject desiredPoolableObject, Vector2 spawningPoint)
        {
            await Task.Delay(UnitsConverterStaticClass.ConvertSecondsToMilliseconds(delay), cancellationToken);
            if (cancellationToken.IsCancellationRequested)
            {
                Debug.Log("Cancelled");
            }
            cancellationToken.ThrowIfCancellationRequested();
            genericPoolSystem.GetPoolableObject(desiredPoolableObject, spawningPoint);
            CancelAwait();
        }

        private void CancelAwait()
        {
            if (cancellationTokenSource == null) return;
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
            cancellationTokenSource = null;
        }
    }

    [System.Serializable]
    class VFXData
    {
        public List<string> AnimationNameIds => animationNameIds;
        public List<PoolableObjectSpawnAsyncArgsStruct> PoolableObjectSpawnAsyncArgsStructs => poolableObjectSpawnAsyncArgsStructs;

        [SerializeField]
        private List<string> animationNameIds;
        [SerializeField]
        private List<PoolableObjectSpawnAsyncArgsStruct> poolableObjectSpawnAsyncArgsStructs;

        public string GetAnimationNameId(string desiredNameId)
        {
            return animationNameIds.Find(x => x.Equals(desiredNameId));
        }
    }
}
