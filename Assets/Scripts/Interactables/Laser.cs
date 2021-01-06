using UnityEngine;

namespace Interactables
{
    public class Laser : MonoBehaviour

    {
        [SerializeField] private Camera cam;
        [SerializeField] private LineRenderer line;
        [SerializeField] private Transform firepoint;
        
        private Quaternion p_rotation;

        private void Awake()
        {
            if (cam == null) cam = Camera.main;
            DisableLaser();
        }

        private void Update()
        {
            if (Input.GetButtonDown("Fire2")) EnableLaser();
            
            if (Input.GetButton("Fire2")) UpdateLaser();
            
            if (Input.GetButtonUp("Fire2")) DisableLaser();
            
            RotateToMouse();
        }

        private void DisableLaser() => line.enabled = false;

        private void UpdateLaser()
        {
            var mousepos = (Vector2)cam.ScreenToWorldPoint(Input.mousePosition);
            line.SetPosition(0, firepoint.position);
            line.SetPosition(1, mousepos);
        }

        private void EnableLaser() => line.enabled = true;

        private void RotateToMouse()
        {
            Vector2 direction = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            p_rotation.eulerAngles = new Vector3(0, 0, angle);
            transform.rotation = p_rotation;
        }
    }
}
