using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorModel : MonoBehaviour {

    public float Red;
    public float Green;
    public float Blue;

    public float Cyan;
    public float Yellow;
    public float Magenta;

    public float White;

    public static ColorModel operator +(ColorModel cmx, ColorModel cmy)
    {
        ColorModel colorModel = cmx;
        colorModel.Red += cmy.Red;
        colorModel.Green += cmy.Green;
        colorModel.Blue += cmy.Blue;

        colorModel.Cyan += cmy.Cyan;
        colorModel.Yellow += cmy.Yellow;
        colorModel.Magenta += cmy.Magenta;

        colorModel.White += cmy.White;

        return colorModel;
    }
}
