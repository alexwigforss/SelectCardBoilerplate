using UnityEngine;
using UnityEngine.InputSystem;
public class Cardpicker : MonoBehaviour {
	Vector3 mousePos;
	public static GameObject selectedCard = null;
	public static Rigidbody selectedRigidBody = null;
	public static Card selectedScript = null;
	RaycastHit prevhit = new RaycastHit();
	bool isDragging = false;
	private float distanceToTarget = 0f;
	public void OnLook(InputAction.CallbackContext context) {
		if (isDragging) {

			mousePos = Mouse.current.position.ReadValue();
			selectedRigidBody.useGravity = false;
			// TODO: Z poition is for distance from camera, When we rotate the camera , the distance to the target will change,
			// so we need to calculate the distance to the target (and or table) and use that instead of a fixed value.
			selectedRigidBody.position = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10));
			print(Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10)));
		}

		mousePos = Mouse.current.position.ReadValue();
		Ray ray = Camera.main.ScreenPointToRay(mousePos);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit)) {
			// print("Distance to target: " + hit.distance);
			// distanceToTarget = hit.distance;
			if (hit.colliderEntityId != prevhit.colliderEntityId) {
				if (hit.collider.CompareTag("Card")) {
					print("HIT: " + hit.transform.name);
					UnselectCardIfNotNull();
					selectedCard = hit.transform.gameObject;
					selectedRigidBody = selectedCard.GetComponent<Rigidbody>();
					selectedScript = selectedRigidBody.GetComponent<Card>();
					selectedScript.SetSelected();
				} else if (hit.collider.CompareTag("Table")) {
					UnselectCardIfNotNull();
				}
			}
			prevhit = hit;
		}
	}

	private static void UnselectCardIfNotNull() {
		if (selectedScript != null) {
			selectedScript.SetUnSelected();
			selectedScript = null;
			selectedRigidBody = null;
			selectedCard = null;
		}
	}

	public void OnPick(InputAction.CallbackContext context) {
		if (selectedRigidBody == null) return;
		if (context.started) {
			isDragging = true;
		}
		else if(context.canceled) {
			isDragging = false;
			selectedRigidBody.useGravity = true;
		}
	}
}
