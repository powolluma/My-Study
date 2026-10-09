using System.Collections.Generic;

namespace Task4
{
    public class Form1
    {
        private void Walk(List<TreeNode> nodes, int order, List<string> result)
        {
            if (nodes == null || nodes.Count == 0) return;
            foreach (TreeNode node in nodes)
            {
                if (order == 0) result.Add(node.Value);
                Walk(node.Children, order, result);
                if (order == 1) result.Add(node.Value);
            }
        }
    }

    public class TreeNode
    {
        public string Value;
        public List<TreeNode> Children = new List<TreeNode>();
    }
}
