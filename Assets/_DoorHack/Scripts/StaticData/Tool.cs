using UnityEngine;

namespace StaticData
{
    [CreateAssetMenu(fileName = "Tool", menuName = "ScriplableObjects/Tool", order = 0)]
    public class Tool : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public int Pin1 { get; private set; }
        [field: SerializeField] public int Pin2 { get; private set; }
        [field: SerializeField] public int Pin3 { get; private set; }
    }
}