using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour
{
	MeshRenderer mesh;
	Material mat;
	Color originalColor;
	bool isSelected = false;
	void Start() { 
		mesh = GetComponent<MeshRenderer>();
		mat = mesh.material;
		originalColor = mat.color;
	}
	public void SetSelected() {
		isSelected = true;
		mat.color = Color.blue;
	}
	public void SetUnSelected() {
		isSelected = false;
		mat.color = originalColor;
	}

}