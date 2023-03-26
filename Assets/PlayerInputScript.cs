using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class PlayerInputScript : MonoBehaviour
{
	[Header("Character Input Values")]
	public Vector2 move;
	public Vector2 mouse;
	public bool dash;
	public bool grapple;



#if ENABLE_INPUT_SYSTEM
	public void OnMove(InputValue value)
	{
		MoveInput(value.Get<Vector2>());
	}

	public void OnLook(InputValue value)
	{
		MouseInput(value.Get<Vector2>());
	}
	public void OnDash(InputValue value)
    {
		DashInput(value.isPressed);
    }
	public void OnGrapple(InputValue value)
	{
		GrappleInput(value.isPressed);
	}
#endif

	public void MoveInput(Vector2 newMoveDirection)
	{
		move = newMoveDirection;
	}

	public void MouseInput(Vector2 newLookDirection)
	{
		mouse = newLookDirection;
	}

	public void DashInput(bool newDashState)
	{
		dash = newDashState;
	}

	public void GrappleInput(bool newGrappleState)
    {
		grapple = newGrappleState;
    }

	private void SetCursorState(bool newState)
	{
		Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
	}
}
