using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerate : MonoBehaviour
{
    public static LevelGenerate _lg = null;
    public GameObject[] levelNormal, levelBonus;
    public List<Level> getlistLevelAll;
    public List<int> getlistLevelAllIndex;
    public int stepBonus;
    private List<int> listLevelNormal, listLevelBonus;
    private void Awake()
    {
        if (_lg == null)
        {
            _lg = this;
        }
        else
        {
            Destroy(gameObject);
        }
        // PlayerPrefs.DeleteKey("Level");
        // PlayerPrefs.DeleteKey("LevelID");
        // PlayerPrefs.DeleteKey("LevelType");
        // PlayerPrefs.DeleteKey("SelectIndex");
        // PlayerPrefs.DeleteKey("Mixer");
        getlistLevelAll = new List<Level>();
        listLevelNormal = new List<int>();
        listLevelBonus = new List<int>();
        //GetLevelData(true,true);

    }
    private void MixerNormalLevel(bool _mixed = true)
    {
        if (levelNormal.Length > 0)
        {
            listLevelNormal = MyUtils.UniqueRandomInt(0, levelNormal.Length, _mixed);
            SetLevelItems(levelNormal, listLevelNormal);
        }
    }
    private void MixerBonusLevel(bool _mixed = true)
    {
        if (levelBonus.Length > 0)
        {
            listLevelBonus = MyUtils.UniqueRandomInt(0, levelBonus.Length, _mixed);
            SetLevelItems(levelBonus, listLevelBonus, 1);
        }
    }
    private void SetLevelItems(GameObject[] _level, List<int> _index, int _type = 0)
    {
        for (int i = 0; i < _level.Length; i++)
        {
            int id = _index[i];
            Level lvl = _level[id].GetComponent<Level>();
            lvl.ID = id;
            lvl.TYPE = _type;
        }
    }
    private void MixerLevelItems()
    {

        List<int> listCopyIndex = listLevelNormal;
        List<Level> listCopyObject = new List<Level>();
        for (int i = 0; i < levelNormal.Length; i++)
        {
            listCopyObject.Add(levelNormal[listLevelNormal[i]].GetComponent<Level>());
        }
        if (stepBonus > 2 && listLevelBonus.Count > 0)
        {
            for (int i = 0; i < listLevelBonus.Count; i++)
            {
                int step = ((i + 1) * (stepBonus - 1));
                if (step <= levelNormal.Length)
                {
                    listCopyIndex.Insert(step + i, listLevelBonus[i]);
                    listCopyObject.Insert(step + i, levelBonus[listLevelBonus[i]].GetComponent<Level>());
                }
            }
        }
        getlistLevelAll = listCopyObject;
        getlistLevelAllIndex = listCopyIndex;
        //MainController._mc.listLevels = listCopyObject;
        //print("all levels: " + getlistLevelAll.Count);
        //MyUtils.printList(listCopyIndex);
        SaveLevelData(getlistLevelAll);
    }
    private void SaveLevelData(List<Level> _list)
    {
        string strID = "";
        string strTYPE = "";
        for (int i = 0; i < _list.Count; i++)
        {
            Level lvl = _list[i];
            //print("save: " + lvl.ID + " type: " + lvl.TYPE);
            strID += lvl.ID + ",";
            strTYPE += lvl.TYPE + ",";
        }
        print("save... " + strID + " type " + strTYPE);
        PlayerPrefs.SetString("LevelID", strID);
        PlayerPrefs.SetString("LevelType", strTYPE);
    }
    public void GetLevelData(bool _normal = true, bool _bonus = true)
    {
        if (_normal)
        {
            PlayerPrefs.DeleteKey("LevelID");
            PlayerPrefs.DeleteKey("LevelType");
            PlayerPrefs.DeleteKey("Mixer");
        }

        if (PlayerPrefs.GetString("LevelID").Length < 1)
        {
            MixerNormalLevel(_normal);
            MixerBonusLevel(_bonus);
            MixerLevelItems();
        }
        else
        {
            getlistLevelAll = new List<Level>();
            getlistLevelAllIndex = new List<int>();
            string[] stringsID = PlayerPrefs.GetString("LevelID").Split(","[0]);
            string[] stringsType = PlayerPrefs.GetString("LevelType").Split(","[0]);
            print(PlayerPrefs.GetString("LevelID") + " ++++ " + PlayerPrefs.GetString("LevelType"));
            for (int i = 0; i < stringsID.Length - 1; i++)
            {
                //print("get: " + stringsID[i] + " type: " + stringsType[i]);
                int type = System.Convert.ToInt32(stringsType[i]);
                int id = System.Convert.ToInt32(stringsID[i]);
                if (type == 0)
                {
                    getlistLevelAll.Add(levelNormal[id].GetComponent<Level>());
                }
                if (type == 1)
                {
                    getlistLevelAll.Add(levelBonus[id].GetComponent<Level>());
                }
                getlistLevelAllIndex.Add(id);
            }
            print(getlistLevelAll.Count + " >>> total level <<<< ");
            //MyUtils.printList(getlistLevelAllIndex);
        }
    }
    public void saveSelectIndex(int _index, int _mixerEventID)
    {
        PlayerPrefs.SetInt("SelectIndex", _index);
        PlayerPrefs.SetInt("Mixer", _mixerEventID);
    }
    public LevelReturnData callSelectIndex()
    {
        LevelReturnData lrd  = new LevelReturnData();
        lrd.index = PlayerPrefs.GetInt("SelectIndex");
        lrd.mixerEventID =  PlayerPrefs.GetInt("Mixer");
        return lrd;
    }

}
