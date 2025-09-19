using UnityEngine;

public class FallingObject : MonoBehaviour
{
    private GameController controller;
    private float floorY;
    private Transform basket;
    private bool isBlue;

    public void Init(GameController controller, float floorY, Transform basket, bool isBlue)
    {
        this.controller = controller;
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
            controller.RegisterCatch(isBlue);
            Destroy(gameObject);
        }
    }
}