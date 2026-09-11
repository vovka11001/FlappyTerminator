using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class BirdMover : MonoBehaviour
{
   [SerializeField] private float _jumpForce;
   [SerializeField] private float _minRotationZ;
   [SerializeField] private float _maxRotationZ;
   [SerializeField] private float _rotationSpeed;
   [SerializeField] private float _speed;
   
   private Rigidbody2D _rigidbody2D;
   private Quaternion _minRotation;
   private Quaternion _maxRotation;
   
   private void Awake()
   {
      _rigidbody2D = GetComponent<Rigidbody2D>();
      
      _rigidbody2D.freezeRotation = true;
      
      _minRotation = Quaternion.Euler(0f, 0f, _minRotationZ);
      _maxRotation = Quaternion.Euler(0f, 0f, _maxRotationZ);
   }
   
   private void Update()
   {
      transform.rotation = Quaternion.Lerp(transform.rotation, _minRotation,_rotationSpeed * Time.deltaTime);
   }

   public void Jump()
   {
      _rigidbody2D.velocity = new Vector2(_speed,_jumpForce);
      transform.rotation = _maxRotation;
   }
}