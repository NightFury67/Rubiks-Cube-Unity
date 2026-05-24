using UnityEngine;

public class CubeNavScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    Vector2 firstpresspos;
    Vector2 secondpresspos;
    Vector2 currentswipe;

    public GameObject Target;
    float speed = 400f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Swipe();
        if(transform.rotation != Target.transform.rotation)
        {
            var step = speed*Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Target.transform.rotation, step);
        }
    }        

    void Swipe()
    {
        if (Input.GetMouseButtonDown(1))
        {
            firstpresspos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }

        if (Input.GetMouseButtonUp(1))
        {
            secondpresspos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            currentswipe = new Vector2(secondpresspos.x- firstpresspos.x, secondpresspos.y  - firstpresspos.y);
            currentswipe.Normalize();

            if (LeftSwipe(currentswipe))
            {
                Target.transform.Rotate(0, 90, 0, Space.World);
            }
            else if (RightSwipe(currentswipe))
            {
                Target.transform.Rotate(0, -90, 0, Space.World);
            }
            else if (UpLeftSwipe(currentswipe))
            {
                Target.transform.Rotate(90, 0, 0, Space.World);
            }
            else if (UpRightSwipe(currentswipe))
            {
                Target.transform.Rotate(0, 0, -90, Space.World);
            }
            else if (DownLeftSwipe(currentswipe))
            {
                Target.transform.Rotate(0, 0, 90, Space.World);
            }
            else if (DownRightSwipe(currentswipe))
            {
                Target.transform.Rotate(-90, 0, 0, Space.World);
            }
        }
    }

    bool LeftSwipe(Vector2 pos)
    {
        return currentswipe.x < 0 && currentswipe.y > -0.5f && currentswipe.y<0.5f ;
    }

    bool RightSwipe(Vector2 pos)
    {
        return currentswipe.x > 0 && currentswipe.y > -0.5f && currentswipe.y < 0.5f;
    }

    bool UpLeftSwipe(Vector2 pos)
    {
        return currentswipe.y > 0 && currentswipe.x< 0f;
    }

    bool UpRightSwipe(Vector2 pos)
    {
        return currentswipe.y > 0 && currentswipe.x >0f;
    }

    bool DownLeftSwipe(Vector2 pos)
    {
        return currentswipe.y < 0 && currentswipe.x < 0f;
    }

    bool DownRightSwipe(Vector2 pos)
    {
        return currentswipe.y < 0 && currentswipe.x >0f;
    }
}
