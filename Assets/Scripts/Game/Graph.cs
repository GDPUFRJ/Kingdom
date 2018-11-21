using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vertex<T> : MonoBehaviour
{
    public List<T> Neighbors;
}

public class Edge<T> : MonoBehaviour
{
    public T start;
    public T end;

    public virtual int GetWeight() { return 0; }
}

public class Graph<T>
{
    public List<Vertex<T>> vertexes = new List<Vertex<T>>();
    public List<Edge<T>> edges = new List<Edge<T>>();

    public void Dijkstra(Vertex<T> source, Vertex<T> dest)
    {
        
    }

}

//TODO: IMPLEMENTAR ALGUNS ALGORITMOS
//IMPLEMENTAR TODO O FUNCIONAMENTO BASE DA COISA
//SUBISTITUIR O COGIDO CHECHELENTO QUE TRATA AS VIZINHAÇAS
//POR ALGO ELEGANTE COMO UM GRAFO!
