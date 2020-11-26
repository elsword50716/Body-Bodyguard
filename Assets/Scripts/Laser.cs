using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public bool isNewLaser;
    public ContactFilter2D enemy;
    public float damagePerSec;
    public float maxLenth;
    public LineRenderer lineRenderer;

    private BoxCollider2D boxCollider2D;
    private List<Collider2D> enemyInLaser;

    private void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        enemyInLaser = new List<Collider2D>();
        if (isNewLaser)
        {
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.useWorldSpace = false;
            lineRenderer.positionCount = 2;
            lineRenderer.SetPosition(0, Vector2.zero);
            lineRenderer.SetPosition(1, transform.InverseTransformPoint(transform.position + transform.right * maxLenth));
        }
    }

    private void Update()
    {
        if (!isNewLaser)
        {
            if (boxCollider2D.OverlapCollider(enemy, enemyInLaser) > 0)
            {
                foreach (var enemy in enemyInLaser)
                {
                    if (enemy.TryGetComponent<EnemyAI>(out var enemyAI))
                    {
                        enemyAI.GetDamaged(damagePerSec * Time.deltaTime);
                    }
                    if (enemy.TryGetComponent<EnemyLairAI>(out var enemyLair))
                    {
                        enemyLair.GetDamaged(damagePerSec * Time.deltaTime);
                    }
                }
            }
        }
        else
        {
            var hit = Physics2D.Raycast(transform.position, transform.right, maxLenth, enemy.layerMask);
            Debug.DrawLine(transform.position, transform.position + transform.right * maxLenth, Color.blue);
            if (hit.collider != null)
            {
                lineRenderer.SetPosition(1, transform.InverseTransformPoint(hit.point));
                if (hit.collider.TryGetComponent<EnemyAI>(out var enemyAI))
                {
                    enemyAI.GetDamaged(damagePerSec * Time.deltaTime);
                }
                if (hit.collider.TryGetComponent<EnemyLairAI>(out var enemyLair))
                {
                    enemyLair.GetDamaged(damagePerSec * Time.deltaTime);
                }
            }
            else
            {
                lineRenderer.SetPosition(1, transform.InverseTransformPoint(transform.position + transform.right * maxLenth));
            }
        }





    }
}
