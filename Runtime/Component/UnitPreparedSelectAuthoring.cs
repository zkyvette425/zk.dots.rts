using System.Collections;
using Unity.Entities;
using UnityEngine;

namespace RTS.Runtime.Component
{
    public class UnitPreparedSelectAuthoring : MonoBehaviour
    {
        private class UnitPreparedSelectAuthoringBaker : Baker<UnitPreparedSelectAuthoring>
        {
            public override void Bake(UnitPreparedSelectAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<UnitPreparedSelect>(entity);
                SetComponentEnabled<UnitPreparedSelect>(entity,false);
            }
        }
    }

    public struct UnitPreparedSelect : IComponentData,IEnableableComponent
    {
    }
}