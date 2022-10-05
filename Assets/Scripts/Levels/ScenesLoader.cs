using UnityEngine;
using IJunior.TypedScenes;

public class ScenesLoader : MonoBehaviour
{
    [SerializeField] private LevelSaver _saver;

    private void Awake()
    {
        CompletedLevelsCounter counter = new CompletedLevelsCounter();

        switch (_saver.LoadLevelIndex())
        {
            case 0:
                IJunior.TypedScenes.TutorialLevel.Load(counter);
                break;
            case 1:
                Level_1.Load(counter);
                break;
            case 2:
                Level_2.Load(counter);
                break;
            case 3:
                Level_3.Load(counter);
                break;
            case 4:
                Level_4.Load(counter);
                break;
            case 5:
                Level_5.Load(counter);
                break;
            case 6:
                Level_6.Load(counter);
                break;
            case 7:
                Level_7.Load(counter);
                break;
            case 8:
                Level_8.Load(counter);
                break;
            case 9:
                Level_9.Load(counter);
                break;
            case 10:
                Level_10.Load(counter);
                break;
            default:
                IJunior.TypedScenes.TutorialLevel.Load(counter);
                break;
        }
    }
}