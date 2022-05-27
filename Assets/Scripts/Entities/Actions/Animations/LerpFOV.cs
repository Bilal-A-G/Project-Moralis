using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Lerp FOV", menuName = "FSM/Actions/Lerp FOV")]
public class LerpFOV : ActionBase
{
    public GenericReference<float> desiredFOV;
    public GenericReference<string> cameraKey;
    public GenericReference<float> zoomSpeed;

    [System.NonSerialized]
    Camera camera;

    [System.NonSerialized]
    float finalZoom;

    [System.NonSerialized]
    float zoomTime;

    [System.NonSerialized]
    bool debounce;

    public override void Execute(CachedObjectWrapper callingObjects)
    {
        camera = callingObjects.GetGameObjectFromCache(cameraKey).GetComponent<Camera>();

        if (debounce) return;

        finalZoom = camera.fieldOfView;
        debounce = true;
        zoomTime = 0;
    }

    public override void UpdateLoop(CachedObjectWrapper callingObject)
    {
        if (!debounce) return;

        zoomTime += Time.deltaTime * zoomSpeed.GetValue();

        if ((int)finalZoom <= (int)desiredFOV.GetValue() + 1 && (int)finalZoom >= (int)desiredFOV.GetValue() - 1)
        {
            debounce = false;
        }
        else
        {
            finalZoom = Mathf.Lerp(finalZoom, desiredFOV.GetValue(), zoomTime);
            camera.fieldOfView = finalZoom;
        }
    }
}
