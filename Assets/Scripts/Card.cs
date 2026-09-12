using UnityEngine;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour
{
	MeshRenderer mesh;
	Material mat;
	Color originalColor;
	void Start() { 
		mesh = GetComponent<MeshRenderer>();
		mat = mesh.material;
		originalColor = mat.color;
	}
	public void SetSelected() {
		mat.color = Color.blue;
	}
	public void SetUnSelected() {
		mat.color = originalColor;
	}

}