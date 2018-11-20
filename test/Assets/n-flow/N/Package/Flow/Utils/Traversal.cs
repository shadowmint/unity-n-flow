using UnityEngine;

namespace N.Package.Flow.Utils
{
  public class Traversal
  {
    /// Return the first instance of T in the heirarchy of parents or self or null.
    public static T FindComponentInParents<T>(GameObject searchRoot) where T : Component
    {
      var root = searchRoot.transform;
      while (root != null)
      {
        var instance = root.gameObject.GetComponent<T>();
        if (instance != null)
        {
          return instance;
        }

        root = root.parent;
      }

      return null;
    }
  }
}