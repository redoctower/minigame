using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelGenerator : MonoBehaviour
{
    [Range(2, 5)]
    [SerializeField] private int objectsCount = 3;
    public int subjectsCount => objectsCount * 3 - 3;
    [SerializeField] private GameObject objectsFolder;
    [SerializeField] private GameObject subjectsFolder;
    [SerializeField] private List<ObjectData> objectsInGen;
    [SerializeField] private GameObject subprefab;
    private GridLayoutGroup _gridLayoutGroup;
    [SerializeField] private ProgressManager progressManager;


    private void Awake()
    {
        _gridLayoutGroup = subjectsFolder.GetComponent<GridLayoutGroup>();
        GenerateLvl();
    }

    public int currentLVLhealt;

   public void GenerateLvl()
    {
        currentLVLhealt = 0;
        RemoveCurrent();
        List<ObjectData> objs = new List<ObjectData>();
        _gridLayoutGroup.enabled = true;
        while (objs.Count < objectsCount)
        {
            int rand = Random.Range(0, objectsInGen.Count);
            if (!objs.Contains(objectsInGen[rand]))
                objs.Add(objectsInGen[rand]);
        }
        foreach (var item in objs)
        {
            var obj = Instantiate(item);
            obj.transform.parent = objectsFolder.transform;
            foreach (var sub in item.subobjects)
            {
                var subInst = Instantiate(subprefab);
                subInst.transform.parent = subjectsFolder.transform;
                subInst.transform.SetSiblingIndex(Random.Range(0, subjectsFolder.transform.childCount));
                obj.health++;
                currentLVLhealt++;
                var subImg = subInst.GetComponent<Image>();
                obj.activesubs.Add(subInst);
                subImg.sprite = sub;
                obj._progressManager = progressManager;
            }
        }
        Invoke(nameof(DisableLayout), 0.1f);
    }
    private void DisableLayout()
    {
        _gridLayoutGroup.enabled = false;
    }
    
    public void RemoveCurrent()
    {
        foreach(Transform child in subjectsFolder.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in objectsFolder.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void Restart()
    {
        foreach (Transform child in subjectsFolder.transform)
        {
            child.gameObject.SetActive(true);
        }
        foreach (Transform child in objectsFolder.transform)
        {
            child.GetComponent<ObjectData>().health = currentLVLhealt / objectsCount;
        }
    }
}
