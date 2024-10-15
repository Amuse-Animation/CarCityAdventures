using System;
using AmuseEngine.Assets.Scripts.HelplerStaticClasses.AddressablesLoader;
using Cysharp.Threading.Tasks;

namespace CCA.AddressablesContent.Behaviour
{
    public class AddressablesContentBehaviour
    {
        public async UniTaskVoid InitAddressables()
        {
            try
            {
                await AddressablesLoaderStaticClass.InitAddressablesAsyncWithAwait();
            }
            catch (Exception exception)
            {
                
            }
        }
    }
}