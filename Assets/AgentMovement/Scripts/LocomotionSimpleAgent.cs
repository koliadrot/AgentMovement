﻿namespace AgentMovement
{
    using UnityEngine;
    using UnityEngine.AI;

    /// <summary>
    /// Перемещение объекта с аниматоров за агентом
    /// </summary>
    [RequireComponent(typeof(Animator), typeof(NavMeshAgent))]
    public class LocomotionSimpleAgent : MonoBehaviour
    {
        [SerializeField]
        private float speed = 1f;
        [SerializeField]
        private float offsetAgentPosition = 0.9f;

        private Animator anim = default;
        private NavMeshAgent agent = default;
        private Vector2 smoothDeltaPosition = Vector2.zero;
        private Vector2 velocity = Vector2.zero;
        private Vector2 deltaPosition = Vector2.zero;
        private Vector3 worldDeltaPosition = Vector2.zero;
        private Vector3 rootPosition = Vector2.zero;
        private float smooth = default;
        private bool isCanMove = default;

        private const float MIN_MOVE_DISTANCE = 0.5f;
        private const float MIN_SMOOTH = 1f;
        private const float RATE_SMOOTH = 0.15f;
        private const float BLIND_TIME = 1e-5f;

        //TODO:подключить адаптер анимаций
        private const string MOVE = "move";
        private const string VELOCITY_X = "velx";
        private const string VELOCITY_Y = "vely";

        private void Start()
        {
            anim = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();
            agent.updatePosition = false;
            agent.speed = speed;
        }

        private void FixedUpdate() => Locomotion();

        private void OnAnimatorMove() => UpdateAgent();

        private void Locomotion()
        {
            worldDeltaPosition = agent.nextPosition - transform.position;

            deltaPosition.x = Vector3.Dot(transform.right, worldDeltaPosition);
            deltaPosition.y = Vector3.Dot(transform.forward, worldDeltaPosition);

            smooth = Mathf.Min(MIN_SMOOTH, Time.deltaTime / RATE_SMOOTH);
            smoothDeltaPosition = Vector2.Lerp(smoothDeltaPosition, deltaPosition, smooth);

            if (Time.deltaTime > BLIND_TIME)
            {
                velocity = smoothDeltaPosition / Time.deltaTime;
            }

            isCanMove = velocity.magnitude > MIN_MOVE_DISTANCE && agent.remainingDistance > agent.radius;

            AnimatorMovement();

            if (worldDeltaPosition.magnitude > agent.radius)
            {
                transform.position = agent.nextPosition - offsetAgentPosition * worldDeltaPosition;
            }
        }

        private void AnimatorMovement()
        {
            anim.SetBool(MOVE, isCanMove);
            anim.SetFloat(VELOCITY_X, velocity.x);
            anim.SetFloat(VELOCITY_Y, velocity.y);
        }

        private void UpdateAgent()
        {
            rootPosition = anim.rootPosition;
            rootPosition.y = agent.nextPosition.y;
            transform.position = rootPosition;
        }
    }
}
