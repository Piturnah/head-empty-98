using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GremlinControl : MonoBehaviour
{
    //Base Gremlin Control
    public float health = 3f;
    public enum GremlinTypes
    {
        Freeze,
        Headbutt,
        Explosion
    }
    public GremlinTypes gremlinType;
    public LayerMask obstacles;   
    private Vector3 vectorTowardsPlayer;
    private float timeBetweenMovements;
    private float timeLeft;
    public float speed = 5f;
    const float gremlinDrag = 0.005f;
    private float stopLimit;
    private float attackRange;
    private float timeBetweenAttacks;
    private float timeLeftTillAttack;
    Rigidbody rb;

    Animator anim;

    private void Start()
    {
        anim = transform.Find("model").GetComponent<Animator>();

        rb = GetComponent<Rigidbody>();
        timeLeft = timeBetweenMovements;
        if (gremlinType == GremlinTypes.Explosion)
        {
            timeBetweenMovements = 0.3f;
            stopLimit = 2f;
            attackRange = 2f;
            timeBetweenAttacks = 0f;
        }
        else if (gremlinType == GremlinTypes.Headbutt)
        {
            timeBetweenMovements = 3f;
            stopLimit = 2f;
            attackRange = 2f;
            timeBetweenAttacks = 1f;
        }
        else if (gremlinType == GremlinTypes.Freeze)
        {
            timeBetweenMovements = 6f;
            stopLimit = 10f;
            attackRange = 15f;
            timeBetweenAttacks = 5f;
        }
        timeLeftTillAttack = timeBetweenAttacks;
    }

    private void Update()
    {
        vectorTowardsPlayer = PlayerManagement.playerPositionLastFrame - transform.position;
        Vector3 velocity = vectorTowardsPlayer.normalized * speed;
        if (timeLeft < 0)
        {
            //Dash towards player if possible
            if (LineOfSightClear())
            {
                rb.velocity = velocity;            
            }
            timeLeft = timeBetweenMovements;
            FacePlayer();
        }
        else
        {
            timeLeft -= Time.deltaTime;
        }


        if (vectorTowardsPlayer.sqrMagnitude < attackRange * attackRange && timeLeftTillAttack < 0)
        {
            if(gremlinType == GremlinTypes.Explosion)
            {
                ExplosionAttack();
            }
            else if (gremlinType == GremlinTypes.Headbutt)
            {
                HeadbuttAttack();
            }
            else if (gremlinType == GremlinTypes.Freeze)
            {
                FreezeAttack();
            }
            timeLeftTillAttack = timeBetweenAttacks;
        }
        else
        {
            timeLeftTillAttack -= Time.deltaTime;
        }

        anim.SetFloat("velocity", velocity.magnitude);
    }

    private void FacePlayer()
    {
        transform.LookAt(new Vector3(PlayerManagement.playerPositionLastFrame.x, transform.position.y, PlayerManagement.playerPositionLastFrame.z), Vector3.up);
    }

    private void FixedUpdate()
    {
        if(rb.velocity.sqrMagnitude > speed/100f && vectorTowardsPlayer.sqrMagnitude > stopLimit * stopLimit)
        {
            rb.velocity -= Vector3.one * gremlinDrag;
        }
        else
        {
            rb.velocity = Vector3.zero;
            if (gremlinType == GremlinTypes.Freeze)
            {
                FacePlayer();
            }
        }

    }

    private void FreezeAttack()
    {

    }

    private void HeadbuttAttack()
    {

    }

    private void ExplosionAttack()
    {

    }

    private bool LineOfSightClear() 
    {
        Collider[] hits = Physics.OverlapBox(transform.position + (vectorTowardsPlayer/2),
            new Vector3(1f,0.1f,(vectorTowardsPlayer).magnitude),
            Quaternion.LookRotation(PlayerManagement.playerPositionLastFrame));
        return hits.Length == 0;
    }
}
