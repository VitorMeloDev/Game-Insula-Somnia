using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Animator anim;

    [Header("Config Player")]
    public float movementSpeed = 3f;
 
    [Header("Attack Config")]
    public int amountDmg;
    public Transform hitBox;
    [Range(0.2f, 1)]
    public float hitRange = 0.2f;
    public LayerMask hitMask;
    public Collider[] hitInfo;

    private Vector3 direction;
    private bool isWalk;
    
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }

    void FixedUpdate() 
    {
        Move();
    }

    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(direction.magnitude > 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0,targetAngle,0);
            isWalk = true;
        }
        else
        {
            isWalk = false;
        }

        controller.Move(direction * movementSpeed * Time.deltaTime);
        anim.SetBool("isWalk", isWalk);
    }

    void Attack()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            anim.SetTrigger("Attack");
            hitInfo = Physics.OverlapSphere(hitBox.position, hitRange, hitMask);

            foreach(Collider c in hitInfo)
            {
                c.gameObject.SendMessage("GetHit", amountDmg, SendMessageOptions.DontRequireReceiver);
            }
        }
    }

    private void OnDrawGizmosSelected() 
    {
        if(hitBox != null)
        {
            Gizmos.DrawWireSphere(hitBox.position, hitRange);
        }
    }
}
