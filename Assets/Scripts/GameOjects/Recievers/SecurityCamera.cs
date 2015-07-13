using UnityEngine;
using System.Collections;

public class SecurityCamera : BaseReceiver {

	public bool isScanning = false;
	public bool targetFound = false;
	public float FOV = 60.0f;
	public float range = 10;
	public float startAngle = 0.0f;
	public float endAngle = 90.0f;

	public Material normalMat;
	public Material alertedMat;

	public GameObject lens;

	Mesh mesh;
	public Vector3[] verts;
	public Vector2[] UVs;
	public int[] tris;

	public int numAngles = 10;

	// Use this for initialization
	void Start () {
		//mesh = new Mesh();
		mesh = lens.transform.FindChild("View").GetComponent<MeshFilter>().mesh;
		//verts = other.vertices;
		//UVs = mesh.uv;
		//tris = mesh.triangles;

		//lens.transform.FindChild("View").GetComponent<MeshFilter>().mesh = mesh;

		verts = new Vector3[numAngles + 2];
		UpdateViewMesh();
		Quaternion qu = new Quaternion();
		qu.eulerAngles = (-transform.rotation.eulerAngles);
		lens.transform.FindChild("View").localRotation = qu;

		mesh.vertices = verts;

		//UVs = new Vector2[verts.Length];
		//for (int i = 0; i < UVs.Length; i++)
		//{
		//	UVs[i] = new Vector2(verts[i].x, verts[i].y);
		//}
		//mesh.uv = UVs;
		//
		tris = new int[numAngles * 3];
		for (int i = 0; i < numAngles; i++)
		{
			tris[i * 3] = 0;
			tris[i * 3 + 1] = i + 1;
			tris[i * 3 + 2] = i + 2;
		}
		mesh.triangles = tris;
	}
	
	// Update is called once per frame
	void Update () {

		if (state == 0)
		{
			UpdateViewMesh();
			Quaternion qu = new Quaternion();
			qu.eulerAngles = (-transform.rotation.eulerAngles);
			lens.transform.FindChild("View").localRotation = qu;

			SwitchManager sw = GameObject.FindGameObjectWithTag("SwitchManager").GetComponent<SwitchManager>();

			if (Search(sw.FindHead().transform.FindChild("Head").gameObject) 
			    || Search(sw.FindTorso().transform.FindChild("Torso").gameObject) 
			    || Search(sw.FindBase().transform.FindChild("Base").gameObject))
			{
				if (!targetFound)
					Spot ();
			}
			else
			{
				if (targetFound)
					Lost();
			}
		}
		else
		{
			if (targetFound)
				Lost ();
		}
	}

	void Spot()
	{
		GameObject sound = GameObject.FindGameObjectWithTag("SoundEffectManager");
		sound.GetComponent<SoundEffectManager>().PlayCameraSnd();

		targetFound = true;

		GetComponent<MeshRenderer>().material = alertedMat;
	}

	void Lost()
	{
		targetFound = false;
		GetComponent<MeshRenderer>().material = normalMat;
	}

	bool Search(GameObject target)
	{
		Vector3 disp = (target.transform.position - lens.transform.position);
		if (disp.magnitude < range)
		{
			if (Vector3.Angle(transform.right, disp) < FOV / 2.0f)
			{
				RaycastHit2D hit = Physics2D.Raycast(lens.transform.position, disp.normalized);
				if (hit.collider.CompareTag("Frosty"))
					return true;
			}
		}
		return false;
	}


	// Updates the vertices of the view's mesh.
	void UpdateViewMesh()
	{
		//mesh.Clear();
		//return new Mesh();
		Vector2 dir1 = Vector2.Lerp(transform.right, transform.up, FOV / 180.0f);
		Vector2 dir2 = Vector2.Lerp(transform.right, -transform.up, FOV / 180.0f);

		//mesh = new Mesh();

		verts = new Vector3[numAngles + 2];
		verts[0] = Vector3.zero;

		for (int ler = 0; ler <= numAngles; ler++)
		{
			Vector2 dir = Vector2.Lerp(dir1, dir2, ler * 1.0f / numAngles);

			Vector3 vert;

			RaycastHit2D hit = Physics2D.Raycast(lens.transform.position, dir, range);
			if (hit.collider != null)
			{
				vert = new Vector3(hit.point.x, hit.point.y);
			}
			else
			{
				Ray2D ry = new Ray2D(lens.transform.position, dir);
				Vector2 ve = ry.GetPoint(range);
				vert = new Vector3(ve.x, ve.y);
			}

			vert -= new Vector3(lens.transform.position.x, lens.transform.position.y);

			verts[ler + 1] = vert;
		}

		mesh.vertices = verts;

		mesh.uv = new Vector2[numAngles + 2];

		tris = new int[numAngles * 3];
		for (int i = 0; i < numAngles; i++)
		{
			tris[i * 3] = 0;
			tris[i * 3 + 1] = i + 1;
			tris[i * 3 + 2] = i + 2;
		}
		mesh.triangles = tris;
		//return msh;

	}

	public override void Process()
	{
		state = (state == 0) ? 1 : 0;
		if (state == 0)
		{
			lens.transform.FindChild("View").gameObject.GetComponent<MeshRenderer>().enabled = true;
		}
		else
		{
			lens.transform.FindChild("View").gameObject.GetComponent<MeshRenderer>().enabled = false;
		}
	}
}
