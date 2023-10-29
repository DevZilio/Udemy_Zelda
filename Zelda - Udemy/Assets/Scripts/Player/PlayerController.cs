using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private CharacterController _controller;

    [Header("Config player")]
    public int HP;
    public float movimentSpeed = 3f;
    private Vector3 _direction;

    [Header("Run")]
    public KeyCode keyRun = KeyCode.LeftShift;
    public float speedRun = 1.5f;

    [Header("Animation")]
    private Animator _animator;
    private bool _isWalking;
    private bool _isRunning;

    [Header("Attack config")]
    public ParticleSystem fxAttack;
    public Transform hitBox;
    [Range(0.2f, 1f)]
    public float hitRange = 0.5f;
    public Collider[] hitInfo;
    public LayerMask hitMask;
    public float amountDamage;

    private bool _isAttacking;

   


    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Moviment();
        Attack();
    }

   private void Moviment()
{
    float horizontal = Input.GetAxis("Horizontal");
    float vertical = Input.GetAxis("Vertical");

    _direction = new Vector3(horizontal, 0f, vertical).normalized;

    if (_direction.magnitude > 0.1f)
    {
        // Identifica o ângulo e converte para radiano
        float targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg;
        // Utiliza o ângulo encontrado e faz o objeto virar
        transform.rotation = Quaternion.Euler(0, targetAngle, 0);
        _isWalking = true;
    }
    else
    {
        _isWalking = false;
    }

    if (Input.GetKey(keyRun) && _isWalking)
    {
        _isRunning = true;
        _isWalking = false;
    }
    else
    {
        _isRunning = false;
    }

    _controller.Move(_direction * (_isRunning ? speedRun : movimentSpeed) * Time.deltaTime);
    _animator.SetBool("isWalking", _isWalking);
    _animator.SetBool("isRunning", _isRunning);
}


    private void Attack()
    {
         if(Input.GetKeyDown(KeyCode.Space) && !_isAttacking)
         {
             _isAttacking = true;
             _animator.SetTrigger("Attack");
             fxAttack.Emit(1);

             hitInfo = Physics.OverlapSphere(hitBox.position, hitRange, hitMask);

             foreach(Collider c in hitInfo)
             {
                 c.gameObject.SendMessage("GetHit", amountDamage, SendMessageOptions.DontRequireReceiver);
             }

         }
    }

// Essa funcao sera chamada pelo script da animacao Attack (AttackIsDone)
     public void AttackIsDone()
    {
        _isAttacking = false;
             Debug.Log("Attack is Done");
    }


    private  void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hitBox.position, hitRange);   
    }


    void GetHit(int amount)
    {
        HP -= amount;
        if(HP > 0)
        {
            _animator.SetTrigger("Hit");
        }
        else
        {
            _animator.SetTrigger("Death");
        }
    }


private void OnTriggerEnter(Collider other)
{
    if(other.gameObject.tag == "TakeDamage")
    {
        GetHit(1);
    }
}

}
