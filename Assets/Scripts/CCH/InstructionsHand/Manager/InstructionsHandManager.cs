using System.Collections;
using AmuseEngine.Assets.Scripts.ArgsStructObjects.AnimationEventActivatedArgs;
using AmuseEngine.Assets.Scripts.PoolSystem.Manager;
using AmuseEngine.Assets.Scripts.UnityPoolableObjects.ControllerBase;
using AmuseEngine.Assets.Scripts.UnityPoolableObjects.Interface.PoolableObject;
using CCH.InstructionsHand.Controller;
using DG.Tweening;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace CCH.InstructionsHand.Manager
{
    public class InstructionsHandManager : MonoBehaviour
    {
        [SerializeField]
        private PoolableObjectController handPoolableObjectController;

        [SerializeField]
        private GenericPoolSystemManager genericPoolSystemManager;

        [SerializeField]
        private Vector2Variable currentAnimationHandPositionVariable;

        [SerializeField]
        private StringVariable currentAnimationIDStringVariable;

        private IPoolableObject spawnedPoolableObject;
        private InstructionsHandController spawnedInstructionsHandController; 
        
        private const string Frozen = "FrozenID";
        private const string BranchWheel = "BranchWheelID";
        private const string BranchPump = "BranchPumpID";
        private const string BrokenDerrierFix = "BrokenDerrierFixID";
        private const string SprayProgress = "SprayProgressID";
        private const string Wipper = "WipperID";
        private const string Dry = "DryID";
        private const string Drop = "DropIdleID";

        private string currentIDPlaying = string.Empty;
        public void OnAnimationEventIDActivated(AnimationEventActivatedArgsStruct animationEventArgs)
        {

            if (currentIDPlaying.Equals(animationEventArgs.AnimationEventID)) return;

            if (spawnedPoolableObject != null)
            {
                ReturnSpawnedHandToPool();
            }
            
            SpawnHand(animationEventArgs.BonesSocketGOList[0].transform.position);
            currentAnimationHandPositionVariable.Value = animationEventArgs.BonesSocketGOList[0].transform.position;
            if (spawnedInstructionsHandController == null) return;
            StartCoroutine(PlayHandAnimation(animationEventArgs.AnimationEventID));
        }

        IEnumerator PlayHandAnimation(string handEventID)
        {
           // switch (animationEventArgs.AnimationEventID)
           yield return new WaitForEndOfFrame();
            switch (handEventID)
            {
                case Frozen:
                case BranchWheel:
                case BrokenDerrierFix:
                case SprayProgress:
                case Dry:
                    {
                        if (!string.IsNullOrEmpty(currentIDPlaying)) break;
                        spawnedInstructionsHandController.CircleTouchSpriteRenderer.enabled = false;
                        spawnedInstructionsHandController.HandRootTransform.DOLocalMoveX(-2f, 1f).SetLoops(-1, LoopType.Yoyo).SetRelative(true).SetEase(Ease.InOutSine).SetId(spawnedInstructionsHandController.HandRootTransform.name);
                        break;
                    }
                case Wipper:
                case BranchPump:
                    {
                        if (!string.IsNullOrEmpty(currentIDPlaying)) break;
                        spawnedInstructionsHandController.CircleTouchSpriteRenderer.enabled = false;
                        spawnedInstructionsHandController.HandRootTransform.DOLocalMoveY(1f, .5f).SetLoops(-1, LoopType.Yoyo).SetRelative(true).SetEase(Ease.InOutSine).SetId(spawnedInstructionsHandController.HandRootTransform.name);
                    }
                    break;

                case Drop:
                    {
                        if (!string.IsNullOrEmpty(currentIDPlaying)) break;
                        spawnedInstructionsHandController.DoPlayTapAnimation();
                    }
                    break;
            }

            currentIDPlaying = handEventID;
            currentAnimationIDStringVariable.Value = currentIDPlaying;
            
        }

        private void SpawnHand(Vector2 spawnPosition)
        {
            spawnedPoolableObject = genericPoolSystemManager.GetPoolableObject(handPoolableObjectController, spawnPosition);
            spawnedInstructionsHandController = spawnedPoolableObject.MonoBehaviourComponent.GetComponentInChildren<InstructionsHandController>();
        }

        public void ReturnSpawnedHandToPool()
        {
            if (!string.IsNullOrEmpty(currentIDPlaying))
                DOTween.Kill(spawnedInstructionsHandController.HandRootTransform.name);

            if (spawnedInstructionsHandController != null)
                spawnedInstructionsHandController.DoStopAnimation();

            currentIDPlaying = string.Empty;
            spawnedInstructionsHandController = null;
           
            if(spawnedPoolableObject != null)
            {
                genericPoolSystemManager.ReturnObjectToPool(spawnedPoolableObject);
            }

            spawnedPoolableObject = null;
        }


        public void ReturnSpawnedHandToPoolAndCleanVariables()
        {
            if (!string.IsNullOrEmpty(currentIDPlaying))
                DOTween.Kill(spawnedInstructionsHandController.HandRootTransform.name);

            if(spawnedInstructionsHandController != null)
                spawnedInstructionsHandController.DoStopAnimation();

            currentIDPlaying = string.Empty;
            spawnedInstructionsHandController = null;

            currentAnimationHandPositionVariable.Value = Vector2.zero;
            currentAnimationIDStringVariable.Value = string.Empty;

            if (spawnedPoolableObject != null)
            {
                genericPoolSystemManager.ReturnObjectToPool(spawnedPoolableObject);
            }

            spawnedPoolableObject = null;

            
        }

        public void ForceActivateHand()
        {
            StartCoroutine(Wait());
        }

        IEnumerator Wait()
        {
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            if (spawnedInstructionsHandController != null) yield break;
            SpawnHand(currentAnimationHandPositionVariable.Value);
            if (spawnedInstructionsHandController == null) yield break;
            StartCoroutine(PlayHandAnimation(currentAnimationIDStringVariable.Value));
        }
    }
}