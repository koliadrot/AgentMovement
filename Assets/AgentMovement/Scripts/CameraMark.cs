using UnityEngine;

namespace AgentMovement
{
    /// <summary>
    /// Метка камеры
    /// </summary>
    [RequireComponent(typeof(Camera))]
    public class CameraMark : MonoBehaviour
    {
        [SerializeField] private Camera cam;
        public Camera Camera => cam;
    }
}