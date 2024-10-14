using AmuseEngine.Assets.Scripts.AnalyticsScripts.Controllers.AnalyticsApplicationExit;
using AmuseEngine.Assets.Scripts.AnalyticsScripts.Controllers.AnalyticsApplicationTimer;
using AmuseEngine.Assets.Scripts.AnalyticsScripts.Controllers.AnalyticsInit;
using CCH.CustomAmbulanceAnalytics.Controllers;
using CCH.Illness.Controller.Receiver;
using UnityEngine;

namespace CCH.CustomAmbulanceAnalytics.Manager
{
    public class CustomAmbulanceAnalyticsManager : MonoBehaviour
    {
        public static CustomAmbulanceAnalyticsManager Instance;

        [SerializeField]
        private AnalyticsInitController analyticsInitController;

        [SerializeField]
        private CustomAmbulanceAnalyticsController customAmbulanceAnalyticsController;

        [SerializeField]
        private AnalyticsApplicationExitController analyticsApplicationExitController;

        [SerializeField]
        private AnalyticsApplicationTimerController analyticsApplicationTimerController;

        private bool isAnalitycsInitialized = false;

        private void OnEnable()
        {
            if(Instance != null && Instance != this)
                Destroy(gameObject);
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            
        }

        private void Start()
        {
            analyticsInitController.AsyncInitAnalytics(() =>
            {
                isAnalitycsInitialized = true;
                analyticsApplicationTimerController.CapturateStartingTime(Time.time);
            }).Forget();
        }

        public void OnHealingModeActivatedAnalyticsEvent(IllnessReceiverController illnessReceiverController)
        {
            if (!isAnalitycsInitialized) return;
            customAmbulanceAnalyticsController.HandleHealingActivatedAnalyticsCustomEvents(illnessReceiverController);
        }

        public void OnHealingProcessCompleted(IllnessReceiverController illnessReceiverController)
        {
            if (!isAnalitycsInitialized) return;
            customAmbulanceAnalyticsController.HandleHealingCompletedActivatedAnalyticsCustomEvents(illnessReceiverController);
        }

        public void OnAllStickersPlacedInMapEvent()
        {
            if (!isAnalitycsInitialized) return;
            customAmbulanceAnalyticsController.HandleAllPatientensHealedAnalytics();
        }


        protected void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                if (!isAnalitycsInitialized) return;
                analyticsApplicationTimerController.CapturateExitTime(Time.time);
                analyticsApplicationExitController.OnExitGameAnalyticsEvent(analyticsApplicationTimerController.GetApplicationUsedLifeTime());
            }
            else
            {
                analyticsApplicationTimerController.CapturateStartingTime(Time.time);
            }
        }

        protected void OnApplicationFocus(bool hasFocus)
        {
            if (hasFocus)
            {
                if (!isAnalitycsInitialized) return;
                analyticsApplicationTimerController.CapturateStartingTime(Time.time);
            }
        }

        protected void OnApplicationQuit()
        {
            if (!isAnalitycsInitialized) return;
            analyticsApplicationTimerController.CapturateExitTime(Time.time);
            analyticsApplicationExitController.OnExitGameAnalyticsEvent(analyticsApplicationTimerController.GetApplicationUsedLifeTime());
        }
    }
}
