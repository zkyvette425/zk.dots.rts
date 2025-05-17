using RTS.Runtime.Component;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

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
            foreach (var unitPrepared in SystemAPI.Query<RefRO<UnitPreparedSelect>>().WithDisabled<UnitSelect>())
            {
                SystemAPI.GetComponentRW<LocalTransform>(unitPrepared.ValueRO.EffectTarget).ValueRW.Scale = unitPrepared.ValueRO.Scale;
            }
            
            foreach (var unitPrepared in SystemAPI.Query<RefRO<UnitPreparedSelect>>().WithAll<UnitSelect>())
            {
                SystemAPI.GetComponentRW<LocalTransform>(unitPrepared.ValueRO.EffectTarget).ValueRW.Scale = 0;
            }
            
            foreach (var unitPrepared in SystemAPI.Query<RefRO<UnitPreparedSelect>>().WithDisabled<UnitPreparedSelect>())
            {
                SystemAPI.GetComponentRW<LocalTransform>(unitPrepared.ValueRO.EffectTarget).ValueRW.Scale = 0;
            }
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }
}