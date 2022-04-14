using System.Collections;
using System.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ManageMap : MonoBehaviour
{
    [SerializeField]private GameObject[] stars;
    [SerializeField]private List<GameObject> starsList;
    private GameObject _star;
    private GameObject _spawnPlace;
    public GameObject _path;
    public GameObject[] paths;
    public GameObject[] paths2;
    public GameObject[] paths3;
    public GameObject[] paths4;
    public GameObject[] endPath = { null };
    public GameObject _secondStarsGroup;
    public GameObject _firstPathsGroup;
    public GameObject _secondPathsGroup;
    public GameObject _thirdPathGroup;
    public GameObject _forthPathGroup;
    public GameObject _EndPath;
    public static bool _isIlandMode;
    //� �� ����������

    private float _offsetXVar;

    public float _offsetX
    {
        get
        {
            switch (_offsetXVar)
            {
                case 1:
                    return _offsetXVar + 0.5f;
                case 2:
                    return _offsetXVar + 0.5f;
                case 3:
                    return _offsetXVar + 2.5f;
                case 4:
                    return _offsetXVar + 2.5f;
                case 5:
                    return _offsetXVar + 2.5f;
                case 6:
                    return _offsetXVar + 2.5f;
                case 7:
                    return _offsetXVar + 2.5f;
                case 8:
                    return _offsetXVar + 2.5f;
                case 9:
                    return _offsetXVar + 2.5f;
                case 10:
                    return _offsetXVar + 2.5f;
                default:
                    return _offsetXVar + 2;
            }
        }
        set { _offsetXVar = value; }
    }

    private void Start()
    {
        _spawnPlace = GameObject.FindGameObjectWithTag("Symmetry");
        _offsetXVar = MapSpawner._weight;
        StartCoroutine("WaitForList");
    }

    IEnumerator WaitForList()
    {
        yield return new WaitForSeconds(0.1f);
        stars = new GameObject[GameObject.FindGameObjectsWithTag("Star").Length];
        stars = GameObject.FindGameObjectsWithTag("Star");
        for (int i = 0; i < stars.Length; i++)
        {
            starsList.Add(stars[i]);
        }
        ManageStars();
        yield return new WaitForSeconds(0.001f);
    }

    public void ManageStars()
    {
        for (int i = 1; i < starsList.Count; i++) //��������� ����� � ���������� ����
        {
            Vector2 _randPos = new Vector2(starsList[i].transform.position.x + UnityEngine.Random.Range(0.01f, 0.8f), starsList[i].transform.position.y + UnityEngine.Random.Range(0.01f, 0.8f));
            int _rand = UnityEngine.Random.Range(0, starsList.Count);
            if(_rand <= 25)
            {
                starsList[i-1].tag = "Destroy";
                starsList.RemoveAt(i-1);
            }
           starsList[i-1].transform.position = _randPos;         
        }
        GameObject[] destroy = GameObject.FindGameObjectsWithTag("Destroy");
        for (int i = 0; i < destroy.Length; i++)
        {
            Destroy(destroy[i]);
        }
        _spawnPlace.transform.position = new Vector2(_offsetX, _spawnPlace.transform.position.y);
        for (int i = 0; i < starsList.Count; i++) //����������� ���� ��� ���������
        {
            _star = starsList[i].gameObject;
            Vector2 _spawnPos = new Vector2(starsList[i].transform.position.x + 5, starsList[i].transform.position.y + 1);
            Instantiate(_star, _spawnPos, Quaternion.identity, _spawnPlace.transform);
        }
        //�������� ���������� �����
        _spawnPlace.transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            //���������� ����� ������� � ����        
            StartCoroutine("WaitforStars");
    }

    IEnumerator WaitforStars()
    {
        yield return new WaitForSecondsRealtime(0.01f);
        stars = null;
        stars = new GameObject[starsList.Count];

        for (int i = 0; i < stars.Length; i++)
        {
            stars[i] = _secondStarsGroup.transform.GetChild(i).gameObject;
        }
        yield return new WaitForSecondsRealtime(0.01f);
        StartCoroutine("ManagePaths");
    }

    IEnumerator ManagePaths() 
    {
        paths = new GameObject[starsList.Count];
        paths2 = new GameObject[paths.Length];
        paths3 = new GameObject[paths.Length];
        paths4= new GameObject[paths.Length];
        //������ ������
        for (int i = 0; i < starsList.Count; i++)
        {
            Instantiate(_path, _firstPathsGroup.transform);
        }

        for (int i = 0; i < starsList.Count; i++)
        {
            paths[i] = _firstPathsGroup.transform.GetChild(i).gameObject;
        }

        for (int s1 = 0; s1 < starsList.Count; s1++) //�������������� star1
        {
            paths[s1].GetComponent<PathLogic>()._star1 = starsList[s1];
            List<float> distance = new List<float>(); //������ � ������������
            List<float> distList = new List<float>();

            for (int s2 = 0; s2 < starsList.Count; s2++) //�������������� star2
            {
                distance.Add(Vector2.Distance(starsList[s1].transform.position, starsList[s2].transform.position));
                distList.Add(Vector2.Distance(starsList[s1].transform.position, starsList[s2].transform.position));
            }

            distList.Sort(); //�������� ������ � ������������
            float result = distList[1]; //�������� ��������� ���������� �� ������; 1 ������ ��� 0 ��� ��� ������, �� ���� ����� ����������� ����������
            int ind = distance.IndexOf(result); //�������� ������ ������� � ����������� ������� distance, ������������� � � ������ starList
            paths[s1].GetComponent<PathLogic>()._star2 = starsList[ind]; //������������ ���� �� ������ ������
        }
         
        //
        //
        yield return new WaitForSecondsRealtime(0.1f);
        //
        //������ ������

        for (int i = 0; i < stars.Length; i++)
        {
            Instantiate(_path, _secondPathsGroup.transform);
        }

        for (int i = 0; i < stars.Length; i++)
        {
            paths2[i] = _secondPathsGroup.transform.GetChild(i).gameObject;
        }

        for (int s1 = 0; s1 < stars.Length; s1++) 
        {
            paths2[s1].GetComponent<PathLogic>()._star1 = stars[s1];
            List<float> distance = new List<float>(); 
            List<float> distList = new List<float>();

            for (int s2 = 0; s2 < stars.Length; s2++) 
            {
                distance.Add(Vector2.Distance(stars[s1].transform.position, stars[s2].transform.position));
                distList.Add(Vector2.Distance(stars[s1].transform.position, stars[s2].transform.position));
            }

            distList.Sort();
            float result = distList[1]; 
            int ind = distance.IndexOf(result); 
            paths2[s1].GetComponent<PathLogic>()._star2 = stars[ind];
        }
        if (_isIlandMode) StopCoroutine("ManagePaths");
        //
        //
        yield return new WaitForSecondsRealtime(0.1f);
        //
        //������ ���� //������ ��������

        for (int i = 0; i < starsList.Count; i++)
        {
            Instantiate(_path, _thirdPathGroup.transform);
        }

        for (int i = 0; i < stars.Length; i++)
        {
            paths3[i] = _thirdPathGroup.transform.GetChild(i).gameObject;
        }

        for (int s1 = 0; s1 < starsList.Count; s1++) 
        {
            paths3[s1].GetComponent<PathLogic>()._star1 = starsList[s1];
            List<float> distance = new List<float>(); 
            List<float> distList = new List<float>();

            for (int s2 = 0; s2 < starsList.Count; s2++) 
            {
                distance.Add(Vector2.Distance(starsList[s1].transform.position, starsList[s2].transform.position));
                distList.Add(Vector2.Distance(starsList[s1].transform.position, starsList[s2].transform.position));
            }

            distList.Sort(); 
            float result = distList[2]; 
            int ind = distance.IndexOf(result); 
            paths3[s1].GetComponent<PathLogic>()._star2 = starsList[ind]; 
        }

        //
        //
        yield return new WaitForSecondsRealtime(0.1f);
        //
        //������ ������ //�������� ��������

        for (int i = 0; i < stars.Length; i++)
        {
            Instantiate(_path, _forthPathGroup.transform);
        }

        for (int i = 0; i < stars.Length; i++)
        {
            paths4[i] = _forthPathGroup.transform.GetChild(i).gameObject;
        }

        for (int s1 = 0; s1 < stars.Length; s1++)
        {
            paths4[s1].GetComponent<PathLogic>()._star1 = stars[s1];
            List<float> distance = new List<float>();
            List<float> distList = new List<float>();

            for (int s2 = 0; s2 < stars.Length; s2++)
            {
                distance.Add(Vector2.Distance(stars[s1].transform.position, stars[s2].transform.position));
                distList.Add(Vector2.Distance(stars[s1].transform.position, stars[s2].transform.position));
            }

            distList.Sort();
            float result = distList[2];
            int ind = distance.IndexOf(result);
            paths4[s1].GetComponent<PathLogic>()._star2 = stars[ind];
        }
        Instantiate(_path,_EndPath.transform);
        endPath[0] = _EndPath.transform.GetChild(0).gameObject;
        endPath[0].gameObject.GetComponent<PathLogic>()._star1 = starsList[starsList.Count-1];
        endPath[0].gameObject.GetComponent<PathLogic>()._star2 = stars[stars.Length-1];

    }
}
