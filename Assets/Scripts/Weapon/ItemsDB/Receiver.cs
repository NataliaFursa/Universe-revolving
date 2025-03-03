using UnityEngine;

[CreateAssetMenu(fileName = "Receiver", menuName = "Parts/Receiver")]
public class Receiver : IPart
{

    [field: SerializeField] public float delay { private set; get; }
    [field: SerializeField] public float force { private set; get; }
    [field: SerializeField] public int volume { private set; get; }
    [field: SerializeField] public string LegendaryMod { private set; get; }
}
