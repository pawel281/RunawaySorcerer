
using UnityEngine;
[CreateAssetMenu(menuName = "Magic/MagicElement")]
public class MagicElement : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;
    [SerializeField] private Color _color;
    [SerializeField] private string _description;
    [SerializeField] private float mannaCost;
    
    public string Name => _name;
    public Sprite Icon => _icon;
    public Color Color => _color;
    public string Description => _description;
    public float MannaCost => mannaCost;
}
