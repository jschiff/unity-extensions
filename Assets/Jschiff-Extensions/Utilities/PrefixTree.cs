using System.Collections.Generic;
using System;
using System.Text;
using System.Linq;
using Com.Jschiff.UnityExtensions.Extensions;

// A prefix tree for searching goods! Case insensitive
public class PrefixTree<T> where T : class {
    private class Node {
        internal readonly string expansion;
        internal readonly char character;
        internal readonly SortedList<char, Node> children = new SortedList<char, Node>();
        internal T mappedObject = null;

        public Node(char character, string expansion, T mappedObject = null) {
            this.expansion = expansion;
            this.character = char.ToLower(character);
            this.mappedObject = mappedObject;
        }

        public override string ToString() {
            return $"{character}: {expansion}";
        }
    }

    SortedList<char, Node> rootCollection = new SortedList<char, Node>();
    public int Count { get; private set; } = 0;

    public PrefixTree() { }

    public PrefixTree(IEnumerable<T> elementsToAdd, Func<T, string> keyFunc) {
        foreach (var element in elementsToAdd) {
            Add(keyFunc(element), element);
        }
    }

    // Add a string to the prefix tree.
    // Return true if the item was added. False if it already existed.
    public bool Add(string s, T addMe) {
        s = removeWhitespaceAndLowercase(s);
        var fringe = rootCollection;

        StringBuilder sb = new StringBuilder();
        foreach (char c in s) {
            sb.Append(c);
            bool nodeExists = fringe.TryGetValue(c, out var node);

            bool isComplete = sb.Length == s.Length;
            if (!nodeExists) {
                node = new Node(c, sb.ToString(), isComplete ? addMe : null);
                fringe.Add(c, node);
            }
            // If the string is already here, return false.
            else if (node.expansion == s) {
                if (node.mappedObject == null) {
                    node.mappedObject = addMe;
                }
                else {
                    if (node.mappedObject != addMe) {
                        throw new Exception("PrefixTree cannot handle duplicate values");
                    }
                    return false;
                }
            }

            fringe = node.children;
        }

        Count++;
        return true;
    }

    public T ExactSearch(string key) {
        key = removeWhitespaceAndLowercase(key);

        // First navigate to the prefix node (where expansion == prefix)
        var fringe = rootCollection;
        Node node = null;
        foreach (char c in key) {
            bool nodeExists = fringe.TryGetValue(c, out node);
            if (!nodeExists) return null;
            fringe = node.children;
        }

        return node.mappedObject;
    }

    public List<(string, T)> SearchReturnList(string prefix, int maxResults) {
        return Search(prefix, maxResults).ToList();
    }

    public IEnumerable<(string, T)> Search(string prefix, int maxResults) {
        prefix = removeWhitespaceAndLowercase(prefix);
        if (prefix.Length == 0) yield break;

        // First navigate to the prefix node (where expansion == prefix)
        var fringe = rootCollection;
        Node node = null;
        foreach (char c in prefix) {
            bool nodeExists = fringe.TryGetValue(c, out node);
            if (!nodeExists) yield break;
            fringe = node.children;
        }

        Stack<Node> stack = new Stack<Node>();
        stack.Push(node);
        int resultCount = 0;
        while (stack.Count > 0 && resultCount < maxResults) {
            var nodeToExamine = stack.Pop();
            if (nodeToExamine.mappedObject != null) {
                resultCount++;
                yield return (nodeToExamine.expansion, nodeToExamine.mappedObject);
            }

            foreach (var pair in nodeToExamine.children) {
                stack.Push(pair.Value);
            }
        }
    }

    private string removeWhitespaceAndLowercase(string s) {
        s = s.ToLower();
        s = s.TrimWhitespaceQuickly();
        return s;
    }
}