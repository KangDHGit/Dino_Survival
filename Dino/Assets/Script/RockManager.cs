using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockManager : MonoBehaviour
{
    public static RockManager I;

    public GameObject _rockTemplate;
    Vector3 _templatePos;

    float _nowTime;
    float _random_delay = 0;
    [SerializeField] float _minDelay;
    [SerializeField] float _maxDelay;

    [SerializeField] float _minXpos;
    [SerializeField] float _maxXpos;
    
    //public List<Rock> _rocks;
    public List<GameObject> _list_Obj_rock;

    public void Awake()
    {
        I = this;
    }

    public void Init()
    {
        _rockTemplate = transform.Find("Rock").gameObject;
        if(_rockTemplate != null)
            _rockTemplate.SetActive(false);
        _templatePos = _rockTemplate.transform.position;
        _list_Obj_rock = new List<GameObject>();
        DifficultyInit();
    }

    private void Update()
    {
        if (GameManager.I._isIntro || GameManager.I._isGameOver || GameManager.I._isPause)
            return;

        _nowTime += Time.deltaTime;
        if(_nowTime >= _random_delay)
        {
            GameObject cloneObj = Instantiate(_rockTemplate, this.transform);
            Rock cloneRock = cloneObj.GetComponent<Rock>();
            cloneRock.Init();
            _list_Obj_rock.Add(cloneRock.gameObject);

            float xPos = Random.Range(_minXpos, _maxXpos);
            cloneObj.transform.position = new Vector3(xPos, _templatePos.y, 0);

            cloneObj.SetActive(true);

            _random_delay = Random.Range(_minDelay, _maxDelay);
            _nowTime = 0;
        }
    }
    public void DestroyRocks()
    {
        for (int i = 0; i < _list_Obj_rock.Count; i++)
        {
            Destroy(_list_Obj_rock[i]);
        }
        _list_Obj_rock.Clear();
    }

    public void DifficultyInit()
    {
        _minDelay = 0.5f;
        _maxDelay = 1.2f;
        _rockTemplate.GetComponent<Rock>()._dropSpeed = 3.5f;
    }
    public void DifficultyUp()
    {
        if (_maxDelay > _minDelay)
            _maxDelay -= 0.05f;
        _rockTemplate.GetComponent<Rock>()._dropSpeed += 0.5f;
    }
}
