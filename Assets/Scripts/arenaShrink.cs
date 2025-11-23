using UnityEngine;
using UnityEngine.UI;

public class arenaShrink : MonoBehaviour
{
    private Image img;
    private Material mat;
    private CircleCollider2D circle;
    private float size = 1f;

    public float shrink_halflife = 20f;
    private float t = 0f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        img = GetComponent<Image>(); 
        mat = img.material;
        mat.SetFloat("_borderRad", .05f);

        GameObject go = GameObject.Find("ArenaCollider"); // exact GameObject name
        if(go != null){
            circle = go.GetComponent<CircleCollider2D>();
        }
    }

    private float s(float t){
        return Mathf.Exp(-t * Mathf.Log(2f) / shrink_halflife);
    }
    // Update is called once per frame
    void Update(){
        t += Time.deltaTime;
        size = s(t);
        mat.SetFloat("_rad", size);
        circle.radius = size * Camera.main.orthographicSize;
    }

    private void OnTriggerExit2D(Collider2D other) {
        Debug.Log(other.name + " exited the collider!");
    }
 
    void OnDrawGizmos(){
        GameObject go = GameObject.Find("ArenaCollider"); // exact GameObject name
        if(go != null){
            circle = go.GetComponent<CircleCollider2D>();
        }
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + (Vector3)circle.offset, circle.radius);
    }
}
