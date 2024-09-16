using System.Collections.Generic;
using AmuseEngine.Assets.Scripts.InputTouchInteraction.Interface.InputTouchInteraction;
using CCH.Illness.Controller.Receiver;

namespace CCH.CustomArgsStructsObjects.RemedyPhaseObjectArgs
{
    [System.Serializable]
    public struct RemedyPhaseObjectArgs
    {
        // public IllnessType AssignedIllnesType;
        // public IllnessClass RemedyButtonAssignedIllnessClasss;
        public IllnessReceiverController RemedyPhaseIllnessReceiverController;
        public List<IInputTouchInteraction> RemedyInputTouchInteractionList;
    }
}