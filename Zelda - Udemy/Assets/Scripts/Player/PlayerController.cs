using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private CharacterController _controller;

    [Header("Config player")]
    public float movimentSpeed = 3f;
    private Vector3 _direction;

    [Header("Run")]
    public KeyCode keyRun = KeyCode.LeftShift;
    public float speedRun = 1.5f;

    [Header("Animation")]
    private Animator _animator;
    private bool isWalking;
    private bool isRunning;

   


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
        isWalking = true;
    }
    else
    {
        isWalking = false;
    }

    if (Input.GetKey(keyRun) && isWalking)
    {
        isRunning = true;
        isWalking = false;
    }
    else
    {
        isRunning = false;
    }

    _controller.Move(_direction * (isRunning ? speedRun : movimentSpeed) * Time.deltaTime);
    _animator.SetBool("isWalking", isWalking);
    _animator.SetBool("isRunning", isRunning);
}


    private void Attack()
    {
         if(Input.GetKeyDown(KeyCode.Space))
         {
             _animator.SetTrigger("Attack");
         }
    }



}
