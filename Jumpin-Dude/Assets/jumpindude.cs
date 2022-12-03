using UnityEngine;
using UnityEngine.Animations;


/// <summary>
/// Control the player on screen
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class jumpindude : MonoBehaviour
{
    /// <summary>
    /// Prefab for the orbs we will shoot
    /// </summary>
    public GameObject arrowPrefab;

    /// <summary>
    /// How fast our engines can accelerate us
    /// </summary>
    public float EnginePower = 1;

    /// <summary>
    /// How fast we turn in place
    /// </summary>
    public float RotateSpeed = 1;

    /// <summary>
    /// How fast we should shoot our orbs
    /// </summary>
    public float ArrowVelocity = 10;

    public float JumpVelocity = 10;

    public int arrow_count = 0;

    public int health_count = 3;

    public int level_count = 1;

    public Rigidbody2D body = null;
    /// <summary>
    /// Handle moving and firing.
    /// Called by Uniity every 1/50th of a second, regardless of the graphics card's frame rate
    /// </summary>
    // ReSharper disable once UnusedMember.Local
    void FixedUpdate()
    {
        Manoeuvre();
        MaybeFire();
    }

    /// <summary>
    /// Fire if the player is pushing the button for the Fire axis
    /// Unlike the Enemies, the player has no cooldown, so they shoot a whole blob of orbs
    /// </summary>
    void MaybeFire()
    {
        // TODO
        if (Input.GetAxis("Fire1") == 1 && arrow_count < level_count)
        {
            ShootArrow();
            arrow_count++;
        }
        else if (Input.GetAxis("Fire1") == 0)
        {
            arrow_count = 0;
        }
    }

    /// <summary>
    /// Fire one orb.  The orb should be placed one unit "in front" of the player.
    /// transform.right will give us a vector in the direction the player is facing.
    /// It should move in the same direction (transform.right), but at speed OrbVelocity.
    /// </summary>
    private void ShootArrow()
    {
        // TODO
        GameObject orb = Instantiate(arrowPrefab, transform.position + transform.right, Quaternion.identity);
        Rigidbody2D orb_body = orb.GetComponent<Rigidbody2D>();
        orb_body.velocity = transform.right * ArrowVelocity;
    }

    /// <summary>
    /// Accelerate and rotate as directed by the player
    /// Apply a force in the direction (Horizontal, Vertical) with magnitude EnginePower
    /// Note that this is in *world* coordinates, so the direction of our thrust doesn't change as we rotate
    /// Set our angularVelocity to the Rotate axis time RotateSpeed
    /// </summary>
    void Manoeuvre()
    {
        float horizontal = Input.GetAxis("Horizontal");
        //float vertical = Input.GetAxis("Vertical");

        if (Input.GetAxis("Fire2") == 1)
        {
            body.velocity = new Vector2(body.velocity.x, JumpVelocity);

        }


        //new Vector2(horizontal * EnginePower, vertical * EnginePower);
        body.AddForce(new Vector2(horizontal * EnginePower, 0));
    }

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
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
            health_count--;
            Health.SetHealth(health_count);
        }

        if (collision.collider.name.Equals("ExpOrb(Clone)"))
        {
            level_count++;
            LevelCounter.SetLevel(level_count);
        }

        if (health_count == 0)
        {
            Time.timeScale = 0;
        }
    }
}