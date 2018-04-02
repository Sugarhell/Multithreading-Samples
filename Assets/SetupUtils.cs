using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SetupUtils {
    static GameObject Cube;
    static GameObject[] Cubes;

    public static GameObject[] PlaceRandomCubes(int count, float radius)
    {
        Cubes = new GameObject[count];
        Cube = MakeStrippedCube();

        for (int i = 0; i < count; i++)
        {
            var cube = GameObject.Instantiate(Cube);
            Cube.transform.position = UnityEngine.Random.insideUnitSphere * radius;
            Cubes[i] = cube;
        }

        GameObject.Destroy(Cube);

        return Cubes;
    }
    public static GameObject[] PlaceRandomCubes1(int count, float radius)
    {
        Cubes = new GameObject[count];
        Cube = MakeStrippedCube1();

        for (int i = 0; i < count; i++)
        {
            var cube = GameObject.Instantiate(Cube);
            cube.transform.position = UnityEngine.Random.insideUnitSphere * radius;
            Cubes[i] = cube;
        }

        GameObject.Destroy(Cube);

        return Cubes;
    }

    public static GameObject[] PlaceRandomCubes(int count)
    {
        var radius = count / 10f;
        return PlaceRandomCubes(count, radius);
    }

    public static GameObject MakeStrippedCube()
    {
        Cube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        //turn off shadows entirely
        var renderer = Cube.GetComponent<MeshRenderer>();
        renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        renderer.receiveShadows = false;
        renderer.allowOcclusionWhenDynamic = false;

        // disable collision
        var collider = Cube.GetComponent<Collider>();
        collider.enabled = false;

        return Cube;
    }

    public static GameObject MakeStrippedCube1()
    {
        Cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Cube.AddComponent<MonoNonJob>();

        //turn off shadows entirely
        var renderer = Cube.GetComponent<MeshRenderer>();
        renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        renderer.receiveShadows = false;

        // disable collision
        var collider = Cube.GetComponent<Collider>();
        collider.enabled = false;
        

        return Cube;
    }
}

