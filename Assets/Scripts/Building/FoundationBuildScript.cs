using UnityEngine;
using System.Collections;

public class FoundationBuildScript : MonoBehaviour
{
    public Renderer BuildingRenderer;

    private Material _floorMaterial;
    private Material _rimXMaterial;
    private Material _rimZMaterial;
    private Material _baseXMaterial;
    private Material _baseZMaterial;

    private void Start()
    {
        _floorMaterial = BuildingRenderer.materials[0]; // x, y
        _rimXMaterial = BuildingRenderer.materials[1]; // x
        _rimZMaterial = BuildingRenderer.materials[2]; // y
        _baseXMaterial = BuildingRenderer.materials[3]; // x
        _baseZMaterial = BuildingRenderer.materials[4]; // y
    }

    public void RecalculatePosition(Vector3 point)
    {
        Vector3Int position = (point - transform.position).RoundToVectorInt();
        transform.position = position;
    }

    public void RecalculateHeight(float height)
    {
        Vector3Int position = new Vector3(transform.position.x, height, transform.position.z).RoundToVectorInt();
        transform.position = position;
    }

    public void RecalculateSize(Vector3 point)
    {
        Vector3Int size = (point - transform.position).RoundToVectorInt();
        size.y = 1;
        transform.localScale = size;

        _floorMaterial.SetVector("RepeatSize", new Vector4(size.x, size.z, 0, 0));
        _rimXMaterial.SetVector("RepeatSize", new Vector4(size.z, 1, 0, 0));
        _rimZMaterial.SetVector("RepeatSize", new Vector4(size.x, 1, 0, 0));
        _baseXMaterial.SetVector("RepeatSize", new Vector4(size.z, 1, 0, 0));
        _baseZMaterial.SetVector("RepeatSize", new Vector4(size.x, 1, 0, 0));
    }
}
