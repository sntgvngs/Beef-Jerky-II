using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floid : MonoBehaviour
{
    public Vector3 direction;
    public Vector3 intention;

    public FloidSpawner mama;

    static float speed = 3;
    static float jumpSpeed = 10;

    private static Rigidbody playerBody;
    private CharacterController controller;

    private bool m_PreviouslyGrounded;
    private bool m_Jumping;

    public Vector3 avoid;
    // Use this for initialization
    void Start()
    {
        if (playerBody == null)
            playerBody = GameObject.Find("FPSController").GetComponent<Rigidbody>();

        m_Jumping = false;

        controller = GetComponent<CharacterController>();

        direction = Random.insideUnitSphere * 4;
        avoid = Vector3.zero;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Detection playerDetector = collision.gameObject.GetComponent<Detection>();
        if (playerDetector != null)
        {
            // We hit the player!
            playerDetector.hp.Damage(1);
            mama.DestroyFloid(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_PreviouslyGrounded && controller.isGrounded)
        {
            direction.y = 0f;
            m_Jumping = false;
        }
        if (!controller.isGrounded && !m_Jumping && m_PreviouslyGrounded)
        {
            direction.y = 0f;
        }
        m_PreviouslyGrounded = controller.isGrounded;
    }

    private void FixedUpdate()
    {
        if (controller.isGrounded)
        {
            direction.y = jumpSpeed;
            m_Jumping = true;
        } else
        {
            direction += Physics.gravity * 3 * Time.fixedDeltaTime;
        }

        if (avoid.sqrMagnitude == 0)
        {
            intention = (playerBody.position - transform.position);
            if (intention.magnitude < 20)
            {
                intention.Normalize();
            }
            else
                intention = Vector3.zero;
            intention *= speed;
            direction.x += intention.x;
            direction.z += intention.z;
        }    
        else
        {
            intention = avoid;
            direction.x += intention.x * speed;
            direction.z += intention.z * speed;
        }

        Vector2 flatDir = new Vector2(direction.x, direction.z);
        if(flatDir.magnitude > 8)
        {
            direction.x = flatDir.normalized.x * 8;
            direction.z = flatDir.normalized.y * 8;
        }
        controller.Move(direction * Time.fixedDeltaTime);
        avoid = Vector3.zero;
    }
}
