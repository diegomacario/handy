using System;
using UnityEngine;

// A serializable "frame" that captures a Transform's state at a specific timestamp
// and can be flattened into/from a simple float[11]
[Serializable]
public class TransformFrame2
{
    public SerializableVec3 position;
    public SerializableQuat rotation;
    public SerializableVec3 scale;

    public static TransformFrame2 FromTransform(CaptureTransform2 c)
    {
        var ret = new TransformFrame2();

        if (c.captureWorldTransform)
        {
            ret.position = SerializableVec3.FromVector3(c.transform.position);
            ret.rotation = SerializableQuat.FromQuaternion(c.transform.rotation);
            ret.scale = SerializableVec3.FromVector3(c.transform.lossyScale);
        }
        else
        {
            if (c.firstCapture)
            {
                ret.position = SerializableVec3.FromVector3(c.transform.localPosition);
                ret.rotation = SerializableQuat.FromQuaternion(c.transform.localRotation);
                ret.scale = SerializableVec3.FromVector3(c.transform.localScale);
            }
            else
            {
                ret.rotation = SerializableQuat.FromQuaternion(c.transform.localRotation);
            }
        }

        return ret;
    }

    public void CopyToTransform(Transform t, bool captureWorldTransform)
    {
        if (captureWorldTransform)
        {
            t.position   = new Vector3(position.x, position.y, position.z);
            t.rotation   = new Quaternion(rotation.x, rotation.y, rotation.z, rotation.w);
            t.localScale = new Vector3(scale.x, scale.y, scale.z);
        }
        else
        {
            t.localPosition = new Vector3(position.x, position.y, position.z);
            t.localRotation = new Quaternion(rotation.x, rotation.y, rotation.z, rotation.w);
            t.localScale    = new Vector3(scale.x, scale.y, scale.z);
        }
    }

    public static TransformFrame2 FromFlattened(float[] f)
    {
        var ret = new TransformFrame2();
        ret.position = new SerializableVec3();
        ret.position.x = f[0];
        ret.position.y = f[1];
        ret.position.z = f[2];
        ret.rotation = new SerializableQuat();
        ret.rotation.x = f[3];
        ret.rotation.y = f[4];
        ret.rotation.z = f[5];
        ret.rotation.w = f[6];
        ret.scale = new SerializableVec3();
        ret.scale.x = f[7];
        ret.scale.y = f[8];
        ret.scale.z = f[9];
        return ret;
    }

    public float[] Flattened(CaptureTransform2 c)
    {
        if (c.captureWorldTransform || c.firstCapture)
        {
            c.firstCapture = false;

            return new float[10] {
                position.x,
                position.y,
                position.z,
                rotation.x,
                rotation.y,
                rotation.z,
                rotation.w,
                scale.x,
                scale.y,
                scale.z,
            };
        }
        else
        {
            return new float[4] {
                rotation.x,
                rotation.y,
                rotation.z,
                rotation.w,
            };
        }
    }
}
