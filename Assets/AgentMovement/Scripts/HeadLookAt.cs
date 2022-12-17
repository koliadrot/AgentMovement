namespace AgentMovement
{
    using System;
    using UnityEngine;
    using UnityEngine.AI;

    /// <summary>
    /// Корректирует анимацию головы при перемещении
    /// </summary>
    [RequireComponent(typeof(Animator), typeof(NavMeshAgent))]
    public class HeadLookAt : MonoBehaviour
    {
        [NonSerialized]
        public Vector3 LookAtTargetPosition;

        [SerializeField]
        private Transform head = null;
        [SerializeField]
        private float lookAtCoolTime = 0.2f;
        [SerializeField]
        private float lookAtHeatTime = 0.2f;
        [SerializeField]
        private bool looking = true;

        private Vector3 lookAtPosition = default;
        private Vector3 currentDirection = default;
        private Vector3 futureDirection = default;
        private Animator anim = default;
        private float lookAtWeight = default;
        private float lookAtTargetWeight = default;
        private float blendTime = default;
        private NavMeshAgent agent = default;

        private void Start()
        {
            if (!head)
            {
                Debug.LogError("Отсутсвует объект слежения!");
                enabled = false;
                return;
            }
            agent = GetComponent<NavMeshAgent>();
            anim = GetComponent<Animator>();
            LookAtTargetPosition = head.position + transform.forward;
            lookAtPosition = LookAtTargetPosition;
        }

        private void FixedUpdate() => LookAtToTarget();

        private void OnAnimatorIK()
        {
            LookAtTargetPosition.y = head.position.y;
            lookAtTargetWeight = looking ? 1.0f : 0.0f;

            currentDirection = lookAtPosition - head.position;
            futureDirection = LookAtTargetPosition - head.position;

            currentDirection = Vector3.RotateTowards(currentDirection, futureDirection, 6.28f * Time.deltaTime, float.PositiveInfinity);
            lookAtPosition = head.position + currentDirection;

            blendTime = lookAtTargetWeight > lookAtWeight ? lookAtHeatTime : lookAtCoolTime;
            lookAtWeight = Mathf.MoveTowards(lookAtWeight, lookAtTargetWeight, Time.deltaTime / blendTime);
            anim.SetLookAtWeight(lookAtWeight, 0.2f, 0.5f, 0.7f, 0.5f);
            anim.SetLookAtPosition(lookAtPosition);
        }

        private void LookAtToTarget() => LookAtTargetPosition = agent.steeringTarget + transform.forward;
    }

}