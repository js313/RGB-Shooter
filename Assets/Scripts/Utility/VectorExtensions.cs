using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorExtensions
{
  /// <summary>
  /// Create a new Vector3 from the existing Vector3, replacing its X coordinate
  /// </summary>
  /// <param name="vector">Vector3 from which to create the new one</param>
  /// <param name="x">New value for x</param>
  /// <returns>A new Vector3</returns>
  public static Vector3 WithX(this Vector3 vector, float x) =>
    new Vector3(x, vector.y, vector.z);

  /// <summary>
  /// Create a new Vector3 from the existing Vector3, replacing its Y coordinate
  /// </summary>
  /// <param name="vector">Vector3 from which to create the new one</param>
  /// <param name="y">New value for y</param>
  /// <returns>A new Vector3</returns>
  public static Vector3 WithY(this Vector3 vector, float y) =>
    new Vector3(vector.x, y, vector.z);

  /// <summary>
  /// Create a new Vector3 from the existing Vector3, replacing its Z coordinate
  /// </summary>
  /// <param name="vector">Vector3 from which to create the new one</param>
  /// <param name="z">New value for z</param>
  /// <returns>A new Vector3</returns>
  public static Vector3 WithZ(this Vector3 vector, float z) =>
    new Vector3(vector.x, vector.y, z);
}
