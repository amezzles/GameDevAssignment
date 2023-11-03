using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    private Animator animator;
    private float speed = 3f;
    public Vector2[] positions;
    private int index = 0;
    private float startTime;
    private float journeyLength;

    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = positions[0];
        startTime = Time.time;
        journeyLength = Vector2.Distance(transform.localPosition, positions[1]);
        animator = GetComponent<Animator>();
        UpdateScaredAnimation(true);
    }

    // Update is called once per frame
    void Update()
    {
        float distanceCovered = (Time.time - startTime) * speed;
        float fractionOfJourney = distanceCovered / journeyLength;
        Vector2 nextPosition = positions[(index + 1) % positions.Length];

        transform.localPosition = Vector2.Lerp(positions[index], positions[(index + 1) % positions.Length], fractionOfJourney);

        Vector2 direction = nextPosition - (Vector2)transform.localPosition;
        UpdateAnimation(direction);

        if (fractionOfJourney >= 1f)
        {
            index = (index + 1) % positions.Length;

            startTime = Time.time;
            journeyLength = Vector2.Distance(transform.localPosition, positions[(index + 1) % positions.Length]);
        }
    }
    void UpdateAnimation(Vector2 direction)
    {
        direction.Normalize();
        animator.SetFloat("DirectionX", direction.x);
        animator.SetFloat("DirectionY", direction.y);
    }

    void UpdateScaredAnimation(bool scared)
    {
        animator.SetBool("Scared", scared);
    }
}
