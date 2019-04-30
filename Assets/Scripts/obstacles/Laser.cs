using System.Collections.Generic;
using System.Linq;
using behavior;
using UnityEngine;

namespace obstacles {
    [RequireComponent(typeof(LineRenderer))]
    public class Laser : MonoBehaviour {

        [SerializeField] private float hurtAmount = 5f;

        private LineRenderer lineRenderer;

        private void Awake() {
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.enabled = true;

            lineRenderer.SetPosition(0, transform.position);
        }

        private void Update() {
            var ray = new Ray(transform.position, transform.forward);

            float distance = 1000;
            if (Physics.Raycast(ray, out var hit, 1000)) {
                Life hitLife = hit.collider.gameObject.transform.GetComponent<Life>();
                if (hitLife)
                {
                    hitLife.lose(hurtAmount);
                }
                distance = hit.distance;
            }

            lineRenderer.SetPosition(1, ray.GetPoint(distance));
        }
    }
}