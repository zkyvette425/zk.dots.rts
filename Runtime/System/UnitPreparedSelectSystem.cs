using RTS.Runtime.Component;
using RTS.Runtime.Helper;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace RTS.Runtime.System
{
    public partial struct UnitPreparedSelectSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var selectEffect in SystemAPI.Query<RefRO<SelectEffect>>().WithAll<Ally>().WithDisabled<UnitSelect>())
            {
                SystemAPI.GetComponentRW<LocalTransform>(selectEffect.ValueRO.PreparedSelectTarget).ValueRW.Scale = selectEffect.ValueRO.Radius;
                SystemAPI.GetComponentRW<ChangeBaseColor>(selectEffect.ValueRO.PreparedSelectTarget).ValueRW.Color = Config.AllyColor.color2float4() * 0.6f;
            }
            
            foreach (var selectEffect in SystemAPI.Query<RefRO<SelectEffect>>().WithAll<Enemy>().WithDisabled<UnitSelect>())
            {
                SystemAPI.GetComponentRW<LocalTransform>(selectEffect.ValueRO.PreparedSelectTarget).ValueRW.Scale = selectEffect.ValueRO.Radius;
                SystemAPI.GetComponentRW<ChangeBaseColor>(selectEffect.ValueRO.PreparedSelectTarget).ValueRW.Color =
                    Config.EnemyColor.color2float4() * 0.6f;
            }
            
            foreach (var selectEffect in SystemAPI.Query<RefRO<SelectEffect>>().WithAll<UnitSelect>())
            {
                SystemAPI.GetComponentRW<LocalTransform>(selectEffect.ValueRO.PreparedSelectTarget).ValueRW.Scale = 0;
            }
            
            foreach (var selectEffect in SystemAPI.Query<RefRO<SelectEffect>>().WithDisabled<UnitPreparedSelect>())
            {
                SystemAPI.GetComponentRW<LocalTransform>(selectEffect.ValueRO.PreparedSelectTarget).ValueRW.Scale = 0;
            }
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {
            
        }
    }
}