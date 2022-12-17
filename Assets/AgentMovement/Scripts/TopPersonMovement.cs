namespace AgentMovement
{
    using UnityEngine;

    /// <summary>
    /// Перемещение top-down
    /// </summary>
    public class TopPersonMovement : AbstractMovement
    {
        private RaycastHit hitInfo = new RaycastHit();
        private Ray ray = default;

        protected override void Movement()
        {
            if (Input.GetMouseButtonDown(0))
            {
                ray = cameraMark.Camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray.origin, ray.direction, out hitInfo))
                {
                    agent.destination = hitInfo.point;
                }
            }
        }
    }
}