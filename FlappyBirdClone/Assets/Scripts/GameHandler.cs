using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    private void Awake()
    {
        CreatePlr();
    }

    private void CreatePlr()
    {
        Vector3[] vertices = new Vector3[3];
        Vector2[] uv = new Vector2[3];
        int[] triangles = new int[3];

        //create every vertex point
        vertices[0] = new Vector3(0, 0);
        vertices[1] = new Vector3(0, 20);
        vertices[2] = new Vector3(20, 0);

        Vector2[] polConlliderPath = new Vector2[]
        {
            new Vector3(0, 0),
            new Vector3(0, 20),
            new Vector3(20, 0)
        };

        //uv only in range 0 - 1, represented by vertices, same index
        uv[0] = new Vector2(0, 0);
        uv[1] = new Vector2(0, 1);
        uv[2] = new Vector2(1, 0);

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        Mesh mesh = new Mesh();

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
        mesh.name = "PlrMesh";

        GameObject newGameObject = new GameObject("Plr", typeof(MeshFilter), typeof(MeshRenderer));
        newGameObject.transform.localScale = new Vector3(1, 1, 1);
        newGameObject.GetComponent<MeshFilter>().mesh = mesh;

        newGameObject.GetComponent<MeshRenderer>().material = new Material(Shader.Find("Sprites/Default"));
        newGameObject.GetComponent<MeshRenderer>().material.color = Color.red;

        newGameObject.AddComponent<Bird>();
        
        newGameObject.AddComponent<PolygonCollider2D>().SetPath(0, polConlliderPath);

        newGameObject.AddComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX;
        newGameObject.GetComponent<Rigidbody2D>().gravityScale = 35;
        newGameObject.GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Continuous;

        newGameObject.transform.localScale = new Vector3(.5f, .5f, 1);
        newGameObject.transform.SetParent(transform.root);
    }
}
