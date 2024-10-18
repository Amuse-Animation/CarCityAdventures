using CCA.IAP.Controller;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;

namespace CCA.IAP.Manager
{
    public class IAPManager : MonoBehaviour, IDetailedStoreListener
    {
        [SerializeField]
        private IAPController iapController;

        private void OnEnable()
        {
            iapController.InitIAP(this).Forget();
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            Debug.LogError($"IAP Initialization failed: {error}");
        }

        public void OnInitializeFailed(InitializationFailureReason error, string message)
        {
            Debug.LogError($"IAP Initialization failed: {error} \n message: {message}");
        }

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
        {
            throw new System.NotImplementedException();
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            throw new System.NotImplementedException();
        }

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            print("IAP Initialized");
            iapController.CacheIAPStoreData(controller, extensions);
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
        {
            throw new System.NotImplementedException();
        }
        
        public Product DoGetDesiredProduct(string iapId)
        {
            return iapController.GetDesiredProduct(iapId);
        }
    }
}