using UnityEngine;
using System.Collections;

public class PlayerLaser : MonoBehaviour {

    private LineRenderer lineRenderer;
    public int timer = 60;
    RaycastHit2D hit;
    int distance = 10;
    Collider2D playerColli;
    public int damage;
	int finalLayer;                         //layer mask for raycast;

    // Use this for initialization
    void Start () {
        playerColli = GameObject.FindGameObjectWithTag("Player").GetComponent<CircleCollider2D>();

		finalLayer = (1 << 8) | (1 << 11);            // layer mask for enemy and wall only

        lineRenderer = gameObject.GetComponent<LineRenderer>();
        lineRenderer.enabled = true;
        lineRenderer.useWorldSpace = true;
        lineRenderer.sortingLayerName = "Bullet";
        lineRenderer.material.color = Color.green;
        Ray2D ray = new Ray2D(transform.position, transform.up);

        lineRenderer.SetPosition(0, transform.position);

        playerColli.enabled = false;
		hit = Physics2D.Raycast(transform.position , Input.mousePosition - Camera.main.WorldToScreenPoint(GameObject.FindGameObjectWithTag("Player").transform.position), distance, finalLayer);
        playerColli.enabled = true;

        if (hit.collider)
        {
            lineRenderer.SetPosition(1, hit.point);
            if(hit.collider.tag == "Enemy")
            {
                EnemyTakeDamage EnemyDamageScript = hit.collider.gameObject.GetComponent<EnemyTakeDamage>();
                EnemyDamageScript.takeDamage(damage);
            }
        }
        else
            lineRenderer.SetPosition(1, ray.GetPoint(distance));
    }

    // Update is called once per frame
    void FixedUpdate () {
        timer--;
        if(timer <= 0)
        {
            lineRenderer.enabled = false;
        }


    }
}
