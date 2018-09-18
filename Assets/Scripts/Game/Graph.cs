using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Edge<T>
{
    private Vertex<T> node1;
    private Vertex<T> node2;
}

class Vertex<T>
{
    private T data;
    private LinkedList<Edge<T>> neighbors;
}

public interface IVertex<T>
{
    T Data
    {
        get;
        set;
    }
}

public interface IEdge<T>
{
    T Data
    {
        get;
        set;
    }
}

public class Graph<T>
{
    public List<IVertex<T>> vertices;

    public Graph()
    {
        vertices = new List<IVertex<T>>();
    }
}

//TODO: IMPLEMENTAR ALGUNS ALGORITMOS
//IMPLEMENTAR TODO O FUNCIONAMENTO BASE DA COISA
//SUBISTITUIR O COGIDO CHECHELENTO QUE TRATA AS VIZINHAÇAS
//POR ALGO ELEGANTE COMO UM GRAFO!
