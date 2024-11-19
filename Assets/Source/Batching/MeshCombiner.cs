using System.Collections.Generic;
using UnityEngine;

namespace Batching
{
    public class MeshCombiner : MonoBehaviour
    {
        [SerializeField] private List<MeshFilter> _sourceMeshFilters;
        [SerializeField] private MeshFilter _targetMeshFilter;

        [ContextMenu("Combine Meshes")]
        private void CombineMeshes()
        {
            var combine = new CombineInstance[_sourceMeshFilters.Count];

            for (var i = 0; i < _sourceMeshFilters.Count; i++)
            {
                combine[i].mesh = _sourceMeshFilters[i].sharedMesh;
                combine[i].transform = _sourceMeshFilters[i].transform.localToWorldMatrix;
            }

            var mesh = new Mesh();
            mesh.CombineMeshes(combine);
            _targetMeshFilter.mesh = mesh;
        }
    }
}