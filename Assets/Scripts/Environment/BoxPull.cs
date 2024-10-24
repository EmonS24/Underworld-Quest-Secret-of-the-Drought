using UnityEngine;

public class BoxPull : MonoBehaviour
{
    public float defaultMass;
    public float immovableMass;
    public bool beingPushed;
    float xPos;

    public Vector3 lastPos;

    void Start()
    {
        xPos = transform.position.x;
        lastPos = transform.position;
    }

    void FixedUpdate()
    {
        if (!beingPushed)
        {
            transform.position = new Vector3(xPos, transform.position.y);
        }
        else
        {
            xPos = transform.position.x;
        }
    }
}
