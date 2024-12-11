using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    private float startPos, length;
    public GameObject cam;
    public float parallaxEffect;
    public float cloudSpeed;

    private bool isCloud;
    private float cloudOffset = 0f;

    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;

        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (renderer != null && renderer.sortingLayerName == "Cloud")
        {
            isCloud = true;
        }
    }

    void FixedUpdate()
    {
        float distance = cam.transform.position.x * parallaxEffect;
        float movement = cam.transform.position.x * (1 - parallaxEffect);

        transform.position = new Vector3(startPos + distance + cloudOffset, transform.position.y, transform.position.z);

        if (movement > startPos + length)
        {
            startPos += length;
        }
        else if (movement < startPos - length)
        {
            startPos -= length;
        }
        
        if (isCloud)
        {
            cloudOffset += Time.deltaTime * cloudSpeed;

            if (cloudOffset > length)
            {
                cloudOffset -= length;
            }
            else if (cloudOffset < -length)
            {
                cloudOffset += length;
            }
        }
    }
}
