using CCH.Illness.Controller.Receiver;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace CCH.IceBlock.Controller
{
    public class IceBlockController : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;
        [SerializeField]
        private IntVariable currentOwnerIdIntVariable;

        private const string FireTruckName = "FireTruck";
        private const string KidGreen = "KidGreen";
        private const string Police = "Police";
        private const string RaceCar = "RaceCar";

        private void OnEnable()
        {
            SetAnimatorVariables();
        }

        public void SetAssignIllness(IllnessReceiverController owner)
        {
            currentOwnerIdIntVariable.Value = owner.AssignedIllness.OwnerID;
            SetAnimatorVariables();
        }

        private void SetAnimatorVariables()
        {
            int ownerId = currentOwnerIdIntVariable.Value;

            string animationName = ownerId switch
            {
                0 => FireTruckName,
                1 => KidGreen,
                2 => Police,
                3 => RaceCar,
                _ => null
            };

            animator.Play(animationName);
        }
    }
}