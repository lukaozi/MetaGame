using System;
using UnityEditor;

namespace MGEditor
{
    [TypeDrawer]
    public class FloatTypeDrawer: ITypeDrawer
    {
        public bool HandlesType(Type type)
        {
            return type == typeof (float);
        }

        public object DrawAndGetNewValue(Type memberType, string memberName, object value, object target)
        {
            return EditorGUILayout.FloatField(memberName, (float) value);
        }
    }
}