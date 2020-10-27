using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class RotateToMouseSystem : SystemBase
{
    protected override void OnUpdate()
    {
        // Assign values to local variables captured in your job here, so that it has
        // everything it needs to do its work when it runs later.
        // For example,
        //     float deltaTime = Time.DeltaTime;

        // This declares a new kind of job, which is a unit of work to do.
        // The job is declared as an Entities.ForEach with the target components as parameters,
        // meaning it will process all entities in the world that have both
        // Translation and Rotation components. Change it to process the component
        // types you want.

        Camera mainCam = Camera.main;
        float3 mousePos = Input.mousePosition;
        float3 forward = new float3(0, 0, 1);

        Entities.ForEach((ref Translation translation, ref Rotation rotation, in RotateToMouseComponent mouse) =>
        {
            // Implement the work to perform for each entity here.
            // You should only access data that is local or that is a
            // field on this job. Note that the 'rotation' parameter is
            // marked as 'in', which means it cannot be modified,
            // but allows this job to run in parallel with other jobs
            // that want to read Rotation component data.
            // For example,
            //     translation.Value += math.mul(rotation.Value, new float3(0, 0, 1)) * deltaTime;
            float3 screenPos = mainCam.WorldToScreenPoint(translation.Value);
            float3 dir = mousePos - screenPos;
            float angle = math.atan2(-dir.x, dir.y);

            rotation.Value = quaternion.AxisAngle(forward, angle);
        }).WithoutBurst().Run();
    }
}
