using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private Transform[] children;
    public bool hitGround { get; private set; }
    private void Start()
    {
        children = gameObject.GetComponentsInChildren<Transform>();
        StartCoroutine(MovePiece());
    }
    private void Update()
    {
        foreach (Transform transform in children)
        {
            Debug.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - .25f, transform.position.z), Color.yellow);
        }
    }
    private IEnumerator MovePiece()
    {
        foreach(Transform transform in children)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, .25f, 1 << LayerMask.NameToLayer("Floor"));
            if (hit)
            {
                hitGround = true;
                Debug.Log(hit.transform.gameObject.name);
            }
        }
        if (!hitGround)
        {
            transform.localPosition -= Vector3.up * .5f;
            yield return new WaitForSeconds(movementSpeed);
            StartCoroutine(MovePiece());
        }
        else
        {
            foreach (Transform transform in children)
            {
                transform.gameObject.layer = 6;
            }
            yield return null;
        }
        

    }

}
