using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandMoveLeft : ICommand
{
    private Rigidbody2D rb2D;
    private float moveSpeed;

    public CommandMoveLeft(Rigidbody2D rb2D, float moveSpeed)
    {
        this.rb2D = rb2D;
        this.moveSpeed = moveSpeed;
    }

    public void Execute()
    {
        rb2D.velocity = new Vector2(-moveSpeed, rb2D.velocity.y);
    }
}
