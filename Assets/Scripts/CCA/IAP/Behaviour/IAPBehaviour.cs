using System;
using System.Collections.Generic;
using CCA.CustomArgsStructObjects.IAPIdArgs;
using Cysharp.Threading.Tasks;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine.Purchasing;

namespace CCA.IAP.Behaviour
{
    public class IAPBehaviour
    {
        public bool IsIAPInitialized => isIAPInitialized;
        private bool isIAPInitialized;
        
        private IStoreController storeController;
        private IExtensionProvider extensionProvider;
        
        public async UniTaskVoid InitIAP(IDetailedStoreListener listener,List<IAPIdArgsStruct> iapIDList)
        {
            try
            {
                if(isIAPInitialized) return;
              
                InitializationOptions initializationOptions = new InitializationOptions()
                #if UNITY_EDITOR || DEVELOPMENT_BUILD
                                  .SetEnvironmentName("Test");
                #else
                                    .SetEnvironmentName("Production");
                #endif

                await UnityServices.InitializeAsync(initializationOptions);
              
                
                #if UNITY_ANDROID
                ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance(AppStore.GooglePlay));
                #elif UNITY_IOS
                ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance(AppStore.AppleAppStore));
                #endif
                
                 int iapIDListSize = iapIDList.Count;
                 for (int i = 0; i < iapIDListSize; i++)
                 {
                     builder.AddProduct(iapIDList[i].IAPId, iapIDList[i].IAPProductType);
                 }

                 UnityPurchasing.Initialize(listener, builder);
                 isIAPInitialized = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
        
        public void CacheIAPStoreData(IStoreController storeController, IExtensionProvider extensionProvider)
        {
            this.storeController = storeController;
            this.extensionProvider = extensionProvider;
        }

        public Product GetDesiredProduct(string iapId)
        {
            return storeController.products.WithStoreSpecificID(iapId);
        }
    }
}