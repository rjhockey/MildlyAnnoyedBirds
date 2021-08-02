using System.Collections;
using UnityEngine;

public class Bird : MonoBehaviour
{

    [SerializeField] float _launchForce = 500;

    Vector2 _startPosition;
    Rigidbody2D _rigidbody2D;
    SpriteRenderer _spritrender;

    // Awake is called before start of game
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spritrender = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // record start positon in a variable
        _startPosition = _rigidbody2D.position;
        _rigidbody2D.isKinematic = true;
    }

    private void OnMouseDown()
    {
        _spritrender.color = Color.red;
    }

    private void OnMouseUp()
    {
        Vector2 currentPosition = _rigidbody2D.position;
        Vector2 direction = _startPosition - currentPosition;
        direction.Normalize();

        _rigidbody2D.isKinematic = false;
        _rigidbody2D.AddForce(direction * _launchForce);

        _spritrender.color = Color.white;
    }

    private void OnMouseDrag()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3 (mousePosition.x, mousePosition.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // gets called whenever bird collides with something
    private void OnCollisionEnter2D(Collision2D collision)
    {
        StartCoroutine(ResetAfterDelay());
    }

    IEnumerator ResetAfterDelay()
    {
        yield return new WaitForSeconds(3);
        // reset our bird to start position
        _rigidbody2D.position = _startPosition;
        _rigidbody2D.isKinematic = true;
        // set bird to not keep flying at reset
        _rigidbody2D.velocity = Vector2.zero;
    }
}
