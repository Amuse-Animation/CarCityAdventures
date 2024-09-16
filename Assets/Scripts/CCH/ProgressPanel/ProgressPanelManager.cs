using AmuseEngine.Assets.Scripts.Tweens.ColourInterpolator.Controller.ImageColourInterpolator;
using AmuseEngine.Assets.Scripts.Tweens.Scaling.Controller.ScaleRectTransformAxis;
using System.Collections;
using CCH.Illness.Controller.Receiver;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.Events;

public class ProgressPanelManager : MonoBehaviour
{
    [SerializeField]
    private Transform instigator;
    [SerializeField]
    Camera canvasCamera;
    [SerializeField] float _TimeBeforeOpenMap = 2.5f;
    [SerializeField] float _TimeBeforeNewProgressUnlocked = 1.5f;
    [SerializeField] float _TimeBeforeCloseMap = 7f;
    [Space]
    [SerializeField] ScaleRectTransformController _OpenPanelScaler;
    [SerializeField] ScaleRectTransformController _ClosePanelScaler;
    [Space]
    [SerializeField] ScaleRectTransformController _OpenHudScaler;
    [SerializeField] ScaleRectTransformController _CloseHudScaler;
    [Space]
    [SerializeField] ImageColourInterpolatorController _BlurInInterpolator;
    [SerializeField] ImageColourInterpolatorController _BlurOutInterpolator;

    [Space]
    [SerializeField] ParticleSystem _PopPS;
    [Space]
    // The final size must be the aspect ratio simplified (not the pixel length)
    [SerializeField] ScaleRectTransformController[] _VehicleBounceScaler; // FireTruck, KidGreen, Police, RaceCar 
    [Space]
    [SerializeField] ScaleRectTransformController[] _VehicleHudBounceScaler; // FireTruck, KidGreen, Police, RaceCar 

    [Space]
    [SerializeField] RectTransform[] VfxPivotRT; // FireTruck, KidGreen, Police, RaceCar 
    [SerializeField] RectTransform[] VfxHudPivotRT; // FireTruck, KidGreen, Police, RaceCar 

    [SerializeField]
    private UnityEvent onMapOpen;
    [SerializeField]
    private VoidEvent onMapOpenVoidEvent;
    [SerializeField]
    private UnityEvent onMapClosed;
    [SerializeField]
    private VoidEvent onMapClosedVoidEvent;

    [SerializeField]
    private UnityEvent<MapsStickersData> onStickerPlacedOnMap;
    
    [SerializeField]
    private UnityEvent onAllStickersPlacedOnMap;

    [SerializeField]
    private VoidEvent onAllStickersPlacedOnMapVoidEvent;

    [SerializeField]
    private UnityEvent<MapsStickersData> onStickerPlacedOnHUD;

    [SerializeField]
    private UnityEvent onAllStickersPlacedOnHUD;

    [SerializeField]
    private VoidEvent onAllStickersPlacedOnHUDVoidEvent;


    int currentStickersPlaced = 0;
    int currentStickersPlacedOnHUD = 0;

    Coroutine _Coroutine;

    private void Start()
    {
        CloseMap();
    }

#if UNITY_EDITOR // Test
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            OpenMap();
        }
        else if (Input.GetKeyDown(KeyCode.P))
        {
            CloseMap();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            StartCoroutine(ShowProgressCoroutine(0));
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            StartCoroutine(ShowProgressCoroutine(1));
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            StartCoroutine(ShowProgressCoroutine(2));
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            StartCoroutine(ShowProgressCoroutine(3));
        }
    }
