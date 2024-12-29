using UnityEngine;

[ExecuteInEditMode]
public class AutomaticTileTexture : MonoBehaviour
{
    void Start()
    {
        var renderer = GetComponent<Renderer>();
        var texture = new Material(renderer.sharedMaterial);
        texture.mainTextureScale = new Vector2(transform.localScale.x, transform.localScale.z);
        renderer.sharedMaterial = texture;
    }

    void Update()
    {
        if (transform.hasChanged && Application.isEditor && !Application.isPlaying)
        {
            var renderer = GetComponent<Renderer>();
            var texture = new Material(renderer.sharedMaterial);
            texture.mainTextureScale = new Vector2(transform.localScale.x, transform.localScale.z);
            renderer.sharedMaterial = texture;
        }
    }
}