using System;
using System.Collections;
using AmuseEngine.Assets.Scripts.HelplerStaticClasses.MathFormulas;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.Events;

namespace CCH.LoopSurf.Controller
{
    public class LoopSurfController : MonoBehaviour
    {
        [SerializeField]
        private Transform transformToRotate;

        [SerializeField]
        private Transform rotationCentreTransform;

        [SerializeField]
        private float rotationCentreRadiusSize;

        [SerializeField][Range(0f,5f)]
        private float rotationTime;

        [SerializeField][Range(-1, 1)]
        private int rotationDirection = 1;

        [SerializeField]
        private float preActivationWaitingTime = .5f;

        [SerializeField]
        private float preDeactivationWaitingTime = 1f;

        [SerializeField]
        private bool isInLoop;

        [SerializeField]
        private UnityEvent<GameObject> onLoopActivated;

        [SerializeField]
        private VoidEvent onLoopActivatedVoidEvent;

        [SerializeField]
        private UnityEvent<GameObject> onLoopDeactivated;

        [SerializeField]
        private VoidEvent onLoopDeactivatedVoidEvent;

        float elapsedTime = 0;
        float loopRotationSpeed;

        float currentRotationTimeDirection;

        // Update is called once per frame
        void FixedUpdate()
        {
            if(isInLoop)
            {

                loopRotationSpeed = MathFormulasStaticClass.CalculateVelocity(2f * Mathf.PI, (rotationTime * rotationDirection));

                float angleToLoopCentre = MathFormulasStaticClass.CalculateAngleBetweenToObjects(transformToRotate.position, rotationCentreTransform.position);
                angleToLoopCentre += loopRotationSpeed * Time.deltaTime;

                transformToRotate.position = MathFormulasStaticClass.CalculatePositionAlongACircle(rotationCentreTransform.position, rotationCentreRadiusSize, angleToLoopCentre);
                float angleInDegrees = angleToLoopCentre * Mathf.Rad2Deg + 90f;
                transformToRotate.rotation = Quaternion.Euler(0f, 0f, angleInDegrees);

            }
        }

        public void SetRotationDirection(int direction)
        {
            rotationDirection = (int)Mathf.Sign(direction);
        }

        public void EnableDisableLoop(GameObject detectedObject)
        {
            if(!isInLoop)
            {
                transformToRotate = detectedObject.transform;
                StartCoroutine(LoopWaiter(preActivationWaitingTime, ()=> 
                {
                    onLoopActivated.Invoke(detectedObject);

                    if(onLoopDeactivatedVoidEvent != null)
                        onLoopActivatedVoidEvent.Raise();
                }));
            }
            else
            {
                StartCoroutine(LoopWaiter(preDeactivationWaitingTime, () => 
                {
                    transformToRotate = null; 
                    onLoopDeactivated.Invoke(detectedObject);

                    if(onLoopDeactivatedVoidEvent != null)
                        onLoopDeactivatedVoidEvent.Raise();
                }));
                
            }

        }

        IEnumerator LoopWaiter(float waitingTime, Action callback = null)
        {
            yield return new WaitForSeconds(waitingTime);
            isInLoop = !isInLoop;
            callback?.Invoke();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(rotationCentreTransform.position, rotationCentreRadiusSize);
        }
    }
}