using RTS.Runtime.Component;
using Unity.Burst;
using Unity.Entities;
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
            foreach (var unitSelect in SystemAPI.Query<RefRO<UnitSelect>>())
            {
                SystemAPI.GetComponentRW<LocalTransform>(unitSelect.ValueRO.EffectTarget).ValueRW.Scale = unitSelect.ValueRO.Scale;
            }
            
            foreach (var unitSelect in SystemAPI.Query<RefRO<UnitSelect>>().WithDisabled<UnitSelect>())
            {
                SystemAPI.GetComponentRW<LocalTransform>(unitSelect.ValueRO.EffectTarget).ValueRW.Scale = 0;
            }
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }
}