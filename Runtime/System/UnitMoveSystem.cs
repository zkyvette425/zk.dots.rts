using RTS.Runtime.Component;
using RTS.Runtime.Helper;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;

namespace RTS.Runtime.System
{
    public partial struct UnitMoveSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<TargetPosition>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            UnitMoveJob job = new UnitMoveJob()
            {
                DeltaTime = SystemAPI.Time.DeltaTime
            };
            job.ScheduleParallel();
            
            // foreach (var (transform, physicsVelocity,unitMove,targetPosition) in
            //          SystemAPI.Query<RefRW<LocalTransform>, RefRW<PhysicsVelocity>,RefRO<UnitMove>,RefRO<TargetPosition>>())
            // {
            //     
            // }
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }
    
    [BurstCompile]
    public partial struct UnitMoveJob : IJobEntity
    {
        public float DeltaTime;
        
        public void Execute(ref LocalTransform transform,ref PhysicsVelocity physicsVelocity, in UnitMove unitMove,in TargetPosition targetPosition)
        {
            var direction = targetPosition.Position.x0y() - transform.Position;
            if (math.lengthsq(direction) < 2)
            {
                physicsVelocity.Linear = float3.zero;
                physicsVelocity.Angular = float3.zero;
                return;
            } 
            direction = math.normalize(direction);
                
            transform.Rotation =math.slerp(transform.Rotation, quaternion.LookRotation(direction, math.up()),
                DeltaTime * unitMove.RotateSpeed);
                
            physicsVelocity.Linear = direction * unitMove.MoveSpeed;
            physicsVelocity.Angular = float3.zero;
        }
    }
}