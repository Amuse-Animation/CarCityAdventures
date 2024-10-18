using System.Collections.Generic;
using CCA.CustomArgsStructObjects.IAPIdArgs;
using CCA.IAP.Behaviour;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Purchasing;

namespace CCA.IAP.Controller
{
    public class IAPController : MonoBehaviour
    {
        [SerializeField] 
        private List<IAPIdArgsStruct> iapIdList;
        
        private IAPBehaviour iapBehaviour;

        // private void Awake()
        // {
        //     iapBehaviour = new IAPBehaviour();
        // }

        public UniTaskVoid InitIAP(IDetailedStoreListener listener)
        {
            if(iapBehaviour == null)
                iapBehaviour = new IAPBehaviour();
            
            return iapBehaviour.InitIAP(listener, iapIdList);
        }

        public void CacheIAPStoreData(IStoreController storeController, IExtensionProvider extensionProvider)
        {
            iapBehaviour.CacheIAPStoreData(storeController, extensionProvider);
        }
        
        public Product GetDesiredProduct(string iapId)
        {
            return iapBehaviour.GetDesiredProduct(iapId);
        }
    }
}