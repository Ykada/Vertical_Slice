using UnityEngine;

public class Highwayman : BaseHero
{
    private void Awake()
    {
        resNameWithValue.Add("Stun", 30);
        resNameWithValue.Add("Blight", 30);
        resNameWithValue.Add("Disease", 30);
        resNameWithValue.Add("Move", 30);
        resNameWithValue.Add("Bleed", 30);
        resNameWithValue.Add("Debuff", 30);
    }
}
