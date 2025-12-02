using UnityEngine;
using UnityEngine.InputSystem;

public class bool1 : MonoBehaviour
{

    void Start()
    {
        GetComponent<Camera_Movement>().enabled = false;
    }

    void Update()
    {
        if (Keyboard.current.spaceKey.isPressed)
        {
            GetComponent<Camera_Movement>().enabled = true;
        }
    }
}
