
using UnityEngine;
using Mirror;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(CharacterController),typeof(PlayerRotate))]
public class player : NetworkBehaviour
{
    [HideInInspector]public CharacterController controller;
    private Vector3 walk_direktion;
    private Vector3 velocity;
    private float speed;
    private NetworkIdentity NetworkIdentity;

    private PlayerRotate _rotate;
    private PlayerRotate _rotateSmooth;
    private PlayerRotate _currentRotate;

    private audioHelper _audioHelper;

    private RulesHelper _rulesHelper;

    [SyncVar]
    private bool isRunning;

    [SyncVar]
    public float stamina;
    [SyncVar]
    public float maxStamina = 100f;
    [SyncVar]
    public float staminaRegenRate = 5f;
    [SyncVar]
    public float staminaDepletionRate = 10f;

    public float speed_walk;
    [SyncVar]
    public float speed_run;
    public float gravity;
    public float jump_force = 3.0f;
    public Animator animator;

    public Slider slider;




    private void Awake()
    {
        _rotate = GetComponents<PlayerRotate>()[0];
        _rotateSmooth = GetComponents<PlayerRotate>()[1];
        _audioHelper = transform.GetChild(0).GetComponent<audioHelper>();  

#if UNITY_EDITOR
        _currentRotate = _rotate;
#else
        _currentRotate = _rotateSmooth;

#endif
    }
    private void Start()
    {
        controller = GetComponent<CharacterController>();
        NetworkIdentity = GetComponent<NetworkIdentity>();
        Cursor.lockState = CursorLockMode.Locked;
        _rulesHelper = GetComponent<RulesHelper>();
        if (!NetworkIdentity.isLocalPlayer)
        {
            _rotate._cameraHolder.gameObject.SetActive(false);
            _rotateSmooth._cameraHolder.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        if (controller != null && NetworkIdentity.isLocalPlayer)
        {
            Cursor.lockState = Input.GetKeyDown(KeyCode.Mouse1) ? CursorLockMode.None : CursorLockMode.None;
            walk_direktion = transform.right * x + transform.transform.forward * y;
            _currentRotate.Rotate();
            Walk(walk_direktion);
            Run(Input.GetKey(KeyCode.LeftShift)&&stamina>=1);
            Sit(Input.GetKey(KeyCode.LeftControl));
            Emotion(Input.GetKey(KeyCode.C));
            DoGravity(controller.isGrounded);
            Jump(controller.isGrounded && Input.GetKey(KeyCode.Space));
            slider.value = stamina/100;
            if (!isRunning)
            {
                ModifyStamina(staminaRegenRate * Time.deltaTime);
            }
            if (Input.GetKeyDown(KeyCode.Escape)) 
            {
                SceneManager.LoadScene(0); 
            }


        }
    }
    private void Walk(Vector3 direction)
    {
        if (controller != null)
        {
            controller.Move(direction * speed * Time.deltaTime);
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)|| Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
            {
                ClearStates();
                animator.SetBool("isWalking", true);
            }
            
            else
            {
                ClearStates();
            }
            if(!controller.isGrounded)
            {
                speed = 3f;
            }
        }
    }
    private void DoGravity(bool isGrounded)
    {
        if (isGrounded && velocity.y < 0) { velocity.y = -1f; }
        velocity.y -= gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
    private void Jump(bool isGrounded)
    {
        if (isGrounded)
        {
            velocity.y = jump_force;
        }
    }

    private void Sit(bool can)
    {
        controller.height =can ? 2.7f : 3f;
        if (can&& (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))) { speed = speed_walk - 2f; ClearStates();
            animator.SetBool("isSit", true);
        }
    }
    private void Emotion(bool can)
    {
        if (can)
        {
            ClearStates(); animator.SetBool("isEmotion", true);
        }
    }
    private void ClearStates()
    {
        animator.SetBool("isWalking", false);
        animator.SetBool("isSit", false);
        animator.SetBool("isRunning", false);
        animator.SetBool("isEmotion", false);
    }
    private void Run(bool can)
    {

        if (can && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
        {
            ClearStates();
            animator.SetBool("isRunning", true);
            isRunning = true;
            ModifyStamina(-staminaDepletionRate * Time.deltaTime);
        }
        else
        {
            isRunning = false;
        }

        speed = can ? speed_run : speed_walk;
    }

    private void ModifyStamina(float amount)
    {
        stamina = Mathf.Clamp(stamina + amount, 0f, maxStamina);

        if (stamina <= 0f)
        {
            isRunning = false;
        }
    }
}
