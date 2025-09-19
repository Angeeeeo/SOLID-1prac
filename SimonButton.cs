
using UnityEngine;

public class SimonButton : MonoBehaviour
{
    public int buttonIndex;
    public Game2Controller controller;

    public void OnPressed()
	{
    controller.ButtonPressed(buttonIndex);
	}

}