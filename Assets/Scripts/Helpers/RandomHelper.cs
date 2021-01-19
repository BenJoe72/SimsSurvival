using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class RandomHelper
{
    static Random rnd;

    static RandomHelper()
    {
        rnd = new Random();
    }

    public static int Range(int min, int max)
    {
        return rnd.Next(min + max) + min;
    }
}
