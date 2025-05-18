using RTS.Runtime.Component;
using RTS.Runtime.Helper;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace RTS.Runtime.System
{
    public partial struct UnitSelectSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            foreach (var selectEffect in SystemAPI.Query<RefRO<SelectEffect>>().WithAll<Ally>())
            {
                SystemAPI.GetComponentRW<LocalTransform>(selectEffect.ValueRO.SelectTarget).ValueRW.Scale = selectEffect.ValueRO.Radius;
                SystemAPI.GetComponentRW<ChangeBaseColor>(selectEffect.ValueRO.SelectTarget).ValueRW.Color = Config.AllyColor.color2float4();
            }
            foreach (var selectEffect in SystemAPI.Query<RefRO<SelectEffect>>().WithAll<Enemy>())
            {
                SystemAPI.GetComponentRW<LocalTransform>(selectEffect.ValueRO.SelectTarget).ValueRW.Scale = selectEffect.ValueRO.Radius;
                SystemAPI.GetComponentRW<ChangeBaseColor>(selectEffect.ValueRO.SelectTarget).ValueRW.Color = Config.EnemyColor.color2float4();
            }
            
            foreach (var unitSelect in SystemAPI.Query<RefRO<SelectEffect>>().WithDisabled<UnitSelect>())
            {
                SystemAPI.GetComponentRW<LocalTransform>(unitSelect.ValueRO.SelectTarget).ValueRW.Scale = 0;
            }
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }
}