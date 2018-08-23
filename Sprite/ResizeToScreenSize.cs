using UnityEngine;
using System.Collections;

public class ResizeToScreenSize : MonoBehaviour
{
    public bool KeepAspectRatio = true;

    void Awake()
    {
        var sr = GetComponent<SpriteRenderer>();
        if (sr == null) return;

        transform.localScale = new Vector3(1, 1, 1);

        var width = sr.sprite.bounds.size.x;
        var height = sr.sprite.bounds.size.y;
        var worldScreenHeight = Camera.main.orthographicSize * 2.0;
        var worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        if(KeepAspectRatio)
        {
            var scale = Mathf.Max((float)(worldScreenWidth / width), (float)(worldScreenHeight / height));
            transform.localScale = new Vector3(scale, scale, 1.0f);
        }
        else
        {
            transform.localScale = new Vector3((float)(worldScreenWidth / width), (float)(worldScreenHeight / height), 1.0f);
        }
    }
}
