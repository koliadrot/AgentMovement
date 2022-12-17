namespace AgentMovement
{
    using UnityEngine;

    /// <summary>
    /// Тип движения "strafe"
    /// </summary>
    public class Strafe : MonoBehaviour
    {
        private bool isStrafe = default;
        private float strafeVerticalRotation = default;

        private void Update()
        {
            //TODO:Нужна обертка для инпута
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                isStrafe = !isStrafe;
            }
            StrafeMovement();
        }

        private void StrafeMovement()
        {
            if (isStrafe)
            {
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, strafeVerticalRotation, transform.eulerAngles.z);
            }
            else
            {
                strafeVerticalRotation = transform.eulerAngles.y;
            }
        }
    }
}