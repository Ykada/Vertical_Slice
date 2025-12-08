using UnityEngine;

public class Highwayman : BaseHero
{
    
    private void setStats()
    {
        //attack 1
        Attacks temp = selectAttack.Buttons[0].GetComponent<Attacks>();
        temp.DmgMod = 0.85f;
        temp.StartTarget = 0;
        temp.EndTarget = 1;
        temp.Crit = 0;
        temp.DebuffName = "Bleed";
        temp.AccAttack = 95;
        temp.DebuffChance = 100;
        //attack 2
        temp = selectAttack.Buttons[1].GetComponent<Attacks>();
        temp.DmgMod = 0.85f;
        temp.StartTarget = 1;
        temp.EndTarget = 3;
        temp.Crit = 7.5f;
        temp.DebuffName = null;
        temp.AccAttack = 85;
        temp.DebuffChance = 0;
        //attack 3
        temp = selectAttack.Buttons[2].GetComponent<Attacks>();
        temp.DmgMod = 0.2f;
        temp.StartTarget = 1;
        temp.EndTarget = 3;
        temp.Crit = 0;
        temp.DebuffName = null;
        temp.AccAttack = 95;
        temp.DebuffChance = 0;
        //attack 4

    }
}
