using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SaveDuringPlay]
public class LockAxisCamera : CinemachineExtension
{
    [Tooltip("Axis to be locked")]
    public Axis LockAxis = Axis.Z;

    [Tooltip("Lock by this value")]
    public float Value = 10;

    public enum Axis { X, Y, Z }

    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (enabled && stage == CinemachineCore.Stage.Body)
        {
            var pos = state.RawPosition;

            switch (LockAxis)
            {
                case Axis.X:
                    pos.x = Value;
                    break;
                case Axis.Y:
                    pos.y = Value;
                    break;
                case Axis.Z:
                    pos.z = Value;
                    break;
            }
            state.RawPosition = pos;
        }
    }
}