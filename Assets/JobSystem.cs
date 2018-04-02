using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Jobs;
using UnityEngine.Jobs;
using Unity.Collections;

public class JobSystem : BaseObject {

    public Vector3 acceleration = new Vector3(0.0002f, 0.0001f, 0.0002f);
    public Vector3 accelerationM = new Vector3(0.0001f, 0.0001f, 0.0001f);
    public Quaternion Rotation = new Quaternion(0.0001f, 0.0001f, 0.0001f, 0.0001f);

    NativeArray<Vector3> Velocities;
    TransformAccessArray transformsAccessArray;
    AccelerationJob accelJob;
    PositionUpdateJob posJob;

    JobHandle AccelJobHandle;
    JobHandle postJobHandle;
    JobHandle rotJobHandle;
    JobHandle rotAccelJobHandle;

    struct PositionUpdateJob : IJobParallelForTransform
    {
        public NativeArray<Vector3> velocity;
        public float deltatime;

        public void Execute(int i, TransformAccess transform)
        {
            transform.position += velocity[i] * deltatime;
        }
    }

    struct AccelerationJob: IJobParallelFor
    {
        public NativeArray<Vector3> velocity;

        public Vector3 accel;
        public Vector3 accelM;

        public float deltaTime;

        public void Execute(int i)
        {
            velocity[i] += (accel + i * accelM) * deltaTime;
        }
    }




    public void Start()
    {
        Velocities = new NativeArray<Vector3>(m_ObjectCount, Allocator.Persistent);

        m_Objects = SetupUtils.PlaceRandomCubes(m_ObjectCount, m_ObjectPlacementRadius);

        for(int i=0; i < m_ObjectCount; i++)
        {
            var obj = m_Objects[i];
            m_Transforms[i] = obj.transform;
            m_Renderers[i] = obj.GetComponent<Renderer>();
        }

        transformsAccessArray = new TransformAccessArray(m_Transforms);
    }

    public void Update()
    {
        accelJob = new AccelerationJob()
        {
            deltaTime = Time.deltaTime,
            velocity = Velocities,
            accel = acceleration,
            accelM = accelerationM
        };

        posJob = new PositionUpdateJob()
        {
            deltatime = Time.deltaTime,
            velocity = Velocities
        };



        AccelJobHandle = accelJob.Schedule(m_ObjectCount, 64);
        postJobHandle = posJob.Schedule(transformsAccessArray, AccelJobHandle);


    }

    public void LateUpdate()
    {
        postJobHandle.Complete();

    }

    private void OnDestroy()
    {
        Velocities.Dispose();

        transformsAccessArray.Dispose();
    }
}
