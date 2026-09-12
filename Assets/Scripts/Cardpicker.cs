using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public class Cardpicker : MonoBehaviour {
	Vector3 mousePos;
	public static GameObject selectedUnit = null;
	public static Card selectedData = null;
	RaycastHit prevhit = new RaycastHit();
	bool isDragging = false;
	public void OnLook(InputAction.CallbackContext context) {
		if (isDragging) {
			// Not yet implemented: Dragging logic can be added here if needed.
		}

		mousePos = Mouse.current.position.ReadValue();
		Ray ray = Camera.main.ScreenPointToRay(mousePos);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit)) {
			if (hit.colliderEntityId != prevhit.colliderEntityId) {
				if (hit.collider.CompareTag("Card")) {
					print("HIT: " + hit.transform.name);
					UnselectCardIfNotNull();
					selectedUnit = hit.transform.gameObject;
					selectedData = selectedUnit.GetComponent<Card>();
					selectedData.SetSelected();
				} else if (hit.collider.CompareTag("Table")) {
					UnselectCardIfNotNull();
				}
			}
			prevhit = hit;
		}
	}

	private static void UnselectCardIfNotNull() {
		if (selectedData != null) {
			selectedData.SetUnSelected();
		}
	}

	public void OnPick(InputAction.CallbackContext context) {
		if (selectedUnit == null) return;
		if (context.started) {
			isDragging = true;
		}
		else if(context.canceled) {
			isDragging = false;
		}
	}
}
