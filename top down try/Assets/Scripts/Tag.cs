using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tag : MonoBehaviour
{
    [SerializeField]
    private List<Tags> _tags;

    public List<Tags> All => _tags;
    public bool HasTag(Tags t)
    {
        return _tags.Contains(t);
    }

    public bool HasTag(string tagName)
    {
        return _tags.Exists(t => t.Name.Equals( tagName, System.StringComparison.InvariantCultureIgnoreCase));
            
    }
}
