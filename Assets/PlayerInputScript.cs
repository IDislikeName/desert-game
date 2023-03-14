using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class PlayerInputScript : MonoBehaviour
{
	[Header("Character Input Values")]
	public Vector2 move;
	public Vector2 mouse;



#if ENABLE_INPUT_SYSTEM
	public void OnMove(InputValue value)
	{
		MoveInput(value.Get<Vector2>());
	}

	public void OnLook(InputValue value)
	{
		MouseInput(value.Get<Vector2>());
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

	private void SetCursorState(bool newState)
	{
		Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
	}
}
