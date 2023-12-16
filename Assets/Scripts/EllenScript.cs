using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class EllenScript : MonoBehaviour

{
    public float movementSpeed = 2.0f;
    public float sprintSpeed = 5.0f;
    public float rotationSpeed = 0.2f;
    public float animationBlendSpeed = 0.2f;
    public float jumpSpeed = 7.0f;


    CharacterController controller;
    Animator animator;
    Camera characterCamera;
    float rotationAngle = 0.0f;
    float targetAnimationSpeed = 0.0f;
    bool isSprint = false;
    bool isDead = false;
    bool isSpawn = true;
    

    float speedY = 0.0f;
    float gravity = -9.81f;
    bool isJumping = false;

    public CharacterController Controller
    {
        get { return controller = controller ?? GetComponent<CharacterController>(); }
    }

    public Camera CharacterCamera
    {
        get { return characterCamera = characterCamera ?? FindObjectOfType<Camera>(); }
    }

   public Animator CharacterAnimator
    {
        get { return animator = animator ?? GetComponent<Animator>(); }
    }

    private void Start()
    {
       

        Invoke("SpawnStop", 2.30f);
    }


    void Update()
    {

        //Invoke("SpawnStop", 3.0f);


        float vertical = Input.GetAxis("Vertical");
            float horizontal = Input.GetAxis("Horizontal");


            if (Input.GetButtonDown("Jump") && !isJumping)
            {

                isJumping = true;
                CharacterAnimator.SetTrigger("Jump");
                speedY += jumpSpeed;
            }
            if (!Controller.isGrounded)
            {

                speedY += gravity * Time.deltaTime;

            }
            else if (speedY < 0.0f)
            {
                speedY = 0.0f;
            }

        CharacterAnimator.SetFloat("SpeedY", speedY / jumpSpeed);
        if (isJumping && speedY < 0.0f)
        {
            RaycastHit hit;

            if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f, LayerMask.GetMask("Default")))
            {
                isJumping = false;
                CharacterAnimator.SetTrigger("Land");
            }



        }



                isSprint = Input.GetKey(KeyCode.LeftShift);
                Vector3 movement = new Vector3(horizontal, 0.0f, vertical);
                Vector3 rotatedMovement = Quaternion.Euler(0.0f, CharacterCamera.transform.rotation.eulerAngles.y, 0.0f) * movement.normalized;
                Vector3 verticalMovement = Vector3.up * speedY;
                float currentSpeed = isSprint ? sprintSpeed : movementSpeed;
                
        if(!isSpawn && !isDead)
        {
            Controller.Move((verticalMovement + rotatedMovement * movementSpeed) * Time.deltaTime);
        }

                if (rotatedMovement.sqrMagnitude > 0.0f && !isDead)
                {
                    rotationAngle = Mathf.Atan2(rotatedMovement.x, rotatedMovement.z) * Mathf.Rad2Deg;
                    targetAnimationSpeed = isSprint ? 1.0f : 0.5f;
                }
                else
                {
                    targetAnimationSpeed = 0.0f;
                }

                CharacterAnimator.SetFloat("Speed", Mathf.Lerp(CharacterAnimator.GetFloat("Speed"), targetAnimationSpeed, animationBlendSpeed));
                Quaternion currentRotation = Controller.transform.rotation;
                Quaternion targetRotation = Quaternion.Euler(0.0f, rotationAngle, 0.0f);
                Controller.transform.rotation = Quaternion.Lerp(currentRotation, targetRotation, rotationSpeed);

            
        
        if(Input.GetKeyDown(KeyCode.E))
        {
            CharacterAnimator.SetTrigger("Death");
            isDead = true;
        }
       
        if(Input.GetMouseButtonDown(0))
        {
            int random = Random.Range(1, 4);
            CharacterAnimator.SetInteger("Combo", random);
           
            CharacterAnimator.SetTrigger("Fight");
           
        }
    }
    

    public void MeleeAttackStart()
    {

    }
    public void MeleeAttackEnd()
    {

    }
    private void SpawnStop()
    {
        isSpawn = false;
    }
   /* private void SpawnPlay()
    {
        isSpawn = true;
        CharacterAnimator.SetTrigger("Spawn");
    }*/
}
