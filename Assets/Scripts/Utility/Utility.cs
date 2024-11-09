using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
  public static T[] ShuffleArray<T>(T[] array, int seed)
  {
    var random = new System.Random(seed);

    for (int i = 0; i < array.Length - 1; i += 1)
    {
      var randomIndex = random.Next(i, array.Length);
      var tempItem = array[randomIndex];
      array[randomIndex] = array[i];
      array[i] = tempItem;
    }

    return array;
  }
}
