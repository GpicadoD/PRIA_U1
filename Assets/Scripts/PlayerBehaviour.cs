using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public float moveSpeed = 10f;
    // Movement, Camera Controls, and Collisions
    public float rotateSpeed = 75f;
    private float vInput;
    private float hInput;
    private Rigidbody _rb;

    public float JumpVelocity = 5f;
    private bool _isJumping;

    public float DistanceToGround = 0.1f;
    public LayerMask GroundLayer;
    private CapsuleCollider _col;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();

        _col = GetComponent<CapsuleCollider>();

    }

    // Update is called once per frame
    void Update()
    {
        vInput = Input.GetAxis("Vertical") * moveSpeed;
        hInput = Input.GetAxis("Horizontal") * rotateSpeed;
        /* 4
        this.transform.Translate(Vector3.forward * vInput *
        Time.deltaTime);
        this.transform.Rotate(Vector3.up * hInput * Time.deltaTime);
        */
        if (IsGrounded())
        {
            _isJumping |= Input.GetKey(KeyCode.J);
        }
        
    }

    void FixedUpdate()
    {
        // 2
        Vector3 rotation = Vector3.up * hInput;
        // 3
        Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime);
        // 4
        _rb.MovePosition(this.transform.position + this.transform.forward * vInput *
        Time.fixedDeltaTime);
        // 5
        _rb.MoveRotation(_rb.rotation * angleRot);
        if (_isJumping)
        {
            _rb.AddForce(Vector3.up * JumpVelocity, ForceMode.Impulse);
        }
        _isJumping = false;
    }

    private bool IsGrounded()
    {
        // 7
        Vector3 capsuleBottom = new Vector3(_col.bounds.center.x,
        _col.bounds.min.y, _col.bounds.center.z);
        // 8
        bool grounded = Physics.CheckCapsule(_col.bounds.center,
            capsuleBottom, DistanceToGround, GroundLayer,
            QueryTriggerInteraction.Ignore);
        return grounded;
    }


}
