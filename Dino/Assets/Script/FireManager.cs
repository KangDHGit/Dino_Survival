using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManager : MonoBehaviour
{
    public static FireManager I;

    public GameObject _fireTemplate;
    public List<GameObject> _list_Obj_FireSet;

    float _nowTime;
    float _random_delay = 0;
    [SerializeField] float _minDelay;
    [SerializeField] float _maxDelay;

    [SerializeField] float _minXpos;
    [SerializeField] float _maxXpos;
    private void Awake()
    {
        I = this;
    }

    public void Init()
    {
        _fireTemplate = transform.Find("FireSet").gameObject;
        if (_fireTemplate != null)
            _fireTemplate.SetActive(false);
        _list_Obj_FireSet = new List<GameObject>();
        DifficultyInit();
    }

    private void Update()
    {
        if (GameManager.I._isIntro || GameManager.I._isGameOver || GameManager.I._isPause)
            return;

        _nowTime += Time.deltaTime;
        if(_nowTime >=_random_delay)
        {
            GameObject clone = Instantiate(_fireTemplate, this.transform);
            float xPos = Random.Range(_minXpos, _maxXpos);
            clone.transform.position = new Vector3(xPos, clone.transform.position.y, 0);
            clone.SetActive(true);
            if(clone.TryGetComponent(out FireSet fire))
            {
                fire.Init();
                _list_Obj_FireSet.Add(clone.gameObject);
                StartCoroutine(fire.ActiveWarning());
            }
            _random_delay = Random.Range(_minDelay, _maxDelay);
            _nowTime = 0;
        }
    }

    public void DestroyFireSets()
    {
        if(_list_Obj_FireSet != null)
        {
            foreach (GameObject Obj_FireSet in _list_Obj_FireSet)
            {
                Destroy(Obj_FireSet);
            }
            _list_Obj_FireSet.Clear();
        }
    }

    public void DifficultyInit()
    {
        _minDelay = 0.7f;
        _maxDelay = 1.3f;
    }
    public void DifficultyUp()
    {
        if(_maxDelay > _minDelay)
            _maxDelay -= 0.05f;
    }
}
