using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathPoints : MonoBehaviour
{

    public Color rayColor = Color.white;
    public List<Transform> mPath_Objs = new List<Transform>();
    Transform[] mArray_Objs;
    // Update is called once per frame
    void OnDrawGizmos()
    {
        Gizmos.color = rayColor;
        mArray_Objs = GetComponentsInChildren<Transform>();
        mPath_Objs.Clear();
        foreach (Transform path_obj in mArray_Objs)
        {

            if (path_obj != this.transform)
            {

                mPath_Objs.Add(path_obj);
            }

        }

        for (int i = 0; i < mPath_Objs.Count; i++)
        {

            Vector3 position = mPath_Objs[i].position;

            if (i > 0)
            {

                Vector3 previous = mPath_Objs[i - 1].position;
                Gizmos.DrawLine(previous, position);
                Gizmos.DrawWireSphere(position, 0.01f);
            }

        }
    }

}
