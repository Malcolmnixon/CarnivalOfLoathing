using UnityEngine;

public class ActionMaterial : MonoBehaviour
{
    [Tooltip("New material to set when activated")]
    public Material newMaterial;

    private Material _originalMaterial;

    private void Start()
    {
        _originalMaterial = GetComponent<Renderer>().material;
    }

    public void SetMaterial()
    {
        GetComponent<Renderer>().material = newMaterial;
    }

    public void RestoreMaterial()
    {
        GetComponent<Renderer>().material = _originalMaterial;
    }
}
