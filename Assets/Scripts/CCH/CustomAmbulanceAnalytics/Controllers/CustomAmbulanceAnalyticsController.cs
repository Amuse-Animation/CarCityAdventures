using CCH.HealingClasses.Illness;
using CCH.Illness.Controller.Dealer;
using CCH.Illness.Controller.Receiver;
using Unity.Services.Analytics;
using UnityEngine;

namespace CCH.CustomAmbulanceAnalytics.Controllers
{
    public class CustomAmbulanceAnalyticsController : MonoBehaviour
    {
        const string KID_CAR_CURE_STARTED = "kidCarCureStarted";
        const string POLICE_CAR_CURE_STARTED = "policeCarCureStarted";
        const string RACE_CAR_CURE_STARTED = "raceCarCureStarted";
        const string FIRE_TRUCK_CAR_CURE_STARTED = "fireTruckCarCureStarted";

        const string BRANCH_ILLNESS_STARTED = "branchIllnessHealingStarted";
        const string BROKEN_ILLNESS_STARTED = "brokenIllnessHealingStarted";
        const string FROZEN_ILLNESS_STARTED = "frozenIllnessHealingStarted";
        const string RED_EYES_ILLNESS_STARTED = "redEyesIllnessHealingStarted";

        const string FIRE_TRUCK_POSITION = "fireTruckCarHealingPosition";
        const string KID_CAR_POSITION = "kidCarHealingPosition";
        const string POLICE_CAR_POSITION = "policeCarHealingPosition";
        const string RACE_CAR_POSITION = "raceCarHealingPosition";

        const string KID_CAR_CURE_COMPLETED = "kidCarCureCompleted";
        const string POLICE_CAR_CURE_COMPLETED = "policeCarCureCompleted";
        const string RACE_CAR_CURE_COMPLETED = "raceCarCureCompleted";
        const string FIRE_TRUCK_CAR_CURE_COMPLETED = "fireTruckCarCureCompleted";

        const string BRANCH_ILLNESS_COMPLETED = "branchIllnessHealingCompleted";
        const string BROKEN_ILLNESS_COMPLETED = "brokenIllnessHealingCompleted";
        const string FROZEN_ILLNESS_COMPLETED = "frozenIllnessHealingCompleted";
        const string RED_EYES_ILLNESS_COMPLETED = "redEyesIllnessHealingCompleted";
        
        const string ALL_PATIENTS_HEALED = "allPatientsHealed";

        public void HandleHealingActivatedAnalyticsCustomEvents(IllnessReceiverController illnessReceiverController)
        {
            switch (illnessReceiverController.AssignedIllness.PatientIdEnum)
            {
                case PatientIdEnum.None:
                    break;
                case PatientIdEnum.FireTruck:
                    CustomEvent fireTruckEvent = new CustomEvent(FIRE_TRUCK_CAR_CURE_STARTED);
                    CustomEvent fireTruckPositionEvent = new CustomEvent(FIRE_TRUCK_POSITION)
                    {
                         {"patientPosition",  $"{illnessReceiverController.transform.position}"}
                    };
                    AnalyticsService.Instance.RecordEvent(fireTruckEvent);
                    AnalyticsService.Instance.RecordEvent(fireTruckPositionEvent);
                    break;
                case PatientIdEnum.KidGreen:
                    CustomEvent kidTruckEvent = new CustomEvent(KID_CAR_CURE_STARTED);
                    CustomEvent kidTruckPositionEvent = new CustomEvent(KID_CAR_POSITION)
                    {
                         {"patientPosition",  $"{illnessReceiverController.transform.position}"}
                    };
                    AnalyticsService.Instance.RecordEvent(kidTruckEvent);
                    AnalyticsService.Instance.RecordEvent(kidTruckPositionEvent);
                    break;
                case PatientIdEnum.Police:
                    CustomEvent policeTruckEvent = new CustomEvent(POLICE_CAR_CURE_STARTED);
                    CustomEvent policeTruckPositionEvent = new CustomEvent(POLICE_CAR_POSITION)
                    {
                         {"patientPosition",  $"{illnessReceiverController.transform.position}"}
                    };
                    AnalyticsService.Instance.RecordEvent(policeTruckEvent);
                    AnalyticsService.Instance.RecordEvent(policeTruckPositionEvent);
                    break;
                case PatientIdEnum.RaceCar:
                    CustomEvent raceTruckEvent = new CustomEvent(RACE_CAR_CURE_STARTED);
                    CustomEvent raceTruckPositionEvent = new CustomEvent(RACE_CAR_POSITION)
                    {
                         {"patientPosition",  $"{illnessReceiverController.transform.position}"}
                    };
                    AnalyticsService.Instance.RecordEvent(raceTruckEvent);
                    AnalyticsService.Instance.RecordEvent(raceTruckPositionEvent);
                    break;
            }

            switch (illnessReceiverController.AssignedIllnesType)
            {
                case IllnessType.NONE:
                    break;
                case IllnessType.Frozen:
                    CustomEvent frozenIllnessEvent = new CustomEvent(FROZEN_ILLNESS_STARTED);
                    AnalyticsService.Instance.RecordEvent(frozenIllnessEvent);
                    break;
                case IllnessType.Branch:
                    CustomEvent branchIllnessEvent = new CustomEvent(BRANCH_ILLNESS_STARTED);
                    AnalyticsService.Instance.RecordEvent(branchIllnessEvent);
                    break;
                case IllnessType.Broken:
                    CustomEvent brokenIllnessEvent = new CustomEvent(BROKEN_ILLNESS_STARTED);
                    AnalyticsService.Instance.RecordEvent(brokenIllnessEvent);
                    break;
                case IllnessType.RedEyes:
                    CustomEvent redEyesIllnessEvent = new CustomEvent(RED_EYES_ILLNESS_STARTED);
                    AnalyticsService.Instance.RecordEvent(redEyesIllnessEvent);
                    break;
                case IllnessType.Random:
                    break;
            }
        }

