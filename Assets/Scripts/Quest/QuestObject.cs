using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Scriptable Objects/QuestObject"), System.Serializable]
public class QuestObject : ScriptableObject
{
    public string Name;
    public GameObject QuestUI;
}
