using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorModel : MonoBehaviour {

    public float Red;
    public float Green;
    public float Blue;

    //Not planned on being used anymore
    //public float Cyan;
    //public float Yellow;
    //public float Magenta;

    //public float White;

    public ColorModel() { Red = 0; Green = 0; Blue = 0; }

    public ColorModel(float red, float green, float blue)
    {
        Red = red;
        Green = green;
        Blue = blue;
    }

    public static ColorModel operator +(ColorModel cmx, ColorModel cmy)
    {
        ColorModel colorModel = cmx;
        colorModel.Red += cmy.Red;
        colorModel.Green += cmy.Green;
        colorModel.Blue += cmy.Blue;

        //Not planned on being used anymore
        //colorModel.Cyan += cmy.Cyan;
        //colorModel.Yellow += cmy.Yellow;
        //colorModel.Magenta += cmy.Magenta;

        //colorModel.White += cmy.White;

        return colorModel;
    }

    public static bool operator >=(ColorModel l, ColorModel r)
    {
        return l.Red >= r.Red && l.Green >= l.Red && l.Blue >= r.Blue;
    }

    public static bool operator <=(ColorModel l, ColorModel r)
    {
        return l.Red <= r.Red && l.Green <= l.Red && l.Blue <= r.Blue;
    }
}
