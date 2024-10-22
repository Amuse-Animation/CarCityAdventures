using AmuseEngine.Assets.Scripts.IAP.Manager;
using AmuseEngine.Assets.Scripts.InternetContent.AddressablesContent.Manager;
using AmuseEngine.Assets.Scripts.ScriptableVariables.UnityServicesData;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CCA.CustomScripts.ServicesInitialization
{
    public class CarCityAdventureServicesInitializationManager : MonoBehaviour
    {
        [SerializeField]
        private IAPManager iapManager;
        
        [SerializeField]
        private AddressableContentManager addressableContentManager;
        
        [SerializeField]
        private ScriptableVariableUnityServicesData scriptableVariableUnityServicesData;

        [SerializeField] 
        private string menuSceneName;


        public async void Awake()
        {
            scriptableVariableUnityServicesData.Value.IAPManager = iapManager;
            scriptableVariableUnityServicesData.Value.AddressableContentManager = addressableContentManager;
            addressableContentManager.InitAddressablesAsync().Forget();
        }

        private void OnEnable()
        {
            addressableContentManager.OnAddressableInitialized.AddListener(OnAddressableInitialized);
            iapManager.OnIAPInitialized.AddListener(OnServicesInitialized);
        }
        
        private void OnDisable()
        {
            addressableContentManager.OnAddressableInitialized.RemoveListener(OnAddressableInitialized);
            iapManager.OnIAPInitialized.RemoveListener(OnServicesInitialized);
        }
        
        private void OnAddressableInitialized()
        {
            AsyncOperation currentOperation = LoadScene("UIViewScene", LoadSceneMode.Additive);
            currentOperation.completed += x =>
            {
              iapManager.InitializeIAPSystem();  
            };
        }

        private void OnServicesInitialized()
        {
            iapManager.PreCalculateIfHasAnySubscriptionActiveOrBoughtLifeTimeProduct();
            LoadScene(menuSceneName, LoadSceneMode.Additive);
        }

        private AsyncOperation LoadScene(string sceneName, LoadSceneMode mode)
        {
            return SceneManager.LoadSceneAsync(sceneName, mode);
        }
    }
}