using System;
using _SaveTheVillage.Scripts.UI.Windows;

namespace _SaveTheVillage.Scripts.StaticData.Windows
{
  [Serializable]
  public class WindowConfig
  {
    public WindowId WindowId;
    public WindowBase Template;
  }
}