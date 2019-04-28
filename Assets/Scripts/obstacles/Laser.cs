using System.Collections;
using UnityEngine;

namespace obstacles {
    [RequireComponent(typeof(LineRenderer))]
    public class Laser : MonoBehaviour {
        private LineRenderer lineRenderer;
        private bool firing = true;

        private void Awake() {
            lineRenderer = GetComponent<LineRenderer>();
        }

        IEnumerator fireLaser() {
            lineRenderer.enabled = true;
            while (firing) {
                var ray = new Ray(transform.position, transform.forward);
                lineRenderer.SetPosition(0, ray.origin);
                lineRenderer.SetPosition(1, ray.GetPoint(100));
                yield return null;
            }

            lineRenderer.enabled = false;
        }
    }
}