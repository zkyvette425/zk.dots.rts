using Unity.Entities;
using UnityEngine;

namespace RTS.Runtime.Component
{
    public class UnitSelectAuthoring : MonoBehaviour
    {
        private class UnitSelectAuthoringBaker : Baker<UnitSelectAuthoring>
        {
            public override void Bake(UnitSelectAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<UnitSelect>(entity);
                SetComponentEnabled<UnitSelect>(entity,false);
            }
        }
    }

    public struct UnitSelect : IComponentData, IEnableableComponent
    {
    }
}