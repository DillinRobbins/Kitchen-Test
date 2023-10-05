using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private float moveSpeed = 7f;
    private IInteractable activeInteractable;

    private Dictionary<KeyCode, ICommand> moveKeyBindings = new();
    private Dictionary<KeyCode, ICommand> pressKeyBindings = new();

    KeyCode down = KeyCode.S;
    KeyCode up = KeyCode.W;
    KeyCode left = KeyCode.A;
    KeyCode right = KeyCode.D;
    KeyCode interact = KeyCode.Space;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();

        // Map input keys to commands.
        moveKeyBindings[up] = new CommandMoveUp(rb2D, moveSpeed);
        moveKeyBindings[down] = new CommandMoveDown(rb2D, moveSpeed);
        moveKeyBindings[left] = new CommandMoveLeft(rb2D, moveSpeed);
        moveKeyBindings[right] = new CommandMoveRight(rb2D, moveSpeed);
        pressKeyBindings[interact] = new CommandInteract(this, GetComponent<PlayerInventory>());
    }

    private void Update()
    {
        NoKeyPressedResetVelocity();
        GetKeysExecuteCommands();
        ClampDiagonalVelocity();
    }

    private void GetKeysExecuteCommands()
    {
        foreach (var kvp in moveKeyBindings)
        {
            if (Input.GetKey(kvp.Key))
            {
                kvp.Value.Execute();
            }
        }

        foreach(var kvp in pressKeyBindings)
        {
            if(Input.GetKeyDown(kvp.Key))
            {
                kvp.Value.Execute();
            }
        }
    }

    private void NoKeyPressedResetVelocity()
    {
        if (!Input.GetKey(left) && !Input.GetKey(right))
            rb2D.velocity = new Vector2(0, rb2D.velocity.y);

        if (!Input.GetKey(down) && !Input.GetKey(up))
            rb2D.velocity = new Vector2(rb2D.velocity.x, 0);
    }

    private void ClampDiagonalVelocity()
    {
        Vector2 currentVelocity = rb2D.velocity;

        // Calculate the current speed (magnitude of the velocity).
        float currentSpeed = currentVelocity.magnitude;

        // Check if the current speed exceeds the maximum speed.
        if (currentSpeed > moveSpeed)
        {
            // Clamp the velocity to the maximum speed while preserving the direction.
            rb2D.velocity = currentVelocity.normalized * moveSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        activeInteractable = collision.GetComponent<IInteractable>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        activeInteractable = null;
    }

    public IInteractable GetActiveInteractable()
    {
        return activeInteractable;
    }
}
