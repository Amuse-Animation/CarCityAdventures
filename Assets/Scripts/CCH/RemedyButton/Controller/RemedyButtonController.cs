using AmuseEngine.Assets.Scripts.Swappers.Controller.SwipeBetweenTwoSprites;
using UnityEngine;

namespace CCH.RemedyButton.Controller
{
    public class RemedyButtonController : MonoBehaviour
    {
        [SerializeField] 
        private SwipeBetweenTwoSpritesController swipeBetweenTwoSpritesController;
        public void InitInteractionHandler(Sprite nonInteractedSpriteState, Sprite interactedSpriteState)
        {
            swipeBetweenTwoSpritesController.DoSetSprites(nonInteractedSpriteState, interactedSpriteState);
            swipeBetweenTwoSpritesController.DoActivateFirstSprite();

        }

        public void ShowButtonActivated()
        {
            swipeBetweenTwoSpritesController.DoActivateSecondSprite();
        }

    }

}