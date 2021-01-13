using GUI;
using Managers;
using Plugins.Tools;
using UnityEngine;

namespace Player
{
    public class Laser : MonoBehaviour, MMEventListener<LoadedEvent>

    {
        [SerializeField] private Camera cam;
        [SerializeField] private LineRenderer line;

        [SerializeField] private Transform firePoint;
        [SerializeField] private Transform target;

        private Quaternion p_rotation;

        private void Awake()
        {
            if (target == null) target = transform;
            if (cam == null) cam = Camera.main;
            DisableLaser();
        }

        private void Update()
        {
            if(GameManager.Instance.gameIsPaused) return;
            
            if (Input.GetButtonDown("Fire2")) EnableLaser();

            if (Input.GetButton("Fire2")) UpdateLaser();

            if (Input.GetButtonUp("Fire2")) DisableLaser();

            RotateToMouse();
        }

        private void DisableLaser() => line.enabled = false;

        private void UpdateLaser()
        {
            var mousePos = (Vector2)cam.ScreenToWorldPoint(Input.mousePosition);
            line.SetPosition(0, firePoint.position);
            line.SetPosition(1, mousePos);
        }

        private void EnableLaser()
        {
            line.enabled = true;
            SoundManager.Instance.Play("Laser", true);
        }

        private void RotateToMouse()
        {
            Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
            Vector3 targetPos = transform.position;

            var direction = new Vector2(mousePos.x - targetPos.x, mousePos.y - targetPos.y);
            target.up = direction;
        }

        private void OnEnable()
        {
            this.MMEventStartListening();
            cam = Camera.main;
        } 

        private void OnDisable() => this.MMEventStopListening();

        public void OnMMEvent(LoadedEvent eventType)
        {
            if (cam == null) cam = Camera.main;
        }
    }
}
