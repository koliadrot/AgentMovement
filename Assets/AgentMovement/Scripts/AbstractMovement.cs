using UnityEngine;
using UnityEngine.AI;

namespace AgentMovement
{
    /// <summary>
    /// Абстрактная реализация движения
    /// </summary>
    //TODO:Добавить обертку Input
    [RequireComponent(typeof(LocomotionSimpleAgent))]
    public abstract class AbstractMovement : MonoBehaviour
    {
        protected NavMeshAgent agent;
        protected CameraMark cameraMark;
        protected virtual void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            cameraMark = FindObjectOfType<CameraMark>();
        }

        /// <summary>
        /// Перемещение
        /// </summary>
        protected abstract void Movement();

        private void Update() => Movement();
    }
}