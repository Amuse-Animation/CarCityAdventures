using AmuseEngine.Assets.Scripts.HelplerStaticClasses.LoadDataSerializer;
using CCA.CustomArgsStructObjects.MainMenuStruct.CharacterWorldButton;
using UnityEngine;

namespace CCA.MainMenuScripts.CharacterWorldButton.Controller
{
    public class CarCityAdventureCharacterWorldButtonController : MonoBehaviour
    {
        [SerializeField] 
        private SpriteRenderer worldButtonIconSpriteRenderer;
        
        [SerializeField]
        private CarCityAdventureCharacterWorldButtonDataArgsStruct characterWorldButtonDataArgs;
        
        public CarCityAdventureCharacterWorldButtonDataArgsStruct GetCharacterWorldButtonDataArgs()
        {
            return characterWorldButtonDataArgs;
        }

        public bool HasWorldContentBeenPreviouslyDownloaded()
        {
            return LoadDataSerializerStaticClass.HasSaveFileBeenCreated(characterWorldButtonDataArgs.TitleFileName, characterWorldButtonDataArgs.StorageFolderName);
        }

        public void ChangeWorldButtonIcon(Sprite newIcon)
        {
            worldButtonIconSpriteRenderer.sprite = newIcon;
        }
    }
}