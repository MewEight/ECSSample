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

        InputComponent input = new InputComponent();
        FireBulletComponent fireBullet = new FireBulletComponent();

        if (Input.GetKey(KeyCode.W))
        {
            input.movement += new float2(0, 1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            input.movement += new float2(-1, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            input.movement += new float2(0, -1);
        }
        if (Input.GetKey(KeyCode.D))
        {
            input.movement += new float2(1, 0);
        }

        if (Input.GetMouseButtonDown(0))
        {
            fireBullet.fire = true;
        }

        Entities.ForEach((ref InputComponent player, ref FireBulletComponent fire) =>
        {
            player.movement = input.movement;
            fire.fire = fireBullet.fire;
        }).ScheduleParallel();
    }
}
