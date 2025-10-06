using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Movement : MonoBehaviour
{
    private Collision coll;
    [HideInInspector]
    public Rigidbody2D rb;
    private AnimationScript anim;

    [Space]
    [Header("Stats")]
    public float speed = 10;
    public float jumpForce = 50;
    public float slideSpeed = 5;
    public float wallJumpLerp = 10;
    public float dashSpeed = 20;

    [Space]
    [Header("Gravity & Slide")]
    [Tooltip("Baseline gravity when not grabbing walls or dashing")]
    public float baseGravity = 3f;
    [Tooltip("Minimum downward speed while touching a wall and sliding")]
    public float minWallSlideSpeed = 2f;

    [Space]
    [Header("Booleans")]
    public bool canMove;
    public bool wallGrab;
    public bool wallJumped;
    public bool wallSlide;
    public bool isDashing;

    [Space]
    [Header("Moving Platform Follow (Layer-based)")]
    [Tooltip("勾选 Ground 等作为可跟随的平台层")]
    public LayerMask movingPlatformLayer;   // 在 Inspector 勾选 Ground
    private Transform currentPlatform;
    private Vector3 lastPlatformPos;
    private bool onMovingPlatform;
    private static bool InLayer(GameObject go, LayerMask mask)
        => (mask.value & (1 << go.layer)) != 0;

    [Space]
    [Header("Polish")]
    public ParticleSystem dashParticle;
    public ParticleSystem jumpParticle;
    public ParticleSystem wallJumpParticle;
    public ParticleSystem slideParticle;

    public float fallLength = 5f;

    private bool groundTouch;
    private bool hasDashed;

    public int side = 1;

    void Start()
    {
        coll = GetComponent<Collision>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<AnimationScript>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        float xRaw = Input.GetAxisRaw("Horizontal");
        float yRaw = Input.GetAxisRaw("Vertical");
        Vector2 dir = new Vector2(x, y);

        Walk(dir);
        anim.SetHorizontalMovement(x, y, rb.velocity.y);

        // Wall Grab toggle
        if (coll.onWall && Input.GetButton("Fire3") && canMove)
        {
            if (side != coll.wallSide)
                anim.Flip(side * -1);
            wallGrab = true;
            wallSlide = false;
        }

        if (Input.GetButtonUp("Fire3") || !coll.onWall || !canMove)
        {
            wallGrab = false;
            wallSlide = false;
        }

        // Gravity handling for grab / normal
        if (wallGrab && !isDashing)
        {
            if (y > 0.05f)
            {
                // climb (up while grabbing)
                rb.gravityScale = 0;
                if (x > .2f || x < -.2f)
                    rb.velocity = new Vector2(rb.velocity.x, 0);

                float speedModifier = 0.5f; // reduced climb speed
                rb.velocity = new Vector2(rb.velocity.x, y * (speed * speedModifier));
            }
            else
            {
                // slide (neutral or holding down while grabbing)
                rb.gravityScale = baseGravity;
                WallSlide();
            }
        }
        else
        {
            rb.gravityScale = baseGravity;
        }

        // Only slide when grabbing + stick neutral/down
        if (coll.onWall && !coll.onGround)
        {
            if (wallGrab && y <= 0f)
            {
                wallSlide = true;
                WallSlide();
            }
        }

        if (!coll.onWall || coll.onGround)
            wallSlide = false;

        // Jump
        if (Input.GetButtonDown("Jump"))
        {
            // 起跳时解除平台跟随（防止被平台继续“拖走”）
            onMovingPlatform = false;
            currentPlatform = null;

            anim.SetTrigger("jump");

            if (coll.onGround)
                Jump(Vector2.up, false);
            if (coll.onWall && !coll.onGround)
                WallJump();
        }

        // Dash
        if (Input.GetButtonDown("Fire1") && !hasDashed)
        {
            if (xRaw != 0 || yRaw != 0)
                Dash(xRaw, yRaw);
        }

        if (coll.onGround && !groundTouch)
        {
            GroundTouch();
        }

        if (!coll.onGround && !coll.onWall)
        {
            groundTouch = false;
        }

        // Sidedness
        if (x > 0)
            side = 1;
        if (x < 0)
            side = -1;

        // Particle while sliding
        if (wallSlide)
            SlideParticle(dir);
    }

    void FixedUpdate()
    {
        // 跟随平台的位移（基于 Layer）
        if (onMovingPlatform && currentPlatform != null)
        {
            const float platformMoveEps = 0.0001f;
            Vector3 delta = currentPlatform.position - lastPlatformPos;
            if (delta.sqrMagnitude > platformMoveEps)
            {
                rb.MovePosition(rb.position + (Vector2)delta);
            }
            lastPlatformPos = currentPlatform.position;
        }
    }

    void OnCollisionStay2D(Collision2D c)
    {
        // ① 必须在指定 Layer（如 Ground）
        if (!InLayer(c.collider.gameObject, movingPlatformLayer)) return;

        // ② 推荐只跟随 Kinematic 平台，避免把静止地面当“移动平台”
        var platRb = c.rigidbody;
        if (platRb != null && platRb.bodyType != RigidbodyType2D.Kinematic)
        {
            // 如果你想也跟随 Dynamic 地面，移除此 return
            return;
        }

        // ③ 仅当从上方踩住（法线向上）
        foreach (var contact in c.contacts)
        {
            if (contact.normal.y > 0.6f)
            {
                if (currentPlatform != c.collider.transform)
                {
                    currentPlatform = c.collider.transform;
                    lastPlatformPos = currentPlatform.position;
                }
                onMovingPlatform = true;
                return;
            }
        }
    }

    void OnCollisionExit2D(Collision2D c)
    {
        if (currentPlatform && c.collider.transform == currentPlatform)
        {
            currentPlatform = null;
            onMovingPlatform = false;
        }
    }

    private void GroundTouch()
    {
        groundTouch = true;
        hasDashed = false;
        isDashing = false;
        wallJumped = false;
        GetComponent<BetterJumping>().enabled = true;

        // particles
        jumpParticle.Play();
    }

    private void Dash(float x, float y)
    {
        if (!canMove) return;

        hasDashed = true;
        anim.SetTrigger("dash");
        dashParticle.Play();

        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2(x, y);

        rb.velocity += dir.normalized * dashSpeed;
        StartCoroutine(DashWait());
    }

    IEnumerator DashWait()
    {
        isDashing = true;
        GetComponent<BetterJumping>().enabled = false;
        rb.gravityScale = 0;

        yield return new WaitForSeconds(.3f);

        dashParticle.Stop();
        rb.gravityScale = baseGravity;
        GetComponent<BetterJumping>().enabled = true;
        wallJumped = false;
        isDashing = false;
    }

    IEnumerator GroundDash()
    {
        yield return new WaitForSeconds(.15f);
        if (coll.onGround)
            hasDashed = false;
    }

    private void WallJump()
    {
        if ((side == 1 && coll.onRightWall) || side == -1 && !coll.onRightWall)
        {
            side *= -1;
            anim.Flip(side);
        }

        StopCoroutine(DisableMovement(0));
        StartCoroutine(DisableMovement(.1f));

        Vector2 wallDir = coll.onRightWall ? Vector2.left : Vector2.right;

        Jump((Vector2.up / 1.5f + wallDir / 1.5f), true);

        wallJumpParticle.Play();
    }

    private void Jump(Vector2 dir, bool wall)
    {
        slideParticle.Stop();
        if (coll.onGround)
            rb.velocity = new Vector2(rb.velocity.x, 0);

        if (wall)
        {
            rb.velocity = new Vector2(0, 0);
            rb.velocity += dir * jumpForce * 1.15f;
        }
        else
        {
            rb.velocity += dir * jumpForce;
        }
    }

    IEnumerator DisableMovement(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }

    void WallSlide()
    {
        if (coll.wallSide != side)
            anim.Flip(side * -1);

        if (!canMove)
            return;

        bool pushingWall = false;
        if ((rb.velocity.x > 0 && coll.onRightWall) || (rb.velocity.x < 0 && coll.onLeftWall))
        {
            pushingWall = true;
        }

        float push = pushingWall ? 0 : rb.velocity.x;

        // Clamp Y to ensure we always go down at least min slide speed, but allow gravity to go faster
        float minSlide = Mathf.Max(minWallSlideSpeed, slideSpeed);
        float yVel = Mathf.Min(rb.velocity.y, -minSlide);
        rb.velocity = new Vector2(push, yVel);
    }

    private void Walk(Vector2 dir)
    {
        if (!canMove)
            return;

        if (wallGrab)
            return;

        if (!wallJumped)
        {
            rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);
        }
        else
        {
            rb.velocity = Vector2.Lerp(rb.velocity, (new Vector2(dir.x * speed, rb.velocity.y)), wallJumpLerp * Time.deltaTime);
        }
    }

    private void SlideParticle(Vector2 dir)
    {
        var main = slideParticle.main;

        if (wallSlide)
        {
            slideParticle.Play();
            main.startColor = Color.white;
        }
        else
        {
            main.startColor = Color.clear;
        }
    }

    int ParticleSide()
    {
        int particleSide = coll.onRightWall ? 1 : -1;
        return particleSide;
    }
}
