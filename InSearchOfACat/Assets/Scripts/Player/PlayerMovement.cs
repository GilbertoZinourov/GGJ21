using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 2f, jumpForce = 3f;
    [SerializeField] private Animator playerBodyAnim, spriteAnim;
    [SerializeField] private LayerMask groundMask;
    [SerializeField] private Vector2 gravity = new Vector2(0, -9.8f);
    private Rigidbody2D _rb;
    private float _input;
    private bool _facingRight = true;
    public bool isHiding;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        gravity = Physics2D.gravity;
    }

    private void Update()
    {
        GetPlayerInput();
        GetJumpInPut();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void GetJumpInPut()
    {
        if (Input.GetKeyDown(KeyCode.W) &&
            Physics2D.CircleCast(transform.position, .5f, Vector2.down, .6f, groundMask))
        {
            //Debug.Log("Ground");
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if (_rb.velocity.y < 0)
        {
            Physics2D.gravity = gravity * 2;
        }
        else
        {
            if (Physics2D.gravity != gravity)
            {
                //Debug.Log("gravity norm");

                Physics2D.gravity = gravity;
            }
        }
    }

    private void MovePlayer()
    {
        if (Mathf.Abs(_input) > .2f)
        {
            isHiding = false;
            spriteAnim.SetBool("Hiding", false);
        }
        transform.position += new Vector3(_input * movementSpeed, 0, 0) * Time.deltaTime;
        CharacterRotation();
        DoWalkAnimation();
    }

    private void DoWalkAnimation()
    {
        spriteAnim.SetFloat("WalkInput", Mathf.Abs(_input));
    }

    private void CharacterRotation()
    {
        if (_input > 0 && !_facingRight)
        {
            _facingRight = true;
            playerBodyAnim.SetBool("FacingRight", _facingRight);
        }
        else
        {
            if (_input < 0 && _facingRight)
            {
                _facingRight = false;
                playerBodyAnim.SetBool("FacingRight", _facingRight);
            }
        }
    }

    private void GetPlayerInput()
    {
        _input = Input.GetAxisRaw("Horizontal");
    }

    public void Hide()
    {
        isHiding = true;
        spriteAnim.SetBool("Hiding", true);
    }
}