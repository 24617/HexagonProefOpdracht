using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerUnit : NetworkBehaviour
{

    Vector3 velocity;
    Vector3 bestGuessPosition;

    float ourLatency;
    float latencySmoothingFactor = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // This function runs on all PlayerUnits

        transform.Translate(velocity * Time.deltaTime);

        if( hasAuthority == false)
        {
            bestGuessPosition = bestGuessPosition + (velocity * Time.deltaTime);

            transform.position = Vector3.Lerp(transform.position, bestGuessPosition, Time.deltaTime);

            return;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            this.transform.Translate(0, 1, 0);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            Destroy(gameObject);
        }

        if(/* input */ true)
        {
            velocity = new Vector3(1, 0, 0);

           

            CmdUpdateVelocity(velocity, transform.position);
        }
    }

    [Command]
    void CmdUpdateVelocity(Vector3 v, Vector3 p)
    {
        // Server
        transform.position = p;
        velocity = v;

        RpcUpdateVelocity(velocity, transform.position);

    }

    [ClientRpc]
    void RpcUpdateVelocity(Vector3 v, Vector3 p)
    {
        // Client
        if (hasAuthority)
        {
            return;
        }

        //transform.position = p;
        velocity = v;
        bestGuessPosition = p + (v * (ourLatency));
        

    }

}
