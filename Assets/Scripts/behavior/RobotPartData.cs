using UnityEngine;

namespace behavior {
    [CreateAssetMenu(menuName = "RobotPart", fileName = "New RobotPart")]
    public class RobotPartData: ScriptableObject {
        public Vector3 position;
        public Mesh mesh;
    }
}