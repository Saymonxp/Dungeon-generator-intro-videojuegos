using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1;

    [SerializeField]
    private Transform _shooter;
    
    private Rigidbody2D _rb;

    private float _inputMagnitude;
    private float _rotAngle;
    private float _AuxHorizontal;
    private float _AuxVertical;

    Animator _Animator;
    
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _Animator = gameObject.GetComponent<Animator>();
    }
    
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (horizontal != 0 || vertical != 0){
            _AuxHorizontal = horizontal;
            _AuxVertical = vertical;
        }
        
        Vector2 _dir  = new Vector2(_AuxHorizontal, _AuxVertical);
        _dir.Normalize(); //Direction


        transform.position = new Vector3(transform.position.x  + Time.deltaTime * _speed * horizontal, transform.position.y  + Time.deltaTime * _speed * vertical, transform.position.z);

        if (vertical == -1){
            _Animator.SetBool("Front", true);
        } else if ( vertical == 1){
            _Animator.SetBool("Back", true);
        }

        if (vertical == 0){
            _Animator.SetBool("Front", false);
            _Animator.SetBool("Back", false);
        }

        if (horizontal == -1){
            _Animator.SetBool("Left", true);
        } else if ( horizontal == 1){
            _Animator.SetBool("Right", true);
        }

        if (horizontal == 0){
            _Animator.SetBool("Left", false);
            _Animator.SetBool("Right", false);
        }

        //ShootPoint Rotation
        float angle = Mathf.Atan2(_dir.y, _dir.x) * Mathf.Rad2Deg - 90;
        
        _shooter.rotation = Quaternion.Euler(0,0,angle);
        
        
    }
    
    // private void FixedUpdate()
    // {
    //     _rb.velocity = transform.up * _speed * _inputMagnitude;
    // }
}
