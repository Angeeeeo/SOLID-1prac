using UnityEngine;

public class FallingObject : MonoBehaviour
{
    private ICatchHandler handler;
    private float floorY;
    private Transform basket;
    private bool isBlue;

    public void Init(ICatchHandler handler, float floorY, Transform basket, bool isBlue)
    {
        this.handler = handler;
        this.floorY = floorY;
        this.basket = basket;
        this.isBlue = isBlue;
    }

    void Update()
    {
        if (transform.position.y < floorY - 1f)
            Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform == basket)
        {
            handler.RegisterCatch(isBlue);
            Destroy(gameObject);
        }
    }
}