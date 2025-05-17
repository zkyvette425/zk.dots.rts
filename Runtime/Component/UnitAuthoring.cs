using Unity.Collections;
using Unity.Entities;
using UnityEngine;

namespace RTS.Runtime.Component
{
    public class UnitAuthoring : MonoBehaviour
    {
        public string unitName;
        
        private class PlayerAuthoringBaker : Baker<UnitAuthoring>
        {
            public override void Bake(UnitAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity,new Unit()
                {
                    Name = authoring.unitName
                });
            }
        }
    }

    public struct Unit : IComponentData
    {
        public FixedString64Bytes Name;
    }
}