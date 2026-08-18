using UnityEngine;
using System.Collections;

public class Controller : MonoBehaviour
{
    // Player animation
    private Animator animate;

    // Player Animation Hashes
    private int WalkingHash;
    private int RunningHash;
    private int WalkingBackHash;
    private int WalkingLeftHash;
    private int WalkingRightHash;
    private int JumpingHash;

    private int AimingHash;
    private int FiringHash;
    private int BowWalkingHash;
    private int BowEquippingHash;
    private int BowUnequippingHash;

    private int SwordEquippingHash;
    private int SwordUnequippingHash;
    // private int Slashing1Hash;
    // private int Slashing2Hash;
    // private int Slashing3Hash;

    // Weapon handling
    private bool isWeaponEquipped = false;

    void Start()
    {
        animate = GetComponent<Animator>();
        if (animate == null) { Debug.LogError("Animator component not found!"); return; }

        // Initialize hashes
        WalkingHash = Animator.StringToHash("IsWalking");
        RunningHash = Animator.StringToHash("IsRunning");
        WalkingBackHash = Animator.StringToHash("IsWalkingBack");
        WalkingLeftHash = Animator.StringToHash("IsWalkingLeft");
        WalkingRightHash = Animator.StringToHash("IsWalkingRight");
        JumpingHash = Animator.StringToHash("IsJumping");

        AimingHash = Animator.StringToHash("IsAiming");
        FiringHash = Animator.StringToHash("IsFiring");
        BowEquippingHash = Animator.StringToHash("IsBowE");
        BowUnequippingHash = Animator.StringToHash("IsBowU");

        SwordEquippingHash = Animator.StringToHash("IsSwordE");
        SwordUnequippingHash = Animator.StringToHash("IsSwordU");
        // Slashing1Hash = Animator.StringToHash("IsSlashing1");
        // Slashing2Hash = Animator.StringToHash("IsSlashing2");
        // Slashing3Hash = Animator.StringToHash("IsSlashing3");
    }

    void Update()
    {
        HandleMovement();
        HandleActions();
    }

    void HandleMovement()
    {
        bool walk = Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow);
        bool run = Input.GetKey("w") && Input.GetKey(KeyCode.LeftShift);
        bool walkback = Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow);
        bool walkL = Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow);
        bool walkR = Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow);
        bool jump = Input.GetKeyDown(KeyCode.Space);
        bool bowwalk = (walk || walkback || walkL || walkR) && Input.GetMouseButton(1);

        animate.SetBool(WalkingHash, walk);
        animate.SetBool(RunningHash, run);
        animate.SetBool(WalkingBackHash, walkback);
        animate.SetBool(WalkingLeftHash, walkL);
        animate.SetBool(WalkingRightHash, walkR);
        animate.SetBool(JumpingHash, jump);    
    }

    void HandleActions()
    {
        bool punch = Input.GetMouseButtonDown(0);
        bool kick = Input.GetMouseButtonDown(1);

        bool swordequip = Input.GetKeyDown(KeyCode.Alpha1);
        bool swordunequip = Input.GetKey(KeyCode.Alpha1);
        // bool slash1 = Input.GetMouseButtonDown(0);
        // bool slash2 = Input.GetMouseButtonDown(0);
        // bool slash3 = Input.GetMouseButton(0);

        bool aim = Input.GetMouseButton(1);
        bool fire = Input.GetMouseButtonDown(0);
        bool bowequip = Input.GetMouseButton(1);
        bool bowunequip = Input.GetMouseButtonUp(1);

        // Sword equip
        animate.SetBool(SwordEquippingHash, swordequip);
        animate.SetBool(SwordUnequippingHash, swordunequip);

        // Bow equip
        animate.SetBool(BowEquippingHash, bowequip);
        animate.SetBool(BowUnequippingHash, bowunequip);

        // Combat animations
        // animate.SetBool(Slashing1Hash, slash1);
        // animate.SetBool(Slashing2Hash, slash2);
        // animate.SetBool(Slashing3Hash, slash3);

        // Aiming
        animate.SetBool(AimingHash, aim);

        // Firing
        animate.SetBool(FiringHash, fire);
    }
}