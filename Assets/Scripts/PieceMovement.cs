using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceMovement : MonoBehaviour
{
    private float numOfTimesMoved;
    private GameManager gm;
    [SerializeField] private Transform[] children;
    public bool hitGround { get; private set; }
    private bool doneMoving;
    private bool hitLeft;
    private bool hitRight;
    private float raycastLength = .3f;
    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
        children = gameObject.GetComponentsInChildren<Transform>();
        StartCoroutine(MovePiece());
        StartCoroutine(RandomMovement());
        StartCoroutine(RandomTurns());
    }
    private void Update()
    {
        foreach (Transform transform in children)
        {
            Debug.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - raycastLength, transform.position.z), Color.yellow);
            Debug.DrawLine(transform.position, new Vector3(transform.position.x + raycastLength, transform.position.y, transform.position.z), Color.yellow);
            Debug.DrawLine(transform.position, new Vector3(transform.position.x - raycastLength, transform.position.y, transform.position.z), Color.yellow);
        }
        foreach (Transform transform in children)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, raycastLength, 1 << LayerMask.NameToLayer("Floor"));
            if (hit)
            {
                hitGround = true;
            }
        }


    }
    private IEnumerator RandomMovement()
    {
        if (!hitGround)
        {
            numOfTimesMoved++;
            var rand = Random.Range(0, 3);
            foreach (Transform transform in children)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.right, raycastLength, 1 << LayerMask.NameToLayer("Wall"));
                RaycastHit2D hit2 = Physics2D.Raycast(transform.position, Vector2.right, raycastLength, 1 << LayerMask.NameToLayer("Wall"));
                if (hit) //bout to hit left
                {
                    hitLeft = true;
                }
                if (hit2)//bout to hit right
                {
                    hitRight = true;
                }
            }
            switch (rand)
            {
                case 0:
                    if (!hitLeft) transform.localPosition -= Vector3.right / 2;
                    break;
                case 1:
                    if (!hitRight) transform.localPosition += Vector3.right / 2;
                    break;
                case 2:
                    break;
            }


            yield return new WaitForSeconds(.3f);
            if (numOfTimesMoved < gm.maxMovements)
            {
                StartCoroutine(RandomMovement());
            }
            else doneMoving = true;
        }
        
    }
    private IEnumerator RandomTurns()
    {
        if (!hitGround)
        {
            var rand = Random.Range(0, 3);
            foreach (Transform transform in children)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.right, raycastLength, 1 << LayerMask.NameToLayer("Wall"));
                RaycastHit2D hit2 = Physics2D.Raycast(transform.position, Vector2.right, raycastLength, 1 << LayerMask.NameToLayer("Wall"));
                if (hit) //bout to hit left
                {
                    hitLeft = true;
                }
                if (hit2)//bout to hit right
                {
                    hitRight = true;
                }
            }
            switch (rand)
            {
                case 0:
                    if (!hitLeft && !hitRight)
                        gameObject.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z - 90);
                    break;
                case 1:
                    if (!hitLeft && !hitRight)
                        gameObject.transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z + 90);
                    break;
                case 2:
                    break;
            }
            if (!doneMoving)
            {
                yield return new WaitForSeconds(.7f);
            }
            if (!doneMoving)
            {
                StartCoroutine(RandomTurns());
            }
        }

    }
    private IEnumerator MovePiece()
    {
        if (!hitGround)
        {
            transform.localPosition -= Vector3.up * .5f;
            yield return new WaitForSeconds(doneMoving ? gm.fastMovementSpeed : gm.movementSpeed);
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
