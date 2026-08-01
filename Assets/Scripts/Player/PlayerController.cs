using System;
using System.Runtime.CompilerServices;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class PlayerController : Singleton<PlayerController>
{
    [SerializeField] private float moveSpeed = 1f;

    private PlayerInputSystem playerInput;
    private TrailRenderer trailRenderer;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidBody;
    private Animator animator;


    private float dashCooldownTime = 2f;
    private float dashDuration = 0.3f;

    private float dashMultiplier = 4f;
    
    private float dashStartTime = 0;
    private bool dashActive = false;

    
    private Vector2 movement;
    

    public PlayerInputSystem Input => playerInput;



    protected override void Awake()
    {
        base.Awake();

        playerInput = new PlayerInputSystem();
        spriteRenderer = GetComponent<SpriteRenderer>();
        trailRenderer = GetComponent<TrailRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }


    private void OnEnable()
    {
        playerInput?.Enable();
    }

    private void OnDisable()
    {
        playerInput?.Disable();
    }

    private void OnDestroy()
    {
        playerInput?.Dispose();
    }


    private void Update()
    {
        HandleInventory();
        PlayerInput();
        Dash();
    }

    private void HandleInventory()
    {
        bool needUpdate = false;
        bool isDelta = false;
        int slotIndex = 0;

        if (playerInput.Inventory.Slot1.WasPressedThisFrame())
        {
            slotIndex = 0;
            needUpdate = true;
        }
        if (playerInput.Inventory.Slot2.WasPressedThisFrame())
        {
            slotIndex = 1;
            needUpdate = true;
        }
        if (playerInput.Inventory.Slot3.WasPressedThisFrame())
        {
            slotIndex = 2;
            needUpdate = true;
        }
        if (playerInput.Inventory.Slot4.WasPressedThisFrame())
        {
            slotIndex = 3;
            needUpdate = true;
        }
        if (playerInput.Inventory.Slot5.WasPressedThisFrame())
        {
            slotIndex = 4;
            needUpdate = true;
        }

        if (playerInput.Inventory.PreviousSlot.WasPressedThisFrame())
        {
            isDelta = true;
            slotIndex = -1;
            needUpdate = true;
        }
        else if (playerInput.Inventory.NextSlot.WasPressedThisFrame())
        {
            isDelta = true;
            slotIndex = 1;
            needUpdate = true;
        }

        if (!needUpdate) { return; }
        
        EventBus<ActiveInventoryChanged>.Raise(new ActiveInventoryChanged(slotIndex, isDelta));
    }

    private void FixedUpdate()
    {
        SetPlayerFacingDirection();
        Move();
    }



    private void PlayerInput()
    {
        movement = playerInput.Player.Move.ReadValue<Vector2>();
        animator.SetFloat("MoveX", movement.x);
        animator.SetFloat("MoveY", movement.y);

    }


    private void Move()
    {
        rigidBody.MovePosition(rigidBody.position + movement * (moveSpeed * Time.fixedDeltaTime));
    }

    private void Dash()
    {
        bool dashReady = (Time.time - dashStartTime) > dashCooldownTime;

        if (dashReady)
        {
            if (playerInput.Player.Dash.WasPressedThisFrame())
            {
                trailRenderer.enabled = true;
                moveSpeed *= dashMultiplier;
                dashStartTime = Time.time;
                dashActive = true;
            }
        }

        if (dashActive && (Time.time - dashStartTime) > dashDuration)
        {
            trailRenderer.enabled = false;
            moveSpeed /= dashMultiplier;
            dashActive = false;
        }
    }

    private void SetPlayerFacingDirection()
    {
        Vector2 mousePosition = playerInput.Player.MouseLook.ReadValue<Vector2>();
        Vector2 playerPosition = Camera.main.WorldToScreenPoint(transform.position);
        
        spriteRenderer.flipX = mousePosition.x < playerPosition.x;
    }
}
