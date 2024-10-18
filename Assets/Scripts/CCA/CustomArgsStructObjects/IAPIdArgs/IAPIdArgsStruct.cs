using UnityEngine;
using UnityEngine.Purchasing;

namespace CCA.CustomArgsStructObjects.IAPIdArgs
{
    [System.Serializable]
    public struct IAPIdArgsStruct
    { 
        public string IAPId;
        public ProductType IAPProductType;
    }
}