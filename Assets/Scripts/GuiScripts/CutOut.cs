using UnityEngine;
using System.Collections;

public class CutOut : Abstract {

	Mesh mesh;
	float meshHeight;
	float uvHeight;
	
	private tk2dSprite _sprite = null;
	
	void Start(){
		mesh = (GetComponent(typeof(MeshFilter)) as MeshFilter).mesh;
		meshHeight = mesh.vertices[2].y-mesh.vertices[0].y;
		uvHeight = mesh.uv[2].x - mesh.uv[0].x;
	}
	
	/// <summary>
	/// Cuts out sprite from the top.
	/// </summary>
	/// <param name='progress'>
	/// possible values 0-1
	/// </param>
	public void CutOutTop(float progress){
		
		Vector3[] vertices = new Vector3[4];
		
		vertices[0].x = mesh.vertices[0].x;
		vertices[0].y = mesh.vertices[0].y;
		vertices[0].z = mesh.vertices[0].z;
		
		vertices[1].x = mesh.vertices[1].x;
		vertices[1].y = mesh.vertices[1].y;
		vertices[1].z = mesh.vertices[1].z;
		
		vertices[2].x = mesh.vertices[2].x;
		vertices[2].y = mesh.vertices[0].y+progress*meshHeight;
		vertices[2].z = mesh.vertices[2].z;
		
		vertices[3].x = mesh.vertices[3].x;
		vertices[3].y = mesh.vertices[0].y+progress*meshHeight;
		vertices[3].z = mesh.vertices[3].z;
		
		mesh.vertices = vertices;
		
		Vector2[] uv = new Vector2[4];
		
		uv[0].x = mesh.uv[0].x;
		uv[0].y = mesh.uv[0].y;
		
		uv[1].x = mesh.uv[1].x;
		uv[1].y = mesh.uv[1].y;
		
		uv[2].x = mesh.uv[0].x+progress*uvHeight;
		uv[2].y = mesh.uv[2].y;
		
		uv[3].x = mesh.uv[0].x+progress*uvHeight;
		uv[3].y = mesh.uv[3].y;

		mesh.uv = uv;
	}
	//хаки череваты последтсвиями, для таких вот CutOut цвет менять так
	public void SetColor(Color c){
		Color[] meshColors = new Color[4];
		SetColors(meshColors, c);
		mesh.colors = meshColors;
	}
	protected void SetColors(Color[] dest, Color c)
	{
		for (int i = 0; i < 4; ++i)
			dest[i] = c;
	}
}
