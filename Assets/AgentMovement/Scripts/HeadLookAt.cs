using System;
using UnityEngine;

namespace AgentMovement
{
    /// <summary>
    /// Корректирует анимацию головы при перемещении
    /// </summary>
    [RequireComponent(typeof(Animator))]
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

        private Vector3 lookAtPosition;
        private Vector3 currentDirection;
        private Vector3 futureDirection;
        private Animator anim;
        private float lookAtWeight = 0.0f;
        private float lookAtTargetWeight = 0.0f;
        private float blendTime = 0.0f;

        private void Start()
        {
            if (!head)
            {
                Debug.LogError("Отсутсвует объект слежения!");
                enabled = false;
                return;
            }
            anim = GetComponent<Animator>();
            LookAtTargetPosition = head.position + transform.forward;
            lookAtPosition = LookAtTargetPosition;
        }

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
    }

}