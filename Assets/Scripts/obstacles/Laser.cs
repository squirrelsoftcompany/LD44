using System.Collections.Generic;
using System.Linq;
using behavior;
using UnityEngine;

namespace obstacles {
    [RequireComponent(typeof(LineRenderer))]
    public class Laser : MonoBehaviour {
        private LineRenderer lineRenderer;
        [SerializeField] private float hurtAmount = 1f;
        private Dictionary<Life, float> hitObjects;
        [SerializeField] private float hitRate = 0.5f;

        private void Awake() {
            lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.enabled = true;
            hitObjects = new Dictionary<Life, float>();
        }

        private void Update() {
            
            foreach (var key in hitObjects.Keys.ToList()) {
                hitObjects[key] = hitObjects[key] - Time.deltaTime;
                if (hitObjects[key] < 0) {
                    hitObjects.Remove(key);
                }
            }

            var ray = new Ray(transform.position, transform.forward);
            lineRenderer.SetPosition(0, ray.origin);
            if (Physics.Raycast(ray, out var hit, 100)) {
                lineRenderer.SetPosition(1, hit.point);
                var hitLife = hit.transform.GetComponent<Life>();

                if (hitLife && !hitObjects.ContainsKey(hitLife)) {
                    hitObjects[hitLife] = hitRate;
                    hitLife.lose(hurtAmount);
                }
            } else {
                lineRenderer.SetPosition(1, ray.GetPoint(100));
            }
        }
    }
}