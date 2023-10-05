using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandMoveUp : ICommand
{
    private Rigidbody2D rb2D;
    private float moveSpeed;

    public CommandMoveUp(Rigidbody2D rb2D, float moveSpeed)
    {
        this.rb2D = rb2D;
        this.moveSpeed = moveSpeed;
    }

    public void Execute()
    {
        rb2D.velocity = new Vector2(rb2D.velocity.x, moveSpeed);
    }
}