#endif

    public void ShowProgress(IllnessReceiverController illnessReceiverController)
    {

        if (_Coroutine != null)
        {
            Debug.LogWarning($"ShowProgressCoroutine is already working.");
            return;
        }

        _Coroutine = StartCoroutine(ShowProgressCoroutine(illnessReceiverController.AssignedIllness.OwnerID));
    }

    private IEnumerator ShowProgressCoroutine(int illnessOwner)
    {
        yield return new WaitForSeconds(_TimeBeforeOpenMap); // Wait for cam return

        OpenMap();

        yield return new WaitForSeconds(_TimeBeforeNewProgressUnlocked);

        // Show progress

        if (_VehicleBounceScaler.Length <= illnessOwner)
        {
            Debug.LogWarning("Owner not implemented or it is null");
        }
        else
        {
            _PopPS.Stop();
            _PopPS.transform.localScale = Vector3.one;
            _PopPS.transform.position = new Vector3(VfxPivotRT[illnessOwner].position.x, VfxPivotRT[illnessOwner].position.y, canvasCamera.nearClipPlane);

            _PopPS.Play();
            _VehicleBounceScaler[illnessOwner].DoScaleRectTransform();
        }

        currentStickersPlaced++;
        currentStickersPlaced = Mathf.Min(currentStickersPlaced, _VehicleBounceScaler.Length);
        onStickerPlacedOnMap.Invoke(new MapsStickersData(instigator, _VehicleBounceScaler.Length, currentStickersPlaced, illnessOwner));



        yield return new WaitForSeconds(_TimeBeforeCloseMap);

        CloseMap();
        
        if (currentStickersPlaced >= _VehicleHudBounceScaler.Length)
        {
            onAllStickersPlacedOnMap.Invoke();

            if (onAllStickersPlacedOnMapVoidEvent)
                onAllStickersPlacedOnMapVoidEvent.Raise();
        }

        yield return new WaitForSeconds(2f);

        _VehicleHudBounceScaler[illnessOwner].DoScaleRectTransform();
        _PopPS.Stop();
        _PopPS.transform.localScale = Vector3.one * 0.5f;
        _PopPS.transform.position = new Vector3(VfxHudPivotRT[illnessOwner].position.x, VfxHudPivotRT[illnessOwner].position.y, canvasCamera.nearClipPlane);
        _PopPS.Play();



        currentStickersPlacedOnHUD++;
        currentStickersPlacedOnHUD = Mathf.Min(currentStickersPlacedOnHUD, _VehicleHudBounceScaler.Length);
        onStickerPlacedOnHUD.Invoke(new MapsStickersData(instigator, _VehicleHudBounceScaler.Length, currentStickersPlacedOnHUD, illnessOwner));



        if (currentStickersPlacedOnHUD >= _VehicleHudBounceScaler.Length)
        {
            yield return new WaitForSeconds(2f);

            onAllStickersPlacedOnHUD.Invoke();

            if (onAllStickersPlacedOnHUDVoidEvent)
                onAllStickersPlacedOnHUDVoidEvent.Raise();
        }

        _Coroutine = null;
    }


    public void OpenMap()
    {
        _CloseHudScaler.DoScaleRectTransform();
        _OpenPanelScaler.DoScaleRectTransform();
        _BlurInInterpolator.DoInterpolateColour();
        // Stop controls
        onMapOpen.Invoke();
        if(onMapOpenVoidEvent != null)
            onMapOpenVoidEvent.Raise();
    }

    public void CloseMap()
    {
        
        _ClosePanelScaler.DoScaleRectTransform();
        _BlurOutInterpolator.DoInterpolateColour();
        _OpenHudScaler.DoScaleRectTransform();
        // Resume controls
        onMapClosed.Invoke();
        if(onMapClosedVoidEvent != null)
            onMapClosedVoidEvent.Raise();
    }

}


[System.Serializable]
public struct MapsStickersData
{
    public Transform Instigator => instigator;
    public int MaxStickersInMap => maxStickersInMap;
    public int CurrentAmountOfStickersPlaced => currentAmountOfStickersPlaced;
    public int StickerID => stickerID;

    private Transform instigator;
    private int maxStickersInMap;
    private int currentAmountOfStickersPlaced;
    private int stickerID;

    public MapsStickersData(Transform instigator, int maxStickersInMap, int currentAmountOfStickersPlaced, int stickerID)
    {
        this.instigator = instigator;
        this.maxStickersInMap = maxStickersInMap;
        this.currentAmountOfStickersPlaced = currentAmountOfStickersPlaced;
        this.stickerID = stickerID;
    }


}