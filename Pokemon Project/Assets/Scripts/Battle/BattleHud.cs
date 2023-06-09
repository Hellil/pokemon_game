using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{
    [SerializeField] Text nameText;
    [SerializeField] Text levelText;
    [SerializeField] Text statusText;
    [SerializeField] HPBar hpBar;
    [SerializeField] GameObject expBar;

    [SerializeField] Color psnColor;
    [SerializeField] Color brnColor;
    [SerializeField] Color slpColor;
    [SerializeField] Color parColor;
    [SerializeField] Color frzColor;

    Pokemon pkmn;
    Dictionary<ConditionID, Color> statusColors;

    public void SetData(Pokemon pokemon)
    {
        pkmn = pokemon;

        nameText.text = pokemon.Base.Name;
        SetLevel();
        hpBar.SetHP((float) pokemon.HP / pokemon.MaxHP);
        SetExp();

        statusColors = new Dictionary<ConditionID, Color>()
        {
            {ConditionID.psn, psnColor},
            {ConditionID.brn, brnColor},
            {ConditionID.slp, slpColor},
            {ConditionID.par, parColor},
            {ConditionID.frz, frzColor},
        };

        SetStatusText();
        pkmn.OnStatusChanged += SetStatusText;
    }

    void SetStatusText()
    {
        if (pkmn.Status == null)
        {
            statusText.text = "";
        }
        else
        {
            statusText.text = pkmn.Status.ID.ToString().ToUpper();
            statusText.color = statusColors[pkmn.Status.ID];
        }
    }

    public void SetLevel()
    {
        levelText.text = "Lvl " + pkmn.Level;
    }

    public void SetExp()
    {
        if (expBar == null) return;

        float normalizedExp = GetNormalizedExp();
        expBar.transform.localScale = new Vector3(normalizedExp, 1, 1);
    }

    public IEnumerator SetExpSmooth(bool reset=false)
    {
        if (expBar == null) yield break;

        if (reset)
        {
            expBar.transform.localScale = new Vector3(0, 1, 1);
        }

        float normalizedExp = GetNormalizedExp();
        yield return expBar.transform.DOScaleX(normalizedExp, 1.5f).WaitForCompletion();
    }

    float GetNormalizedExp()
    {
        int currentLevelExp = pkmn.Base.GetExpForLevel(pkmn.Level);
        int nextLevelExp = pkmn.Base.GetExpForLevel(pkmn.Level + 1);

        float normalizedExp = (float) (pkmn.Exp - currentLevelExp) / (nextLevelExp - currentLevelExp);
        return Mathf.Clamp01(normalizedExp);
    }

    public IEnumerator UpdateHP()
    {
        if (pkmn.HPChanged)
        {
            yield return hpBar.SetHPSmooth((float)pkmn.HP / pkmn.MaxHP);
            pkmn.HPChanged = false;
        }
    }
}
