using System.Collections.Generic;
using System.Text;

namespace N.Package.Flow
{
  [System.Serializable]
  public class FlowComponentDebugHeirarchy
  {
    public string Identity;
    public List<FlowComponentDebugHeirarchy> Children;
    public bool Updated { get; set; }

    public override string ToString()
    {
      return string.Join("\n", ToString(0));
    }

    public IEnumerable<string> ToString(int depth)
    {
      var wasUpdated = Updated ? "UPDATED" : "";
      yield return $"{Indent(depth)} {Identity} {wasUpdated}";
      foreach (var child in Children)
      {
        foreach (var line in child.ToString(depth + 1))
        {
          yield return line;
        }
      }
    }

    private string Indent(int depth)
    {
      return new string('-', depth);
    }
  }
}