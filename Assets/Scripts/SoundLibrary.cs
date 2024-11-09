using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundLibrary : MonoBehaviour
{
    public SoundGroup[] groups;

    Dictionary<string, AudioClip[]> groupDictionary;

    void Awake()
    {
        groupDictionary = groups.ToDictionary(x => x.groupId, x => x.group);
    }

    public AudioClip GetClipFromName(string name)
    {
        if (groupDictionary.ContainsKey(name))
        {
            var sounds = groupDictionary[name];
            return sounds[Random.Range(0, sounds.Length)];
        }
        return null;
    }

    [System.Serializable]
    public class SoundGroup
    {
        public string groupId;
        public AudioClip[] group;
    }
}
