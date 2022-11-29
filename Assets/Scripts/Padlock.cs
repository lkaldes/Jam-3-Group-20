using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Padlock : MonoBehaviour
{
    
    public GameObject Puzzle;
    public GameObject Padlocks;
    public MouseControl MouseCntrl; 

    public Button TLB;
    public Button TMB;
    public Button TRB;
    public Button MLB;
    public Button MMB;
    public Button MRB;
    public Button BLB;
    public Button BMB;
    public Button BRB;

    bool TL = false;
    bool TM = false;
    bool TR = false;
    bool ML = false;
    bool MM = false;
    bool MR = false;
    bool BL = false;
    bool BM = false;
    bool BR = false;

    public Color color0;
    public Color color1;

    public void TTL()
    {
        ColorBlock cb = TLB.colors;
        if (TL)
        {
            TL = false;
            cb.normalColor = color0;
            cb.highlightedColor = color0;
            cb.pressedColor = color0;
        }
        else
        {
            TL = true;
            cb.normalColor = color1;
            cb.highlightedColor = color1;
            cb.pressedColor = color1;
        }
        TLB.colors = cb;
    }

    public void TTM()
    {
        ColorBlock cb = TMB.colors;
        if (TM)
        {
            TM = false;
            cb.normalColor = color0;
            cb.highlightedColor = color0;
            cb.pressedColor = color0;
        }
        else
        {
            TM = true;
            cb.normalColor = color1;
            cb.highlightedColor = color1;
            cb.pressedColor = color1;
        }
        TMB.colors = cb;
    }

    public void TTR()
    {
        ColorBlock cb = TRB.colors;
        if (TR)
        {
            TR = false;
            cb.normalColor = color0;
            cb.highlightedColor = color0;
            cb.pressedColor = color0;
        }
        else
        {
            TR = true;
            cb.normalColor = color1;
            cb.highlightedColor = color1;
            cb.pressedColor = color1;
        }
        TRB.colors = cb;
    }

    public void TML()
    {
        ColorBlock cb = MLB.colors;
        if (ML)
        {
            ML = false;
            cb.normalColor = color0;
            cb.highlightedColor = color0;
            cb.pressedColor = color0;
        }
        else
        {
            ML = true;
            cb.normalColor = color1;
            cb.highlightedColor = color1;
            cb.pressedColor = color1;
        }
        MLB.colors = cb;
    }

    public void TMM()
    {
        ColorBlock cb = MMB.colors;
        if (MM)
        {
            MM = false;
            cb.normalColor = color0;
            cb.highlightedColor = color0;
            cb.pressedColor = color0;
        }
        else
        {
            MM = true;
            cb.normalColor = color1;
            cb.highlightedColor = color1;
            cb.pressedColor = color1;
        }
        MMB.colors = cb;
    }

    public void TMR()
    {
        ColorBlock cb = MRB.colors;
        if (MR)
        {
            MR = false;
            cb.normalColor = color0;
            cb.highlightedColor = color0;
            cb.pressedColor = color0;
        }
        else
        {
            MR = true;
            cb.normalColor = color1;
            cb.highlightedColor = color1;
            cb.pressedColor = color1;
        }
        MRB.colors = cb;
    }

    public void TBL()
    {
        ColorBlock cb = BLB.colors;
        if (BL)
        {
            BL = false;
            cb.normalColor = color0;
            cb.highlightedColor = color0;
            cb.pressedColor = color0;
        }
        else
        {
            BL = true;
            cb.normalColor = color1;
            cb.highlightedColor = color1;
            cb.pressedColor = color1;
        }
        BLB.colors = cb;
    }

    public void TBM()
    {
        ColorBlock cb = BMB.colors;
        if (BM)
        {
            BM = false;
            cb.normalColor = color0;
            cb.highlightedColor = color0;
            cb.pressedColor = color0;
        }
        else
        {
            BM = true;
            cb.normalColor = color1;
            cb.highlightedColor = color1;
            cb.pressedColor = color1;
        }
        BMB.colors = cb;
    }

    public void TBR()
    {
        ColorBlock cb = BRB.colors;
        if (BR)
        {
            BR = false;
            cb.normalColor = color0;
            cb.highlightedColor = color0;
            cb.pressedColor = color0;
        }
        else
        {
            BR = true;
            cb.normalColor = color1;
            cb.highlightedColor = color1;
            cb.pressedColor = color1;
        }
        BRB.colors = cb;
    }


    // Update is called once per frame
    void Update()
    {
        if (TL && !TM && !TR && ML && !MM && !MR && BL && BM && BR)
        {
            Padlocks.SetActive(false);
            Puzzle.SetActive(false);
            MouseCntrl.Resume();
        }
    }
}
