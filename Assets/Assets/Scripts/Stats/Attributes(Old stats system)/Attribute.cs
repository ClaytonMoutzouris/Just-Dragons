using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attribute : BaseAttribute {

    List<RawBonus> _rawBonuses;
    List<FinalBonus> _finalBonuses;

    protected int _finalValue;

    public Attribute(int startingValue) : base(startingValue)
    {
        _rawBonuses = new List<RawBonus>();
        _finalBonuses = new List<FinalBonus>();

        _finalValue = baseValue();
    }

    public void addRawBonus(RawBonus bonus)
    {
        _rawBonuses.Add(bonus);
    }

    public void addFinalBonus(FinalBonus bonus)
    {
        _finalBonuses.Add(bonus);
    }

    public void removeRawBonus(RawBonus bonus)
    {
        if(_rawBonuses.IndexOf(bonus) >= 0)
        _rawBonuses.Remove(bonus);
    }

    public void removeFinalBonus(FinalBonus bonus)
    {
        if (_finalBonuses.IndexOf(bonus) >= 0)
            _finalBonuses.Remove(bonus);
    }

    protected void applyRawBonuses()
    {
        // Adding value from raw
        int rawBonusValue = 0;
        float rawBonusMultiplier = 0;

        foreach (RawBonus bonus in _rawBonuses)
        {
            rawBonusValue += bonus.baseValue();
            rawBonusMultiplier += bonus.baseMultiplier();
        }

        _finalValue += rawBonusValue;
        _finalValue *= (int)(1 + rawBonusMultiplier);

    }

    protected void applyFinalBonuses()
    {
        // Adding value from final
        int finalBonusValue = 0;
        float finalBonusMultiplier = 0;

        foreach (FinalBonus bonus in _finalBonuses)
        {
            finalBonusValue += bonus.baseValue();
            finalBonusMultiplier += bonus.baseMultiplier();
        }

        _finalValue += finalBonusValue;
        _finalValue *= (int)(1 + finalBonusMultiplier);
    }

    public virtual int calculateValue(){

        _finalValue = baseValue();

        applyRawBonuses();
        applyFinalBonuses();     

        return _finalValue;

    }

    public int finalValue()
    {
        return calculateValue();
    }
}
