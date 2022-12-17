namespace AgentMovement
{
    using UnityEngine;

    /// <summary>
    /// Метка камеры
    /// </summary>
    [RequireComponent(typeof(Camera))]
    public class CameraMark : MonoBehaviour
    {
        [SerializeField]
        private Camera cam = default;
        public Camera Camera => cam;
    }
}