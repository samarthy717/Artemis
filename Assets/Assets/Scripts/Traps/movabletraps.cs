using UnityEngine;

public enum MovementType
{
    Line,
    Circle,
    VLine,
    Rectangle
}

public class TrapMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust the speed of movement
    public float radius = 5f; // Adjust the radius of the circle movement
    public float rectangleWidth = 5f;
    public float rectangleHeight = 3f;
    public MovementType movementType = MovementType.Line; // Default movement type is a line

    private Vector3 startPosition;
    private float elapsedTime = 0f;
    public float ThresholdTime=2f;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        switch (movementType)
        {
            case MovementType.Line:
                MoveInHorizontalLine();
                break;
            case MovementType.VLine:
                MoveInVerticalLine();
                break;
            case MovementType.Circle:
                MoveInCircle();
                break;
            case MovementType.Rectangle:
                MoveInRectangle();
                break;
        }
    }

    void MoveInHorizontalLine()
    {
        // Move along a straight line
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);

        // You can customize the condition to change direction or reset the position
        if (elapsedTime >= ThresholdTime)
        {
            moveSpeed = -moveSpeed;
            elapsedTime = 0f;
        }
    }
    void MoveInVerticalLine()
    {
        // Move along a straight line
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);

        // You can customize the condition to change direction or reset the position
        if (elapsedTime >= ThresholdTime)
        {
            moveSpeed = -moveSpeed;
            elapsedTime = 0f;
        }
    }

    void MoveInCircle()
    {
        // Move in a circle pattern
        float x = startPosition.x + Mathf.Sin(elapsedTime * moveSpeed) * radius;
        float y = startPosition.y + Mathf.Cos(elapsedTime * moveSpeed) * radius;

        transform.position = new Vector3(x, y, startPosition.z);

        // You can customize the condition to change direction or reset the position
        if (elapsedTime >= ThresholdTime)
        {
            elapsedTime = 0f;
        }
    }

    void MoveInRectangle()
    {
        elapsedTime += Time.deltaTime;

        // NPC trap movement in a rectangle
        float x = startPosition.x + Mathf.PingPong(elapsedTime * moveSpeed, rectangleWidth) - rectangleWidth / 2;
        float y = startPosition.y + Mathf.PingPong(elapsedTime * moveSpeed * 0.5f, rectangleHeight) - rectangleHeight / 2;

        transform.position = new Vector3(x, y, startPosition.z);
    }
}
