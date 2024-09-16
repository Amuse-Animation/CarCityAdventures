using System.IO;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.Events;

namespace CCH.LevelStartInit.Controllers
{
    public class LevelStartInitController : MonoBehaviour
    {
        [SerializeField]
        private Transform levelStartTransform;

        [SerializeField]
        private Vector2Variable carCurrentPositionVariable;

        [SerializeField]
        private UnityEvent<LevelStartDataStruct> onLevelStartDataSetted;

        LevelStartDataStruct levelDataStructClass;
        string LEVEL_START_PATH = "";


        private void Awake()
        {
            LEVEL_START_PATH = Application.persistentDataPath + "/level.json";
            
        }

        public void DoUseCurrentLevelStartData()
        {

            LoadStartData();

            if (levelDataStructClass == null)
            {
                DoSaveLevelStartData();
            }
            else
            {
                DoLoadLevelStartData();
            }

            onLevelStartDataSetted.Invoke(levelDataStructClass);

            DeleteJsonFile();

        }

        private void LoadStartData()
        {
            LEVEL_START_PATH = Application.persistentDataPath + "/level.json";

            if (File.Exists(LEVEL_START_PATH))
            {
                string levelData = File.ReadAllText(LEVEL_START_PATH);
                levelDataStructClass = JsonUtility.FromJson<LevelStartDataStruct>(levelData);
            }
        }

        public void DoLoadLevelStartData()
        {
            if (levelDataStructClass == null)
                LoadStartData();

            if (levelDataStructClass == null) return;
            levelStartTransform.position = levelDataStructClass.LevelStartPosition;
        }


        public void DoSaveLevelStartData()
        {

            LEVEL_START_PATH = Application.persistentDataPath + "/level.json";
            levelDataStructClass = new LevelStartDataStruct(carCurrentPositionVariable.Value);
            string levelDataJson = JsonUtility.ToJson(levelDataStructClass, true);
            File.WriteAllText(LEVEL_START_PATH, levelDataJson);

        }

        private void DeleteJsonFile()
        {
            if (File.Exists(LEVEL_START_PATH))
                File.Delete(LEVEL_START_PATH);
        }

        public void DoDeleteJsonFile()
        {
            DeleteJsonFile();
        }
    }

    [System.Serializable]
    public class LevelStartDataStruct
    {
        public Vector3 LevelStartPosition => levelStartcameraPosition;

        [SerializeField]
        private Vector3 levelStartcameraPosition;


        public LevelStartDataStruct(Vector3 levelStartTransformPosition)
        {
            this.levelStartcameraPosition = levelStartTransformPosition;
        }
    }
}