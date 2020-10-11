using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class InputSystem : SystemBase
{
    protected override void OnUpdate()
    {
        float deltaTime = Time.DeltaTime;

        float2 movement = new float2();

        if (Input.GetKey(KeyCode.W))
        {
            movement += new float2(0, 1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            movement += new float2(-1, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement += new float2(0, -1);
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement += new float2(1, 0);
        }


        Entities.ForEach((ref Translation t, in PlayerComponent player) =>
        {
            t.Value += new float3(movement * deltaTime, 0) * player.moveSpeed;
        }).ScheduleParallel();
    }
}
