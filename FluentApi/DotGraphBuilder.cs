using System;
using System.Collections.Generic;

namespace FluentApi.Graph
{
    public enum NodeShape
    {
        Box,
        Ellipse
    }

    public class DotGraphBuilder
    {
        protected Graph graph;
        public static DotGraphBuilder UndirectedGraph(string graphName)
        {
            var newGraph = new DotGraphBuilder(graphName, false);
            return newGraph;
        }

        public static DotGraphBuilder DirectedGraph(string graphName)
        {
            var newGraph = new DotGraphBuilder(graphName, true);
            return newGraph;
        }
        
        public DotGraphBuilder(string graphName, bool isDirected)
        {
            this.graph = new Graph(graphName, isDirected, false);
        }

        public DotGraphBuilder(Graph inputGraph)
        {
            this.graph = inputGraph;
        }

        public string Build() => graph.ToDotFormat();

        public Node AddNode(string nodeContent)
        {
            graph.AddNode(nodeContent);

            var newNode = new Node(graph, nodeContent);
            return newNode;
        }

        public DotEdge AddEdge(string source, string destination)
        {
            graph.AddEdge(source, destination);

            var newDotEdge = new DotEdge(graph, source, destination);
            return newDotEdge;
        }
    }


    public class Node : DotGraphBuilder
    {
        private string nodeContent;
        public Node(Graph graph, string inputContent) : base(graph)
        {
            nodeContent = inputContent;
        }

        public DotGraphBuilder With(Action<NodeBuilder> action)
        {
            foreach (var node in graph.Nodes)
                if (node.Name == nodeContent)
                {
                    action(new NodeBuilder(node));
                    break;
                }
            return this;
        }
    }


    public class DotEdge : DotGraphBuilder
    {
        private GraphEdge edge;
        public DotEdge(Graph graph, string fromNode, string toNode) : base(graph)
        {
            edge = new GraphEdge(fromNode, toNode, graph.Directed);
            //edge.SourceNode = fromNode;
            //edge.DestinationNode = toNode;
        }

        public DotGraphBuilder With(Action<EdgeBuilder> someAction)
        {
            foreach (var graphEdge in graph.Edges)
                if (graphEdge.SourceNode == edge.SourceNode &&
                    graphEdge.DestinationNode == edge.DestinationNode)
                {
                    someAction(new EdgeBuilder(graphEdge));
                    break;
                }
            return this;
        }
    }

    public class NodeBuilder : Properties<NodeBuilder>
    {
        public NodeBuilder(GraphNode node)
        {
            properties = node.Attributes;
        }

        public NodeBuilder Shape(NodeShape form)
        {
            properties["shape"] = form.ToString().ToLower();
            return this;
        }
    }

    public class EdgeBuilder : Properties<EdgeBuilder>
    {
        public EdgeBuilder(GraphEdge edge)
        {
            properties = edge.Attributes;
        }
        public EdgeBuilder Weight(double weight)
        {
            properties["weight"] = weight.ToString();
            return this;
        }
    }

    public class Properties<T> where T : class
    {
        protected Dictionary<string, string> properties =
            new Dictionary<string, string>();
        public T Color(string newColor)
        {
            properties["color"] = newColor;
            return this as T;
        }

        public T Label(string newLabel)
        {
            properties["label"] = newLabel;
            return this as T;
        }

        public T FontSize(int newfSize)
        {
            properties["fontsize"] = newfSize.ToString();
            return this as T;
        }
    }
}