        public void HandleHealingCompletedActivatedAnalyticsCustomEvents(IllnessReceiverController illnessReceiverController)
        {
            switch (illnessReceiverController.AssignedIllness.PatientIdEnum)
            {
                case PatientIdEnum.None:
                    break;
                case PatientIdEnum.FireTruck:
                    CustomEvent fireTruckEventCompleted = new CustomEvent(FIRE_TRUCK_CAR_CURE_COMPLETED);
                    AnalyticsService.Instance.RecordEvent(fireTruckEventCompleted);
                    break;
                case PatientIdEnum.KidGreen:
                    CustomEvent kidTruckEventCompleted = new CustomEvent(KID_CAR_CURE_COMPLETED);
                    AnalyticsService.Instance.RecordEvent(kidTruckEventCompleted);
                    break;
                case PatientIdEnum.Police:
                    CustomEvent policeTruckEventCompleted = new CustomEvent(POLICE_CAR_CURE_COMPLETED);
                    AnalyticsService.Instance.RecordEvent(policeTruckEventCompleted);
                    break;
                case PatientIdEnum.RaceCar:
                    CustomEvent raceTruckEventCompleted = new CustomEvent(RACE_CAR_CURE_COMPLETED);
                    AnalyticsService.Instance.RecordEvent(raceTruckEventCompleted);
                    break;
            }

            switch (illnessReceiverController.AssignedIllnesType)
            {
                case IllnessType.NONE:
                    break;
                case IllnessType.Frozen:
                    CustomEvent frozenIllnessEventCompleted = new CustomEvent(FROZEN_ILLNESS_COMPLETED);
                    AnalyticsService.Instance.RecordEvent(frozenIllnessEventCompleted);
                    break;
                case IllnessType.Branch:
                    CustomEvent branchIllnessEventCompleted = new CustomEvent(BRANCH_ILLNESS_COMPLETED);
                    AnalyticsService.Instance.RecordEvent(branchIllnessEventCompleted);
                    break;
                case IllnessType.Broken:
                    CustomEvent brokenIllnessEventCompleted = new CustomEvent(BROKEN_ILLNESS_COMPLETED);
                    AnalyticsService.Instance.RecordEvent(brokenIllnessEventCompleted);
                    break;
                case IllnessType.RedEyes:
                    CustomEvent redEyesIllnessEventCompleted = new CustomEvent(RED_EYES_ILLNESS_COMPLETED);
                    AnalyticsService.Instance.RecordEvent(redEyesIllnessEventCompleted);
                    break;
                case IllnessType.Random:
                    break;
            }
        }

        public void HandleAllPatientensHealedAnalytics()
        {
            CustomEvent allPatientiHealedEvent = new CustomEvent(ALL_PATIENTS_HEALED);
            AnalyticsService.Instance.RecordEvent(allPatientiHealedEvent);
        }
    }
}
