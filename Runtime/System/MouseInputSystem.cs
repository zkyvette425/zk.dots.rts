using System;
using RTS.Runtime.Component;
using RTS.Runtime.Manager;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;
using Plane = UnityEngine.Plane;
using Ray = UnityEngine.Ray;

namespace RTS.Runtime.System
{
    public partial class MouseInputSystem : SystemBase 
    {
        protected override void OnStartRunning()
        {
            base.OnStartRunning();
            Selector.Instance.ClickUp += InstanceOnClickUp;
        }

        protected override void OnStopRunning()
        {
            base.OnStopRunning();
            Selector.Instance.ClickUp -= InstanceOnClickUp;
        }

        protected override void OnUpdate()
        {
            if (Selector.Instance.State == SelectState.Move || Selector.Instance.State == SelectState.SingleSelect)
            {
                RayAddPreparedSelect();
            }
            else
            {
                var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
                var query = new EntityQueryBuilder(Allocator.Temp).WithAll<LocalTransform>().WithPresent<UnitSelect>().Build(entityManager);
                var entities = query.ToEntityArray(Allocator.Temp);
                var transforms = query.ToComponentDataArray<LocalTransform>(Allocator.Temp);
                for (int i = 0; i < entities.Length; i++)
                {
                    var localTransform = transforms[i];
                    var entity = entities[i];
                    bool contains = Selector.Instance.Contains(localTransform.Position, Camera.main);
                    entityManager.SetComponentEnabled<UnitPreparedSelect>(entity,contains);
                }
            }
            
            if (Input.GetMouseButtonDown(1))
            {
                var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
                Plane plane = new Plane(math.up(),float3.zero);
                float2 position = float2.zero;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (plane.Raycast(ray, out float distance))
                {
                    var point = ray.GetPoint(distance);
                    position = new float2(point.x, point.z);
                }
                var query = new EntityQueryBuilder(Allocator.Temp).WithAll<TargetPosition,UnitSelect>().Build(entityManager);
                var entities = query.ToEntityArray(Allocator.Temp);
                var positions = GenerateTargetPositions(position,entities.Length);

                for (var index = 0; index < entities.Length; index++)
                {
                    var t = entities[index];
                    entityManager.SetComponentData(t, new TargetPosition { Position = positions[index] });
                }
            }
            
        }

        private void RayAddPreparedSelect()
        {
            var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            
            //remove
            {
                var query = entityManager.CreateEntityQuery(typeof(UnitPreparedSelect));
                foreach (var entity in query.ToEntityArray(Allocator.Temp))
                {
                    entityManager.SetComponentEnabled<UnitPreparedSelect>(entity,false);
                }
            }

            //add
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                var input = new RaycastInput
                {
                    Start = ray.GetPoint(0),
                    End = ray.GetPoint(1000),
                    Filter = new CollisionFilter()
                    {
                        BelongsTo = ~0u,
                        CollidesWith = ~0u,
                        GroupIndex = 0
                    }
                };
                var query = entityManager.CreateEntityQuery(typeof(PhysicsWorldSingleton));
                var physicsWorld = query.GetSingleton<PhysicsWorldSingleton>();
                if (physicsWorld.CastRay(input, out var hit))
                {
                    if (entityManager.HasComponent<Unit>(hit.Entity))
                    {
                        entityManager.SetComponentEnabled<UnitPreparedSelect>(hit.Entity,true);
                    }
                }
            }
        }
        
        private void InstanceOnClickUp(object sender, EventArgs e)
        {
            var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
            var unitPreparedSelectQuery = new EntityQueryBuilder(Allocator.Temp).WithAll<UnitPreparedSelect>().Build(entityManager);
            if (unitPreparedSelectQuery.IsEmpty)
            {
                return;
            }
                
            var selectedQuery = new EntityQueryBuilder(Allocator.Temp).WithAll<LocalTransform,UnitSelect>().Build(entityManager);
            {
                var entities = selectedQuery.ToEntityArray(Allocator.Temp);
                var transforms = selectedQuery.ToComponentDataArray<LocalTransform>(Allocator.Temp);
                for (var index = 0; index < entities.Length; index++)
                {
                    var entity = entities[index];
                    var localTransform = transforms[index];
                    if (!Selector.Instance.Contains(localTransform.Position, Camera.main))
                    {
                        entityManager.SetComponentEnabled<UnitSelect>(entity, false);
                    }
                }
            }
                
            {
                var entities = unitPreparedSelectQuery.ToEntityArray(Allocator.Temp);
                foreach (var entity in entities)
                {
                    entityManager.SetComponentEnabled<UnitPreparedSelect>(entity,false);
                    entityManager.SetComponentEnabled<UnitSelect>(entity,true);
                }
            }
        }

        private NativeArray<float2> GenerateTargetPositions(float2 position,int count)
        {
            var result = new NativeArray<float2>(count, Allocator.Temp);
            if (count == 0)
            {
                return result;
            }
            result[0] = position;
            if (count == 1)
            {
                return result;
            }

            const float ringSize = 2.2f;
            int ring = 0;
            int index = 1;

            while (index < count)
            {
                var ringPositionCount = 3 + ring * 2;
                for (int i = 0; i < ringPositionCount; i++)
                {
                    var angle = i * (math.PI2 / ringPositionCount);
                    var ringVector = math.rotate(quaternion.RotateY(angle), new float3(ringSize * (ring + 1),0, 0));
                    var ringPosition = position + ringVector.xz;
                    result[index] = ringPosition;
                    index++;
                    if (index > ringPositionCount)
                    {
                        break;
                    }
               
                }
                ring++;
               
            }
            return result;
        }
    }
}