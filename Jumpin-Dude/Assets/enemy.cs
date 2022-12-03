using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the behavior of an on-screen enemy.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class enemy : MonoBehaviour
{
    /// <summary>
    /// How fast the enemy can accelerate
    /// </summary>
    public float EnginePower = 1;

    /// <summary>
    /// How close it tries to get to the player
    /// </summary>
    public float ApproachDistance = 5;

    /// <summary>
    /// How fast the orbs it fires should move
    /// </summary>
    public float OrbVelocity = 10;

    /// <summary>
    /// How heavy the orbs it fires should be
    /// </summary>
    public float OrbMass = .5f;

    /// <summary>
    /// Period the enemy should wait between shots
    /// </summary>
    public float CoolDownTime = 1;

    public float fire_time = 1;

    public float CooldownJump = 500;

    public float jump_time = 500;

    /// <summary>
    /// Prefab for the orb it fires
    /// </summary>

    public GameObject ArrowPrefab;
    public GameObject ExpPrefab;

    /// <summary>
    /// Transform from the player object
    /// Used to find the player's position
    /// </summary>
    private Transform player;

    /// <summary>
    /// Our rigid body component
    /// Used to apply forces so we can move around
    /// </summary>
    private Rigidbody2D rigidBody;

    /// <summary>
    /// Vector from us to the player
    /// </summary>
    private Vector2 OffsetToPlayer => player.position - transform.position;

    /// <summary>
    /// Unit vector in the direction of the player, relative to us
    /// </summary>
    private Vector2 HeadingToPlayer => OffsetToPlayer.normalized;

    /// <summary>
    /// Initialize player and rigidBody fields
    /// </summary>
    // ReSharper disable once UnusedMember.Local
    void Start()
    {
        player = FindObjectOfType<jumpindude>().transform;
        rigidBody = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Fire if it's time to do so
    /// </summary>
    // ReSharper disable once UnusedMember.Local
    void Update()
    {
        // TODO
        if (Time.time > fire_time)
        {

            fire_time = Time.time + CoolDownTime;
            Fire();
        }


    }

    /// <summary>
    /// Make a new orb, place it next to us, but shifted one unit in the direction of the player
    /// Set its mass to OrbMass and its velocity to OrbVelocity units per second, in the direction of the player
    /// </summary>
    private void Fire()
    {
        // TODO
        GameObject orb = Instantiate(ArrowPrefab, new Vector3(HeadingToPlayer.x + transform.position.x, HeadingToPlayer.y + transform.position.y, transform.position.z), Quaternion.identity);
        Rigidbody2D orb_body = orb.GetComponent<Rigidbody2D>();
        orb_body.velocity = HeadingToPlayer * OrbVelocity;
        orb_body.mass = OrbMass;
    }

    /// <summary>
    /// Accelerate in the direction of the player, unless we're nearby
    /// </summary>
    // ReSharper disable once UnusedMember.Local
    void FixedUpdate()
    {
        if (Time.time > jump_time)
        {

            jump_time = Time.time + CooldownJump;
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, 3);
        }

    }

    void OnBecameInvisible()
    {
        // TODO
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name.Equals("Arrow(Clone)"))
        {
            Destroy(gameObject);
            Scorekeeper.ScorePoints(1);
            GameObject exporb = Instantiate(ExpPrefab, transform.position, Quaternion.identity);
        }
    }

    /// <summary>
    /// If this is called, we're off screen, so give the player a point.
    /// </summary>
    // ReSharper disable once UnusedMember.Local

}