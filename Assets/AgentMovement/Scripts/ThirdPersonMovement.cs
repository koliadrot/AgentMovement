using UnityEngine;

namespace AgentMovement
{
    /// <summary>
    /// Перемещение от 3-го лица
    /// </summary>
    public class ThirdPersonMovement : AbstractMovement
    {
        private Vector3 inputDirection;
        private Vector3 targetDirection;
        private float targetRotation;
        private float vertical;
        private float horizontal;

        private const string VERTICAL = "Vertical";
        private const string HORIZONTAL = "Horizontal";

        protected override void Movement()
        {
            vertical = Input.GetAxis(VERTICAL);
            horizontal = Input.GetAxis(HORIZONTAL);

            inputDirection.x = horizontal;
            inputDirection.z = vertical;

            targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + cameraMark.Camera.transform.eulerAngles.y;
            targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;
            if (inputDirection.magnitude != 0f)
            {
                agent.destination = transform.position + targetDirection;
            }
        }
    }
}