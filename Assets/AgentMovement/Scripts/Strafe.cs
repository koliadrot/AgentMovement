using UnityEngine;

namespace AgentMovement
{
    /// <summary>
    /// Тип движения "strafe"
    /// </summary>
    public class Strafe : MonoBehaviour
    {
        private bool isStrafe;

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
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0f, transform.eulerAngles.z);
            }
        }
    }
}