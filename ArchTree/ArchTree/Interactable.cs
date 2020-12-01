using System;

namespace ArchTree
{
    public class Interactable
    {
        public Guid Guid { get; set; } = Guid.NewGuid();
        public virtual void GenerateCollisionMesh()
        {
            // Generates collision mesh
        }

        private void DropShadow()
        {
            // Drops shadow according to mesh
        }
    }
}