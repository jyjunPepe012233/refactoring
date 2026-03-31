using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _character;
    [SerializeField] private float _speed = 3f;
    
    void Update()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (input.magnitude != 0)
        {
            Vector3 delta = input * (Time.deltaTime * _speed);
            _camera.transform.position += delta;
        }
        
        if (Vector2.Distance(_camera.transform.position, _character.position) > 0.1f)
        {
            _character.position = Vector2.Lerp(_character.position, _camera.transform.position, Time.deltaTime * 5);
            _character.rotation = Quaternion.Slerp(_character.rotation, Quaternion.LookRotation(Vector3.forward, _camera.transform.position - _character.position), Time.deltaTime * 5);
        }
    }
}
