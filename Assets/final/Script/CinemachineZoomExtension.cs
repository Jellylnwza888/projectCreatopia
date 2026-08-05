using UnityEngine;
using Cinemachine;

public class CinemachineZoomExtension : CinemachineExtension
{
    [Header("Zoom Settings")]
    public float zoomSpeed = 10f;
    public float minFOV = 10f;
    public float maxFOV = 60f;
    
    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage,
        ref CameraState state,
        float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Finalize)
        {
            float scrollInput = Input.mouseScrollDelta.y;
            if (scrollInput != 0)
            {
                // Modify FOV based on scroll input
                float newFOV = state.Lens.FieldOfView - (scrollInput * zoomSpeed);
                newFOV = Mathf.Clamp(newFOV, minFOV, maxFOV);
                state.Lens.FieldOfView = newFOV;
            }
        }
    }
}
