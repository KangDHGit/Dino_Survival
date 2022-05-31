using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public float _xposLimit;
    public float _xposReturn;
    public float _moveSpeed;
    GameManager _gameMgr;
    // Start is called before the first frame update
    void Start()
    {
        _gameMgr = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveCloud();
    }
    void MoveCloud()
    {
        if (_gameMgr._isGameOver == true)
            return;
        transform.Translate(new Vector3(-_moveSpeed * Time.deltaTime, 0));
        if(transform.position.x <= _xposLimit)
        {
            transform.position = new Vector3(_xposReturn, 0);
        }    
    }
}
