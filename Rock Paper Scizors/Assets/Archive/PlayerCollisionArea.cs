using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionArea : MonoBehaviour
{
    private CharacterForm characterForm;
    private bool isSetupDone;
    // Start is called before the first frame update
    void Start()
    {
        characterForm = gameObject.GetComponentInParent<CharacterForm>();
        isSetupDone = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isSetupDone)
        {
            if (collision.CompareTag("Player"))
            {
                Debug.Log($"I've collided with {collision.gameObject}.");
                characterForm.CurrentForm.OnTriggerEnter2D(collision);
            }
          //  Debug.Log($"I've triggered OnTriggerEnter2D on CurrentForm.");
        }
    }
}
