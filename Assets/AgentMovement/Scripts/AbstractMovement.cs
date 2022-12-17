namespace AgentMovement
{
    using UnityEngine;
    using UnityEngine.AI;

    /// <summary>
    /// Абстрактная реализация движения
    /// </summary>
    //TODO:Добавить обертку Input
    [RequireComponent(typeof(NavMeshAgent))]
    public abstract class AbstractMovement : MonoBehaviour
    {
        protected NavMeshAgent agent = default;
        protected CameraMark cameraMark = default;

        protected virtual void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            cameraMark = FindObjectOfType<CameraMark>();
            if (cameraMark == null)
            {
                Debug.LogError($"Не найдена метка {nameof(CameraMark)} для камеры");
            }
        }

        /// <summary>
        /// Перемещение
        /// </summary>
        protected abstract void Movement();

        private void Update() => Movement();
    }
}