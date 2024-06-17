using InfimaGames.LowPolyShooterPack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class TestDummy : MonoBehaviour
{
	/// <summary>
	/// Movement. This is our main, and base, component that handles the character's movement.
	/// It contains all of the logic relating to moving, running, crouching, jumping...etc
	/// </summary>
	#region FIELDS SERIALIZED

	[Title(label: "Acceleration")]

	[Tooltip("How fast the character's speed increases.")]
	[SerializeField]
	private float acceleration = 9.0f;

	[Tooltip("Acceleration value used when the character is in the air. This means either jumping, or falling.")]
	[SerializeField]
	private float accelerationInAir = 3.0f;

	[Tooltip("How fast the character's speed decreases.")]
	[SerializeField]
	private float deceleration = 11.0f;

	[Title(label: "Speeds")]

	[Tooltip("The speed of the player while walking.")]
	[SerializeField]
	private float speedWalking = 4.0f;

	[Tooltip("How fast the player moves while aiming.")]
	[SerializeField]
	private float speedAiming = 3.2f;

	[Tooltip("How fast the player moves while aiming.")]
	[SerializeField]
	private float speedCrouching = 3.5f;

	[Tooltip("How fast the player moves while running."), SerializeField]
	private float speedRunning = 6.8f;

	[Title(label: "Walking Multipliers")]

	[Tooltip("Value to multiply the walking speed by when the character is moving forward."), SerializeField]
	[Range(0.0f, 1.0f)]
	private float walkingMultiplierForward = 1.0f;

	[Tooltip("Value to multiply the walking speed by when the character is moving sideways.")]
	[Range(0.0f, 1.0f)]
	[SerializeField]
	private float walkingMultiplierSideways = 1.0f;

	[Tooltip("Value to multiply the walking speed by when the character is moving backwards.")]
	[Range(0.0f, 1.0f)]
	[SerializeField]
	private float walkingMultiplierBackwards = 1.0f;

	[Title(label: "Air")]

	[Tooltip("How much control the player has over changes in direction while the character is in the air.")]
	[Range(0.0f, 1.0f)]
	[SerializeField]
	private float airControl = 0.8f;

	[Tooltip("The value of the character's gravity. Basically, defines how fast the character falls.")]
	[SerializeField]
	private float gravity = 1.1f;

	[Tooltip("The value of the character's gravity while jumping.")]
	[SerializeField]
	private float jumpGravity = 1.0f;

	[Tooltip("The force of the jump.")]
	[SerializeField]
	private float jumpForce = 100.0f;

	[Tooltip("Force applied to keep the character from flying away while descending slopes.")]
	[SerializeField]
	private float stickToGroundForce = 0.03f;

	[Title(label: "Crouching")]

	[Tooltip("Setting this to false will always block the character from crouching.")]
	[SerializeField]
	private bool canCrouch = true;

	[Tooltip("If true, the character will be able to crouch/un-crouch while falling, which can lead to " +
				"some slightly interesting results.")]
	[SerializeField, ShowIf(nameof(canCrouch), true)]
	private bool canCrouchWhileFalling = false;

	[Tooltip("If true, the character will be able to jump while crouched too!")]
	[SerializeField, ShowIf(nameof(canCrouch), true)]
	private bool canJumpWhileCrouching = true;

	[Tooltip("Height of the character while crouching.")]
	[SerializeField, ShowIf(nameof(canCrouch), true)]
	private float crouchHeight = 1.0f;

	[Tooltip("Mask of possible layers that can cause overlaps when trying to un-crouch. Very important!")]
	[SerializeField, ShowIf(nameof(canCrouch), true)]
	private LayerMask crouchOverlapsMask;

	[Title(label: "Rigidbody Push")]

	[Tooltip("Force applied to other rigidbodies when walking into them. This force is multiplied by the character's " +
				"velocity, so it is never applied by itself, that's important to note.")]
	[SerializeField]
	private float rigidbodyPushForce = 1.0f;

	private HudInfoHandler hudHandler;

	#endregion

	#region FIELDS

	/// <summary>
	/// Controller.
	/// </summary>
	private CharacterController controller;

	/// <summary>
	/// Player Character.
	/// </summary>
	private CharacterBehaviour playerCharacter;
	/// <summary>
	/// The player character's equipped weapon.
	/// </summary>
	private WeaponBehaviour equippedWeapon;

	/// <summary>
	/// Default height of the character.
	/// </summary>
	private float standingHeight;

	/// <summary>
	/// Velocity.
	/// </summary>
	private Vector3 velocity;

	/// <summary>
	/// Is the character on the ground.
	/// </summary>
	private bool isGrounded;
	/// <summary>
	/// Was the character standing on the ground last frame.
	/// </summary>
	private bool wasGrounded;

	/// <summary>
	/// Is the character jumping?
	/// </summary>
	private bool jumping;
	/// <summary>
	/// If true, the character controller is crouched.
	/// </summary>
	private bool crouching;

	/// <summary>
	/// Stores the Time.time value when the character last jumped.
	/// </summary>
	private float lastJumpTime;

	#endregion

	/// <summary>
	/// Moves the character.
	/// </summary>
	private void MoveCharacter()
	{
		//Get Movement Input!                       playerCharacter.GetInputMovement() camera direction
		Vector2 frameInput = Vector3.ClampMagnitude(playerCharacter.GetInputMovement(), 1.0f);
		//Calculate local-space direction by using the player's input.
		var desiredDirection = new Vector3(frameInput.x, 0.0f, frameInput.y);

		//Running speed calculation.
		if (playerCharacter.IsRunning())
			desiredDirection *= speedRunning;
		else
		{
			//Crouching Speed.
			if (crouching)
				desiredDirection *= speedCrouching;
			else
			{
				//Aiming speed calculation.
				if (playerCharacter.IsAiming())
					desiredDirection *= speedAiming;
				else
				{
					//Multiply by the normal walking speed.
					desiredDirection *= speedWalking;
					//Multiply by the sideways multiplier, to get better feeling sideways movement.
					desiredDirection.x *= walkingMultiplierSideways;
					//Multiply by the forwards and backwards multiplier.
					desiredDirection.z *=
						(frameInput.y > 0 ? walkingMultiplierForward : walkingMultiplierBackwards);
				}
			}
		}

		//World space velocity calculation.
		desiredDirection = transform.TransformDirection(desiredDirection);
		//Multiply by the weapon movement speed multiplier. This helps us modify speeds based on the weapon!
		if (equippedWeapon != null)
			desiredDirection *= equippedWeapon.GetMultiplierMovementSpeed();

		//Apply gravity!
		if (isGrounded == false)
		{
			//Get rid of any upward velocity.
			if (wasGrounded && !jumping)
				velocity.y = 0.0f;

			//Movement.
			velocity += desiredDirection * (accelerationInAir * airControl * Time.deltaTime);
			//Gravity.
			velocity.y -= (velocity.y >= 0 ? jumpGravity : gravity) * Time.deltaTime;
		}
		//Normal Movement On Ground.
		else if (!jumping)
		{
			//Update velocity with movement on the ground values.
			velocity = Vector3.Lerp(velocity, new Vector3(desiredDirection.x, velocity.y, desiredDirection.z), Time.deltaTime * (desiredDirection.sqrMagnitude > 0.0f ? acceleration : deceleration));
		}

		//Velocity Applied.
		Vector3 applied = velocity * Time.deltaTime;
		//Stick To Ground Force. Helps with making the character walk down slopes without floating.
		if (controller.isGrounded && !jumping)
			applied.y = -stickToGroundForce;

		//Move.
		controller.Move(applied);
	}
	
}
